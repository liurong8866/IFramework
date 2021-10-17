using System.Collections.Generic;

namespace IFramework.Engine
{
    public class UIPanelStack
    {
        private readonly Stack<PanelInfo> stack = new Stack<PanelInfo>();

        public void Push<T>() where T : UIPanel
        {
            Push(UIKit.GetPanel<T>());
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
                PanelSearcher searcher = PanelSearcher.Allocate();
                searcher.GameObjectName = view.Transform.name;
                UIManager.Instance.RemoveUI(searcher);
                searcher.Recycle();
            }
        }

        /// <summary>
        /// 出栈，代表打开UI界面
        /// </summary>
        public void Pop()
        {
            PanelInfo previousPanelInfo = stack.Pop();
            PanelSearcher searcher = PanelSearcher.Allocate();
            searcher.Level = previousPanelInfo.Level;
            searcher.AssetBundleName = previousPanelInfo.AssetBundleName;
            searcher.Keyword = previousPanelInfo.Key;
            searcher.TypeName = previousPanelInfo.PanelName;
            UIManager.Instance.OpenUI(searcher);
            searcher.Recycle();
        }
    }
}
