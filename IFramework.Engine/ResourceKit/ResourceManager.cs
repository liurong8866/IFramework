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
using IFramework.Core;
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
        // 资源表
        private ResourceTable resourceTable = new ResourceTable();
        
        /*-----------------------------*/
        /* 初始化Manager 自动加载         */
        /*-----------------------------*/
        
        /// <summary>
        /// 全局自动加载一次，请不要手动调用
        /// </summary>
        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            // 初始化Manager自身
            Instance.StartInitAsync();
        }
        
        /// <summary>
        /// 只是为了解决调用异步初始化方法问题
        /// </summary>
        private void StartInitAsync()
        {
            StartCoroutine(InitAsync());
        }

        /// <summary>
        /// 异步初始化
        /// </summary>
        private static IEnumerator InitAsync()
        {
            if(isInit) yield break;
            
            isInit = true;
            
            Log.Info("正在初始化 Resource Manager");
            
            // 初始化各种对象池
            Instance.InitPools();

            // 初始化Manager自身
            yield return Instance.InitResourceManagerAsync();
        }

        /// <summary>
        /// 初始化各种对象池
        /// </summary>
        private void InitPools()
        {
            // Resource 加载器
            ObjectPool<Resource>.Instance.Init(40,20);
        }

        /// <summary>
        /// 初始化Manager自身（异步）
        /// </summary>
        private IEnumerator InitResourceManagerAsync()
        {
            yield return null;
        }
        
        /*-----------------------------*/
        /* 获取Resource资源管理          */
        /*-----------------------------*/
        
        /// <summary>
        /// 获取资源加载器
        /// </summary>
        /// <param name="searcher">查询器</param>
        /// <param name="create">如果没有，是否创建加载器</param>
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
                    resourceTable.Add(resource);
                }
            }
            
            return resource;
        }

        /// <summary>
        /// 获取资源加载器
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
    }
}