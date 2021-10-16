using IFramework.Core;
using IFramework.Engine;
using UnityEditor;
using UnityEngine;

namespace IFramework.Editor
{
    [CustomEditor(typeof(UIPanel), true)]
    public class UIPanelInspector : UnityEditor.Editor
    {
        private ViewController controller => target as ViewController;
        private GenerateInfo generateInfo;

        private bool overwrite1;
        private bool overwrite2;
        private bool overwrite3;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // 避免混用布局遮挡
            EditorGUILayout.GetControlRect();
            
            // 开始布局
            GUILayout.BeginVertical();
            GUILayout.Space(15);
            
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
            
            // 操作按钮
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("生成脚本")) {
                ViewControllerGenerator.GenerateCode(overwrite1 && overwrite2 && overwrite3);
                // 结束GUI绘制，解决编辑器扩展运行报错EndLayoutGroup: BeginLayoutGroup must be called first
                GUIUtility.ExitGUI();
            }
            
            // // 如果文件存在则显示
            // if (File.Exists(generateInfo.ScriptAssetsClassName)) {
            //     // 加载类资源
            //     MonoScript scriptObject = AssetDatabase.LoadAssetAtPath<MonoScript>(generateInfo.ScriptAssetsClassName);
            //     if (GUILayout.Button("选择", GUILayout.Width(60))) {
            //         Selection.objects = new Object[] { scriptObject };
            //     }
            //     if (GUILayout.Button("打开", GUILayout.Width(60))) {
            //         AssetDatabase.OpenAsset(scriptObject);
            //     }
            // }
            // else {
            //     // 按钮变灰
            //     GUI.enabled = false;
            //     GUILayout.Button("选择", GUILayout.Width(60));
            //     GUILayout.Button("打开", GUILayout.Width(60));
            //     GUI.enabled = true;
            // }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();
        }
    }
}
