using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace IFramework.Core
{
    public class Table<T> : IEnumerable<List<T>>, IDisposable
    {
        // 定义一个数据字典列表
        private Dictionary<string, List<T>> dictionary = new Dictionary<string, List<T>>();

        /// <summary>
        /// 获取数据
        /// </summary>
        public List<T> Get(string key)
        {
            return dictionary.TryGetValue(key, out List<T> list) ? list : new List<T>();
        }

        /// <summary>
        /// 获取Key值数据的第一条
        /// </summary>
        public T GetFirst(string key)
        {
            return dictionary.TryGetValue(key, out List<T> list) ? list[0]: default(T);
        }
        
        /// <summary>
        /// 获取Key值数据的第一条
        /// </summary>
        public T GetLast(string key)
        {
            return dictionary.TryGetValue(key, out List<T> list) ? list[list.Count-1]: default(T);
        }
        
        /// <summary>
        /// 获取数据
        /// </summary>
        public List<string> GetKeys()
        {
            return dictionary.Keys.ToList();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        public void Add(string key, T data)
        {
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
        public void Add(T data, Func<T, string> getKey)
        {
            // 根据外部方法获得key
            string key = getKey(data);
            Add(key, data);
        }

        /// <summary>
        /// 删除数据
        /// </summary>
        public void Remove(string key)
        {
            dictionary.Remove(key);
        }
        
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="condition">条件函数</param>
        public void Remove(string key, Func<T, bool> condition)
        {
            List<T> list = Get(key);

            for (int i = list.Count - 1; i >= 0; i--) {
                if (condition(list[i])) {
                    list.Remove(list[i]);
                }
            }
            // 如果key值为空，则删除key
            if (list?.Count == 0) {
                Remove(key);
            }
        }
        
        /// <summary>
        /// 删除Key值数据的第一条
        /// </summary>
        public void RemoveFirst(string key)
        {
            List<T> list = Get(key);
            list?.RemoveAt(0);
            
            // 如果key值为空，则删除key
            if (list?.Count == 0) {
                Remove(key);
            }
        }

        /// <summary>
        /// 删除Key值数据的最后一条
        /// </summary>
        public void RemoveLast(string key)
        {
            List<T> list = Get(key);
            list?.RemoveAt(list.Count-1);
            
            // 如果key值为空，则删除key
            if (list?.Count == 0) {
                Remove(key);
            }
        }
        
        /// <summary>
        /// 删除数据
        /// </summary>
        public void Remove(T data, Func<T, string> getKey)
        {
            // 根据外部方法获得key
            string key = getKey(data);
            Remove(key);
        }

        /// <summary>
        /// 清空字典
        /// </summary>
        public void Clear()
        {
            foreach (List<T> value in dictionary.Values) {
                value.Clear();
            }
            dictionary.Clear();
        }

        /// <summary>
        /// 回收方法
        /// </summary>
        public virtual void Dispose()
        {
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
        public IEnumerator<List<T>> GetEnumerator()
        {
            return dictionary.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
