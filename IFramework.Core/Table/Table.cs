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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IFramework.Core {
    public class Table<T> : IEnumerable<T>, IDisposable {

        // 定义一个数据字典列表
        private Dictionary<string, List<T>> dictionary = new Dictionary<string, List<T>>();

        /// <summary>
        /// 获取数据
        /// </summary>
        public List<T> Get(string key) {
            return dictionary.TryGetValue(key, out List<T> list) ? list : new List<T>();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        public void Add(string key, T data) {
            // 如果字典中有该键，则直接添加到该键对应的List中
            if (dictionary.ContainsKey(key)) {
                dictionary[key].Add(data);
            }
            // 没有则创建List并添加到字典
            else {
                List<T> list = ListPool<T>.Allocate();
                list.Add(data);
                dictionary.Add(key, list);
            }
        }

        /// <summary>
        /// 添加数据，如果key的获取方式比较复杂，可以传递一个代理方法
        /// </summary>
        /// <param name="data">需要传的数据</param>
        /// <param name="getKey">获取key的代理方法</param>
        public void Add(T data, Func<T, string> getKey) {
            // 根据外部方法获得key
            string key = getKey(data);
            Add(key, data);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public void Remove(string key) {
            dictionary.Remove(key);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public void Remove(T data, Func<T, string> getKey) {
            // 根据外部方法获得key
            string key = getKey(data);
            Remove(key);
        }

        /// <summary>
        /// 清空字典
        /// </summary>
        public void Clear() {
            foreach (List<T> value in dictionary.Values) {
                value.Clear();
            }
            dictionary.Clear();
        }

        /// <summary>
        /// 回收方法
        /// </summary>
        public void Dispose() {
            //先回收字典元素对象
            foreach (List<T> value in dictionary.Values) {
                value.Recycle();
            }

            // 再回收字典本身
            dictionary.Recycle();
            dictionary = null;
        }

        /// <summary>
        /// 实现迭代器
        /// </summary>
        public IEnumerator<T> GetEnumerator() {
            return dictionary.SelectMany(d => d.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

    }
}
