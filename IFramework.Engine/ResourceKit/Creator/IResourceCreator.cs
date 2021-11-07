namespace IFramework.Engine
{
    /// <summary>
    /// 资源创建者
    /// </summary>
    public interface IResourceCreator
    {
        /// <summary>
        /// 匹配方法
        /// </summary>
        bool Match(ResourceSearcher searcher);

        /// <summary>
        /// 创建资源
        /// </summary>
        IResource Create(ResourceSearcher searcher);
    }
}
