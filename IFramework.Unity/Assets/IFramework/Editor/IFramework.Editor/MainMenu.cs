using System.IO;
using IFramework.Core;
using UnityEditor;
using UnityEngine;

namespace IFramework.Editor
{
    public class MainMenu
    {
        // 快捷键的设置
        // % - CTRL on Windows / CMD on OSX
        // # - Shift
        // & - Alt
        // LEFT/RIGHT/UP/DOWN - Arrow keys
        // F1 … F2 - F keys
        // HOME,END,PGUP,PGDN 
        // 字母键 - _ + 字母（如:_g代表按键）

        /*----------------------------- IFramework -----------------------------*/
        
        public const string CON_MENU_TOOL_RESKIT = "IFramework/AssetBundle";
        [MenuItem(CON_MENU_TOOL_RESKIT, false, 1)]
        private static void AssetBundleWindow()
        {
            AssetBundleKit.OpenAssetBundleWindow();
        }

        public const string CON_MENU_TOOL_UIKIT = "IFramework/UI Kit Config";
        [MenuItem(CON_MENU_TOOL_UIKIT, false, 21)]
        private static void UIKitWindow()
        {
            UIKit.OpenUIConfigWindow();
        }

        // public const string CON_MENU_TOOL_CLEAR = "IFramework/Clear Data";
        // [MenuItem(CON_MENU_TOOL_CLEAR, false, 31)]
        // private static void Clear()
        // {
        //     Log.Info("缓存数据清理 开始！");
        //     PlayerPrefs.DeleteAll();
        //     Directory.Delete(Application.persistentDataPath, true);
        //
        //     if (EditorApplication.isPlaying) { EditorApplication.isPlaying = false; }
        //     Log.Info("缓存数据清理 完成！");
        // }

        /*----------------------------- GameObject 右键菜单 -----------------------------*/

        public const string CON_MENU_BIND = "GameObject/I Kit - Bind %b";
        [MenuItem(CON_MENU_BIND, false, 30)]
        private static void UiKitBind()
        {
            UIKit.BindScript();
        }
        
        public const string CON_MENU_VIEW = "GameObject/I Kit - Add View";
        [MenuItem(CON_MENU_VIEW, false, 31)]
        private static void UiKitAddView() { }
        
        public const string CON_MENU_GENCODE = "GameObject/I Kit - Generate Code";
        [MenuItem(CON_MENU_GENCODE, false, 32)]
        private static void UiKitCreateCode() { }

        /*----------------------------- Asset 右键菜单 -----------------------------*/
        
        public const string CON_MENU_ASSET_MARK = "Assets/I Kit - Mark AssetBundle";
        [MenuItem(CON_MENU_ASSET_MARK, false, 120)]
        private static void MarkAssetBundle()
        {
            AssetBundleKit.MarkAssetBundle();
        }
        
        public const string CON_MENU_ASSET_GENCODE = "Assets/I Kit - Generate Code";
        [MenuItem(CON_MENU_ASSET_GENCODE, true, 121)]
        private static void AssetCreateCode() { }
        // 控制是否可用
        [MenuItem(CON_MENU_ASSET_GENCODE, false, 121)]
        private static bool AssetCreateCodeValidate()
        {
            return false;
        }

        // [MenuItem("Assets/I Kit - EditorWindow 生命周期", true, 123)]
        // private static void OpguiWindowLifeCircle()
        // {
        //     GuiWindowLifeCircle.Open();
        // }
    }
}
