using System.Collections.Generic;
using System.Linq;
using IFramework.Core;

namespace IFramework.Engine
{
    /// <summary>
    /// Asset缓存表
    /// </summary>
    public sealed class AssetTable : Table<AssetInfo>
    {
        /// <summary>
        /// 获取资源
        /// </summary>
        public AssetInfo GetAssetInfo(ResourceSearcher searcher)
        {
            // 在缓存中获取资源
            string assetName = searcher.AssetName.ToLowerInvariant();
            List<AssetInfo> assetInfoList = Get(assetName);
            if (assetInfoList.Nothing()) return null;

            // 过滤AssetBundleName
            if (searcher.AssetBundleName.NotEmpty()) {
                assetInfoList = assetInfoList.Where(info => info.AssetBundleName == searcher.AssetBundleName).ToList();
            }

            // 过滤AssetType，
            if (searcher.AssetType.NotEmpty()) {
                short code = searcher.AssetType.ToCode();
                if (code != 0) {
                    List<AssetInfo> newInfo = assetInfoList.Where(info => info.AssetTypeCode == code).ToList();

                    // 如果找到就用，找不到就忽略
                    if (newInfo.Any()) {
                        assetInfoList = newInfo;
                    }
                }
            }
            return assetInfoList.FirstOrDefault();
        }
    }
}
