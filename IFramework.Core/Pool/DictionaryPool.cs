using System.Collections.Generic;

namespace IFramework.Core
{
    public class DictionaryPool<TKey, TValue>
    {
        private static readonly int capacity = 10;

        /// <summary>
        /// 栈对象：存储多个字典
        /// </summary>
        private static readonly Stack<Dictionary<TKey, TValue>> cache = new Stack<Dictionary<TKey, TValue>>(capacity);

        /// <summary>
        /// 出栈：从栈中获取某个字典数据
        /// </summary>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> Allocate()
        {
            if (cache.Count == 0) {
                return new Dictionary<TKey, TValue>(capacity);
            }
            return cache.Pop();
        }

        /// <summary>
        /// 入栈：将字典数据存储到栈中 
        /// </summary>
        /// <param name="release"></param>
        public static void Release(Dictionary<TKey, TValue> release)
        {
            release.Clear();
            cache.Push(release);
        }
    }

    public static class DictionaryPoolExtensions
    {
        /// <summary>
        /// 对字典拓展 自身入栈 的方法
        /// </summary>
        public static void Recycle<TKey, TValue>(this Dictionary<TKey, TValue> self)
        {
            DictionaryPool<TKey, TValue>.Release(self);
        }
    }
}
