using System;
using IFramework.Core;

namespace IFramework.Engine
{
    /// <summary>
    /// 描述资源依赖关系类
    /// </summary>
    [Serializable]
    public class AssetDependence
    {
        public string AssetBundleName;

        public string[] Depends;

        public AssetDependence(string assetBundleName, string[] depends)
        {
            AssetBundleName = assetBundleName;

            if (depends.NotEmpty()) {
                Depends = depends;
            }
        }

        public override string ToString()
        {
            string result = "AssetName: " + AssetBundleName;

            if (Depends != null) {
                foreach (string depend in Depends) {
                    result += "#: " + depend;
                }
            }
            return result;
        }
    }
}
