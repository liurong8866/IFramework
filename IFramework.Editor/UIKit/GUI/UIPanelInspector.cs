using System;
using IFramework.Core;
using IFramework.Engine;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IFramework.Editor
{
    [CustomEditor(typeof(UIPanel), true)]
    public class UIPanelInspector : UnityEditor.Editor
    {
        private UIPanel panel => target as UIPanel;
        private UIPanelGenerateInfo panelGenerateInfo;

        private bool overwrite1;
        private bool overwrite2;
        private bool overwrite3;

        private void OnEnable()
        {
            GameObject obj = EditorUtils.SelectedObject() as GameObject;
            if (obj != null) {
                if (!EditorUtils.IsPrefab(obj)) {
                    obj = PrefabUtility.GetCorrespondingObjectFromSource(obj);
                    if (obj.Nothing()) {
                        return;
                    }
                }
                string prefabPath = AssetDatabase.GetAssetPath(obj);

                // 根据Prefab路径获取Script生成路径
                string scriptPath = DirectoryUtils.GetPathByFullName(prefabPath);

                // 取UIPrefab默认路径右侧路径
                scriptPath = scriptPath.Right(Configure.UIKit.UIPrefabPath.Value, false, true);

                // 组装生成信息
                panelGenerateInfo = new UIPanelGenerateInfo {
                    ScriptName = obj.name,
                    ScriptPath = DirectoryUtils.CombinePath(Configure.UIKit.UIScriptPath.Value, scriptPath)
                };
            }
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            // 避免混用布局遮挡
            EditorGUILayout.GetControlRect();

            // 开始布局
            GUILayout.BeginVertical();

            // Prefab路径
            GUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(" ");
            overwrite1 = EditorGUILayout.Toggle(overwrite1, GUILayout.Width(20));
            overwrite2 = EditorGUILayout.Toggle(overwrite2, GUILayout.Width(20));
            overwrite3 = EditorGUILayout.Toggle(overwrite3, GUILayout.Width(20));
            EditorGUILayout.LabelField("覆盖.cs文件，请选三次!");
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            // 操作按钮
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("生成脚本")) {
                UIPanelGenerator.GenerateCode(overwrite1 && overwrite2 && overwrite3);
                overwrite1 = overwrite2 = overwrite3 = false;
                // 结束GUI绘制，解决编辑器扩展运行报错EndLayoutGroup: BeginLayoutGroup must be called first
                GUIUtility.ExitGUI();
            }
            // 如果文件存在则显示
            if (panelGenerateInfo.NotEmpty() && FileUtils.Exists(panelGenerateInfo.ScriptAssetsClassName)) {
                // 加载类资源
                if (GUILayout.Button("选择", GUILayout.Width(60))) {
                    MonoScript scriptObject = AssetDatabase.LoadAssetAtPath<MonoScript>(panelGenerateInfo.ScriptAssetsClassName);
                    Selection.objects = new Object[] { scriptObject };
                }
                if (GUILayout.Button("打开", GUILayout.Width(60))) {
                    MonoScript scriptObject = AssetDatabase.LoadAssetAtPath<MonoScript>(panelGenerateInfo.ScriptAssetsClassName);
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
}
