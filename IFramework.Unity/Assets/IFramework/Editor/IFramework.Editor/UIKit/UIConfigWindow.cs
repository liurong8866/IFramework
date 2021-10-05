using IFramework.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace IFramework.Editor
{
    public class UIConfigWindow : EditorWindow
    {
        public static void Open()
        {
            //创建窗口
            UIConfigWindow window = GetWindow<UIConfigWindow>(false, "UI Kit 参数设置");
            window.Show();
        }
        
        
        //绘制窗口时调用
        private void OnGUI()
        {
            GUILayout.Space(20);

            // 脚本默认命名空间
            EditorGUILayout.BeginHorizontal();
            Configure.DefaultNameSpace.Value = EditorGUILayout.TextField("脚本默认命名空间: ", Configure.DefaultNameSpace.Value);
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            // UI脚本生成路径
            EditorGUILayout.BeginHorizontal();
            Configure.UIScriptPath.Value = EditorGUILayout.TextField("UI 脚本路径: ", Configure.UIScriptPath.Value);
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            // UI Prefab 生成路径
            EditorGUILayout.BeginHorizontal();
            Configure.UIPrefabPath.Value = EditorGUILayout.TextField("UI 预设路径: ", Configure.UIPrefabPath.Value);
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            // ViewController 脚本生成路径
            EditorGUILayout.BeginHorizontal();
            Configure.ViewControllerScriptPath.Value = EditorGUILayout.TextField("ViewController 脚本路径: ", Configure.ViewControllerScriptPath.Value);
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(10);
            
            // ViewController Prefab 生成路径
            EditorGUILayout.BeginHorizontal();
            Configure.ViewControllerPrefabPath.Value = EditorGUILayout.TextField("ViewController 预设路径: ", Configure.ViewControllerPrefabPath.Value);
            EditorGUILayout.EndHorizontal();
            
            GUILayout.Space(30);
            
            // 操作按钮
            if (GUILayout.Button("恢复默认")) {
                // 取消 EditorGUILayout.TextField 焦点，否则不更新。 GUILayout.TextField没有这个问题
                GUI.FocusControl(null);
                UIKit.ResetConfig();
            }
            
            // if (GUILayout.Button("生成 AB 常量")) { AssetBundleScript.GenerateConstScript(); }
            //
            // if (GUILayout.Button("清空已生成的 AB 包")) { AssetBundleBuilder.ForceClearAssetBundles(); }
            // GUILayout.EndHorizontal();

            GUILayout.Space(10);
        }
    }
}
