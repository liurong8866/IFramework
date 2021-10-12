using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    [MonoSingleton("[Framework]/ResourceManager")]
    public sealed class ResourceManager : MonoSingleton<ResourceManager>
    {
        // 是否被初始化
        private static bool isInit;
        // 当前协程数量
        [SerializeField]
        private int currentCoroutineCount;
        // 最大协程数量
        private readonly int maxCoroutineCount = 8;
        // 异步加载任务列表
        private readonly LinkedList<IResourceLoadTask> asyncLoadTasks = new LinkedList<IResourceLoadTask>();
        // 资源列表
        private readonly ResourceTable resourceTable = new ResourceTable();
        // Resource在ResourceManager中 删除的问题，定时收集列表中的Resource然后删除
        private bool isResourceMapDirty;

        /*----------------------------- 初始化Manager 自动加载 -----------------------------*/

        public static void Init()
        {
            if (isInit) return;

            isInit = true;
            InitPool();

            // 初始化Manager自身
            Instance.InitResourceManager();
        }

        /// <summary>
        /// 只是为了解决调用异步初始化方法问题
        /// </summary>
        public void InitAsync()
        {
            StartCoroutine(DoInitAsync());
        }

        /// <summary>
        /// 异步初始化
        /// </summary>
        private static IEnumerator DoInitAsync()
        {
            if (isInit) yield break;

            isInit = true;
            InitPool();

            // 初始化Manager自身
            yield return Instance.InitResourceManagerAsync();
        }

        /// <summary>
        /// 初始化各种对象池
        /// </summary>
        private static void InitPool()
        {
            ObjectPool<Resource>.Instance.Init(40, 20);
            ObjectPool<AssetResource>.Instance.Init(40, 20);
            ObjectPool<AssetBundleResource>.Instance.Init(40, 20);
            ObjectPool<ResourceSearcher>.Instance.Init(40, 20);
            ObjectPool<ResourceLoader>.Instance.Init(40, 20);
        }

        /// <summary>
        /// 初始化Manager自身（异步）
        /// </summary>
        private void InitResourceManager()
        {
            if (Platform.IsSimulation) {
                // 获取所有AssetBundle资源信息
                AssetBundleConfig config = new AssetBundleConfig();
                Environment.Instance.InitAssetBundleConfig(config);
                AssetBundleConfig.ConfigFile = config;
            }
            else {
                AssetBundleConfig.ConfigFile.Reset();
                List<string> configFiles;

                // 未进行过热更新
                if (Configure.LoadAssetFromStream) {
                    Zip zip = new Zip();
                    configFiles = zip.GetFileInInner(Constant.ASSET_BUNDLE_CONFIG_FILE);
                }
                // 进行过热更新
                else {
                    configFiles = DirectoryUtils.GetFiles(Platform.PersistentData.Root, Constant.ASSET_BUNDLE_CONFIG_FILE);
                }

                if (configFiles != null) {
                    foreach (string file in configFiles) {
                        AssetBundleConfig.ConfigFile.LoadFromFile(file);
                    }
                }
            }
        }

        /// <summary>
        /// 初始化Manager自身（异步）
        /// </summary>
        private IEnumerator InitResourceManagerAsync()
        {
            if (Platform.IsSimulation) {
                AssetBundleConfig config = new AssetBundleConfig();
                Environment.Instance.InitAssetBundleConfig(config);
                AssetBundleConfig.ConfigFile = config;
                yield return null;
            }
            else {
                AssetBundleConfig.ConfigFile.Reset();
                List<string> configFiles = new List<string>();

                // 未进行过热更新
                if (Configure.LoadAssetFromStream) {
                    configFiles.Add(Platform.FilePathPrefix + Platform.RuntimeStreamAssetBundlePath);
                }
                // 进行过热更新
                else {
                    string persistentPath = DirectoryUtils.CombinePath(Platform.PersistentData.Root, Constant.ASSET_BUNDLE_CONFIG_FILE);
                    configFiles.Add(Platform.FilePathPrefix + persistentPath);
                }

                foreach (string file in configFiles) {
                    yield return AssetBundleConfig.ConfigFile.LoadFromFileAsync(file);
                }
                yield return null;
            }
        }

        /*----------------------------- 获取Resource资源管理 -----------------------------*/

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="searcher">查询器</param>
        /// <param name="create">如果没有，是否创建</param>
        /// <returns></returns>
        public IResource GetResource(ResourceSearcher searcher, bool create = false)
        {
            // 从资源表中获取资源
            IResource resource = resourceTable.GetResource(searcher);
            if (resource != null) return resource;

            // 如果资源为空，并需要创建资源，则创建并加入资源表
            if (create) {
                resource = ResourceCreatorFactory.Create(searcher);

                if (resource != null) {
                    resourceTable.Add(resource.AssetName, resource);
                }
            }
            return resource;
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        public T GetResource<T>(ResourceSearcher searcher, bool create = false) where T : class, IResource
        {
            return GetResource(searcher, create) as T;
        }

        /*----------------------------- 异步加载资源 -----------------------------*/

        /// <summary>
        /// 添加加载任务到队列
        /// </summary>
        public void AddResourceLoadTask(IResourceLoadTask task)
        {
            if (task == null) return;

            // 放置队列末尾
            asyncLoadTasks.AddLast(task);

            // 尝试加载下一个资源
            NextResourceLoadTask();
        }

        /// <summary>
        /// 如果没有到达缓冲池上限，同时开启多个资源的加载
        /// </summary>
        private void NextResourceLoadTask()
        {
            // 任务列表空，不执行
            if (asyncLoadTasks.Count == 0) return;

            // 达到最大协程数则暂时不执行
            if (currentCoroutineCount >= maxCoroutineCount) return;

            // 从队列头部加载
            IResourceLoadTask task = asyncLoadTasks.First.Value;

            // 删除列表中的任务，避免重复执行
            asyncLoadTasks.RemoveFirst();

            // 当前协程数+1
            currentCoroutineCount++;

            // 启动协程
            StartCoroutine(task.LoadAsync(OnResourceLoadTaskFinish));
        }

        /// <summary>
        /// 当资源加载完毕后，更新当前协程数量
        /// </summary>
        private void OnResourceLoadTaskFinish()
        {
            // 执行完毕，当前协程数-1
            currentCoroutineCount--;

            // 递归调用下一条加载任务
            NextResourceLoadTask();
        }

        /*----------------------------- 释放资源 -----------------------------*/

        private void Update()
        {
            if (isResourceMapDirty) {
                RemoveUnusedResource();
            }
        }

        /// <summary>
        /// 是否脏数据
        /// </summary>
        public void ClearOnUpdate()
        {
            isResourceMapDirty = true;
        }

        /// <summary>
        /// 清除不在使用的资源
        /// </summary>
        private void RemoveUnusedResource()
        {
            if (!isResourceMapDirty) return;

            isResourceMapDirty = false;

            foreach (IResource resource in resourceTable.ToList()) {
                if (resource.Counter <= 0 && resource.State != ResourceState.Loading) {
                    if (resource.Release()) {
                        resourceTable.Remove(resource.AssetName.ToLowerInvariant());
                        resource.Recycle();
                    }
                }
            }
        }
    }
}
