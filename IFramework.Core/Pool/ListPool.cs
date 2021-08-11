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

using System.Collections.Generic;

namespace IFramework.Core
{
    /// <summary>
    /// 链表对象池：存储相关对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class ListPool<T>
    {
        private static int capacity = 10;
        
        /// <summary>
        /// 栈对象：存储多个List
        /// </summary>
        static Stack<List<T>> cache = new Stack<List<T>>(capacity);

        /// <summary>
        /// 出栈：获取某个List对象
        /// </summary>
        /// <returns></returns>
        public static List<T> Get()
        {
            if (cache.Count == 0)
            {
                return new List<T>(capacity);
            }

            return cache.Pop();
        }

        /// <summary>
        /// 入栈：将List对象添加到栈中
        /// </summary>
        /// <param name="release"></param>
        public static void Release(List<T> release)
        {
            release.Clear();
            cache.Push(release);
        }
    }

    /// <summary>
    /// 链表对象池 拓展方法类
    /// </summary>
    public static class ListPoolExtensions
    {
        /// <summary>
        /// 给List拓展 自身入栈 的方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        public static void Release2Pool<T>(this List<T> self)
        {
            ListPool<T>.Release(self);
        }
    }
}