using System.IO;
using IFramework.Core;
using UnityEditor;
using UnityEngine;

namespace IFramework.Editor
{
    public static class MainMenu
    {
        // 快捷键的设置
        // % - CTRL on Windows / CMD on OSX
        // # - Shift
        // & - Alt
        // LEFT/RIGHT/UP/DOWN - Arrow keys
        // F1 … F2 - F keys
        // HOME,END,PGUP,PGDN 
        // 字母键 - _ + 字母（如:_g代表按键）

        // 两个MenuItem 的下标相差20以上 就会出现 分割线

        /*----------------------------- IFramework -----------------------------*/

        public const string CON_MENU_TOOL_RESKIT = "IFramework/AssetBundle";

        [MenuItem(CON_MENU_TOOL_RESKIT, false, 1)]
        private static void AssetBundleWindow()
        {
            AssetBundleKit.OpenAssetBundleWindow();
        }

        public const string CON_MENU_TOOL_UIKIT = "IFramework/UI Config";

        [MenuItem(CON_MENU_TOOL_UIKIT, false, 31)]
        private static void UIKitWindow()
        {
            UIKit.OpenUIConfigWindow();
        }

        public const string CON_MENU_TOOL_CLEAR = "IFramework/Tools/Clear Cache";

        [MenuItem(CON_MENU_TOOL_CLEAR, false, 51)]
        private static void Clear()
        {
            Log.Info("缓存数据清理 开始！");
            PlayerPrefs.DeleteAll();
            Directory.Delete(Application.persistentDataPath, true);
            if (EditorApplication.isPlaying) {
                EditorApplication.isPlaying = false;
            }
            Log.Info("缓存数据清理 完成！");
        }
        
        public const string CON_MENU_ASSET_REMOVE_MISS = "IFramework/Tools/Clear Missing";
        [MenuItem(CON_MENU_ASSET_REMOVE_MISS, false, 52)]
        private static void AssetRemoveMiss1()
        {
            UIKit.AssetRemoveMiss();
        }

        /*----------------------------- GameObject 右键菜单 -----------------------------*/

        
        public const string CON_MENU_UIROOT = "GameObject/IFramework - UIRoot &r";

        [MenuItem(CON_MENU_UIROOT, false, 31)]
        private static void UIKitAddUIRoot()
        {
            UIKit.AddUIRootScript();
        }

        [MenuItem(CON_MENU_UIROOT, true)]
        private static bool UIKitAddUIRootValidate()
        {
            return UIKit.AddUIRootScriptValidate();
        }
        
        public const string CON_MENU_BIND = "GameObject/IFramework - Bind &b";

        [MenuItem(CON_MENU_BIND, false, 32)]
        private static void UIKitBind()
        {
            UIKit.AddBindScript();
        }
        
        [MenuItem(CON_MENU_BIND, true)]
        private static bool UIKitBindValidate()
        {
            return UIKit.AddBindScriptValidate();
        }
        
        public const string CON_MENU_VIEW = "GameObject/IFramework - View Controller &v";

        [MenuItem(CON_MENU_VIEW, false, 33)]
        private static void UIKitAddView()
        {
            UIKit.AddViewScript();
        }

        [MenuItem(CON_MENU_VIEW, true)]
        private static bool UIKitAddViewValidate()
        {
            return UIKit.AddBindScriptValidate();
        }
        
        
        // public const string CON_MENU_GENCODE = "GameObject/I Kit - Generate Code";
        //
        // [MenuItem(CON_MENU_GENCODE, false, 33)]
        // private static void UIKitCreateCode()
        // {
        //     UIKit.ViewControllerGenerate();
        // }

        /*----------------------------- Asset 右键菜单 -----------------------------*/

        public const string CON_MENU_ASSET_MARK = "Assets/IFramework - Mark AssetBundle";

        [MenuItem(CON_MENU_ASSET_MARK, false, 120)]
        private static void MarkAssetBundle()
        {
            AssetBundleKit.MarkAssetBundle();
        }

        public const string CON_MENU_ASSET_GENCODE = "Assets/IFramework - Generate UI Code";

        [MenuItem(CON_MENU_ASSET_GENCODE, false, 121)]
        private static void AssetCreateCode()
        {
            UIKit.UIPanelGenerate();
        }

        // [MenuItem(CON_MENU_ASSET_REMOVE_MISS, false, 122)]
        // private static void AssetRemoveMiss()
        // {
        //     UIKit.AssetRemoveMiss();
        // }
        
        // [MenuItem("Assets/I Kit - EditorWindow 生命周期", true, 123)]
        // private static void OpguiWindowLifeCircle()
        // {
        //     GuiWindowLifeCircle.Open();
        // }
    }
}
