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
using UnityEngine;

namespace IFramework.Engine
{
    [MonoSingleton("[Framework]/ResourceManager")]
    public class ResourceManager : MonoSingleton<ResourceManager>
    {
        [SerializeField] 
        private int currentCoroutineCount = 0;
        private int maxCoroutineCount = 8; //最快协成大概在6到8之间
        private readonly LinkedList<IResourceLoadTask> asyncLoadTasks = new LinkedList<IResourceLoadTask>();

        private ResourceTable resourceTable = new ResourceTable();

        private static bool inited = false;
        
        /*-----------------------------*/
        /* 初始化Manager                */
        /*-----------------------------*/
        
        public static void Init()
        {
            if(inited) return;
            inited = true;
            
            ObjectPool<Resource>.Instance.Init(40,20);

            Instance.InitResourceManager();
        }

        public static IEnumerator InitAsync()
        {
            if(inited) yield break;
            inited = true;
            
            ObjectPool<Resource>.Instance.Init(40,20);

            yield return Instance.InitResourceManagerAsync();
        }
        
        public void InitResourceManager()
        {
            
        }
        
        public IEnumerator InitResourceManagerAsync()
        {
            yield return null;
        }
        
        
        /*-----------------------------*/
        /* 获取Resource资源管理          */
        /*-----------------------------*/
        
        public IResource GetResource(ResourceSearchRule searchRule, bool create = false)
        {
            IResource resource = resourceTable.GetResource(searchRule);

            if (resource != null)
            {
                return resource;
            }

            if (create)
            {
                resource = ResourceFactory.Create(searchRule);
            }

            if (resource != null)
            {
                resourceTable.Add(resource);
            }

            return resource;
        }

        public T GetResource<T>(ResourceSearchRule rule) where T : class, IResource
        {
            return GetResource(rule) as T;
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

            asyncLoadTasks.AddLast(task);
            
            TryStartNextResourceLoadTask();
        }
        
        /// <summary>
        /// 如果没有到达缓冲池上限，同时开启多个资源的加载
        /// </summary>
        private void TryStartNextResourceLoadTask()
        {
            if (asyncLoadTasks.Count == 0) return;

            if (currentCoroutineCount >= maxCoroutineCount) return;

            var task = asyncLoadTasks.First.Value;
            
            asyncLoadTasks.RemoveFirst();

            currentCoroutineCount++;
            
            StartCoroutine(task.LoadAsync(OnIEnumeratorTaskFinish));
        }
        
        /// <summary>
        /// 当资源加载完毕后，更新当前协程数量
        /// </summary>
        private void OnIEnumeratorTaskFinish()
        {
            currentCoroutineCount--;
            
            TryStartNextResourceLoadTask();
        }
    }
}