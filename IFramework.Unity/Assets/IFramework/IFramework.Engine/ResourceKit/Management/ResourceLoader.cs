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
using IFramework.Engine.Management;
using UnityEngine;
using Object=UnityEngine.Object;

namespace IFramework.Engine
{
    public sealed class ResourceLoader : Disposeble, IPoolable, IRecyclable
    {
        // 当前加载资源到数量
        private int loadingCount = 0;
        // 资源列表
        private readonly List<IResource> resources = new List<IResource>();
        // 缓存到Sprite资源字典
        private readonly Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
        // 等待加载的资源
        private readonly LinkedList<IResource> waitForLoadList = new LinkedList<IResource>();
        // 等待回收的对象
        private List<Object> tobeUnloadedObjects;
        // 当前资源的回调
        private Action callback;
        // 回调事件列表
        private LinkedList<CallbackCleaner> callbackCleanerList;
        
        /// <summary>
        /// 分配资源函数
        /// </summary>
        public static ResourceLoader Allocate()
        {
            return ObjectPool<ResourceLoader>.Instance.Allocate();
        }

        /*-----------------------------*/
        /* 同步加载资源                     */
        /*-----------------------------*/

        /// <summary>
        /// 同步加载资源
        /// </summary>
        public Object Load(string assetName)
        {
            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName);
            IResource resource = Load(searcher);
            return resource.Asset;
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        public T Load<T>(string assetName) where T : Object
        {
            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName, null, typeof(T));
            IResource resource = Load(searcher);
            return resource.Asset as T;
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        public T Load<T>(string bundleName, string assetName) where T : Object
        {
            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName, bundleName, typeof(T));
            IResource resource = Load(searcher);
            return resource.Asset as T;
        }
        
        /// <summary>
        /// 同步加载资源
        /// </summary>
        public IResource Load(ResourceSearcher searcher)
        {
            // 添加到加载任务列表
            AddToLoad(searcher);

            // 完全加载等待加载的资源
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
        /* 异步加载资源                     */
        /*-----------------------------*/
        
        /// <summary>
        /// 异步加载资源
        /// </summary>
        public void LoadAsync(Action callback = null)
        {
            this.callback = callback;
            
            // 异步加载
            LoadAsync();
        }

        private void LoadAsync()
        {
            // 如果当前没有加载数量，则调用回调函数
            if (loadingCount == 0)
            {
                if (callback != null)
                {
                    Action action = callback;
                    callback = null;
                    action();
                }

                return;
            }
            
            // 当前循环节
            LinkedListNode<IResource> currentNode = waitForLoadList.First;
            LinkedListNode<IResource> nextNode = null;
            IResource resource;
            
            // 遍历所有需要等待加载到资源
            while (currentNode != null)
            {
                nextNode = currentNode.Next;
                resource = currentNode.Value;
                
                // 如果依赖资源加载完毕，则可以删除
                if (resource.IsDependResourceLoaded())
                {
                    waitForLoadList.Remove(currentNode);
                    
                    if (resource.State != ResourceState.Ready)
                    {
                        resource.RegisterOnLoadedEvent(OnResourceLoaded);
                        resource.LoadASync();
                    }
                    else
                    {
                        loadingCount--;
                    }
                }
                
                currentNode = nextNode;
            }
        }
        
        /*-----------------------------*/
        /* 添加资源到任务列表          */
        /*-----------------------------*/
        
        /// <summary>
        /// 添加资源到任务列表
        /// </summary>
        public void AddToLoad(List<string> list)
        {
            if(list == null) return;

            foreach (string assetName in list)
            {
                using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName);
                AddToLoad(searcher);
            }
        } 
        
