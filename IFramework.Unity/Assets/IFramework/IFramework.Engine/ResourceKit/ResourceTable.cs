using System.Collections.Generic;
using System.Linq;
using IFramework.Core;

namespace IFramework.Engine
{
    public sealed class ResourceTable : Table<IResource>
    {
        public IResource GetResource(ResourceSearcher searcher)
        {
            string assetName = searcher.AssetName;
            List<IResource> resources = Get(assetName.ToLowerInvariant());

            // 过滤资源类型
            if (searcher.AssetType != null) { resources = resources.Where(res => res.AssetType == searcher.AssetType).ToList(); }

            // 过滤AssetBundle
            if (searcher.AssetBundleName != null) { resources = resources.Where(res => res.AssetBundleName == searcher.AssetBundleName).ToList(); }
            return resources.FirstOrDefault();
        }
    }
}
