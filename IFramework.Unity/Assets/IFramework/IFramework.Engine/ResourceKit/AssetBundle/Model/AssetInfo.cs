using System;
using IFramework.Core;

namespace IFramework.Engine
{
    /// <summary>
    /// 参与打包的Asset资源信息
    /// </summary>
    [Serializable]
    public class AssetInfo
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        public string AssetName;

        /// <summary>
        /// 所属AssetBundle包名称
        /// </summary>
        public string AssetBundleName;

        /// <summary>
        /// 所属AssetBundle包索引
        /// </summary>
        public int AssetBundleIndex;

        /// <summary>
        /// 资源类型
        /// </summary>
        public short AssetType;

        /// <summary>
        /// 资源类型编码
        /// </summary>
        public short AssetTypeCode;

        /// <summary>
        /// 资源全称 AssetBundleName.AssetName
        /// </summary>
        public string FullName => AssetBundleName.IsNullOrEmpty() ? AssetName.ToLowerInvariant() : AssetBundleName.ToLowerInvariant() + "." + AssetName.ToLowerInvariant();

        public AssetInfo() { }

        public AssetInfo(string assetName, string assetBundleName, int assetBundleIndex, short assetType, short assetTypeCode = 0)
        {
            AssetName = assetName;
            AssetBundleName = assetBundleName;
            AssetBundleIndex = assetBundleIndex;
            AssetType = assetType;
            AssetTypeCode = assetTypeCode;
        }
    }
}
