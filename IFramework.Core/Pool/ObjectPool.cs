using System;

namespace IFramework.Core
{
    /// <summary>
    /// 对象池实现类
    /// </summary>
    public class ObjectPool<T> : Pool<T>, ISingleton where T : class, IPoolable, new()
    {
        protected ObjectPool()
        {
            factory = new DefaultFactory<T>();
        }

        public static ObjectPool<T> Instance => SingletonProperty<ObjectPool<T>>.Instance;

        void ISingleton.OnInit() { }

        public void Dispose()
        {
            SingletonProperty<ObjectPool<T>>.Dispose();
        }

        /// <summary>
        /// 初始化对象池
        /// </summary>
        /// <param name="maxCount">最大容量</param>
        /// <param name="initCount">初始化的最大数量</param>
        public void Init(int maxCount, int initCount)
        {
            Capacity = maxCount;

            if (maxCount > 0) {
                // 按破水桶原则，初始化最小值
                initCount = Math.Min(maxCount, initCount);
            }

            // 如果数量小于初始化容量，则新增
            for (int i = Count; i < initCount; i++) { Recycle(factory.Create()); }
        }

        /// <summary>
        /// 对象池容量
        /// </summary>
        public int Capacity {
            get => capacity;
            set {
                capacity = value;

                // 如果当前数量超出最大容量，则释放无用数据
                // ReSharper disable once InvertIf
                if (capacity > 0 && capacity < Count) {
                    for (int i = Count; i > capacity; i--) { cache.Pop(); }
                }
            }
        }

        /// <summary>
        /// 分配对象
        /// </summary>
        public override T Allocate()
        {
            T t = base.Allocate();
            t.IsRecycled = false;
            return t;
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        public override bool Recycle(T t)
        {
            // 如果对象已被回收，则返回
            if (t == null || t.IsRecycled) return false;

            // 如果有最大数量限制
            if (capacity > 0) {
                // 如果当前数量超过最大数量，不回收到对象池
                if (Count >= capacity) {
                    t.OnRecycled();
                    return false;
                }
            }

            // 回收到对象池
            t.OnRecycled();
            t.IsRecycled = true;
            cache.Push(t);
            return true;
        }
    }
}
