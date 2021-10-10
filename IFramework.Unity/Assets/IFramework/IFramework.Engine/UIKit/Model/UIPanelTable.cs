using System.Collections.Generic;
using System.Linq;
using IFramework.Core;

namespace IFramework.Engine
{
    public class UIPanelTable : Table<IPanel>
    {
        public IEnumerable<IPanel> GetPanelList(PanelSearcher searcher)
        {
            // 关键字相同 + GameObject名称相同/Panel是同一个对象
            if (searcher.Keyword.NotEmpty() && (searcher.GameObjectName.NotEmpty() || searcher.Panel.NotEmpty())) {
                return Get(searcher.Keyword)
                       .Where(p => p.Transform.name == searcher.GameObjectName || p == searcher.Panel);
            }

            if (searcher.Keyword.NotEmpty()) {
                return Get(searcher.Keyword);
            }

            if (searcher.Panel.NotEmpty()) {
                return Get(searcher.Panel.Transform.gameObject.name).Where(p => p == searcher.Panel);
            }

            if (searcher.GameObjectName.NotEmpty()) {
                return Get(searcher.GameObjectName);
            }
            return Enumerable.Empty<IPanel>();
        }
        
        // public IEnumerable<IPanel> GetPanelList(PanelSearcher searcher)
        // {
        //     if (searcher.Keyword.NotEmpty() && (searcher.GameObjectName.NotEmpty() || searcher.Panel.NotEmpty())) {
        //         return Get(searcher.Keyword)
        //                .Where(p => p.Transform.name == searcher.GameObjectName || p == searcher.Panel);
        //     }
        //
        //     if (searcher.Keyword.NotEmpty()) {
        //         return Get(searcher.Keyword);
        //     }
        //
        //     if (searcher.Panel.NotEmpty()) {
        //         return Get(searcher.Panel.Transform.gameObject.name).Where(p => p == searcher.Panel);
        //     }
        //
        //     if (searcher.GameObjectName.NotEmpty()) {
        //         return Get(searcher.GameObjectName);
        //     }
        //     return Enumerable.Empty<IPanel>();
        // }
    }
}
