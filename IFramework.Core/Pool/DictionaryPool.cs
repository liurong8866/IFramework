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

namespace IFramework.Core {
    public class DictionaryPool<TKey, TValue> {

        private static int capacity = 10;

        /// <summary>
        /// 栈对象：存储多个字典
        /// </summary>
        private static Stack<Dictionary<TKey, TValue>> cache = new Stack<Dictionary<TKey, TValue>>(capacity);

        /// <summary>
        /// 出栈：从栈中获取某个字典数据
        /// </summary>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> Allocate() {
            if (cache.Count == 0) {
                return new Dictionary<TKey, TValue>(capacity);
            }
            return cache.Pop();
        }

        /// <summary>
        /// 入栈：将字典数据存储到栈中 
        /// </summary>
        /// <param name="release"></param>
        public static void Release(Dictionary<TKey, TValue> release) {
            release.Clear();
            cache.Push(release);
        }

    }

    public static class DictionaryPoolExtensions {

        /// <summary>
        /// 对字典拓展 自身入栈 的方法
        /// </summary>
        public static void Recycle<TKey, TValue>(this Dictionary<TKey, TValue> self) {
            DictionaryPool<TKey, TValue>.Release(self);
        }

    }
}
