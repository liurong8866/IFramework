using System;
using System.Collections.Generic;
using System.Linq;
using IFramework.Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IFramework.Engine
{
    /// <summary>
    /// 资源调用的超级类，配置了多个适配器，根据资源请求类型动态选择资源器加载
    /// </summary>
    public sealed class ResourceLoader : Disposable, IPoolable, IRecyclable
    {
        // 资源列表
        private readonly List<IResource> resourceList = new List<IResource>();

        // 缓存到Sprite资源字典
        private readonly Dictionary<string, Sprite> spriteMap = new Dictionary<string, Sprite>();

        //依赖资源名称缓存，防止重复添加内存
        private readonly List<string> tempDepends = new List<string>();

        // 等待加载的资源
        private readonly LinkedList<IResource> waitForLoadList = new LinkedList<IResource>();

        // 回调事件列表
        private LinkedList<CallbackCleaner> callbackCleanerList;

        // 当前资源的回调
        private Action currentCallback;

        // 当前加载资源到数量
        private int loadingCount;

        // 等待回收的对象
        private List<Object> unloadObjectList;

        /// <summary>
        /// 实现接口
        /// </summary>
        public void OnRecycled()
        {
            ReleaseAllResource();
        }

        public bool IsRecycled { get; set; }

        /// <summary>
        /// 回收函数
        /// </summary>
        public void Recycle()
        {
            if (unloadObjectList.NotEmpty()) {
                foreach (Object obj in unloadObjectList) { obj.DestroySelf(); }
                unloadObjectList.Clear();
                unloadObjectList = null;
            }
            ObjectPool<ResourceLoader>.Instance.Recycle(this);
        }

        /// <summary>
        /// 分配资源函数
        /// </summary>
        public static ResourceLoader Allocate()
        {
            return ObjectPool<ResourceLoader>.Instance.Allocate();
        }

        /*----------------------------- 同步加载资源 -----------------------------*/

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
            while (waitForLoadList.Count > 0) {
                IResource first = waitForLoadList.First.Value;
                waitForLoadList.RemoveFirst();
                loadingCount--;
                first?.Load();
            }

            // 从加载的资源中找到本次要打开的资源
            IResource resource = ResourceManager.Instance.GetResource(searcher);
            resource.IfNothing(() => Log.Error("资源加载失败：" + searcher), () => tempDepends.Clear());
            return resource;
        }

        /// <summary>
        /// 同步加载Sprite
        /// </summary>
        public Sprite LoadSprite(string spriteName, string bundleName = null)
        {
            // 如果是模拟器模式，直接加载Resources文件夹下到资源
            if (Platform.IsSimulation) {
                // 如果未缓存，则缓存
                if (!spriteMap.ContainsKey(spriteName)) {
                    // 加载Texture资源
                    Texture2D texture = bundleName == null ? Load<Texture2D>(spriteName) : Load<Texture2D>(bundleName, spriteName);
                    // 创建Sprite
                    Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
                    // 添加到缓存
                    spriteMap.Add(spriteName, sprite);
                }
                return spriteMap[spriteName];
            }

            // 非模拟器模式，直接加载AssetBundle
            return Load<Sprite>(spriteName, bundleName);
        }

        /*----------------------------- 异步加载资源 -----------------------------*/

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public void LoadAsync(Action callback = null)
        {
            currentCallback = callback;
            // 异步加载
            LoadAsyncMethod();
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        private void LoadAsyncMethod()
        {
            // 如果加载完毕，调用回调方法
            if (loadingCount == 0) {
                currentCallback.InvokeSafe();
                currentCallback = null;
                return;
            }

            // 当前循环节
            LinkedListNode<IResource> currentNode = waitForLoadList.First;

            // 遍历所有需要等待加载到资源
            while (currentNode != null) {
                IResource resource = currentNode.Value;
                LinkedListNode<IResource> nextNode = currentNode.Next;

                // 如果依赖资源加载完毕，则可以删除
                if (resource.IsDependResourceLoaded()) {
                    waitForLoadList.Remove(currentNode);

                    if (resource.State != ResourceState.Ready) {
                        // 注册回调方法
                        resource.RegisterOnLoadedEvent(OnResourceLoaded);
                        // 异步调用
                        resource.LoadASync();
                    }
                    else { loadingCount--; }
                }
                currentNode = nextNode;
            }
        }

        /*----------------------------- 添加资源到任务列表 -----------------------------*/

        /// <summary>
        /// 添加资源到任务列表
        /// </summary>
        public void AddToLoad(List<string> list)
        {
            if (list == null) return;

            foreach (string assetName in list) {
                using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName);
                AddToLoad(searcher);
            }
        }

        /// <summary>
        /// 添加资源到任务列表
        /// </summary>
        public ResourceLoader AddToLoad(string assetName, Action<bool, IResource> callback = null, bool last = true)
        {
            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName);
            AddToLoad(searcher, callback, last);
            return this;
        }

        /// <summary>
        /// 添加资源到任务列表
        /// </summary>
        public ResourceLoader AddToLoad<T>(string assetName, Action<bool, IResource> callback = null, bool last = true)
        {
            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName, null, typeof(T));
            AddToLoad(searcher, callback, last);
            return this;
        }

        /// <summary>
        /// 添加资源到任务列表
        /// </summary>
        public ResourceLoader AddToLoad(string assetName, string bundleName, Action<bool, IResource> callback = null, bool last = true)
        {
            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName, bundleName);
            AddToLoad(searcher, callback, last);
            return this;
        }

        /// <summary>
        /// 添加资源到任务列表
        /// </summary>
        public ResourceLoader AddToLoad<T>(string assetName, string bundleName, Action<bool, IResource> callback = null, bool last = true)
        {
            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName, bundleName, typeof(T));
            AddToLoad(searcher, callback, last);
            return this;
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
            resource.IfNothing(() => resource = ResourceManager.Instance.GetResource(searcher, true));

            // 如果有回调，则注册回到方法
            if (callback != null) {
                callbackCleanerList.IfNothing(() => callbackCleanerList = new LinkedList<CallbackCleaner>());
                // 加入清空清单
                callbackCleanerList.AddLast(new CallbackCleaner(resource, callback));
                // 注册加载完毕事件
                resource.RegisterOnLoadedEvent(callback);
            }

            // 获取依赖
            List<string> depends = resource.GetDependResourceList();

            if (depends != null) {
                // 遍历所有依赖并加载
                foreach (string depend in depends) {
                    // 如果已经加载过资源，不再重复加载，避免内存泄露
                    if (!tempDepends.Contains(depend)) {
                        using ResourceSearcher searchRule = ResourceSearcher.Allocate(depend, null, typeof(AssetBundle));
                        // 添加到已加载的资源
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
            if (cachedResource != null) return;

            // 记录资源加载次数
            resource.Hold();

            // 资源添加到缓存
            resourceList.Add(resource);

            // 放入等待加载列表
            if (resource.State != ResourceState.Ready) {
                loadingCount++;
                last.iif(() => waitForLoadList.AddLast(resource), () => waitForLoadList.AddFirst(resource));
            }
        }

        /// <summary>
        /// 在缓存的资源中查找
        /// </summary>
        private IResource GetResourceInCache(ResourceSearcher searcher)
        {
            return resourceList.Nothing() ? null : resourceList.FirstOrDefault(resource => searcher.Match(resource));
        }

        /*----------------------------- 资源加载完毕后回调 -----------------------------*/

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

            if (loadingCount == 0) {
                RemoveAllCallbacks(false);
                currentCallback?.Invoke();
            }
        }

        /*----------------------------- 释放资源 -----------------------------*/

        /// <summary>
        /// 释放资源
        /// </summary>
        public void ReleaseResource(string assetName)
        {
            if (assetName.Nothing()) return;

            // 清空模拟器模式下加载的资源
            if (Platform.IsSimulation) {
                if (spriteMap.ContainsKey(assetName)) {
                    Sprite sprite = spriteMap[assetName];
                    sprite.DestroySelf();
                    spriteMap.Remove(assetName);
                }
            }

            // 在缓存中查找资源，如果没有则返回
            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName);
            IResource resource = ResourceManager.Instance.GetResource(searcher);
            if (resource == null) return;

            // 清除待下载列表中的资源
            if (waitForLoadList.Remove(resource)) {
                loadingCount--;

                if (loadingCount == 0) { currentCallback = null; }
            }

            // 从缓存中删除，并释放资源
            if (resourceList.Remove(resource)) {
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
            if (assetNames.Nothing()) return;

            foreach (string assetName in assetNames) { ReleaseResource(assetName); }
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
            if (Platform.IsSimulation) {
                foreach (KeyValuePair<string, Sprite> sprite in spriteMap) { sprite.Value.DestroySelf(); }
                spriteMap.Clear();
            }

            if (resourceList.Count > 0) {
                //确保首先删除的是AB，这样能对Asset的卸载做优化
                resourceList.Reverse();

                foreach (IResource resource in resourceList) {
                    resource.UnRegisterOnLoadedEvent(OnResourceLoaded);
                    resource.Release();
                }
                resourceList.Clear();

                if (!ResourceManager.IsApplicationQuit) { ResourceManager.Instance.ClearOnUpdate(); }
            }

            // 释放所有回调事件
            RemoveAllCallbacks(true);
        }

        /// <summary>
        /// 释放所有加载的资源
        /// </summary>
        public void ReleaseAllInstantiateResource()
        {
            foreach (IResource resource in resourceList) {
                if (resource.UnloadImage) {
                    if (waitForLoadList.Remove(resource)) loadingCount--;
                    RemoveCallback(resource, true);
                    resourceList.Remove(resource);
                    resource.UnRegisterOnLoadedEvent(OnResourceLoaded);
                    resource.Release();
                }
            }
            ResourceManager.Instance.ClearOnUpdate();
        }

        /// <summary>
        /// 释放某资源的回调事件
        /// </summary>
        private void RemoveCallback(IResource resource, bool release)
        {
            if (!callbackCleanerList.Nothing()) {
                LinkedListNode<CallbackCleaner> currentNode = callbackCleanerList.First;

                // 遍历所有需要释放的事件
                while (currentNode != null) {
                    CallbackCleaner cleaner = currentNode.Value;
                    LinkedListNode<CallbackCleaner> nextNode = currentNode.Next;

                    if (cleaner.Is(resource)) {
                        if (release) cleaner.Release();
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
            if (callbackCleanerList != null) {
                int count = callbackCleanerList.Count;

                for (int i = 0; i < count; i++) {
                    if (release) { callbackCleanerList.Last.Value.Release(); }
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
            if (unloadObjectList == null) { unloadObjectList = new List<Object>(); }
            unloadObjectList.Add(obj);
        }
    }
}
