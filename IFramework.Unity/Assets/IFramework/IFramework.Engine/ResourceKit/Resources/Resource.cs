using System;
using System.Collections;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// Resources文件夹下的资源管理类
    /// </summary>
    public sealed class Resource : AbstractResource
    {
        private string path;

        /// <summary>
        /// 从缓冲池获取对象
        /// </summary>
        public static Resource Allocate(string name)
        {
            Resource resource = ObjectPool<Resource>.Instance.Allocate();

            if (resource != null) {
                resource.AssetName = name;
                resource.path = name.Substring(ResourcesUrlType.RESOURCES.Length);
            }
            return resource;
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        public override bool Load()
        {
            if (!IsLoadable || AssetName.IsNullOrEmpty()) return false;

            State = ResourceState.Loading;
            asset = AssetType != null ? Resources.Load(path, AssetType) : Resources.Load(path);

            if (asset == null) {
                Log.Error("资源加载失败：" + path);
                OnResourceLoadFailed();
                return false;
            }
            State = ResourceState.Ready;
            return true;
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public override void LoadASync()
        {
            if (!IsLoadable || AssetName.IsNullOrEmpty()) return;

            State = ResourceState.Loading;
            ResourceManager.Instance.AddResourceLoadTask(this);
        }

        /// <summary>
        /// 重写异步加载方法
        /// </summary>
        public override IEnumerator LoadAsync(Action callback)
        {
            ResourceRequest request = AssetType != null ? Resources.LoadAsync(path, AssetType) : Resources.LoadAsync(path);
            yield return request;

            if (!request.isDone) {
                Log.Error("资源加载失败：" + assetName);
                OnResourceLoadFailed();
                callback();
                yield break;
            }
            asset = request.asset;
            State = ResourceState.Ready;
            callback();
        }

        /// <summary>
        /// 回收资源到缓冲池
        /// </summary>
        public override void Recycle()
        {
            ObjectPool<Resource>.Instance.Recycle(this);
        }
    }
}
