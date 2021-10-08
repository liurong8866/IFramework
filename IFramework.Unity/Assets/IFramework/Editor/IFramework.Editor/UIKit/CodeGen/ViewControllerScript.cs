using System;
using System.Linq;
using System.Reflection;
using IFramework.Core;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IFramework.Editor
{
    public class ViewControllerScript
    {
        private static readonly ConfigString generateNamespace = new ConfigString("GENERATE_NAMESPACE");
        private static readonly ConfigString generateClassName = new ConfigString("GENERATE_CLASS_NAME");
        private static readonly ConfigString gameObjectName = new ConfigString("GAME_OBJECT_NAME");

        /// <summary>
        /// 生成脚本
        /// </summary>
        public static void GenerateCode()
        {
            GameObject go = Selection.objects.First() as GameObject;

            if (!go) {
                Log.Warning("需要选择 GameObject");
                return;
            }

            // 如果当前是Bind类型，并且不包含ViewController，则认为是子节点，向上查找
            if (go.GetComponent<AbstractBind>() && !go.GetComponent<ViewController>()) {
                ViewController parentController = go.GetComponentInParent<ViewController>();

                // 如果找到ViewController，则
                if (parentController) { go = parentController.gameObject; }
            }
            // 生成脚本
            Log.Info("生成脚本: 开始");
            ViewController controller = go.GetComponent<ViewController>();

            RootViewControllerInfo rootControllerInfo = new RootViewControllerInfo {
                GameObjectName = controller.name
            };

            // 搜索所有绑定对象
            BindCollector.SearchBind(go.transform, "", rootControllerInfo);

            // 生成Controller层
            ViewControllerTemplate.Instance.Generate(controller);
            // 生成Model层
            ViewControllerDesignerTemplate.Instance.Generate(controller, rootControllerInfo);

            // 保存信息
            generateNamespace.Value = controller.Namespace;
            generateClassName.Value = controller.ScriptName;
            gameObjectName.Value = go.name;

            // 刷新项目资源
            AssetDatabase.Refresh();
            Log.Info("生成脚本: 完成");
        }

        [DidReloadScripts]
        private static void AddComponentToGameObject()
        {
            if (generateClassName.Value.IsNullOrEmpty()) {
                Clear();
                return;
            }
            Log.Info("生成脚本: 开始编译");

            // 通过反射获得生成的类
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            Assembly defaultAssembly = assemblies.First(assembly => assembly.GetName().Name == "Assembly-CSharp");
            string typeName = generateNamespace + "." + generateClassName;
            Type type = defaultAssembly.GetType(typeName);

            // 如果类型为空，代表着获取失败，生成的类有问题
            if (type == null) {
                Log.Error("生成脚本: 编译失败，请检查生成设置是否正确");
                return;
            }
            Log.Info("生成脚本: " + type);

            // 获取ViewController所在对象
            GameObject go = GameObject.Find(gameObjectName.Value);

            if (!go) {
                Log.Error("上次的 ViewController 生成失败,找不到 GameObject:{0}".Format(gameObjectName));
                Clear();
                return;
            }
            // 给对象添加组件
            Component component = go.GetComponent(type);

            if (!component) { component = go.AddComponent(type); }

            // ViewController的序列化对象
            SerializedObject serializedObject = new SerializedObject(component);

            RootViewControllerInfo rootViewController = new RootViewControllerInfo {
                GameObjectName = gameObjectName.Value
            };

            // 搜索所有绑定
            BindCollector.SearchBind(go.transform, "", rootViewController);

            // 循环设置Bind
            foreach (BindInfo bindInfo in rootViewController.BindInfoList) {
                string name = bindInfo.Name;
                string componentName = bindInfo.BindScript.ComponentName.Split('.').Last();
                serializedObject.FindProperty(name).objectReferenceValue = go.transform.Find(bindInfo.PathToElement).GetComponent(componentName);
            }
            
            // 生成Prefab
            ViewController controller = go.GetComponent<ViewController>();
            serializedObject.FindProperty("Namespace").stringValue = controller.Namespace;
            serializedObject.FindProperty("ScriptName").stringValue = controller.ScriptName;
            serializedObject.FindProperty("ScriptPath").stringValue = controller.ScriptPath;
            serializedObject.FindProperty("PrefabPath").stringValue = controller.PrefabPath;
            
            if (controller.GetType() != type) {
                // 立即销毁，不允许Asset被销毁
                Object.DestroyImmediate(controller, false);
            }
            serializedObject.ApplyModifiedPropertiesWithoutUndo();

            string path = "Assets/" + controller.PrefabPath;
            
            // 如果不存在，则生成文件夹
            DirectoryUtils.Create(path);

            // 保存预设
            EditorUtils.SavePrefab(go, path + "/{0}.prefab".Format(go.name) );

            // 清理缓存数据
            Clear();
            
            // 标记场景未保存
            EditorUtils.MarkCurrentSceneDirty();
            
            Log.Info("生成脚本: 编译完成");
            
        }

        // 清理缓存数据
        private static void Clear()
        {
            generateNamespace.Clear();
            gameObjectName.Clear();
        }
    }
}
