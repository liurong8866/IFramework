using System;
using System.IO;
using IFramework.Core;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using Object = System.Object;

namespace IFramework.Editor
{
    [CustomEditor(typeof(ViewController), true)]
    public class ViewControllerInspector : UnityEditor.Editor
    {
        private ViewController controller => target as ViewController;

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

            // 开始布局
            GUILayout.BeginVertical();
            
            // 命名空间
            GUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("命名空间");
            // GUILayout.Label("命名空间", GUILayout.Width(100));
            controller.Namespace = EditorGUILayout.TextField(controller.Namespace).Trim();
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            // 脚本名称
            GUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("脚本名称");
            // GUILayout.Label("脚本名称", GUILayout.Width(60));
            GUILayout.Label("Assets/", GUILayout.Width(44));
            controller.ScriptName = EditorGUILayout.TextField(controller.ScriptName).Trim();
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            // 生成路径
            GUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("脚本路径");
            // GUILayout.Label("生成路径", GUILayout.Width(60));
            GUILayout.Label("Assets/", GUILayout.Width(44));
            controller.ScriptPath = EditorGUILayout.TextField(controller.ScriptPath).Trim();
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            // Prefab路径
            GUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("预设路径");
            // GUILayout.Label("生成路径", GUILayout.Width(60));
            GUILayout.Label("Assets/", GUILayout.Width(44));
            controller.PrefabPath = EditorGUILayout.TextField(controller.PrefabPath).Trim();
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            // 注释
            EditorGUILayout.PrefixLabel("注释");
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
                ViewControllerScript.GenerateCode();
                // 结束GUI绘制，解决编辑器扩展运行报错EndLayoutGroup: BeginLayoutGroup must be called first
                GUIUtility.ExitGUI();
            }

            // 全路径
            string fileFullPath = "Assets/" + controller.ScriptPath + "/" + controller.ScriptName + ".cs";

            // 如果文件存在则显示
            if (File.Exists(fileFullPath)) {
                MonoScript scriptObject = AssetDatabase.LoadAssetAtPath<MonoScript>(fileFullPath);

                if (GUILayout.Button("选择", GUILayout.Width(60))) { Selection.objects = new UnityEngine.Object[] { scriptObject }; }

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
