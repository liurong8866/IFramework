using System.Collections.Generic;
using System.Linq;
using IFramework.Core;

namespace IFramework.Engine
{
    /// <summary>
    /// Asset资源字典维护表
    /// </summary>
    public sealed class AssetBundleInfo
    {
        public string Key { get; }

        // 资源依赖关系列表
        public List<AssetDependence> AssetDepends { get; private set; }

        // AssetName作为Key的字典
        private Dictionary<string, AssetInfo> assetNameMap;
        public IEnumerable<AssetInfo> AssetInfos => assetNameMap.Values;

        // AssetName+AssetBundleName 作为Key的字典
        private Dictionary<string, AssetInfo> assetFullNameMap;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AssetBundleInfo(string key)
        {
            Key = key;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AssetBundleInfo(AssetBundleData data)
        {
            Key = data.Key;
            SetSerializeData(data);
        }

        /// <summary>
        /// 数据重置
        /// </summary>
        public void Reset()
        {
            AssetDepends?.Clear();
            assetNameMap?.Clear();
        }

        /// <summary>
        /// 添加资源到字典表
        /// </summary>
        public bool AddAssetInfo(AssetInfo assetInfo)
        {
            assetNameMap ??= new Dictionary<string, AssetInfo>();
            assetFullNameMap ??= new Dictionary<string, AssetInfo>();
            string assetKey = assetInfo.AssetName.ToLowerInvariant();

            // 添加到AssetInfo字典
            if (assetNameMap.ContainsKey(assetKey)) {
                using ResourceSearcher searcher = ResourceSearcher.Allocate(assetInfo.AssetName);
                AssetInfo oldInfo = GetAssetInfo(searcher);
                if (oldInfo != null) {
                    Log.Warning("资源已存在: {0}\n旧包: {1}, 新包: {2}", assetInfo.AssetName, AssetDepends[oldInfo.AssetBundleIndex].AssetBundleName, AssetDepends[oldInfo.AssetBundleIndex].AssetBundleName);
                }
            }
            else {
                assetNameMap.Add(assetKey, assetInfo);
            }

            // 添加到AssetUUID字典
            assetKey = assetInfo.FullName;
            if (assetFullNameMap.ContainsKey(assetKey)) {
                using ResourceSearcher searcher = ResourceSearcher.Allocate(assetInfo.AssetName, assetInfo.AssetBundleName);
                AssetInfo oldInfo = GetAssetInfo(searcher);
                if (oldInfo != null) {
                    Log.Warning("资源已存在: {0}\n旧包: {1}, 新包: {2}", assetInfo.AssetName, AssetDepends[oldInfo.AssetBundleIndex].AssetBundleName, AssetDepends[oldInfo.AssetBundleIndex].AssetBundleName);
                }
            }
            else {
                assetFullNameMap.Add(assetKey, assetInfo);
            }
            return true;
        }

        /// <summary>
        /// 从字典表获取资源信息
        /// </summary>
        public AssetInfo GetAssetInfo(ResourceSearcher searcher)
        {
            AssetInfo assetInfo = null;

            // 如果查询条件含有AssetBundleName，并且BundleMap 不为空
            if (searcher.AssetBundleName != null) {
                assetFullNameMap?.TryGetValue(searcher.FullName, out assetInfo);
            }
            // 如果查询条件没有AssetBundleName，并且BundleMap 不为空
            else {
                assetNameMap?.TryGetValue(searcher.AssetName, out assetInfo);
            }
            return assetInfo;
        }

        /// <summary>
        /// 添加资源关系信息
        /// </summary>
        public int AddAssetDependence(string assetName, string[] depends)
        {
            if (assetName.Nothing()) return -1;
            AssetDepends ??= new List<AssetDependence>();
            ResourceSearcher searcher = ResourceSearcher.Allocate(assetName);
            AssetInfo assetInfo = GetAssetInfo(searcher);
            if (assetInfo != null) {
                return assetInfo.AssetBundleIndex;
            }
            AssetDepends.Add(new AssetDependence(assetName, depends));
            int index = AssetDepends.Count - 1;
            AddAssetInfo(new AssetInfo(assetName, null, index, ResourceLoadType.ASSET_BUNDLE));
            return index;
        }

        /// <summary>
        /// 获取资源包信息
        /// </summary>
        public string GetAssetBundleName(string assetName, int index)
        {
            if (AssetDepends.Nothing()) return "";
            if (index > AssetDepends.Count) return "";
            if (assetNameMap.ContainsKey(assetName)) {
                return AssetDepends[index].AssetBundleName;
            }
            return "";
        }

        /// <summary>
        /// 获取依赖资源
        /// </summary>
        public string[] GetAssetBundleDepends(string assetBundleName)
        {
            string[] result = null;
            AssetDependence dependence = GetAssetDepend(assetBundleName);
            if (dependence != null) {
                result = dependence.Depends;
            }
            return result;
        }

        /// <summary>
        /// 获取依赖资源
        /// </summary>
        public AssetDependence GetAssetDepend(string assetName)
        {
            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName);
            AssetInfo assetInfo = GetAssetInfo(searcher);
            if (assetInfo == null) return null;
            return AssetDepends.Nothing() ? null : AssetDepends[assetInfo.AssetBundleIndex];
        }

        /// <summary>
        /// 设置序列化数据
        /// </summary>
        private void SetSerializeData(AssetBundleData data)
        {
            if (data == null) return;
            AssetDepends = new List<AssetDependence>(data.AssetDependencies);
            if (data.AssetInfos != null) {
                foreach (AssetInfo assetInfo in data.AssetInfos) {
                    AddAssetInfo(assetInfo);
                }
            }
        }

        /// <summary>
        /// 获取序列化数据
        /// </summary>
        public AssetBundleData GetSerializeData()
        {
            AssetBundleData data = new AssetBundleData();
            data.Key = Key;
            data.AssetDependencies = AssetDepends.ToArray();
            if (assetNameMap != null) {
                data.AssetInfos = assetNameMap.Values.ToArray();
            }
            return data;
        }
    }
}
