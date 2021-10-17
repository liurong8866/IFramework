using System.Collections.Generic;
using System.Linq;
using IFramework.Core;

namespace IFramework.Engine
{
    public class UIPanelTable : Table<IPanel>
    {
        /// <summary>
        /// 查找PanelList
        /// </summary>
        public IEnumerable<IPanel> GetPanelList(PanelSearcher searcher)
        {
            // 根据Key查找
            IEnumerable<IPanel> panelList = Get(searcher.Key);
            
            // 如果有PanelId，则继续查找
            if (searcher.PanelId.NotEmpty()) {
                panelList = panelList.Where(panel => panel.Info.PanelId == searcher.PanelId);
            }
            
            return panelList;
        }
        
        /// <summary>
        /// 查找第一个Panel
        /// </summary>
        public IPanel GetPanelFirst(PanelSearcher searcher)
        {
            return GetPanelList(searcher).FirstOrDefault();
        }
        
        /// <summary>
        /// 查找最后一个Panel
        /// </summary>
        public IPanel GetPanelLast(PanelSearcher searcher)
        {
            return GetPanelList(searcher).LastOrDefault();
        }
    }
}
