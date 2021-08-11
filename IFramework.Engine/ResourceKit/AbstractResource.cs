/*****************************************************************************
 * MIT License
 * 
 * Copyright (c) 2021 liurong
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *****************************************************************************/

using System;
using System.Collections;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// 资源处理抽象类
    /// </summary>
    public abstract class AbstractResource : Counter, IResource, IPoolable
    {
        // 资源名称
        protected string assetName;
        // 资源实体
        protected UnityEngine.Object asset;
        // 加载状态
        protected ResourceState state = ResourceState.Waiting;
        // 资源加载完毕事件
        private event Action<bool, IResource> onResourceLoadDone;
        
        
        /// <summary>
        /// 资源名称
        /// </summary>
        public string AssetName
        {
            get { return assetName; }
            protected set { assetName = value; }
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
        public UnityEngine.Object Asset
        {
            get { return asset; }
        }
        
        /// <summary>
        /// 加载进度
        /// </summary>
        public float Progress { 
            get
            {
                switch (state)
                {
                    case ResourceState.Loading: return CalculateProgress();
                    case ResourceState.Ready: return 1;
                }

                return 0;
            } 
        }
        
        protected virtual float CalculateProgress()
        {
            return 0;
        }
        
        /// <summary>
        /// 资源加载状态
        /// </summary>
        public ResourceState State
        {
            get { return state; }
            set
            {
                state = value;
                if (state == ResourceState.Ready)
                {
                    // 通知资源加载完成
                    NotifyResourceLoaded(true);
                }
            }
        }
        
        /// <summary>
        /// 是否可以加载 state == ResourceState.Waiting
        /// </summary>
        protected bool IsLoadable
        {
            get{ return state == ResourceState.Waiting; }
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public AbstractResource()
        {
            IsRecycled = false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        protected AbstractResource(string assetName)
        {
            IsRecycled = false;
            this.assetName = assetName;
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        public abstract bool LoadSync();
        
        /// <summary>
        /// 异步加载资源
        /// </summary>
        public abstract void LoadASync();

        /// <summary>
        /// 卸载图片资源
        /// </summary>
        public virtual bool UnloadImage(bool flag)
        {
            return false;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public bool Release()
        {
            switch (state)
            {
                case ResourceState.Loading: return false; 
                case ResourceState.Waiting: return true; 
            }

            OnReleaseResource();

            state = ResourceState.Waiting;
            onResourceLoadDone = null;
            return true;
        }

        /// <summary>
        /// 释放Resource
        /// </summary>
        protected virtual void OnReleaseResource()
        {
            //如果Image 直接释放了，这里会直接变成NULL
            if (asset != null)
            {
                // 如果不是场景中的GameObject，则释放资源,比如prefab
                if (!(asset is GameObject))
                {
                    Resources.UnloadAsset(asset);
                }
                asset = null;
            }
        }

        /// <summary>
        /// 获取依赖的资源
        /// </summary>
        public virtual string[] GetDependResourceList()
        {
            return null;
        }

        /// <summary>
        /// 记录依赖资源
        /// </summary>
        protected void HoldDependResource()
        {
            string[] depends = GetDependResourceList();
            
            if (depends.IsNullOrEmpty()) return;
            
            for (int i = depends.Length - 1; i >= 0; i--)
            {
                ResourceSearchRule searchRule = ResourceSearchRule.Allocate(depends[i]);

                IResource resource = ResourceManager.Instance.GetResource(searchRule, false);
                
                searchRule.Recycle();

                resource?.Retain();
            }
        }
        
        /// <summary>
        /// 释放依赖资源
        /// </summary>
        protected void UnHoldDependResource()
        {
            string[] depends = GetDependResourceList();
            
            if (depends.IsNullOrEmpty()) return;
            
            for (int i = depends.Length - 1; i >= 0; i--)
            {
                ResourceSearchRule searchRule = ResourceSearchRule.Allocate(depends[i]);

                IResource resource = ResourceManager.Instance.GetResource(searchRule, false);
                
                searchRule.Recycle();

                resource?.Release();
            }
        }
        
        /// <summary>
        /// 是否依赖资源加载完毕
        /// </summary>
        public bool IsDependResourceLoadFinish()
        {
            string[] depends = GetDependResourceList();

            if (depends.IsNullOrEmpty()) return true;

            for (int i = depends.Length - 1; i >= 0; i--)
            {
                ResourceSearchRule searchRule = ResourceSearchRule.Allocate(depends[i]);

                IResource resource = ResourceManager.Instance.GetResource(searchRule, false);
                
                searchRule.Recycle();

                if (resource == null || resource.State != ResourceState.Ready) return false;
            }

            return true;
        }

        /// <summary>
        /// 注册资源加载完毕事件
        /// </summary>
        public void RegisterOnLoadedEvent(Action<bool, IResource> listener)
        {
            if (listener == null) return;

            if (state == ResourceState.Ready)
            {
                listener(true, this);
                return;
            }
            
            onResourceLoadDone += listener;
        }

        /// <summary>
        /// 注销资源加载完毕事件
        /// </summary>
        public void UnRegisterOnLoadedEvent(Action<bool, IResource> listener)
        {
            if (listener == null) return;

            if(onResourceLoadDone == null) return;
            
            onResourceLoadDone -= listener;
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
            if (onResourceLoadDone != null)
            {
                onResourceLoadDone(result, this);
                onResourceLoadDone = null;
            }
        }
        
        /*-----------------------------*/
        /* IPoolable 接口实现            */
        /*-----------------------------*/
        
        public bool IsRecycled { get; set; }
        
        public virtual void OnRecycled()
        {
            assetName = null;
            onResourceLoadDone = null;
        }
        
        /*-----------------------------*/
        /* IRecyclable 接口实现          */
        /*-----------------------------*/
        public abstract void Recycle();
        
        /*-----------------------------*/
        /* Counter 接口实现              */
        /*-----------------------------*/

        protected override void OnEmpty()
        {
            if(state == ResourceState.Loading) return;

            Release();
        }
        
        /*-----------------------------*/
        /* IEnumeratorTask 接口实现      */
        /*-----------------------------*/

        public abstract IEnumerator LoadAsync(Action callback);
        
        public override string ToString()
        {
            return string.Format("Name:{0}\t State:{1}\t Count:{2}", AssetName, State, Count);
        }
    }
}