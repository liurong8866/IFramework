using IFramework.Core;
using UnityEditor;
using UnityEngine;

namespace IFramework.Editor
{
    public class UIConfigWindow : EditorWindow
    {
        public static void Open()
        {
            //创建窗口
            UIConfigWindow window = GetWindow<UIConfigWindow>(false, "UIKit 参数设置");
            window.Show();
        }

        //绘制窗口时调用
        private void OnGUI()
        {
            GUILayout.Space(20);

            // 脚本默认命名空间
            EditorGUILayout.BeginHorizontal();
            Configure.DefaultNameSpace.Value = EditorGUILayout.TextField("默认命名空间: ", Configure.DefaultNameSpace.Value);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(20);

            // UIPanel 脚本路径
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("UIPanel 脚本: ");
            EditorGUILayout.LabelField("Assets/", GUILayout.Width(44));
            Configure.UIScriptPath.Value = EditorGUILayout.TextField(Configure.UIScriptPath.Value);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);
            EditorGUILayout.HelpBox("UI脚本会分析 UIPrefab 所在的目录结构动态生成相对路径", MessageType.None, false);
            
            GUILayout.Space(5);
            // UIPanel 预设路径
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("UIPanel Prefab: ");
            EditorGUILayout.LabelField("Assets/", GUILayout.Width(44));
            Configure.UIPrefabPath.Value = EditorGUILayout.TextField(Configure.UIPrefabPath.Value);
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(20);

            // ViewController 脚本路径
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("ViewController 脚本: ");
            EditorGUILayout.LabelField("Assets/", GUILayout.Width(44));
            Configure.ViewControllerScriptPath.Value = EditorGUILayout.TextField(Configure.ViewControllerScriptPath.Value);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(5);

            // ViewController 预设路径
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("ViewController Prefab: ");
            EditorGUILayout.LabelField("Assets/", GUILayout.Width(44));
            Configure.ViewControllerPrefabPath.Value = EditorGUILayout.TextField(Configure.ViewControllerPrefabPath.Value);
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(20);

            // 描述信息
            EditorGUILayout.HelpBox("所有生成路径均在Assets文件夹内，请记得修改默认命名空间！", MessageType.Info);
            GUILayout.Space(30);

            // 恢复默认
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("");

            if (GUILayout.Button("      恢复默认      ")) {
                // 取消 EditorGUILayout.TextField 焦点，否则不更新。 GUILayout.TextField没有这个问题
                GUI.FocusControl(null);
                UIKit.ResetConfig();
            }
            // 加空元素是为了占位
            GUILayout.Label("");
            // EditorGUILayout.LabelField("");
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(10);
        }
    }
}
