using System;
using System.Collections;
using System.Collections.Generic;
using IFramework.Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IFramework.Engine
{
    /// <summary>
    /// 资源处理抽象类
    /// </summary>
    public abstract class AbstractResource : Countor, IResource, IPoolable
    {
        // 资源名称
        protected string assetName;

        // 资源实体
        protected Object asset;

        // 加载状态
        protected ResourceState state = ResourceState.Waiting;

        // 资源加载完毕事件
        private event Action<bool, IResource> OnResourceLoaded;

        /// <summary>
        /// 构造函数
        /// </summary>
        protected AbstractResource()
        {
            IsRecycled = false;
            OnZero = OnEmpty;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected AbstractResource(string assetName)
        {
            IsRecycled = false;
            this.assetName = assetName;
            OnZero = OnEmpty;
        }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string AssetName {
            get => assetName;
            protected set => assetName = value;
        }

        /// <summary>
        /// 所属AssetBundle包名称
        /// </summary>
        public virtual string AssetBundleName { get; set; }

        /// <summary>
        /// 资源类型
        /// </summary>
        public Type AssetType { get; set; }

        /// <summary>
        /// 资源对象
        /// </summary>
        public Object Asset => asset;

        /// <summary>
        /// 资源加载状态
        /// </summary>
        public ResourceState State {
            get => state;
            set {
                state = value;

                if (state == ResourceState.Ready) {
                    // 通知资源加载完成
                    NotifyResourceLoaded(true);
                }
            }
        }

        /// <summary>
        /// 是否卸载图片资源
        /// </summary>
        public virtual bool UnloadImage { get; set; } = false;

        /// <summary>
        /// 是否可以加载 state == ResourceState.Waiting
        /// </summary>
        protected bool IsLoadable => state == ResourceState.Waiting;

        /// <summary>
        /// 同步加载资源
        /// </summary>
        public abstract bool Load();

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public abstract void LoadASync();

        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual bool Release()
        {
            switch (state) {
                case ResourceState.Loading: return false;
                case ResourceState.Waiting: return true;
                case ResourceState.Ready: break;
                default: throw new ArgumentOutOfRangeException();
            }
            OnReleaseResource();
            state = ResourceState.Waiting;
            OnResourceLoaded = null;
            return true;
        }

        /// <summary>
        /// 释放Resource
        /// </summary>
        protected virtual void OnReleaseResource()
        {
            //如果Image 直接释放了，这里会直接变成NULL
            if (asset == null) return;

            // 如果不是场景中的GameObject，则释放资源,比如prefab
            // TODO sprite也被释放了...
            if (!(asset is GameObject)) {
                Resources.UnloadAsset(asset);
            }
            asset = null;
        }

        /// <summary>
        /// 获取依赖的资源
        /// </summary>
        public virtual List<string> GetDependResourceList()
        {
            return null;
        }

        /// <summary>
        /// 记录依赖资源
        /// </summary>
        protected void HoldDependResource()
        {
            DoLoopDependResource(resource => resource.IfNullOrEmpty(() => resource.Hold()), typeof(AssetBundle));
        }

        /// <summary>
        /// 释放依赖资源
        /// </summary>
        protected void UnHoldDependResource()
        {
            DoLoopDependResource(resource => resource.IfNullOrEmpty(() => resource.UnHold()));
        }

        /// <summary>
        /// 是否依赖资源加载完毕
        /// </summary>
        public bool IsDependResourceLoaded()
        {
            return DoLoopDependResource(resource => resource == null || resource.State != ResourceState.Ready);
        }

        private bool DoLoopDependResource(Func<IResource, bool> action, Type assetType = null)
        {
            // 获取依赖资源
            List<string> depends = GetDependResourceList();
            if (depends.IsNullOrEmpty()) return true;

            foreach (string depend in depends) {
                using ResourceSearcher searcher = ResourceSearcher.Allocate(depend, null, assetType);
                IResource resource = ResourceManager.Instance.GetResource(searcher);

                if (action.InvokeSafe(resource)) {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 注册资源加载完毕事件
        /// </summary>
        public void RegisterOnLoadedEvent(Action<bool, IResource> listener)
        {
            if (listener == null) return;

            if (state == ResourceState.Ready) {
                listener(true, this);
                return;
            }
            OnResourceLoaded += listener;
        }

        /// <summary>
        /// 注销资源加载完毕事件
        /// </summary>
        public void UnRegisterOnLoadedEvent(Action<bool, IResource> listener)
        {
            if (listener == null) return;
            if (OnResourceLoaded == null) return;

            OnResourceLoaded -= listener;
        }

        /// <summary>
        /// 资源加载失败调用方法
        /// </summary>
        protected void OnResourceLoadFailed()
        {
            state = ResourceState.Waiting;
            NotifyResourceLoaded(false);
        }

        /// <summary>
        /// 资源加载完毕通知
        /// </summary>
        private void NotifyResourceLoaded(bool result)
        {
            if (OnResourceLoaded == null) return;

            OnResourceLoaded(result, this);
            OnResourceLoaded = null;
        }

        /*----------------------------- 接口实现 -----------------------------*/

        public bool IsRecycled { get; set; }

        public virtual void OnRecycled()
        {
            assetName = null;
            OnResourceLoaded = null;
        }

        public abstract void Recycle();

        protected virtual void OnEmpty()
        {
            if (state == ResourceState.Loading) return;

            Release();
        }

        public abstract IEnumerator LoadAsync(Action callback);

        public override string ToString()
        {
            return $"AssetName:【{AssetName}】 AssetBundleName:【{AssetBundleName}】 State:【{State}】 Counter:【{Counter}】";
        }
    }
}
