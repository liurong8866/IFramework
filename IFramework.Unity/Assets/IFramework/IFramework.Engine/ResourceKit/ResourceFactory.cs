using System.Collections.Generic;
using System.Linq;
using IFramework.Core;

namespace IFramework.Engine
{
    /// <summary>
    /// 资源创建工厂
    /// </summary>
    public static class ResourceFactory
    {
        /// <summary>
        /// 资源列表
        /// </summary>
        private static readonly List<IResourceCreator> creators = new List<IResourceCreator> {
            new AssetBundleResourceCreator(),
            new AssetBundleSceneCreator(),
            new AssetResourceCreator(),
            new ResourceCreator(),
            new NetImageResourceCreator(),
            new NetVideoResourceCreator(),
            new NetAudioResourceCreator()
        };

        /// <summary>
        /// 生产方法
        /// </summary>
        public static IResource Create(ResourceSearcher searcher)
        {
            IResource resource = creators
                                 // 找到对应资源的创建者
                                .Where(creator => creator.Match(searcher))
                                 // 创建创建者（一般是从缓冲池分配获得）
                                .Select(creator => creator.Create(searcher))
                                 // 如果有多个，取第一个
                                .FirstOrDefault();

            if (resource == null) {
                Log.Error("未找到相关资源加载器，加载资源失败! {0}", searcher.ToString());
            }
            return resource;
        }

        /// <summary>
        /// 添加生产者
        /// </summary>
        public static void AddCreator(IResourceCreator creator)
        {
            creators.Add(creator);
        }

        /// <summary>
        /// 添加生产者
        /// </summary>
        public static void AddCreator<T>() where T : IResourceCreator, new()
        {
            creators.Add(new T());
        }

        /// <summary>
        /// 删除生产者
        /// </summary>
        public static void RemoveCreator<T>() where T : IResourceCreator, new()
        {
            creators.RemoveAll(creator => creator.GetType() == typeof(T));
        }
    }
}
