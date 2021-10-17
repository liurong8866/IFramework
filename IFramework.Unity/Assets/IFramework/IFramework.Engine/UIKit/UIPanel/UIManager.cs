using System;
using System.Collections.Generic;
using System.Linq;
using IFramework.Core;
using UnityEditor;

namespace IFramework.Engine
{
    public class UIManager : ManagerBehaviour<UIManager>
    {
        protected UIManager() { }

        /// <summary>
        /// 打开面板
        /// </summary>
        public IPanel OpenUI(PanelSearcher searcher)
        {
            IPanel panel = null;

            // 如果是单例模式，则在缓存中查找
            if (searcher.OpenType == PanelOpenType.Single) {
                // 根据searcher获取首个对象，如果找不到则创建
                panel = GetUI(searcher);
            }

            // 如果不是单例模式，或者为空，则创建UI对象
            panel ??= CreateUI(searcher);

            // 打开并显示
            panel.Open(searcher.Data);
            panel.Show();
            return panel;
        }

        /// <summary>
        /// 显示IPanel
        /// </summary>
        public void ShowUI(PanelSearcher searcher)
        {
            IPanel panel = GetUI(searcher);
            panel?.Show();
        }

        /// <summary>
        /// 隐藏IPanel
        /// </summary>
        public void HideUI(PanelSearcher searcher)
        {
            IPanel panel = GetUI(searcher);
            if (panel.Nothing()) return;

            // 清除缓存 如果是单例窗口，直接删除
            if (panel.Info.OpenType == PanelOpenType.Single) {
                panel.Hide();
            }
            // 如果是多窗口
            else {
                // 如果没有窗口ID则，关闭所有窗口
                if (searcher.PanelId.Nothing()) {
                    IEnumerable<IPanel> panelList = UIKit.Table.GetPanelList(searcher);
                    foreach (IPanel item in panelList) {
                        item.Hide();
                    }
                }
                // 有ID则删除对应ID的窗口
                else {
                    panel.Hide();
                }
            }
        }

        /// <summary>
        /// 隐藏所有IPanel
        /// </summary>
        public void HideAllUI()
        {
            foreach (List<IPanel> panel in UIKit.Table) {
                PanelSearcher searcher = PanelSearcher.Allocate();
                searcher.Key = panel[0].Info.Key;
                HideUI(searcher);
            }
        }

        /// <summary>
        /// 关闭IPanel
        /// </summary>
        public void CloseUI(PanelSearcher searcher)
        {
            IPanel panel = GetUI(searcher);
            if (panel.Nothing()) return;

            // 清除缓存 如果是单例窗口，直接删除
            if (panel.Info.OpenType == PanelOpenType.Single) {
                if (panel is UIPanel) {
                    // 关闭面板
                    panel.Close();
                    panel.Info.Recycle();
                    panel.Info = null;
                    UIKit.Table.Remove(searcher.Key);
                }
            }
            // 如果是多窗口
            else {
                // 如果没有窗口ID则，关闭所有窗口
                if (searcher.PanelId.Nothing()) {
                    IEnumerable<IPanel> panelList = UIKit.Table.GetPanelList(searcher);
                    foreach (IPanel item in panelList) {
                        // 关闭面板
                        item.Close();
                        item.Info.Recycle();
                        item.Info = null;
                    }
                    UIKit.Table.Remove(searcher.Key);
                }
                // 有ID则删除对应ID的窗口
                else {
                    if (panel is UIPanel) {
                        // 关闭面板
                        panel.Close();
                        UIKit.Table.Remove(searcher.Key, p => p.Info.PanelId == searcher.PanelId);
                        panel.Info.Recycle();
                        panel.Info = null;
                    }
                }
            }
        }

        /// <summary>
        /// 关闭所有IPanel
        /// </summary>
        public void CloseAllUI()
        {
            List<string> keys = UIKit.Table.GetKeys();
            foreach (string key in keys) {
                PanelSearcher searcher = PanelSearcher.Allocate();
                searcher.Key = key;
                CloseUI(searcher);
            }
            UIKit.Table.Clear();
        }

        /// <summary>
        /// 移除IPanel
        /// </summary>
        public void RemoveUI(PanelSearcher searcher)
        {
            UIKit.Table.Remove(searcher.Key);
        }

        /// <summary>
        /// 获取UIPanel
        /// </summary>
        public IPanel GetUI(PanelSearcher searcher)
        {
            return UIKit.Table.GetPanelFirst(searcher);
        }

        /// <summary>
        /// 创建UI
        /// </summary>
        public IPanel CreateUI(PanelSearcher searcher)
        {
            // 加载Panel
            PanelLoader loader = new PanelLoader();
            IPanel panel = loader.LoadPanel(searcher);

            // 设置Panel层级
            panel.PanelLevel(searcher.Level);
            // 重置默认大小
            panel.ResetPanelSize();

            // 设置GameObject名字，空则取面板类名称
            panel.Transform.gameObject.name = searcher.PanelName;
            searcher.PanelId = Guid.NewGuid().ToString();
            panel.Info = PanelInfo.Allocate(searcher);
            panel.Init(searcher.Data);

            // 使用searcher关键字作为键值缓存
            UIKit.Table.Add(searcher.Key, panel);
            return panel;
        }
    }
}
