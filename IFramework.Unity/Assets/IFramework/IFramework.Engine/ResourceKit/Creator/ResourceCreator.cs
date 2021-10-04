namespace IFramework.Engine
{
    public sealed class ResourceCreator : IResourceCreator
    {
        /// <summary>
        /// 匹配方法
        /// </summary>
        public bool Match(ResourceSearcher searcher)
        {
            return searcher.AssetName.StartsWith(ResourcesUrlType.RESOURCES);
        }

        /// <summary>
        /// 创建资源
        /// </summary>
        public IResource Create(ResourceSearcher searcher)
        {
            IResource resource = Resource.Allocate(searcher.AssetName);
            resource.AssetType = searcher.AssetType;
            return resource;
        }
    }
}
