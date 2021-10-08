using System;
using System.IO;
using IFramework.Core;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IFramework.Editor
{
    [CustomEditor(typeof(ViewController), true)]
    public class ViewControllerInspector : UnityEditor.Editor
    {
        private ViewController controller => target as ViewController;
        bool overwrite1 = false;
        bool overwrite2 = false;
        bool overwrite3 = false;
        
        private void OnEnable()
        {
            InitController();
        }

        private void InitController()
        {
            // 命名空间
            controller.Namespace.IfNullOrEmpty(() => { controller.Namespace = Configure.DefaultNameSpace.Value; });

            // 脚本名称取当前对象名
            controller.ScriptName.IfNullOrEmpty(() => { controller.ScriptName = controller.name; });

            // 脚本路径
            controller.ScriptPath.IfNullOrEmpty(() => { controller.ScriptPath = Configure.ViewControllerScriptPath.Value; });

            // Prefab路径
            controller.PrefabPath.IfNullOrEmpty(() => { controller.PrefabPath = Configure.ViewControllerPrefabPath.Value; });
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // 避免混用布局遮挡
            // EditorGUILayout.GetControlRect();

            // 开始布局
            GUILayout.BeginVertical();
            GUILayout.Space(15);
            // 命名空间
            GUILayout.BeginHorizontal();
            // EditorGUILayout.PrefixLabel("命名空间");
            EditorGUILayout.LabelField("命名空间", GUILayout.Width(70));
            controller.Namespace = EditorGUILayout.TextField(controller.Namespace).Trim();
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            // 脚本名称
            GUILayout.BeginHorizontal();
            // EditorGUILayout.PrefixLabel("脚本名称");
            EditorGUILayout.LabelField("脚本名称", GUILayout.Width(70));
            GUILayout.Label("Assets/", GUILayout.Width(44));
            controller.ScriptName = EditorGUILayout.TextField(controller.ScriptName).Trim();
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            // 生成路径
            GUILayout.BeginHorizontal();
            // EditorGUILayout.PrefixLabel("脚本路径");
            EditorGUILayout.LabelField("脚本路径", GUILayout.Width(70));
            GUILayout.Label("Assets/", GUILayout.Width(44));
            controller.ScriptPath = EditorGUILayout.TextField(controller.ScriptPath).Trim();

            if (controller.AsScriptSubPath) { GUILayout.Label($"/{controller.ScriptName}/"); }
            controller.AsScriptSubPath = EditorGUILayout.Toggle(controller.AsScriptSubPath, GUILayout.Width(20));
            GUILayout.EndHorizontal();
            // GUILayout.Space(5);
            // EditorGUILayout.HelpBox("勾选后，脚本名称作为子路径", MessageType.None, false);
            GUILayout.Space(5);

            // Prefab路径
            GUILayout.BeginHorizontal();
            // EditorGUILayout.PrefixLabel("预设路径");
            EditorGUILayout.LabelField("预设路径", GUILayout.Width(70));
            GUILayout.Label("Assets/", GUILayout.Width(44));
            controller.PrefabPath = EditorGUILayout.TextField(controller.PrefabPath).Trim();

            if (controller.AsPrefabSubPath) { GUILayout.Label($"/{controller.ScriptName}/"); }
            controller.AsPrefabSubPath = EditorGUILayout.Toggle(controller.AsPrefabSubPath, GUILayout.Width(20));
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
            EditorGUILayout.LabelField("覆盖.cs文件，危险操作，请选三次!!!");
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            
            // 注释
            EditorGUILayout.PrefixLabel("类注释");
            GUILayout.Space(5);
            GUILayout.BeginHorizontal();
            controller.Comment = EditorGUILayout.TextArea(controller.Comment, GUILayout.Height(40));
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            
            // 提示
            EditorGUILayout.HelpBox("生成代码时会同时更新Prefab，如果没有则创建", MessageType.Info);
            GUILayout.Space(10);

            // 操作按钮
            GUILayout.BeginHorizontal();

            if (GUILayout.Button("生成脚本")) {
                ViewControllerScript.GenerateCode(overwrite1 && overwrite2 && overwrite3);
                // 结束GUI绘制，解决编辑器扩展运行报错EndLayoutGroup: BeginLayoutGroup must be called first
                GUIUtility.ExitGUI();
            }

            // 如果文件存在则显示
            if (File.Exists(controller.ScriptAssetsClassName)) {
                // 加载类资源
                MonoScript scriptObject = AssetDatabase.LoadAssetAtPath<MonoScript>(controller.ScriptAssetsClassName);

                if (GUILayout.Button("选择", GUILayout.Width(60))) { Selection.objects = new Object[] { scriptObject }; }

                if (GUILayout.Button("打开", GUILayout.Width(60))) { AssetDatabase.OpenAsset(scriptObject); }
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
}
