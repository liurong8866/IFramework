namespace IFramework.Engine
{
    public sealed class AssetResourceCreator : IResourceCreator
    {
        public bool Match(ResourceSearcher searcher)
        {
            AssetInfo assetInfo = AssetBundleConfig.ConfigFile.GetAssetInfo(searcher);
            if (assetInfo != null) {
                return assetInfo.AssetType == ResourceLoadType.ASSET_BUNDLE_ASSET;
            }

            // TODO
            // foreach (var subProjectAssetBundleConfigFile in AssetBundleSettings.SubProjectAssetBundleConfigFiles)
            // {
            //     assetData = subProjectAssetBundleConfigFile.GetAssetData(resSearchKeys);
            //     
            //     if (assetData != null)
            //     {
            //         return assetData.AssetType == ResLoadType.ABAsset;
            //     }
            // }
            return false;
        }

        public IResource Create(ResourceSearcher searcher)
        {
            return AssetResource.Allocate(searcher.AssetName, searcher.AssetBundleName, searcher.AssetType);
        }
    }
}
