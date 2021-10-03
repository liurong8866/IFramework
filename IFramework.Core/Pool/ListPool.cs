using System.Collections.Generic;

namespace IFramework.Core
{
    /// <summary>
    /// 链表对象池：存储相关对象
    /// </summary>
    public static class ListPool<T>
    {
        private static readonly int capacity = 10;

        /// <summary>
        /// 栈对象：存储多个List
        /// </summary>
        private static readonly Stack<List<T>> cache = new Stack<List<T>>(capacity);

        /// <summary>
        /// 出栈：获取某个List对象
        /// </summary>
        public static List<T> Allocate()
        {
            if (cache.Count == 0) {
                return new List<T>(capacity);
            }
            return cache.Pop();
        }

        /// <summary>
        /// 入栈：将List对象添加到栈中
        /// </summary>
        public static void Release(List<T> release)
        {
            release.Clear();
            cache.Push(release);
        }
    }

    public static class ListPoolExtensions
    {
        /// <summary>
        /// 给List拓展 自身入栈 的方法
        /// </summary>
        public static void Recycle<T>(this List<T> self) { ListPool<T>.Release(self); }
    }
}
