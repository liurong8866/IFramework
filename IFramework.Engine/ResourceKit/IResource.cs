using System;
using System.Collections.Generic;
using IFramework.Core;
using Object = UnityEngine.Object;

namespace IFramework.Engine
{
    public enum ResourceState
    {
        Waiting = 0,
        Loading = 1,
        Ready = 2
    }

    public interface IResource : ICountor, IRecyclable, IResourceLoadTask
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        string AssetName { get; }

        /// <summary>
        /// 所属AssetBundle包名称
        /// </summary>
        string AssetBundleName { get; }

        /// <summary>
        /// 资源类型
        /// </summary>
        Type AssetType { get; set; }

        /// <summary>
        /// 资源对象
        /// </summary>
        Object Asset { get; }

        /// <summary>
        /// 资源加载状态
        /// </summary>
        ResourceState State { get; }

        /// <summary>
        /// 卸载图片资源
        /// </summary>
        bool UnloadImage { get; set; }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        bool Load();

        /// <summary>
        /// 异步加载资源
        /// </summary>
        void LoadASync();

        /// <summary>
        /// 释放资源
        /// </summary>
        bool Release();

        /// <summary>
        /// 获取依赖的资源
        /// </summary>
        List<string> GetDependResourceList();

        /// <summary>
        /// 是否依赖资源加载完毕
        /// </summary>
        bool IsDependResourceLoaded();

        /// <summary>
        /// 注册资源加载完毕事件
        /// </summary>
        void RegisterOnLoadedEvent(Action<bool, IResource> listener);

        /// <summary>
        /// 注销资源加载完毕事件
        /// </summary>
        void UnRegisterOnLoadedEvent(Action<bool, IResource> listener);
    }
}
