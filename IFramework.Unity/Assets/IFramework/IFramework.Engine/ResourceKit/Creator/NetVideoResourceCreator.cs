namespace IFramework.Engine
{
    public class NetVideoResourceCreator : IResourceCreator
    {
        public bool Match(ResourceSearcher searcher) { return searcher.AssetName.StartsWith(ResourcesUrlType.VIDEO); }

        public IResource Create(ResourceSearcher searcher) { return NetVideoResource.Allocate(searcher.AssetName); }
    }
}
