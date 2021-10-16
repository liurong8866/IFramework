using System;
using System.Collections.Generic;
using System.Reflection;
using IFramework.Core;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.VersionControl;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IFramework.Editor
{
    /// <summary>
    /// UIPanel 代码生成器
    /// </summary>
    public class UIPanelGenerator
    {
        private static readonly ConfigDateTime generateTime = new ConfigDateTime("GENERATE_TIME");
        private static readonly ConfigString generateUIPrefabPath = new ConfigString("GENERATE_UI_PREFAB_PATH");

        /// <summary>
        /// 生成脚本
        /// </summary>
        public static void GenerateCode(bool overwrite = false)
        {
            generateTime.Value = DateTime.Now;
            Log.Clear();
            Object[] objects = Selection.GetFiltered(typeof(GameObject), SelectionMode.Assets | SelectionMode.TopLevel);
            GenerateCode(objects, overwrite);
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        public static void GenerateCode(Object[] objects, bool overwrite)
        {
            try {
                EditorUtility.DisplayProgressBar("", "正在生成 UI Code ...", 0);
                for (int i = 0; i < objects.Length; i++) {
                    GenerateCode(objects[i] as GameObject, AssetDatabase.GetAssetPath(objects[i]), overwrite);
                    EditorUtility.DisplayProgressBar("", "正在生成 UI Code ...", (float)(i + 1) / objects.Length);
                }
                AssetDatabase.Refresh();
            } finally {
                EditorUtility.ClearProgressBar();
            }
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        private static void GenerateCode(GameObject obj, string prefabPath, bool overwrite)
        {
            if (obj == null) return;

            // 如果不是Prefab，则退出
            PrefabAssetType prefabType = PrefabUtility.GetPrefabAssetType(obj);
            if (prefabType == PrefabAssetType.NotAPrefab) return;
            
            // 是否临时实例
            bool isTemp = false;
            // 实例化Prefab
            GameObject clone;

            // 获取PrefabPath
            string assetPath = AssetDatabase.GetAssetPath(obj);
            // 如果AssetPath路径是空，说明当前不是Prefab，而是其实例
            if (assetPath.Nothing()) {
                clone = obj;
            }
            else {
                isTemp = true;
                clone = PrefabUtility.InstantiatePrefab(obj) as GameObject;
                if (clone == null) return;
            }
            RootPanelInfo rootPanelInfo = new RootPanelInfo {
                GameObjectName = clone.name.Replace("(clone)", string.Empty)
            };

            // 查询所有Bind
            BindCollector.SearchBind(clone.transform, "", rootPanelInfo);

            // 生成 UIPanel脚本
            GenerateUIPanelCode(obj, prefabPath, rootPanelInfo, overwrite);

            // 获取Prefab路径, 如果多个则用;分隔
            if (assetPath.NotEmpty()) {
                if (generateUIPrefabPath.Value.Nothing()) {
                    generateUIPrefabPath.Value = assetPath;
                }
                else {
                    generateUIPrefabPath.Value += ";" + assetPath;
                }
            }

            //销毁刚实例化的对象
            if (isTemp) Object.DestroyImmediate(clone);
        }

        /// <summary>
        /// 生成UIPanelCode
        /// </summary>
        public static void GenerateUIPanelCode(GameObject obj, string prefabPath, RootPanelInfo rootPanelInfo, bool overwrite)
        {
            // 根据Prefab路径获取Script生成路径
            string scriptPath = DirectoryUtils.GetPathByFullName(prefabPath);

            // 取UIPrefab默认路径右侧路径
            scriptPath = scriptPath.Right(Configure.UIPrefabPath.Value, false, true);

            // 组装生成信息
            UIPanelGenerateInfo panelGenerateInfo = new UIPanelGenerateInfo {
                ScriptName = obj.name,
                ScriptPath = DirectoryUtils.CombinePath(Configure.UIScriptPath.Value, scriptPath)
            };

            // 生成 .cs文件
            UIPanelTemplate.Instance.Generate(panelGenerateInfo, rootPanelInfo, overwrite);

            // 生成 .designer.cs
            UIPanelDesignerTemplate.Instance.Generate(panelGenerateInfo, rootPanelInfo, true);

            // 生成UIElement组件
            foreach (ElementInfo elementInfo in rootPanelInfo.ElementInfoList) {
                string elementPath = "";
                if (elementInfo.BindInfo.BindScript.BindType == BindType.Element) {
                    elementPath = DirectoryUtils.CombinePath(panelGenerateInfo.ScriptPath, obj.name);
                }
                else {
                    elementPath = DirectoryUtils.CombinePath(panelGenerateInfo.ScriptPath, obj.name, "Components");
                }
                CreateUIElementCode(elementPath, elementInfo, overwrite);
            }
        }

        /// <summary>
        /// 生成UIElement
        /// </summary>
        private static void CreateUIElementCode(string generateDirPath, ElementInfo elementInfo, bool overwrite)
        {
            UIPanelGenerateInfo panelGenerateInfo = new UIPanelGenerateInfo {
                ScriptName = elementInfo.BindInfo.BindScript.ComponentName,
                ScriptPath = generateDirPath
            };

            // 生成.cs
            UIElementTemplate.Instance.Generate(panelGenerateInfo, elementInfo, overwrite);

            // 生成.designer.cs
            UIElementDesignerTemplate.Instance.Generate(panelGenerateInfo, elementInfo, true);

            // 水平遍历，深度递归调用
            foreach (ElementInfo childElement in elementInfo.ElementInfoList) {
                string elementDir = DirectoryUtils.CombinePath(panelGenerateInfo.ScriptPath, panelGenerateInfo.ScriptName);
                CreateUIElementCode(elementDir, childElement, overwrite);
            }
        }

        /// <summary>
        /// 替换组件
        /// </summary>
        [DidReloadScripts]
        private static void AddComponentToGameObject()
        {
            string pathStr = generateUIPrefabPath.Value;
            if (pathStr.Nothing()) {
                Clear();
                return;
            }
            Log.Info("生成脚本: 正在编译");

            // 获取路径
            Assembly assembly = ReflectionExtension.GetAssemblyCSharp();
            string[] paths = pathStr.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            try {
                foreach (string path in paths) {
                    GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
                    // 设置对象引用属性
                    SetObjectRefToProperty(go, assembly);
                    // 更新进度条
                    Log.Info("生成脚本: 正在序列化 UIPrefab " + go.name);
                }
            }
            catch (Exception e) {
                Log.Error(e.Message);
                Clear();
                return;
            }
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            // 标记场景未保存
            EditorUtils.MarkCurrentSceneDirty();
            Log.Info("生成脚本: 生成完毕，耗时{0}秒", generateTime.DeltaSeconds);
            Clear();
        }

        /// <summary>
        /// 设置对象引用属性
        /// </summary>
        private static void SetObjectRefToProperty(GameObject go, Assembly assembly)
        {
            Stack<Transform> elementStack = new Stack<Transform>();

            // 绑定Root节点组件
            BindComponent(go.transform, go.name, assembly);

            // 添加Root节点
            elementStack.Push(go.transform);

            // 生成从下到上的Element栈
            BindCollector.GetElementStack(go.transform, elementStack);
            while (elementStack.Count > 0) {
                // 待处理节点
                Transform elementTran = elementStack.Pop();

                // 取得该节点下所有IBind组件
                SetObjectRefToProperty(elementTran, elementTran.name, assembly);
            }
        }

        /// <summary>
        /// 设置对象引用属性
        /// </summary>
        private static void SetObjectRefToProperty(Transform tran, string prefabName, Assembly assembly)
        {
            Component component = BindComponent(tran, prefabName, assembly);
            // 获取序列化对象
            SerializedObject serialized = new SerializedObject(component);
            // 查找该组件下所有IBind类型子组件
            IBind[] allBind = tran.GetComponentsInChildren<IBind>(true);

            // 循环设置对象引用
            foreach (IBind elementBind in allBind) {
                // 取得属性名称
                string propertyName = elementBind.Transform.name;
                // 如果没有该属性，则跳出本次循环，执行下次循环
                if (serialized.FindProperty(propertyName) == null) continue;
                // 设置对象引用
                serialized.FindProperty(propertyName).objectReferenceValue = elementBind.Transform.gameObject;
            }
            // 确认修改
            serialized.ApplyModifiedPropertiesWithoutUndo();
        }

        /// <summary>
        /// 绑定组件并返回
        /// </summary>
        private static Component BindComponent(Transform tran, string prefabName, Assembly assembly)
        {
            IBind bind = tran.GetComponent<IBind>();
            string className = "";

            // 获取className，如果有组件，则取组件名称，否则取Prefab文件名
            if (bind.NotEmpty()) {
                className = Configure.DefaultNameSpace + "." + bind.ComponentName;

                // 如果不是DefaultElement组件，则先立即销毁组件，接下来会再次添加到
                if (bind.BindType != BindType.DefaultElement) {
                    AbstractBind abstractBind = tran.GetComponent<AbstractBind>();
                    if (abstractBind.NotEmpty()) {
                        Object.DestroyImmediate(abstractBind, true);
                    }
                }
            }
            else {
                className = Configure.DefaultNameSpace + "." + prefabName;
            }

            // 反射类型
            Type t = assembly.GetType(className);
            // 绑定组件
            Component component = tran.GetComponent(t) ?? tran.gameObject.AddComponent(t);
            return component;
        }

        // 清理缓存数据
        private static void Clear()
        {
            generateTime.Clear();
            generateUIPrefabPath.Clear();
        }
    }
}
