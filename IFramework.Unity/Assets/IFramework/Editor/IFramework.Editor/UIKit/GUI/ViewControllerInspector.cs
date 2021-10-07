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
            controller.ScriptsPath.IfNullOrEmpty(() => { controller.ScriptsPath = Configure.ViewControllerScriptPath.Value; });

            // Prefab路径
            controller.PrefabPath.IfNullOrEmpty(() => { controller.PrefabPath = Configure.ViewControllerPrefabPath.Value; });
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            //
            GUILayout.BeginVertical();
            //
            GUILayout.BeginHorizontal();
            GUILayout.Label("命名空间", GUILayout.Width(60));
            controller.Namespace = EditorGUILayout.TextField(controller.Namespace);
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            //
            GUILayout.BeginHorizontal();
            GUILayout.Label("脚本名称", GUILayout.Width(60));
            controller.ScriptName = EditorGUILayout.TextField(controller.ScriptName);
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            //
            GUILayout.BeginHorizontal();
            GUILayout.Label("生成路径", GUILayout.Width(60));
            controller.ScriptsPath = EditorGUILayout.TextField(controller.ScriptsPath);
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            //
            EditorGUILayout.HelpBox("代码生成", MessageType.Info);
            GUILayout.Space(10);

            // if (
            //     Event.current.type == EventType.DragUpdated
            //  && sfxPathRect.Contains(Event.current.mousePosition)
            // ) {
            //     //改变鼠标的外表  
            //     DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
            //
            //     if (DragAndDrop.paths != null && DragAndDrop.paths.Length > 0) {
            //         if (DragAndDrop.paths[0] != "") {
            //             var newPath = DragAndDrop.paths[0];
            //             controller.ScriptsPath = newPath;
            //             AssetDatabase.SaveAssets();
            //             AssetDatabase.Refresh();
            //             EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            //         }
            //     }
            // }
            // GUILayout.BeginHorizontal();
            //
            // controller.GeneratePrefab =
            //         GUILayout.Toggle(controller.GeneratePrefab, "生成Prefab");
            // GUILayout.EndHorizontal();
            //
            // if (controller.GeneratePrefab) {
            //     GUILayout.BeginHorizontal();
            //     GUILayout.Label("Prefab路径", GUILayout.Width(150));
            //     controller.ScriptsPath = GUILayout.TextArea(controller.ScriptsPath, GUILayout.Height(30));
            //     GUILayout.EndHorizontal();
            // }
            // var fileFullPath = controller.ScriptsPath + "/" + controller.ScriptName + ".cs";
            //
            // if (File.Exists(controller.ScriptsPath + "/" + controller.ScriptName + ".cs")) {
            //     var scriptObject = AssetDatabase.LoadAssetAtPath<MonoScript>(fileFullPath);
            //
            //     if (GUILayout.Button("打开脚本", GUILayout.Height(30))) { AssetDatabase.OpenAsset(scriptObject); }
            //
            //     // if (GUILayout.Button("选择脚本", GUILayout.Height(30))) { Selection.objects = new Object[] { scriptObject }; }
            // }
            //
            GUILayout.EndVertical();
        }
    }
}
