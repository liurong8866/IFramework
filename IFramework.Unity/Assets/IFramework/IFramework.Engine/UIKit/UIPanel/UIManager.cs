using System.Linq;
using IFramework.Core;

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
            panel?.Hide();
        }

        /// <summary>
        /// 关闭IPanel
        /// </summary>
        public void CloseUI(PanelSearcher searcher)
        {
            IPanel panel = GetUI(searcher);

            if (panel is UIPanel) {
                panel.Close();
                UIKit.Table.Remove(searcher.Keyword);
                panel.Info.Recycle();
                panel.Info = null;
            }
        }

        /// <summary>
        /// 移除IPanel
        /// </summary>
        public void RemoveUI(PanelSearcher searcher)
        {
            UIKit.Table.Remove(searcher.Keyword);
        }

        /// <summary>
        /// 隐藏所有IPanel
        /// </summary>
        public void HideAllUI()
        {
            foreach (IPanel panel in UIKit.Table) {
                panel.Hide();
            }
        }

        /// <summary>
        /// 关闭所有IPanel
        /// </summary>
        public void CloseAllUI()
        {
            foreach (IPanel panel in UIKit.Table) {
                panel.Close();
                panel.Info.Recycle();
                panel.Info = null;
            }
            UIKit.Table.Clear();
        }

        /// <summary>
        /// 获取UIPanel
        /// </summary>
        public IPanel GetUI(PanelSearcher searcher)
        {
            return UIKit.Table.GetPanelList(searcher).FirstOrDefault();
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
            panel.Transform.gameObject.name = searcher.GameObjectName.NotEmpty() ? searcher.GameObjectName : searcher.TypeName;
            panel.Info = PanelInfo.Allocate(searcher.Keyword, searcher.TypeName, searcher.GameObjectName, searcher.Level, searcher.Data, searcher.AssetBundleName);

            // 使用searcher关键字作为键值缓存
            UIKit.Table.Add(searcher.Keyword, panel);
            panel.Init(searcher.Data);
            return panel;
        }
    }
}
