using UnityEngine;

namespace IFramework.Engine
{
    public sealed class AssetBundleResourceCreator : IResourceCreator
    {
        public bool Match(ResourceSearcher searcher)
        {
            return searcher.AssetType == typeof(AssetBundle);
        }

        public IResource Create(ResourceSearcher searcher)
        {
            return AssetBundleResource.Allocate(searcher.AssetName);
        }
    }
}
