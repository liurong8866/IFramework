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
    /// 简单对象池
    /// </summary>
    public class SimplePool<T> : Pool<T>
    {
        private readonly Action<T> onRecycle;

        /// <summary>
        /// 简单对象池构造函数
        /// </summary>
        /// <param name="creater">工厂生产方法</param>
        /// <param name="onRecycle">恢复初始状态方法</param>
        /// <param name="count">缓冲数量</param>
        public SimplePool(Func<T> creater, Action<T> onRecycle = null, int count = 0)
        {
            factory = new CustomFactory<T>(creater);

            this.onRecycle = onRecycle;
            
            for (int i = 0; i < count; i++)
            {
                cache.Push(factory.Create());
            }
        }
        
        /// <summary>
        /// 回收对象
        /// </summary>
        public override bool Recycle(T t)
        {
            onRecycle.InvokeSafe(t);

            if (t != null)
            {
                cache.Push(t);
            }
            
            return true;
        }
    }
}