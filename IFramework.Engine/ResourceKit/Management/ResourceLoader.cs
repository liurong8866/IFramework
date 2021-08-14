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
using System.Collections.Generic;
using System.Linq;
using IFramework.Core;
using UnityEngine;
using Object=UnityEngine.Object;

namespace IFramework.Engine
{
    public class ResourceLoader : Disposeble, IPoolable, IRecyclable
    {
        
        // 资源列表
        private readonly List<IResource> resources = new List<IResource>();
        // 等待加载的资源
        private readonly LinkedList<IResource> waitForLoadList = new LinkedList<IResource>();
        // 当前加载资源到数量
        private int loadingCount = 0;
        
        // 等待回收的对象
        private List<Object> tobeUnloadedObjects;

        // private LinkedList<CallbackMethod> callbackRecordList;

        /// <summary>
        /// 回收函数
        /// </summary>
        public void Recycle()
        {
            if (tobeUnloadedObjects.IsNullOrEmpty())
            {
                foreach (Object obj in tobeUnloadedObjects)
                {
                    obj.DestroySelf();
                }
                tobeUnloadedObjects.Clear();
                tobeUnloadedObjects = null;
            }

            ObjectPool<ResourceLoader>.Instance.Recycle(this);
        }
        
        /// <summary>
        /// 回收资源时销毁
        /// </summary>
        public void DestroyOnRecycle(Object obj)
        {
            if (tobeUnloadedObjects == null)
            {
                tobeUnloadedObjects = new List<Object>();
            }

            tobeUnloadedObjects.Add(obj);
        }
        
        
        /*-----------------------------*/
        /* 加载资源                     */
        /*-----------------------------*/

        /// <summary>
        /// 同步加载资源
        /// </summary>
        public Object Load(string assetName)
        {
            using (ResourceSearcher searcher = ResourceSearcher.Allocate(assetName))
            {
                IResource resource = Load(searcher);
                return resource.Asset;
            }
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        public T Load<T>(string assetName) where T : Object
        {
            using (ResourceSearcher searcher = ResourceSearcher.Allocate(assetName, null, typeof(T)))
            {
                IResource resource = Load(searcher);
                return resource.Asset as T;
            }
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        public T Load<T>(string bundleName, string assetName) where T : Object
        {
            using (ResourceSearcher searcher = ResourceSearcher.Allocate(assetName, bundleName, typeof(T)))
            {
                IResource resource = Load(searcher);
                return resource.Asset as T;
            }
        }
        
        /// <summary>
        /// 同步加载资源
        /// </summary>
        public IResource Load(ResourceSearcher searcher)
        {
            // 添加到加载任务列表
            AddToLoad(searcher);

            // 完全执行加载列表
            while (waitForLoadList.Count > 0)
            {
                IResource first = waitForLoadList.First.Value;
                
                waitForLoadList.RemoveFirst();
                
                loadingCount--;
                
                first?.LoadSync();
            }

            // 从加载到资源中找到本次要打开到资源
            IResource resource = ResourceManager.Instance.GetResource(searcher);
            
            if (resource == null)
            {
                Log.Error("资源加载失败：" + searcher);
            }

            return resource;
        }
        
        /*-----------------------------*/
        /* 加载资源到任务列表          */
        /*-----------------------------*/
        
        /// <summary>
        /// 加载资源到任务列表
        /// </summary>
        public void AddToLoad(List<string> list)
        {
            if(list == null) return;

            foreach (string assetName in list)
            {
                using (ResourceSearcher searcher = ResourceSearcher.Allocate(assetName))
                {
                    AddToLoad(searcher);
                }
            }
        } 
        
        /// <summary>
        /// 加载资源到任务列表
        /// </summary>
        public void AddToLoad(string assetName, Action<bool, IResource> callback = null, bool last = true)
        {
            using (ResourceSearcher searcher = ResourceSearcher.Allocate(assetName))
            {
                AddToLoad(searcher, callback, last);
            }
        }
        
        /// <summary>
        /// 加载资源到任务列表
        /// </summary>
        public void AddToLoad<T>(string assetName, Action<bool, IResource> callback = null, bool last = true)
        {
            using (ResourceSearcher searcher = ResourceSearcher.Allocate(assetName, null, typeof(T)))
            {
                AddToLoad(searcher, callback, last);
            }
        }

        /// <summary>
        /// 加载资源到任务列表
        /// </summary>
        public void AddToLoad(string bundleName, string assetName, Action<bool, IResource> callback = null, bool last = true)
        {
            using (ResourceSearcher searcher = ResourceSearcher.Allocate(assetName, bundleName))
            {
                AddToLoad(searcher, callback, last);
            }
        }
        
        /// <summary>
        /// 加载资源任务列表
        /// </summary>
        public void AddToLoad<T>(string bundleName, string assetName, Action<bool, IResource> callback = null, bool last = true)
        {
            using (ResourceSearcher searcher = ResourceSearcher.Allocate(assetName, bundleName, typeof(T)))
            {
                AddToLoad(searcher, callback, last);
            }
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="searcher">查询器</param>
        /// <param name="callback">完成后</param>
        /// <param name="last"></param>
        private void AddToLoad(ResourceSearcher searcher, Action<bool, IResource> callback = null, bool last = true)
        {
            // 在缓存的资源中查找
            IResource resource = GetResourceInCache(searcher);

            if (resource == null)
            {
                resource = ResourceManager.Instance.GetResource(searcher, true);
            }

            if (callback != null)
            {
                // TODO AddResListenerRecord(res, listener); 
                // if (mCallbackRecordList == null)
                // {
                //     mCallbackRecordList = new LinkedList<CallBackWrap>();
                // }
                //
                // mCallbackRecordList.AddLast(new CallBackWrap(res, listener));
                resource.RegisterOnLoadedEvent(callback);
            }

            // 获取依赖
            List<string> depends = resource.GetDependResourceList();

            if (depends != null)
            {
                foreach (string depend in depends)
                {
                    using (ResourceSearcher searchRule = ResourceSearcher.Allocate(depend, null, typeof(AssetBundle)))
                    {
                        // 递归加载依赖资源
                        AddToLoad(searchRule);
                    }
                }
            }
            
            AddToLoadList(resource, last);
        }
        
        /// <summary>
        /// 添加资源到列表
        /// </summary>
        private void AddToLoadList(IResource resource, bool last)
        {
            using (ResourceSearcher searcher = ResourceSearcher.Allocate(resource.AssetName, resource.AssetBundleName, resource.AssetType))
            {
                // 在缓存的资源中查找，再次确保
                IResource cachedResource = GetResourceInCache(searcher);
                
                if(cachedResource != null) return;

                // 记录资源加载次数
                resource.Retain();
                
                // 资源添加到缓存
                this.resources.Add(resource);

                if (resource.State == ResourceState.Ready)
                {
                    loadingCount++;
                    
                    if (last)
                    {
                        waitForLoadList.AddLast(resource);
                    }
                    else
                    {
                        waitForLoadList.AddFirst(resource);
                    }
                }
            }
        }
        
        /// <summary>
        /// 在缓存的资源中查找
        /// </summary>
        private IResource GetResourceInCache(ResourceSearcher searcher)
        {
            if (resources.IsNullOrEmpty())
            {
                return null;
            }

            return resources.FirstOrDefault(resource => searcher.Match(resource));
        }

        protected override void DisposeManaged()
        {
            throw new System.NotImplementedException();
        }

        public void OnRecycled()
        {
            throw new System.NotImplementedException();
        }

        public bool IsRecycled { get; set; }
        
    }
}