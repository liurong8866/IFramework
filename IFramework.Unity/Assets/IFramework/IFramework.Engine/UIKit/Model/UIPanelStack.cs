using System.Collections.Generic;

namespace IFramework.Engine
{
    public class UIPanelStack
    {
        private readonly Stack<PanelInfo> stack = new Stack<PanelInfo>();

        public void Push<T>() where T : UIPanel
        {
            // Push();
        }

        /// <summary>
        /// 入栈，代表UI界面从屏幕消除
        /// </summary>
        /// <param name="view"></param>
        public void Push(IPanel view)
        {
            if (view != null) {
                stack.Push(view.Info);
                view.Close();
                PanelSearcher panelSearcher = PanelSearcher.Allocate();
                panelSearcher.GameObjectName = view.Transform.name;
                UIManager.Instance.RemoveUI(panelSearcher);
                panelSearcher.Recycle();
            }
        }

        public void Pop()
        {
            PanelInfo previousPanelInfo = stack.Pop();
            PanelSearcher searcher = PanelSearcher.Allocate();
            searcher.GameObjectName = previousPanelInfo.GameObjectName;
            searcher.Level = previousPanelInfo.Level;
            searcher.Data = previousPanelInfo.Data;
            searcher.AssetBundleName = previousPanelInfo.AssetBundleName;
            searcher.Keyword = previousPanelInfo.PanelType;
            UIManager.Instance.OpenUI(searcher);
            searcher.Recycle();
        }
    }
}
