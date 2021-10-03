namespace IFramework.Engine
{
    public sealed class AssetBundleSceneCreator : IResourceCreator
    {
        public bool Match(ResourceSearcher searcher)
        {
            AssetInfo assetInfo = AssetBundleConfig.ConfigFile.GetAssetInfo(searcher);

            if (assetInfo != null) {
                return assetInfo.AssetType == ResourceLoadType.ASSET_BUNDLE_SCENE;
            }
            return false;
        }

        public IResource Create(ResourceSearcher searcher) { return AssetBundleScene.Allocate(searcher.AssetName); }
    }
}
