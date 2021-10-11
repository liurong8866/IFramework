using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// UI Took Kit
    /// </summary>
    public class UIKit
    {
        /// <summary>
        /// UIRoot
        /// </summary>
        public UIRoot Root => UIRoot.Instance;

        /// <summary>
        /// UI 堆栈
        /// </summary>
        public static readonly UIPanelStack Stack = new UIPanelStack();

        /// <summary>
        /// UIPanel  管理（数据结构）
        /// </summary>
        public static readonly UIPanelTable Table = new UIPanelTable();

        /// <summary>
        /// 打开Panel
        /// </summary>
        /// <param name="level">UI层级</param>
        /// <typeparam name="T">面板类型</typeparam>
        public static T OpenPanel<T>(UILevel level) where T : UIPanel
        {
            return OpenPanel<T>(level, PanelOpenType.Single, null);
        }

        /// <summary>
        /// 打开Panel
        /// </summary>
        /// <param name="openType">单例模式/原型模式</param>
        /// <typeparam name="T">面板类型</typeparam>
        public static T OpenPanel<T>(PanelOpenType openType) where T : UIPanel
        {
            return OpenPanel<T>(UILevel.Common, openType, null);
        }

        /// <summary>
        /// 打开Panel
        /// </summary>
        /// <param name="data">数据</param>
        /// <typeparam name="T">面板类型</typeparam>
        public static T OpenPanel<T>(IData data) where T : UIPanel
        {
            return OpenPanel<T>(UILevel.Common, PanelOpenType.Single, data);
        }

        /// <summary>
        /// 打开Panel
        /// </summary>
        /// <param name="level">UI层级</param>
        /// <param name="openType">单例模式/原型模式</param>
        /// <typeparam name="T">面板类型</typeparam>
        public static T OpenPanel<T>(UILevel level, PanelOpenType openType) where T : UIPanel
        {
            return OpenPanel<T>(level, openType, null);
        }

        /// <summary>
        /// 打开Panel
        /// </summary>
        /// <param name="level">UI层级</param>
        /// <param name="openType">单例模式/原型模式</param>
        /// <param name="data">数据</param>
        /// <param name="assetBundleName">AssetBundle资源名称</param>
        /// <param name="prefabName">Prefab名称</param>
        /// <typeparam name="T">面板类型</typeparam>
        public static T OpenPanel<T>(UILevel level, PanelOpenType openType, IData data, string assetBundleName = null, string prefabName = null) where T : UIPanel
        {
            PanelSearcher searcher = PanelSearcher.Allocate();
            searcher.Level = level;
            searcher.OpenType = openType;
            searcher.Data = data;
            searcher.Keyword = typeof(T).FullName;
            searcher.TypeName = typeof(T).Name;
            searcher.AssetBundleName = assetBundleName;
            searcher.GameObjectName = prefabName;

            // 打开UI
            T panel = UIManager.Instance.OpenUI(searcher) as T;
            searcher.Recycle();
            return panel;
        }

        /// <summary>
        /// 获取面板
        /// </summary>
        public static T GetPanel<T>() where T : UIPanel
        {
            PanelSearcher searcher = PanelSearcher.Allocate();
            searcher.Keyword = typeof(T).FullName;
            searcher.TypeName = typeof(T).Name;
            IPanel panel = UIManager.Instance.GetUI(searcher);
            searcher.Recycle();
            return panel as T;
        }

        /// <summary>
        /// 显示面板
        /// </summary>
        public static void ShowPanel<T>() where T : UIPanel
        {
            PanelSearcher searcher = PanelSearcher.Allocate();
            searcher.Keyword = typeof(T).FullName;
            searcher.TypeName = typeof(T).Name;
            UIManager.Instance.ShowUI(searcher);
            searcher.Recycle();
        }

        /// <summary>
        /// 隐藏面板
        /// </summary>
        public static void HidePanel<T>() where T : UIPanel
        {
            PanelSearcher searcher = PanelSearcher.Allocate();
            searcher.Keyword = typeof(T).FullName;
            searcher.TypeName = typeof(T).Name;
            UIManager.Instance.HideUI(searcher);
            searcher.Recycle();
        }

        /// <summary>
        /// 隐藏所有面板
        /// </summary>
        public static void HideAllPanel()
        {
            UIManager.Instance.HideAllUI();
        }

        /// <summary>
        /// 关闭面板
        /// </summary>
        public static void ClosePanel<T>() where T : UIPanel
        {
            PanelSearcher searcher = PanelSearcher.Allocate();
            searcher.Keyword = typeof(T).FullName;
            searcher.TypeName = typeof(T).Name;
            UIManager.Instance.CloseUI(searcher);
            searcher.Recycle();
        }

        /// <summary>
        /// 关闭所有面板
        /// </summary>
        public static void CloseAllPanel()
        {
            UIManager.Instance.CloseAllUI();
        }

        /// <summary>
        /// 后退
        /// </summary>
        /// <param name="currentPanelName">当前面板名称</param>
        public static void Back(string currentPanelName)
        {
            if (currentPanelName.NotEmpty()) {
                PanelSearcher searcher = PanelSearcher.Allocate();
                searcher.Keyword = currentPanelName;
                searcher.TypeName = currentPanelName;
                searcher.GameObjectName = currentPanelName;
                UIManager.Instance.CloseUI(searcher);
                searcher.Recycle();
            }
            Stack.Pop();
        }

        /// <summary>
        /// 后退
        /// </summary>
        /// <param name="currentPanel">当前面板</param>
        public static void Back(UIPanel currentPanel)
        {
            if (currentPanel.NotEmpty()) {
                PanelSearcher searcher = PanelSearcher.Allocate();
                searcher.Keyword = currentPanel.GetType().FullName;
                searcher.TypeName = currentPanel.GetType().Name;
                searcher.GameObjectName = currentPanel.name;
                UIManager.Instance.CloseUI(searcher);
                searcher.Recycle();
            }
            Stack.Pop();
        }

        /// <summary>
        /// 后退
        /// </summary>
        /// <typeparam name="T">当前面板类</typeparam>
        public static void Back<T>()
        {
            PanelSearcher searcher = PanelSearcher.Allocate();
            searcher.Keyword = typeof(T).FullName;
            searcher.TypeName = typeof(T).Name;
            UIManager.Instance.CloseUI(searcher);
            searcher.Recycle();
            Stack.Pop();
        }

        /// <summary>
        /// 设置面板默认大小
        /// </summary>
        public static void ResetPanelSize(IPanel panel)
        {
            RectTransform rectTrans = panel.Transform.As<RectTransform>();
            rectTrans.offsetMin = Vector2.zero;
            rectTrans.offsetMax = Vector2.zero;
            rectTrans.anchoredPosition3D = Vector3.zero;
            rectTrans.anchorMin = Vector2.zero;
            rectTrans.anchorMax = Vector2.one;
            rectTrans.LocalScaleIdentity();
        }
    }
}
