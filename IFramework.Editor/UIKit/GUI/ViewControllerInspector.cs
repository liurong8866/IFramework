using System;
using IFramework.Core;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IFramework.Editor
{
    [CustomEditor(typeof(ViewController), true)]
    public class ViewControllerInspector : UnityEditor.Editor
    {
        // 序列化的字段
        public SerializedViewController serializedViewController;
        private ViewController controller => target as ViewController;
        private GenerateInfo generateInfo;

        private bool overwrite1;
        private bool overwrite2;
        private bool overwrite3;

        private void OnEnable()
        {
            InitController();
        }

        private void Reset()
        {
            InitController();
        }
        
        private void InitController()
        {
            // 命名空间
            controller.Namespace.IfNothing(() => { controller.Namespace = Configure.UIKit.DefaultNameSpace.Value; });
            // 脚本名称取当前对象名
            controller.ScriptName.IfNothing(() => { controller.ScriptName = controller.name; });
            // 脚本路径
            controller.ScriptPath.IfNothing(() => { controller.ScriptPath = Configure.UIKit.ViewControllerScriptPath.Value; });
            // Prefab路径
            controller.PrefabPath.IfNothing(() => { controller.PrefabPath = Configure.UIKit.ViewControllerPrefabPath.Value; });
            // 生成信息
            generateInfo = new ViewControllerGenerateInfo(controller);
            // 初始化序列化字段
            serializedViewController = new SerializedViewController(serializedObject);
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            // 避免混用布局遮挡
            EditorGUILayout.GetControlRect();
            // 开始布局
            GUILayout.BeginVertical();

            // 命名空间
            GUILayout.BeginHorizontal();
            // EditorGUILayout.PrefixLabel("命名空间");
            EditorGUILayout.LabelField("命名空间", GUILayout.Width(70));
            serializedViewController.Namespace.stringValue = EditorGUILayout.TextField(serializedViewController.Namespace.stringValue).FormatName();
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            // 脚本名称
            GUILayout.BeginHorizontal();
            // EditorGUILayout.PrefixLabel("脚本名称");
            EditorGUILayout.LabelField("脚本名称", GUILayout.Width(70));
            GUILayout.Label("Assets/", GUILayout.Width(44));
            serializedViewController.ScriptName.stringValue = EditorGUILayout.TextField(serializedViewController.ScriptName.stringValue).FormatName();
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            // 生成路径
            GUILayout.BeginHorizontal();
            // EditorGUILayout.PrefixLabel("脚本路径");
            EditorGUILayout.LabelField("脚本路径", GUILayout.Width(70));
            GUILayout.Label("Assets/", GUILayout.Width(44));
            serializedViewController.ScriptPath.stringValue = EditorGUILayout.TextField(serializedViewController.ScriptPath.stringValue).FormatName();
            if (controller.AsScriptSubPath) {
                GUILayout.Label($"/{controller.ScriptName}/");
            }
            serializedViewController.AsScriptSubPath.boolValue = EditorGUILayout.Toggle(serializedViewController.AsScriptSubPath.boolValue, GUILayout.Width(20));
            GUILayout.EndHorizontal();
            // GUILayout.Space(5);
            // EditorGUILayout.HelpBox("勾选后，脚本名称作为子路径", MessageType.None, false);
            GUILayout.Space(5);

            // Prefab路径
            GUILayout.BeginHorizontal();
            // EditorGUILayout.PrefixLabel("预设路径");
            EditorGUILayout.LabelField("预设路径", GUILayout.Width(70));
            GUILayout.Label("Assets/", GUILayout.Width(44));
            serializedViewController.PrefabPath.stringValue = EditorGUILayout.TextField(serializedViewController.PrefabPath.stringValue).FormatName();
            if (controller.AsPrefabSubPath) {
                GUILayout.Label($"/{controller.ScriptName}/");
            }
            serializedViewController.AsPrefabSubPath.boolValue = EditorGUILayout.Toggle(serializedViewController.AsPrefabSubPath.boolValue, GUILayout.Width(20));
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            // EditorGUILayout.HelpBox("勾选后，路径将包含脚本名称", MessageType.None, false);

            // Prefab路径
            GUILayout.BeginHorizontal();
            // EditorGUILayout.PrefixLabel(" ");
            EditorGUILayout.LabelField(" ", GUILayout.Width(70));
            overwrite1 = EditorGUILayout.Toggle(overwrite1, GUILayout.Width(20));
            overwrite2 = EditorGUILayout.Toggle(overwrite2, GUILayout.Width(20));
            overwrite3 = EditorGUILayout.Toggle(overwrite3, GUILayout.Width(20));
            EditorGUILayout.LabelField("覆盖.cs文件，请选三次!");
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            // 注释
            EditorGUILayout.PrefixLabel("类注释");
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            serializedViewController.Comment.stringValue = EditorGUILayout.TextArea(serializedViewController.Comment.stringValue, GUILayout.Height(40));
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            // 触发变更Prefab
            serializedObject.ApplyModifiedProperties();

            // 提示
            EditorGUILayout.HelpBox("生成代码时会同时更新Prefab，如果没有则创建", MessageType.Info);
            GUILayout.Space(10);

            // 操作按钮
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("生成脚本")) {
                ViewControllerGenerator.GenerateCode(overwrite1 && overwrite2 && overwrite3);
                overwrite1 = overwrite2 = overwrite3 = false;

                // 结束GUI绘制，解决编辑器扩展运行报错EndLayoutGroup: BeginLayoutGroup must be called first
                GUIUtility.ExitGUI();
            }

            // 如果文件存在则显示
            if (FileUtils.Exists(generateInfo.ScriptAssetsClassName)) {
                // 加载类资源
                MonoScript scriptObject = AssetDatabase.LoadAssetAtPath<MonoScript>(generateInfo.ScriptAssetsClassName);
                if (GUILayout.Button("选择", GUILayout.Width(60))) {
                    Selection.objects = new Object[] { scriptObject };
                }
                if (GUILayout.Button("打开", GUILayout.Width(60))) {
                    AssetDatabase.OpenAsset(scriptObject);
                }
            }
            else {
                // 按钮变灰
                GUI.enabled = false;
                GUILayout.Button("选择", GUILayout.Width(60));
                GUILayout.Button("打开", GUILayout.Width(60));
                GUI.enabled = true;
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
    }

    /// <summary>
    /// 用于在Inspector自定义面板中更改时触发修改，保存Prefab
    /// </summary>
    public class SerializedViewController
    {
        public SerializedProperty Namespace;
        public SerializedProperty ScriptName;
        public SerializedProperty ScriptPath;
        public SerializedProperty AsScriptSubPath;
        public SerializedProperty PrefabPath;
        public SerializedProperty AsPrefabSubPath;
        public SerializedProperty Comment;

        public SerializedViewController(SerializedObject serializedObject)
        {
            Namespace = serializedObject.FindProperty("Namespace");
            ScriptName = serializedObject.FindProperty("ScriptName");
            ScriptPath = serializedObject.FindProperty("ScriptPath");
            AsScriptSubPath = serializedObject.FindProperty("AsScriptSubPath");
            PrefabPath = serializedObject.FindProperty("PrefabPath");
            AsPrefabSubPath = serializedObject.FindProperty("AsPrefabSubPath");
            Comment = serializedObject.FindProperty("Comment");
        }
    }
}
