using System.Collections.Generic;
using System.Linq;
using IFramework.Core;

namespace IFramework.Engine
{
    public class UIPanelTable : Table<IPanel>
    {
        public IEnumerable<IPanel> GetPanelList(PanelSearcher searcher)
        {
            if (searcher.PanelType.NotEmpty() && (searcher.GameObjectName.NotEmpty() || searcher.Panel.NotEmpty())) {
                return Get(searcher.PanelType.FullName)
                       .Where(p => p.Transform.name == searcher.GameObjectName || p == searcher.Panel);
            }

            if (searcher.PanelType.NotEmpty()) {
                return Get(searcher.PanelType.FullName);
            }

            if (searcher.Panel.NotEmpty()) {
                return Get(searcher.Panel.Transform.gameObject.name).Where(p => p == searcher.Panel);
            }

            if (searcher.GameObjectName.NotEmpty()) {
                return Get(searcher.GameObjectName);
            }
            
            return Enumerable.Empty<IPanel>();
        }
    }
}
