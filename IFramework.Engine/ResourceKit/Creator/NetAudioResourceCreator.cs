namespace IFramework.Engine
{
    public class NetAudioResourceCreator : IResourceCreator
    {
        public bool Match(ResourceSearcher searcher)
        {
            return searcher.AssetName.StartsWith(ResourcesUrlType.AUDIO_MP3) || searcher.AssetName.StartsWith(ResourcesUrlType.AUDIO_WAV) || searcher.AssetName.StartsWith(ResourcesUrlType.AUDIO_OGG);
        }

        public IResource Create(ResourceSearcher searcher)
        {
            return NetAudioResource.Allocate(searcher.AssetName);
        }
    }
}
