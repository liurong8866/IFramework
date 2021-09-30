/*****************************************************************************
 * MIT License
 * 
 * Copyright (c) 2021 liurong
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *****************************************************************************/

using System.IO;
using IFramework.Core;
using IFramework.Editor.Settings;
using UnityEditor;

namespace IFramework.Editor {
    public class AssetBundleMark {

        [InitializeOnLoadMethod]
        static void OnLoad() {
            Selection.selectionChanged = SelectionChanged;
        }

        private static void SelectionChanged() {
            var path = EditorUtils.GetSelectedPath();
            if (!string.IsNullOrEmpty(path)) {
                Menu.SetChecked(MainMenu.CON_MENU_ASSET_MARK, CheckMarked(path));
            }
            else {
                Menu.SetChecked(MainMenu.CON_MENU_ASSET_MARK, false);
            }
        }

        /// <summary>
        /// 标记资源为AssetBundle
        /// </summary>
        public static void MarkAssetBundle() {
            MarkAssetBundle(EditorUtils.GetSelectedPath());
        }

        /// <summary>
        /// 标记资源为AssetBundle
        /// </summary>
        public static void MarkAssetBundle(string path) {
            if (path.IsNotNullOrEmpty()) {
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
        public static bool CheckMarked(string path) {
            AssetImporter ai = AssetImporter.GetAtPath(path);

            // if (ai.assetBundleName.IsNullOrEmpty()) return false;
            DirectoryInfo dir = new DirectoryInfo(path);
            return ai.assetBundleName.Equals(dir.Name.Replace(".", "-").ToLower());
        }

    }
}
