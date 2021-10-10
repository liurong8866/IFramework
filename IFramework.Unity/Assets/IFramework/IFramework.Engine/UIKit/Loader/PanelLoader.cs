using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    public class PanelLoader : IPanelLoader
    {
        ResourceLoader loader = ResourceLoader.Allocate();
        
        public GameObject Load(PanelSearcher searcher)
        {
            if (searcher.PanelType.IsNotNullOrEmpty() && searcher.GameObjectName.IsNullOrEmpty()) {
                return loader.Load<GameObject>(searcher.PanelType.Name);
            }

            if (searcher.AssetBundleName.IsNotNullOrEmpty()) {
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
