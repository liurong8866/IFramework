using System;

namespace IFramework.Core
{
    /// <summary>
    /// 自定义对象工厂
    /// </summary>
    public class CustomFactory<T> : IFactory<T>
    {
        protected Func<T> creater;

        public CustomFactory(Func<T> creater) { this.creater = creater; }

        public T Create() { return creater(); }
    }
}
