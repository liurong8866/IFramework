using System.IO;
using IFramework.Core;
using IFramework.Editor.Settings;
using UnityEditor;

namespace IFramework.Editor
{
    public class AssetBundleMark
    {
        [InitializeOnLoadMethod]
        private static void OnLoad()
        {
            Selection.selectionChanged = SelectionChanged;
        }

        private static void SelectionChanged()
        {
            string path = EditorUtils.SelectedAssetsPath();
            if (!string.IsNullOrEmpty(path)) {
                // "Assets/I Kit - Mark AssetBundle";
                Menu.SetChecked(MainMenu.CON_MENU_ASSET_MARK, CheckMarked(path));
            }
            else {
                // "Assets/I Kit - Mark AssetBundle";
                Menu.SetChecked(MainMenu.CON_MENU_ASSET_MARK, false);
            }
        }

        /// <summary>
        /// 标记资源为AssetBundle
        /// </summary>
        public static void MarkAssetBundle()
        {
            MarkAssetBundle(EditorUtils.SelectedAssetsPath());
        }

        /// <summary>
        /// 标记资源为AssetBundle
        /// </summary>
        public static void MarkAssetBundle(string path)
        {
            if (path.NotEmpty()) {
                // 根据路径获取AssetBundle
                AssetImporter ai = AssetImporter.GetAtPath(path);

                // 如果已标记，取消标记，否则标记
                if (CheckMarked(path)) {
                    Menu.SetChecked(MainMenu.CON_MENU_ASSET_MARK, false);
                    ai.assetBundleName = null;
                }
                else {
                    DirectoryInfo dir = new DirectoryInfo(path);
                    Menu.SetChecked(MainMenu.CON_MENU_ASSET_MARK, true);
                    ai.assetBundleName = dir.Name.Replace(".", "-");
                }

                // 消除无用
                AssetDatabase.RemoveUnusedAssetBundleNames();
                // 刷新至关重要，直接影响AssetBundleWindow的自动刷新
                AssetDatabase.Refresh();
                // 同时AssetBundleWindow加载数据
                KeyEvent.Send(EventEnums.AssetBundleMark);
            }
        }

        /// <summary>
        /// 检查是否标记过。根据文件路径与assetBundleName比较
        /// </summary>
        public static bool CheckMarked(string path)
        {
            AssetImporter ai = AssetImporter.GetAtPath(path);

            // if (ai.assetBundleName.IsNullOrEmpty()) return false;
            DirectoryInfo dir = new DirectoryInfo(path);
            return ai.assetBundleName.Equals(dir.Name.Replace(".", "-").ToLowerInvariant());
        }
    }
}
