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

using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IFramework.Core;
using IFramework.Core.Environment;
using UnityEditor;
using UnityEngine;

namespace IFramework.Engine
{
    [MonoSingleton("[Framework]/ResourceManager")]
    public sealed class ResourceManager : MonoSingleton<ResourceManager>
    {
        // 是否被初始化
        private static bool isInit = false;
        // 当前协程数量
        [SerializeField] 
        private int currentCoroutineCount = 0;
        // 最大协程数量
        private int maxCoroutineCount = 8; 
        // 异步加载任务列表
        private readonly LinkedList<IResourceLoadTask> asyncLoadTasks = new LinkedList<IResourceLoadTask>();
        // 资源列表
        private readonly ResourceTable resourceTable = new ResourceTable();
        // Resource在ResourceManager中 删除的问题，定时收集列表中的Resource然后删除
        private bool isResourceMapDirty = false;
        
        /*-----------------------------*/
        /* 初始化Manager 自动加载         */
        /*-----------------------------*/

        /// <summary>
        /// 全局自动加载一次，请不要手动调用！！
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnLoad()
        {
            // 初始化Manager自身
            // Instance.StartInitAsync();
            Init();
        }
        
        public static void Init()
        {
            if(isInit) return;

            isInit = true;
            
            Log.Info("正在初始化 Resource Manager");

            InitPool();
            
            // 初始化Manager自身
            Instance.InitResourceManager();
        }

        /// <summary>
        /// 异步初始化
        /// </summary>
        private static IEnumerator InitAsync()
        {
            if(isInit) yield break;
            
            isInit = true;
            
            Log.Info("正在初始化 Resource Manager");

            InitPool();
            
            // 初始化Manager自身
            yield return Instance.InitResourceManagerAsync();
        }

        /// <summary>
        /// 只是为了解决调用异步初始化方法问题
        /// </summary>
        private void StartInitAsync()
        {
            StartCoroutine(InitAsync());
        }

        /// <summary>
        /// 初始化各种对象池
        /// </summary>
        private static void InitPool()
        {
            ObjectPool<AssetBundleResource>.Instance.Init(40,20);
            ObjectPool<AssetResource>.Instance.Init(40,20);
            ObjectPool<Resource>.Instance.Init(40,20);
            ObjectPool<ResourceSearcher>.Instance.Init(40, 20);
            ObjectPool<ResourceLoader>.Instance.Init(40, 20);
        }
        
        /// <summary>
        /// 初始化Manager自身（异步）
        /// </summary>
        private void InitResourceManager()
        {
            if (Configure.IsSimulation)
            {
                // 获取所有AssetBundle资源信息
                AssetDataConfig config = new AssetDataConfig();
                Environment.AddAssetBundleInfoToResourceData(config);
                AssetDataConfig.ConfigFile = config;
            }
            else
            {
                AssetDataConfig.ConfigFile.Reset();
                List<string> configFiles = new List<string>();

                // 未进行过热更新
                if (Configure.LoadAssetFromStream)
                {
                    Zip zip = new Zip();
                    
                    configFiles = zip.GetFileInInner(Constant.ASSET_BUNDLE_CONFIG_FILE);
                }
                // 进行过热更新
                else
                {
                    configFiles = DirectoryUtils.GetFiles(PlatformSetting.PersistentDataPath, Constant.ASSET_BUNDLE_CONFIG_FILE);
                }

                foreach (string file in configFiles)
                {
                    file.LogInfo();
                    AssetDataConfig.ConfigFile.LoadFromFile(file);
                }
            }
        }
        
        /// <summary>
        /// 初始化Manager自身（异步）
        /// </summary>
        private IEnumerator InitResourceManagerAsync()
        {
            if (Configure.IsSimulation)
            {
                AssetDataConfig config = new AssetDataConfig();
                Environment.AddAssetBundleInfoToResourceData(config);
                AssetDataConfig.ConfigFile = config;
                yield return null;
            }
            else
            {
                AssetDataConfig.ConfigFile.Reset();
                List<string> configFiles = new List<string>();

                // 未进行过热更新
                if (Configure.LoadAssetFromStream)
                {
                    string streamPath = Path.Combine(PlatformSetting.StreamingAssetBundlePath, Environment.PlatformName);
                    configFiles.Add(Environment.FilePathPrefix + streamPath);
                }
                // 进行过热更新
                else
                {
                    var persistentPath = Path.Combine(PlatformSetting.StreamingAssetBundlePath, Environment.PlatformName, Constant.ASSET_BUNDLE_CONFIG_FILE);
                    configFiles.Add(Environment.FilePathPrefix + persistentPath);
                    configFiles = DirectoryUtils.GetFiles(PlatformSetting.PersistentDataPath, Constant.ASSET_BUNDLE_CONFIG_FILE);
                }

                foreach (string file in configFiles)
                {
                    file.LogInfo();
                    yield return AssetDataConfig.ConfigFile.LoadFromFileAsync(file);
                }
                yield return null;
            }
        }
        
        /*-----------------------------*/
        /* 获取Resource资源管理          */
        /*-----------------------------*/
        
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
            if (create)
            {
                resource = ResourceFactory.Create(searcher);
                
                if (resource != null)
                {
                    resourceTable.Add(resource.AssetName, resource);
                }
            }
            
            return resource;
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        public T GetResource<T>(ResourceSearcher searcher) where T : class, IResource
        {
            return GetResource(searcher) as T;
        }
        
        /*-----------------------------*/
        /* 异步加载资源                  */
        /*-----------------------------*/
        
        /// <summary>
        /// 添加加载任务到队列
        /// </summary>
        public void AddResourceLoadTask(IResourceLoadTask task)
        {
            if(task == null) return;
            
            // 放置队列末尾
            asyncLoadTasks.AddLast(task);
            
            // 尝试加载下一个资源
            TryStartNextResourceLoadTask();
        }
        
        /// <summary>
        /// 如果没有到达缓冲池上限，同时开启多个资源的加载
        /// </summary>
        private void TryStartNextResourceLoadTask()
        {
            // 任务列表空，不执行
            if (asyncLoadTasks.Count == 0) return;

            // 达到最大协程数则暂时不执行
            if (currentCoroutineCount >= maxCoroutineCount) return;

            // 从队列头部加载
            var task = asyncLoadTasks.First.Value;
            
            // 删除列表中的任务，避免重复执行
            asyncLoadTasks.RemoveFirst();
            
            // 当前协程数+1
            currentCoroutineCount++;
            
            // 启动协程
            StartCoroutine(task.LoadAsync(OnIEnumeratorTaskFinish));
        }
        
        /// <summary>
        /// 当资源加载完毕后，更新当前协程数量
        /// </summary>
        private void OnIEnumeratorTaskFinish()
        {
            // 执行完毕，当前协程数-1
            currentCoroutineCount--;
            
            // 递归调用下一条加载任务
            TryStartNextResourceLoadTask();
        }
        
        /*-----------------------------*/
        /* 释放资源                     */
        /*-----------------------------*/

        private void Update()
        {
            if (isResourceMapDirty)
            {
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

            foreach (IResource resource in resourceTable.ToList())
            {
                if (resource.Count <= 0 && resource.State != ResourceState.Loading)
                {
                    if (resource.Release())
                    {
                        resourceTable.Remove(resource.AssetName.ToLower());
                        resource.Recycle();
                    }
                }
            }

        }
    }
}