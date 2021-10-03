using System.Collections.Generic;

namespace IFramework.Core
{
    /// <summary>
    /// 对象池
    /// </summary>
    public abstract class Pool<T> : IPool<T>
    {
        // 存储相关数据的栈
        protected readonly Stack<T> cache = new Stack<T>();

        // 最大对象数量
        protected int capacity = 12;

        // 对象工厂
        protected IFactory<T> factory;

        /// <summary>
        /// 对象数量
        /// </summary>
        public int Count => cache.Count;

        /// <summary>
        /// 分配对象
        /// </summary>
        public virtual T Allocate() { return cache.Count == 0 ? factory.Create() : cache.Pop(); }

        /// <summary>
        /// 回收对象
        /// </summary>
        public abstract bool Recycle(T t);
    }
}
