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
    public sealed class ResourceLoader : Disposeble, IPoolable, IRecyclable
    {
        // 当前加载资源到数量
        private int loadingCount;
        // 资源列表
        private readonly List<IResource> resourceList = new List<IResource>();
        // 缓存到Sprite资源字典
        private readonly Dictionary<string, Sprite> spriteMap = new Dictionary<string, Sprite>();
        // 等待加载的资源
        private readonly LinkedList<IResource> waitForLoadList = new LinkedList<IResource>();
        // 等待回收的对象
        private List<Object> unloadObjectList;
        // 当前资源的回调
        private Action currentCallback;
        // 回调事件列表
        private LinkedList<CallbackCleaner> callbackCleanerList;
        
        //依赖资源名称缓存，防止重复添加内存
        List<string> tempDepends = new List<string>();
        
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
        public Object Load(string assetName, string bundleName)
        {
            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName, bundleName);
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
        public T Load<T>(string assetName, string bundleName) where T : Object
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

            // 立即加载等待加载的资源
            while (waitForLoadList.Count > 0)
            {
                IResource first = waitForLoadList.First.Value;
                
                waitForLoadList.RemoveFirst();
                
                loadingCount--;
                
                first?.Load();
            }

            // 从加载到资源中找到本次要打开的资源
            IResource resource = ResourceManager.Instance.GetResource(searcher);
            
            if (resource == null)
            {
                Log.Error("资源加载失败：" + searcher);
            }
            else
            {
                //清理缓存的依赖资源名称
                tempDepends.Clear();
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
            this.currentCallback = callback;
            
            // 异步加载
            LoadAsyncMethod();
        }

        private void LoadAsyncMethod()
        {
            if(loadingCount == 0)
            {
                currentCallback.InvokeSafe();
                currentCallback = null;
                return;
            }
            
            // 当前循环节
            LinkedListNode<IResource> currentNode = waitForLoadList.First;

            // 遍历所有需要等待加载到资源
            while (currentNode != null)
            {
                IResource resource = currentNode.Value;
                LinkedListNode<IResource> nextNode = currentNode.Next;

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
        public void AddToLoad(string assetName, string bundleName, Action<bool, IResource> callback = null, bool last = true)
        {
            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName, bundleName);
            AddToLoad(searcher, callback, last);
        }
        
        /// <summary>
        /// 添加资源到任务列表
        /// </summary>
        public void AddToLoad<T>(string assetName, string bundleName, Action<bool, IResource> callback = null, bool last = true)
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
                    // 如果已经加载过资源，不再重复加载，避免内存泄露
                    if (!tempDepends.Contains(depend))
                    {
                        using ResourceSearcher searchRule = ResourceSearcher.Allocate(depend, null, typeof(AssetBundle));
                        
                        tempDepends.Add(depend);
                        
                        // 递归加载依赖资源
                        AddToLoad(searchRule);
                    }
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
            this.resourceList.Add(resource);

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
            return resourceList.IsNullOrEmpty() ? null : resourceList.FirstOrDefault(resource => searcher.Match(resource));
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
            if (Platform.IsSimulation)
            {
                // 如果未缓存，则缓存
                if (!spriteMap.ContainsKey(spriteName))
                {
                    // 加载Texture资源
                    Texture2D texture = Load<Texture2D>(spriteName);
                    // 创建Sprite
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
                    // 添加到缓存
                    spriteMap.Add(spriteName, sprite);
                }
                
                return spriteMap[spriteName];
            }

            // 非模拟器模式，直接加载AssetBundle
            return Load<Sprite>(spriteName);
        }
        
        /// <summary>
        /// 同步加载Sprite
        /// </summary>
        public Sprite LoadSprite(string spriteName, string bundleName)
        {
            // 如果是模拟器模式，直接加载Resources文件夹下到资源
            if (Platform.IsSimulation)
            {
                // 如果未缓存，则缓存
                if (!spriteMap.ContainsKey(spriteName))
                {
                    // 加载Texture资源
                    Texture2D texture = Load<Texture2D>(bundleName, spriteName);
                    // 创建Sprite
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
                    // 添加到缓存
                    spriteMap.Add(spriteName, sprite);
                }
                
                return spriteMap[spriteName];
            }

            // 非模拟器模式，直接加载AssetBundle
            return Load<Sprite>(bundleName, spriteName);
        }
        
        /*-----------------------------*/
        /* 资源加载完毕后回调              */
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
            LoadAsyncMethod();
            
            if (loadingCount == 0)
            {
                RemoveAllCallbacks(false);
                
                currentCallback?.Invoke();
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
            if (Platform.IsSimulation)
            {
                if (spriteMap.ContainsKey(assetName))
                {
                    Sprite sprite = spriteMap[assetName];
                    sprite.DestroySelf();
                    spriteMap.Remove(assetName);
                }
            }

            // 在缓存中查找资源，如果没有则返回
            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName);
            
            IResource resource = ResourceManager.Instance.GetResource((searcher));
                
            if (resource == null) return;

            // 清除待下载列表中的资源
            if (waitForLoadList.Remove(resource))
            {
                loadingCount--;
                if (loadingCount == 0)
                {
                    currentCallback = null;
                }
            }

            // 从缓存中删除，并释放资源
            if (resourceList.Remove(resource))
            {
                // 先释放事件，再释放资源本身
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
            currentCallback = null;
            loadingCount = 0;
            waitForLoadList.Clear();

            // 释放模拟器模式资源
            if (Platform.IsSimulation)
            {
                foreach (var sprite in spriteMap)
                {
                    sprite.Value.DestroySelf();
                }
                spriteMap.Clear();
            }

            if (resourceList.Count > 0)
            {
                //确保首先删除的是AB，这样能对Asset的卸载做优化
                resourceList.Reverse();

                foreach (IResource resource in resourceList)
                {
                    resource.UnRegisterOnLoadedEvent(OnResourceLoaded);
                    resource.Release();
                }
                
                resourceList.Clear();
                
                if (!ResourceManager.IsApplicationQuit)
                {
                    ResourceManager.Instance.ClearOnUpdate();
                }
            }
            
            // 释放所有回调事件
            RemoveAllCallbacks(true);
        }
        
        /// <summary>
        /// 释放某资源的回调事件
        /// </summary>
        private void RemoveCallback(IResource resource, bool release)
        {
            if (!callbackCleanerList.IsNullOrEmpty())
            {
                LinkedListNode<CallbackCleaner> currentNode = callbackCleanerList.First;

                // 遍历所有需要释放的事件
                while (currentNode != null)
                {
                    CallbackCleaner cleaner = currentNode.Value;
                    LinkedListNode<CallbackCleaner> nextNode = currentNode.Next;

                    if (cleaner.Is(resource))
                    {
                        if(release) cleaner.Release();
                    }

                    callbackCleanerList.Remove(currentNode);
                    
                    currentNode = nextNode;
                }
            }
        }
        
        /// <summary>
        /// 释放所有回调事件
        /// </summary>
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

        /// <summary>
        /// 实现Disposable接口
        /// </summary>
        protected override void DisposeManaged()
        {
            ReleaseAllResource();
        }
        
        /// <summary>
        /// 回收资源时销毁
        /// </summary>
        public void DestroyOnRecycle(Object obj)
        {
            if (unloadObjectList == null)
            {
                unloadObjectList = new List<Object>();
            }

            unloadObjectList.Add(obj);
        }

        /// <summary>
        /// 回收函数
        /// </summary>
        public void Recycle()
        {
            if (unloadObjectList.IsNotNullOrEmpty())
            {
                foreach (Object obj in unloadObjectList)
                {
                    obj.DestroySelf();
                }
                unloadObjectList.Clear();
                unloadObjectList = null;
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