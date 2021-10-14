using System;

namespace IFramework.Core
{
    /// <summary>
    /// 简单对象池
    /// </summary>
    public class SimplePool<T> : Pool<T>
    {
        private readonly Action<T> onRecycle;

        /// <summary>
        /// 简单对象池构造函数
        /// </summary>
        /// <param name="creater">工厂生产方法</param>
        /// <param name="onRecycle">恢复初始状态方法</param>
        /// <param name="count">缓冲数量</param>
        public SimplePool(Func<T> creater, Action<T> onRecycle = null, int count = 0)
        {
            factory = new CustomFactory<T>(creater);
            this.onRecycle = onRecycle;
            for (int i = 0; i < count; i++) {
                cache.Push(factory.Create());
            }
        }

        /// <summary>
        /// 回收对象
        /// </summary>
        public override bool Recycle(T t)
        {
            onRecycle.InvokeSafe(t);
            if (t != null) {
                cache.Push(t);
            }
            return true;
        }
    }
}
