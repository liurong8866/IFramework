using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    public class PanelLoader : IPanelLoader
    {
        private ResourceLoader loader = ResourceLoader.Allocate();

        public GameObject Load(PanelSearcher searcher)
        {
            if (searcher.PanelType.NotEmpty() && searcher.GameObjectName.Nothing()) {
                return loader.Load<GameObject>(searcher.PanelType.Name);
            }

            if (searcher.AssetBundleName.NotEmpty()) {
                return loader.Load<GameObject>(searcher.AssetBundleName, searcher.GameObjectName);
            }
            return loader.Load<GameObject>(searcher.GameObjectName);
        }

        public void Unload()
        {
            loader.Recycle();
            loader = null;
        }
    }
}