        /// <summary>
        /// 添加资源到任务列表
        /// </summary>
        public void AddToLoad(string assetName, Action<bool, IResource> callback = null, bool last = true)
        {
            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName);
            AddToLoad(searcher, callback, last);
        }
        
        /// <summary>
        /// 添加资源到任务列表
        /// </summary>
        public void AddToLoad<T>(string assetName, Action<bool, IResource> callback = null, bool last = true)
        {
            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName, null, typeof(T));
            AddToLoad(searcher, callback, last);
        }

        /// <summary>
        /// 添加资源到任务列表
        /// </summary>
        public void AddToLoad(string bundleName, string assetName, Action<bool, IResource> callback = null, bool last = true)
        {
            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName, bundleName);
            AddToLoad(searcher, callback, last);
        }
        
        /// <summary>
        /// 添加资源到任务列表
        /// </summary>
        public void AddToLoad<T>(string bundleName, string assetName, Action<bool, IResource> callback = null, bool last = true)
        {
            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName, bundleName, typeof(T));
            AddToLoad(searcher, callback, last);
        }
        
        /// <summary>
        /// 添加资源到任务列表
        /// </summary>
        /// <param name="searcher">查询器</param>
        /// <param name="callback">完成后</param>
        /// <param name="last"></param>
        private void AddToLoad(ResourceSearcher searcher, Action<bool, IResource> callback = null, bool last = true)
        {
            // 在缓存的资源中查找
            IResource resource = GetResourceInCache(searcher);

            // 如果没有缓存资源，则加载
            if (resource == null)
            {
                resource = ResourceManager.Instance.GetResource(searcher, true);
            }
            
            // 如果有回调，则注册回到方法
            if (callback != null)
            {
                if (callbackCleanerList == null)
                {
                    callbackCleanerList = new LinkedList<CallbackCleaner>();
                }
                // 加入清空清单
                callbackCleanerList.AddLast(new CallbackCleaner(resource, callback));
                // 注册加载完毕事件
                resource.RegisterOnLoadedEvent(callback);
            }

            // 获取依赖
            List<string> depends = resource.GetDependResourceList();

            if (depends != null)
            {
                // 遍历所有依赖并加载
                foreach (string depend in depends)
                {
                    using ResourceSearcher searchRule = ResourceSearcher.Allocate(depend, null, typeof(AssetBundle));
                    // 递归加载依赖资源
                    AddToLoad(searchRule);
                }
            }
            
            // 将资源添加到加载任务列表
            AddToLoadList(resource, last);
        }
        
        /// <summary>
        /// 添加资源到列表
        /// </summary>
        private void AddToLoadList(IResource resource, bool last)
        {
            using ResourceSearcher searcher = ResourceSearcher.Allocate(resource.AssetName, resource.AssetBundleName, resource.AssetType);
            
            // 在缓存的资源中查找，再次确保
            IResource cachedResource = GetResourceInCache(searcher);
                
            if(cachedResource != null) return;

            // 记录资源加载次数
            resource.Retain();
                
            // 资源添加到缓存
            this.resources.Add(resource);

            if (resource.State != ResourceState.Ready)
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
        
        /// <summary>
        /// 在缓存的资源中查找
        /// </summary>
        private IResource GetResourceInCache(ResourceSearcher searcher)
        {
            return resources.IsNullOrEmpty() ? null : resources.FirstOrDefault(resource => searcher.Match(resource));
        }
        
        /*-----------------------------*/
        /* 加载Sprite资源               */
        /*-----------------------------*/

        /// <summary>
        /// 同步加载Sprite
        /// </summary>
        public Sprite LoadSprite(string spriteName)
        {
            // 如果是模拟器模式，直接加载Resources文件夹下到资源
            if (Configure.IsSimulation)
            {
                // 如果未缓存，则缓存
                if (!sprites.ContainsKey(spriteName))
                {
                    // 加载Texture资源
                    Texture2D texture = Load<Texture2D>(spriteName);
                    // 创建Sprite
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
                    // 添加到缓存
                    sprites.Add(spriteName, sprite);
                }
                
                return sprites[spriteName];
            }

            // 非模拟器模式，直接加载AssetBundle
            return Load<Sprite>(spriteName);
        }
        
        /// <summary>
        /// 同步加载Sprite
        /// </summary>
        public Sprite LoadSprite(string bundleName, string spriteName)
        {
            // 如果是模拟器模式，直接加载Resources文件夹下到资源
            if (Configure.IsSimulation)
            {
                // 如果未缓存，则缓存
                if (!sprites.ContainsKey(spriteName))
                {
                    // 加载Texture资源
                    Texture2D texture = Load<Texture2D>(bundleName, spriteName);
                    // 创建Sprite
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
                    // 添加到缓存
                    sprites.Add(spriteName, sprite);
                }
                
                return sprites[spriteName];
            }

            // 非模拟器模式，直接加载AssetBundle
            return Load<Sprite>(bundleName, spriteName);
        }
        
        /*-----------------------------*/
        /* 资源加载完毕后回               */
        /*-----------------------------*/
        
        /// <summary>
        /// 资源加载完成回调
        /// </summary>
        /// <param name="result"></param>
        /// <param name="resource"></param>
        private void OnResourceLoaded(bool result, IResource resource)
        {
            loadingCount--;
            
            // 在这里使用了递归调用
            LoadAsync();
            
            if (loadingCount == 0)
            {
                RemoveAllCallbacks(false);
                
                callback?.Invoke();
            }
        }

        private void RemoveAllCallbacks(bool release)
        {
            if (callbackCleanerList != null)
            {
                int count = callbackCleanerList.Count;
                for (int i = 0; i < count; i++)
                {
                    if (release)
                    {
                        callbackCleanerList.Last.Value.Release();
                    }
                    callbackCleanerList.RemoveLast();
                }
            }
        }

        private void RemoveCallback(IResource resource, bool release)
        {
            if (!callbackCleanerList.IsNullOrEmpty())
            {
                LinkedListNode<CallbackCleaner> currentNode = callbackCleanerList.First;

                while (currentNode != null)
                {
                    CallbackCleaner cleaner = currentNode.Value;

                    if (cleaner.Is(resource))
                    {
                        if(release) cleaner.Release();
                    }

                    callbackCleanerList.Remove(currentNode);
                    
                    currentNode = currentNode.Next;
                }
            }
        }
        
        /*-----------------------------*/
        /* 释放资源                     */
        /*-----------------------------*/

        /// <summary>
        /// 释放资源
        /// </summary>
        public void ReleaseResource(string assetName)
        {
            if(assetName.IsNullOrEmpty()) return;

            // 清空模拟器模式下加载的资源
            if (Configure.IsSimulation)
            {
                if (sprites.ContainsKey(assetName))
                {
                    Sprite sprite = sprites[assetName];
                    sprite.DestroySelf();
                    sprites.Remove(assetName);
                }
            }

            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName);
            IResource resource = ResourceManager.Instance.GetResource((searcher));
                
            if (resource == null) return;

            // 清除下载列表中的资源
            if (waitForLoadList.Remove(resource))
            {
                if (--loadingCount == 0)
                {
                    callback = null;
                }
            }

            if (resources.Remove(resource))
            {
                resource.UnRegisterOnLoadedEvent(OnResourceLoaded);
                resource.Release();
                ResourceManager.Instance.ClearOnUpdate();
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void ReleaseResource(string[] assetNames)
        {
            if(assetNames.IsNullOrEmpty()) return;

            foreach (string assetName in assetNames)
            {
                ReleaseResource(assetName);
            }
        }
        
        /// <summary>
        /// 释放所有资源
        /// </summary>
        public void ReleaseAllResource()
        {
            // 释放模拟器模式资源
            if (Configure.IsSimulation)
            {
                foreach (var sprite in sprites)
                {
                    sprite.Value.DestroySelf();
                }
                sprites.Clear();
            }

            callback = null;
            loadingCount = 0;
            waitForLoadList.Clear();

            if (resources.Count > 0)
            {
                //确保首先删除的是AB，这样能对Asset的卸载做优化
                resources.Reverse();

                foreach (IResource resource in resources)
                {
                    resource.UnRegisterOnLoadedEvent(OnResourceLoaded);
                    resource.Release();
                }
                resources.Clear();
                if (ResourceManager.IsApplicationQuit)
                {
                    ResourceManager.Instance.ClearOnUpdate();
                }
            }
            RemoveAllCallbacks(true);
        }
        
        protected override void DisposeManaged()
        {
            ReleaseAllResource();
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

        /// <summary>
        /// 回收函数
        /// </summary>
        public void Recycle()
        {
            if (tobeUnloadedObjects.IsNotNullOrEmpty())
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
        /// 实现接口
        /// </summary>
        public void OnRecycled()
        {
            ReleaseAllResource();
        }
        
        public bool IsRecycled { get; set; }
        
    }
    

}