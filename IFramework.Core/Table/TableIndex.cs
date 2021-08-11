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
using System.Linq;

namespace IFramework.Core
{
    public class TableIndex<TKey, TData> : IDisposable
    {
        private Dictionary<TKey, List<TData>> dictionary = new Dictionary<TKey, List<TData>>();
        
        private Func<TData, TKey> getKey = null;
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public TableIndex(Func<TData, TKey> getKey)
        {
            this.getKey = getKey;
        }
        
        public IDictionary<TKey, List<TData>> Dictionary
        {
            get { return dictionary; }
        }
        
        public IEnumerable<TData> Get(TKey key)
        {
            if (dictionary.TryGetValue(key, out List<TData> list))
            {
                return list;
            }

            // 返回一个空的集合
            return Enumerable.Empty<TData>();
        }

        public void Add(TData data)
        {
            TKey key = getKey(data);

            if (dictionary.ContainsKey(key))
            {
                dictionary[key].Add(data);
            }
            else
            {
                List<TData> list = ListPool<TData>.Get();
                list.Add(data);
                dictionary.Add(key, list);
            }
        }

        public void Remove(TData data)
        {
            TKey key = getKey(data);
            dictionary.Remove(key);
        }

        public void Clear()
        {
            foreach (List<TData> value in dictionary.Values)
            {
                value.Clear();
            }

            dictionary.Clear();
        }
        
        public void Dispose()
        {
            //先回收字典元素对象
            foreach (List<TData> value in dictionary.Values)
            {
                value.Release2Pool();
            }

            // 再回收字典本身
            dictionary.Release2Pool();

            dictionary = null;
        }
    }
}