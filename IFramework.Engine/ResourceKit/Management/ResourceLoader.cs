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
using IFramework.Core;
using Object=UnityEngine.Object;

namespace IFramework.Engine
{
    public class ResourceLoader : Disposeble, IPoolable, IRecyclable
    {
        // 资源列表
        private readonly List<IResource> resources = new List<IResource>();
        // 等待加载的资源
        private readonly LinkedList<IResource> loadList = new LinkedList<IResource>();
        // 等待回收的对象
        private List<Object> tobeUnloadedObjects;

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
        
        
        private void AddToLoad(ResourceSearcher searcher, Action<bool, IResource> action, bool last = true)
        {
            
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