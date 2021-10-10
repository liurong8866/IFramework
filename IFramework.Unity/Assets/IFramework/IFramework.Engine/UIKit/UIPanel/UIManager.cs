using IFramework.Core;

namespace IFramework.Engine
{
    public class UIManager : ManagerBehaviour<UIManager>
    {
        protected UIManager() { }

        /// <summary>
        /// 单例初始化
        /// </summary>
        public override void OnInit()
        {
            UIRoot uiRoot = UIRoot.Instance;
            Log.Info("currentUIRoot:" + uiRoot);
        }

        public IPanel CreateUI()
        {
            return null;
        }

        public void GetUI() { }

        public IPanel OpenUI(PanelSearcher searcher)
        {
            // if (searcher.OpenType == PanelOpenType.Single) {
            //     var retPanel = UIKit.Table.GetPanelsByPanelSearchKeys(searcher).FirstOrDefault();
            //
            //     if (retPanel == null) {
            //         retPanel = CreateUI(searcher);
            //     }
            //     retPanel.Open(searcher.Data);
            //     retPanel.Show();
            //     return retPanel;
            // }
            // else {
            //     var retPanel = CreateUI(searcher);
            //     retPanel.Open(searcher.Data);
            //     retPanel.Show();
            //     return retPanel;
            // }
            return null;
        }

        public void ShowUI(PanelSearcher searcher) { }

        public void HideUI(PanelSearcher searcher) { }

        public void HideAllUI() { }

        public void CloseUI(PanelSearcher searcher) { }

        public void CloseAllUI() { }

        public void RemoveUI(PanelSearcher searcher) { }

        public void RemoveAllUI() { }
    }
}
