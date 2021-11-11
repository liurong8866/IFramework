using System;

namespace IFramework.Engine
{
    [Serializable]
    public class AssetBundleDatas
    {
        public AssetBundleData[] AssetBundles;
    }

    [Serializable]
    public class AssetBundleData
    {
        public string Key;

        public AssetInfo[] AssetInfos;

        public AssetDependence[] AssetDependencies;
    }
}
