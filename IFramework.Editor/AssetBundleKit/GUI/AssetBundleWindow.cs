using System.Collections.Generic;
using System.IO;
using System.Linq;
using IFramework.Core;
using UnityEditor;
using UnityEngine;

namespace IFramework.Editor
{
    public sealed class AssetBundleWindow : EditorWindow
    {
        private bool isViewChanged = true;
        private Vector2 scrollPosition;
        private List<string> signedList;

        public static void Open()
        {
            //创建窗口
            AssetBundleWindow window = GetWindow<AssetBundleWindow>(false, "资源管理器");
            window.Show();
        }

        private void OnEnable()
        {
            KeyEvent.Register(EventEnums.AssetBundleMark, key => isViewChanged = true);
        }

        private void LoadMarkedList()
        {
            signedList = AssetDatabase.GetAllAssetBundleNames()
                   .SelectMany(asset => {
                        string[] result = AssetDatabase.GetAssetPathsFromAssetBundle(asset);
                        return result.Select(assetName => {
                                    if (AssetBundleMark.CheckMarked(assetName)) {
                                        return assetName;
                                    }
                                    if (AssetBundleMark.CheckMarked(Path.GetDirectoryName(assetName))) {
                                        return Path.GetDirectoryName(assetName);
                                    }
                                    return null;
                                })
                               .Where(assetName => assetName != null)
                               .Distinct();
                    })
                   .ToList();
        }

        //绘制窗口时调用
        private void OnGUI()
        {
            if (isViewChanged) {
                LoadMarkedList();
                isViewChanged = false;
            }
            GUILayout.Space(20);

            // PersistentPath
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.TextField("PersistentPath:", Application.persistentDataPath);
            if (GUILayout.Button("打开目录", GUILayout.Width(100))) {
                EditorUtility.RevealInFinder(Application.persistentDataPath);
            }
            EditorGUILayout.EndHorizontal();
            GUILayout.Space(10);

            // 选择平台
            Configure.CurrentPlatform.Value = GUILayout.Toolbar(Configure.CurrentPlatform.Value, new[] { "Window", "MacOS", "iOS", "Android", "WebGL", "PS4", "PS5", "XboxOne" });
            GUILayout.Space(10);

            // 是否自动生成常量
            Configure.ResourceKit.AutoGenerateName.Value = GUILayout.Toggle(Configure.ResourceKit.AutoGenerateName.Value, "打 AB 包时，自动生成资源名常量代码");
            GUILayout.Space(10);

            // 模拟模式
            Configure.ResourceKit.IsSimulation.Value = GUILayout.Toggle(Configure.ResourceKit.IsSimulation.Value, "模拟模式（勾选后每当资源修改时无需再打 AB 包，开发阶段建议勾选，打真机包时取消勾选并打一次 AB 包）");

            // 操作按钮
            GUILayout.Space(10);
            if (GUILayout.Button("生成 AB 包")) {
                AssetBundleBuilder.BuildAssetBundles();
            }
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("生成 AB 常量")) {
                AssetBundleScript.GenerateConstScript();
            }
            if (GUILayout.Button("清空已生成的 AB 包")) {
                AssetBundleBuilder.ForceClearAssetBundles();
            }
            GUILayout.EndHorizontal();

            // 标记的资源
            GUILayout.Label("已标记的资源:");
            scrollPosition = GUILayout.BeginScrollView(scrollPosition, false, true);
            foreach (string assetsName in signedList) {
                GUILayout.BeginHorizontal();
                GUILayout.Label(assetsName);
                if (GUILayout.Button("选中", GUILayout.Width(60))) {
                    Selection.objects = new[] { AssetDatabase.LoadAssetAtPath<Object>(assetsName) };
                }
                if (GUILayout.Button("取消标记", GUILayout.Width(60))) {
                    AssetBundleMark.MarkAssetBundle(assetsName);
                }
                GUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();
            GUILayout.Space(10);
        }
    }
}
