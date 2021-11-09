using System;
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
        /// <typeparam name="T">面板类型</typeparam>
        public static T OpenPanel<T>() where T : UIPanel
        {
            return OpenPanel<T>(UILevel.Common, PanelOpenType.Single, null);
        }

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
        /// <param name="data">数据</param>
        /// <typeparam name="T">面板类型</typeparam>
        public static T OpenPanel<T>(UILevel level, IData data) where T : UIPanel
        {
            return OpenPanel<T>(level, PanelOpenType.Single, data);
        }
        
        /// <summary>
        /// 打开Panel
        /// </summary>
        /// <param name="openType">单例模式/原型模式</param>
        /// <param name="data">数据</param>
        /// <typeparam name="T">面板类型</typeparam>
        public static T OpenPanel<T>(PanelOpenType openType, IData data) where T : UIPanel
        {
            return OpenPanel<T>(UILevel.Common, openType, data);
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
        public static T OpenPanel<T>(UILevel level, PanelOpenType openType, IData data, string assetBundleName = null) where T : UIPanel
        {
            PanelSearcher searcher = PanelSearcher.Allocate();
            searcher.Level = level;
            searcher.OpenType = openType;
            searcher.Data = data;
            searcher.Key = typeof(T).FullName;
            searcher.PanelName = typeof(T).Name;
            searcher.AssetBundleName = assetBundleName;

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
            searcher.Key = typeof(T).FullName;
            searcher.PanelName = typeof(T).Name;
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
            searcher.Key = typeof(T).FullName;
            UIManager.Instance.ShowUI(searcher);
            searcher.Recycle();
        }

        /// <summary>
        /// 隐藏面板
        /// </summary>
        public static void HidePanel<T>() where T : UIPanel
        {
            PanelSearcher searcher = PanelSearcher.Allocate();
            searcher.Key = typeof(T).FullName;
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
            searcher.Key = typeof(T).FullName;
            UIManager.Instance.CloseUI(searcher);
            searcher.Recycle();
        }
        
        /// <summary>
        /// 关闭面板
        /// </summary>
        public static void ClosePanel<T>(string paneId) where T : UIPanel
        {
            PanelSearcher searcher = PanelSearcher.Allocate();
            searcher.Key = typeof(T).FullName;
            searcher.PanelId = paneId;
            UIManager.Instance.CloseUI(searcher);
            searcher.Recycle();
        }
        
        /// <summary>
        /// 关闭面板，用于关闭多实例设置PanelId
        /// </summary>
        public static void ClosePanel(IPanel panel)
        {
            Type T = panel.GetType();
            PanelSearcher searcher = PanelSearcher.Allocate();
            searcher.Key = T.FullName;
            searcher.PanelId = panel.Info.PanelId;
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
                searcher.Key = currentPanelName;
                searcher.PanelName = currentPanelName;
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
                searcher.Key = currentPanel.GetType().FullName;
                searcher.PanelName = currentPanel.GetType().Name;
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
            searcher.Key = typeof(T).FullName;
            searcher.PanelName = typeof(T).Name;
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
            if (rectTrans != null) {
                rectTrans.offsetMin = Vector2.zero;
                rectTrans.offsetMax = Vector2.zero;
                rectTrans.anchoredPosition3D = Vector3.zero;
                rectTrans.anchorMin = Vector2.zero;
                rectTrans.anchorMax = Vector2.one;
                rectTrans.LocalScaleIdentity();
            }
            else {
                Log.Warning("要加载的{0}未找到RectTransform，请确认格式是否正确".Format(panel.Info?.PanelName));
            }
        }

        /// <summary>
        /// 内部类 给脚本层用的
        /// </summary>
        public static class ScriptUse
        {
            public static UIPanel OpenPanel(string panelName, UILevel level = UILevel.Common, string assetBundleName = null)
            {
                PanelSearcher searcher = PanelSearcher.Allocate();
                searcher.Level = level;
                searcher.Key = panelName;
                searcher.PanelName = panelName;
                searcher.AssetBundleName = assetBundleName;
                UIPanel panel = UIManager.Instance.OpenUI(searcher) as UIPanel;
                searcher.Recycle();
                return panel;
            }
            
            public static UIPanel GetPanel(string panelName)
            {
                PanelSearcher searcher = PanelSearcher.Allocate();
                searcher.Key = panelName;
                UIPanel panel = UIManager.Instance.GetUI(searcher) as UIPanel;
                searcher.Recycle();
                return panel;
            }
            
            public static void ShowPanel(string panelName)
            {
                PanelSearcher searcher = PanelSearcher.Allocate();
                searcher.Key = panelName;
                UIManager.Instance.ShowUI(searcher);
                searcher.Recycle();
            }
            
            public static void HidePanel(string panelName)
            {
                PanelSearcher searcher = PanelSearcher.Allocate();
                searcher.Key = panelName;
                UIManager.Instance.HideUI(searcher);
                searcher.Recycle();
            }

            public static void ClosePanel(string panelName)
            {
                PanelSearcher searcher = PanelSearcher.Allocate();
                searcher.Key = panelName;
                UIManager.Instance.CloseUI(searcher);
                searcher.Recycle();
            }

        }
    }
}
