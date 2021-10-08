using System;

namespace IFramework.Core
{
    /// <summary>
    /// 可持久化的数字类型抽象类
    /// </summary>
    public abstract class AbstractConfigNumeric<T> : AbstractPropertyNumeric<T>, IPersistable<T> where T : struct, IConvertible, IComparable
    {
        protected string key;

        protected AbstractConfigNumeric(string key, T value)
        {
            this.key = key;
            this.value = value;
        }

        protected override T GetValue()
        {
            return Get();
        }

        protected override void SetValue(T value)
        {
            if (IsValueChanged(value)) {
                this.value = value;
                Save(value);
                setted = true;
            }
        }

        public abstract T Get();

        public abstract void Save(T value);
        
        public abstract void Clear();
    }
}
