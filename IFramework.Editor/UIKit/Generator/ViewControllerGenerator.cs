using System;
using System.Collections.Generic;
using System.Reflection;
using IFramework.Core;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IFramework.Editor
{
    /// <summary>
    /// UIPanel 代码生成器
    /// </summary>
    public class ViewControllerGenerator
    {
        private static readonly ConfigDateTime generateTime = new ConfigDateTime("GENERATE_TIME");
        private static readonly ConfigString generateNamespace = new ConfigString("GENERATE_NAMESPACE");
        private static readonly ConfigString generateClassName = new ConfigString("GENERATE_CLASS_NAME");
        private static readonly ConfigString generateObjectName = new ConfigString("GENERATE_ROOT_OBJECT_NAME");
        private static readonly ConfigString generatePrefabFullName = new ConfigString("GENERATE_PREFAB_FULL_NAME");

        /// <summary>
        /// 生成脚本
        /// </summary>
        public static void GenerateCode(bool overwrite)
        {
            generateTime.Value = DateTime.Now;
            Log.Clear();
            GenerateCode(Selection.activeGameObject, overwrite);
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        private static void GenerateCode(GameObject obj, bool overwrite)
        {
            if (obj == null) return;
            
            Bind bind = obj.GetComponent<Bind>();
            if (bind != null) {
                Log.Error("不能在根节点绑定Bind组件，请检查文件是否正确！" + obj.name);
                return;
            }
            
            EditorUtility.DisplayProgressBar("正在生成脚本...", String.Empty, 0);
            Log.Info("生成脚本: 开始");
            RootViewControllerInfo rootControllerInfo = new RootViewControllerInfo {
                GameObjectName = obj.name.FormatName()
            };
            // 搜索所有绑定对象
            BindCollector.SearchBind(obj.transform, "", rootControllerInfo);
            // 生成 UIPanel脚本
            GenerateUIPanelCode(obj, rootControllerInfo, overwrite);
            // 刷新项目资源
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 生成UIPanelCode
        /// </summary>
        private static void GenerateUIPanelCode(GameObject obj, RootViewControllerInfo rootControllerInfo, bool overwrite)
        {
            ViewController controller = obj.GetComponent<ViewController>();
            GenerateInfo generateInfo = new ViewControllerGenerateInfo(controller);

            // 生成Controller层
            ViewControllerTemplate.Instance.Generate(generateInfo, rootControllerInfo, overwrite);

            // 生成Model层
            ViewControllerDesignerTemplate.Instance.Generate(generateInfo, rootControllerInfo, true);

            // 生成UIElement组件
            foreach (ElementInfo elementInfo in rootControllerInfo.ElementInfoList) {
                string elementPath = "";
                if (elementInfo.BindInfo.BindScript.BindType == BindType.Element) {
                    elementPath = DirectoryUtils.CombinePath(generateInfo.ScriptPath, generateInfo.ScriptName);
                }
                else {
                    elementPath = DirectoryUtils.CombinePath(generateInfo.ScriptPath, generateInfo.ScriptName, "Components");
                }
                CreateElementCode(elementPath, elementInfo, overwrite);
            }
            // 保存信息
            generateNamespace.Value = generateInfo.Namespace;
            generateClassName.Value = generateInfo.ScriptName;
            generateObjectName.Value = obj.name.FormatName();
            generatePrefabFullName.Value = generateInfo.PrefabAssetsPath + "/{0}.prefab".Format(obj.name.FormatName());
        }

        /// <summary>
        /// 生成UIElement
        /// </summary>
        private static void CreateElementCode(string generateDirPath, ElementInfo elementInfo, bool overwrite)
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
                CreateElementCode(elementDir, childElement, overwrite);
            }
        }

        /// <summary>
        /// 替换组件
        /// </summary>
        [DidReloadScripts]
        private static void AddComponentToGameObject()
        {
            if (generateClassName.Value.Nothing()) {
                Clear();
                return;
            }
            Log.Info("生成脚本: 正在编译");

            // 用于记录是否临时生成的实例
            bool needDestroy = false;
            // 获取ViewController所在对象
            GameObject go = GameObject.Find(generateObjectName.Value);
            // 如果是从Prefab
            if (!go) {
                // 用于记录是否临时生成的实例
                needDestroy = true;
                // 用于记录是否临时生成的实例
                GameObject goAsset = AssetDatabase.LoadAssetAtPath<GameObject>(generatePrefabFullName.Value);
                // 实例化Prefab
                go = PrefabUtility.InstantiatePrefab(goAsset) as GameObject;
                if (go == null || !go.GetComponent<ViewController>()) {
                    Log.Error("生成脚本: 未找到对象:{0}".Format(generateObjectName.Value));
                    Clear();
                    return;
                }
            }
            Assembly assembly = ReflectionUtility.GetAssemblyCSharp();
            try {
                // 替换脚本
                SetObjectRefToProperty(go, assembly);
            }
            catch (Exception e) {
                Log.Error("生成失败," + e.Message);
                Clear();
                EditorUtility.ClearProgressBar();
                return;
            }
            // 生成Prefab, 初始化字段
            ViewController controller = go.GetComponent<ViewController>();
            GenerateInfo generateInfo = new ViewControllerGenerateInfo(controller);

            // ViewController的序列化对象
            SerializedObject serializedObject = new SerializedObject(controller);
            if (controller) {
                serializedObject.FindProperty("Namespace").stringValue = controller.Namespace;
                serializedObject.FindProperty("ScriptName").stringValue = controller.ScriptName;
                serializedObject.FindProperty("ScriptPath").stringValue = controller.ScriptPath;
                serializedObject.FindProperty("PrefabPath").stringValue = controller.PrefabPath;
                serializedObject.FindProperty("Comment").stringValue = controller.Comment;
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
                string typeName = generateNamespace + "." + generateClassName;
                Type type = assembly.GetType(typeName);
                // 销毁ViewController
                if (controller.GetType() != type) {
                    // 立即销毁，不允许Asset被销毁
                    Object.DestroyImmediate(controller, true);
                }
                // 如果不存在，则生成文件夹
                DirectoryUtils.Create(generateInfo.PrefabAssetsPath);
                // 当根节点，或者其父节点也是prefab，则不保存
                if (go.transform.parent == null || go.transform.parent != null && !PrefabUtility.IsPartOfPrefabInstance(go.transform.parent)) {
                    // string path = generateInfo.PrefabAssetsPath + "/{0}.prefab".Format(go.name);
                    Log.Info("生成脚本: 正在生成预设 " + generatePrefabFullName.Value);
                    EditorUtils.SavePrefab(go, generatePrefabFullName.Value);
                }
                else {
                    Log.Warning($"生成脚本: 未保存游戏对象 {go.name.FormatName()} 的预设，因为该对象属于其他Prefab的一部分。");
                }
            }
            else {
                serializedObject.FindProperty("ScriptPath").stringValue = "Assets/Scripts";
                // Apply the changed properties without an undo.
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }
            // 保存资源
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            //销毁刚实例化的对象
            if (needDestroy) go.DestroySelfImmediate();

            // 标记场景未保存
            EditorUtils.MarkCurrentSceneDirty();
            Log.Info("生成脚本: 生成完毕，耗时{0}秒", generateTime.DeltaSeconds);
            Clear();
            EditorUtility.ClearProgressBar();
        }

        /// <summary>
        /// 设置对象引用属性
        /// </summary>
        private static void SetObjectRefToProperty(GameObject go, Assembly assembly)
        {
            Stack<Transform> elementStack = new Stack<Transform>();
            // 绑定Root节点组件
            BindComponent(go.transform, generateClassName.Value, assembly);
            // 添加Root节点
            elementStack.Push(go.transform);
            // 生成从下到上的Element栈
            BindCollector.GetElementStack(go.transform, elementStack);
            while (elementStack.Count > 0) {
                // 待处理节点
                Transform elementTran = elementStack.Pop();
                IBind bind = elementTran.GetComponent<IBind>();
                // 取得该节点下所有IBind组件
                SetObjectRefToProperty(elementTran, bind.ComponentName, assembly);
            }
        }

        /// <summary>
        /// 设置对象引用属性
        /// </summary>
        private static void SetObjectRefToProperty(Transform tran, string scriptName, Assembly assembly)
        {
            Component component = BindComponent(tran, scriptName, assembly);
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
        private static Component BindComponent(Transform tran, string scriptName, Assembly assembly)
        {
            IBind bind = tran.GetComponent<IBind>();
            string className = "";

            // 获取className，如果有组件，则取组件名称，否则取Prefab文件名
            if (bind.NotEmpty()) {
                className = Configure.UIKit.DefaultNameSpace + "." + bind.ComponentName;

                // 如果不是DefaultElement组件，则先立即销毁组件，接下来会再次添加到
                if (bind.BindType != BindType.DefaultElement) {
                    Bind abstractBind = tran.GetComponent<Bind>();
                    if (abstractBind.NotEmpty()) {
                        Object.DestroyImmediate(abstractBind, true);
                    }
                }
            }
            else {
                className = Configure.UIKit.DefaultNameSpace + "." + scriptName;
            }
            // 反射类型
            Type t = assembly.GetType(className);
            if (t == null) {
                throw new Exception("未找到要绑定的类:"+ className);
            }
            // 绑定组件
            Component component = tran.GetComponent(t) ?? tran.gameObject.AddComponent(t);
            return component;
        }

        // 清理缓存数据
        private static void Clear()
        {
            generateTime.Clear();
            generateClassName.Clear();
            generateNamespace.Clear();
            generateObjectName.Clear();
        }
    }
}
