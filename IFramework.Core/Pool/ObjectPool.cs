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

namespace IFramework.Core
{
    /// <summary>
    /// 对象池实现类
    /// </summary>
    public class ObjectPool<T> : Pool<T>, ISingleton where T : class, IPoolable, new()
    {
        // 最大对象
        private int maxCount;

        /*-----------------------------*/
        /* 实现对象池单例                */
        /*-----------------------------*/

        protected ObjectPool()
        {
            factory = new DefaultFactory<T>();
        }

        public static ObjectPool<T> Instance
        {
            get { return SingletonProperty<ObjectPool<T>>.Instance; }
        }

        void ISingleton.OnInit() { }

        public void Dispose()
        {
            SingletonProperty<ObjectPool<T>>.Dispose();
        }
        
        /*-----------------------------*/
        /* 初始化对象池                  */
        /*-----------------------------*/
        
        /// <summary>
        /// 初始化对象池
        /// </summary>
        /// <param name="maxCount">最大容量</param>
        /// <param name="initCount">初始化的最大数量</param>
        public void Init(int maxCount, int initCount)
        {
            Capacity = maxCount;

            if (maxCount > 0)
            {
                // 按破水桶原则，初始化最小值
                initCount = Math.Min(maxCount, initCount);
            }

            // 如果数量小于初始化容量，则新增
            if (Count < initCount)
            {
                for (int i = Count; i < initCount; i++)
                {
                    Recycle(factory.Create());
                }
            }
        }
        
        /// <summary>
        /// 对象池容量
        /// </summary>
        public int Capacity
        {
            get { return maxCount; }
            set
            {
                maxCount = value;
                
                // 如果当前数量超出最大容量，则释放无用数据
                if (maxCount > 0 && maxCount < Count)
                {
                    for (int i = Count; i > maxCount; i--)
                    {
                        cache.Pop();
                    }
                }
            }
        }
        
        /// <summary>
        /// 分配对象
        /// </summary>
        public override T Allocate()
        {
            T t = base.Allocate();
            t.IsRecycled = false;
            
            return t;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        public override bool Recycle(T t)
        {
            // 如果对象已被回收，则返回
            if (t == null || t.IsRecycled) return false;

            // 如果有最大数量限制
            if (maxCount > 0)
            {
                // 如果当前数量超过最大数量，不回收到对象池
                if (Count >= maxCount)
                {
                    t.OnRecycled();
                    return false;
                }
            }
            
            // 回收到对象池
            t.OnRecycled();
            t.IsRecycled = true;
            cache.Push(t);

            return true;
        }
        
    }
}