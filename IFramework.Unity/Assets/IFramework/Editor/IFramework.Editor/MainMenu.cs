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
using UnityEditor;
using UnityEngine;

namespace IFramework.Editor
{
    public class MainMenu
    {
        // % - CTRL on Windows / CMD on OSX
        // # - Shift
        // & - Alt
        // LEFT/RIGHT/UP/DOWN - Arrow keys
        // F1 … F2 - F keys
        // HOME,END,PGUP,PGDN 
        // 字母键 - _ + 字母（如:_g代表按键）
        
        // IFramework
        public const string CON_MENU_TOOL_RESKIT = "IFramework/AssetBundle";
        public const string CON_MENU_TOOL_UIKIT = "IFramework/UI Setting";
        public const string CON_MENU_TOOL_CLEAR = "IFramework/Clear Data";

        [MenuItem(CON_MENU_TOOL_RESKIT, false, 1)]
        private static void AssetBundleWindow()
        {
            AssetBundleKit.OpenAssetBundleWindow();
        }
        
        [MenuItem(CON_MENU_TOOL_UIKIT, false, 21)]
        private static void UIKitWindow()
        {
        }
        
        [MenuItem(CON_MENU_TOOL_CLEAR, false, 31)]
        private static void Clear()
        {
            Log.Info("缓存数据清理 开始！");
            PlayerPrefs.DeleteAll();
            Directory.Delete(Application.persistentDataPath, true);
            if (EditorApplication.isPlaying)
            {
                EditorApplication.isPlaying = false;
            }
            Log.Info("缓存数据清理 完成！");
        }

        // GameObject的右键菜单
        public const string CON_MENU_BIND = "GameObject/I Kit - Bind %b"; 
        public const string CON_MENU_VIEW = "GameObject/I Kit - Add View";
        public const string CON_MENU_GENCODE = "GameObject/I Kit - Generate Code";
        
        [MenuItem(CON_MENU_BIND, false, 30)]
        private static void UiKitBind()
        {
            UIKit.BindScript();
        }
        
        [MenuItem(CON_MENU_VIEW, false, 31)]
        private static void UiKitAddView()
        {
            
        }
        
        [MenuItem(CON_MENU_GENCODE, false, 32)]
        private static void UiKitCreateCode()
        {
            
        }
        
        // Asset的右键菜单
        public const string CON_MENU_ASSET_MARK = "Assets/I Kit - Mark AssetBundle";
        public const string CON_MENU_ASSET_GENCODE = "Assets/I Kit - Generate Code";
        
        [MenuItem(CON_MENU_ASSET_MARK, false, 120)]
        private static void MarkAssetBundle()
        {
            AssetBundleKit.MarkAssetBundle();
        }

        [MenuItem(CON_MENU_ASSET_GENCODE, true, 121)]
        private static void AssetCreateCode()
        {
            
        }
        // 控制是否可用
        [MenuItem(CON_MENU_ASSET_GENCODE, false, 121)]
        private static bool AssetCreateCodeValidate()
        {
            return false;
        }
        
        [MenuItem("Assets/I Kit - EditorWindow 生命周期", true, 123)]
        private static void OPGUIWindowLifeCircle()
        {
            GUIWindowLifeCircle.Open();
        }
    }
}