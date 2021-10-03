namespace IFramework.Engine
{
    public class NetImageResourceCreator : IResourceCreator
    {
        public bool Match(ResourceSearcher searcher) { return searcher.AssetName.StartsWith(ResourcesUrlType.IMAGE); }

        public IResource Create(ResourceSearcher searcher) { return NetImageResource.Allocate(searcher.AssetName); }
    }
}
