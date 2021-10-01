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
    public static class ListPool<T>
    {
        private static int capacity = 10;

        /// <summary>
        /// 栈对象：存储多个List
        /// </summary>
        private static Stack<List<T>> cache = new Stack<List<T>>(capacity);

        /// <summary>
        /// 出栈：获取某个List对象
        /// </summary>
        public static List<T> Allocate() {
            if (cache.Count == 0) {
                return new List<T>(capacity);
            }
            return cache.Pop();
        }

        /// <summary>
        /// 入栈：将List对象添加到栈中
        /// </summary>
        public static void Release(List<T> release) {
            release.Clear();
            cache.Push(release);
        }
    }

    public static class ListPoolExtensions
    {
        /// <summary>
        /// 给List拓展 自身入栈 的方法
        /// </summary>
        public static void Recycle<T>(this List<T> self) {
            ListPool<T>.Release(self);
        }
    }
}
