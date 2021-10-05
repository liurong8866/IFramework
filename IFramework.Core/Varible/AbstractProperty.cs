using System;

namespace IFramework.Core
{
    /// <summary>
    /// 变量自定义基础类
    /// </summary>
    [Serializable]
    public abstract class AbstractProperty<T> : IDisposable
    {
        // 变量值
        protected T value;

        public AbstractProperty() { }

        public AbstractProperty(T value)
        {
            this.value = value;
        }

        // 解决因其他原因导致值未设置，而不触发事件问题
        protected bool setted = false;

        public T Value { get => GetValue(); set => SetValue(value); }

        /// <summary>
        /// 判断是否值改变
        /// </summary>
        protected virtual bool IsValueChanged(T value)
        {
            return value == null || !value.Equals(this.value) || !setted;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public virtual void Dispose() { }

        // 子类需要实现的抽象方法
        protected abstract T GetValue();

        protected abstract void SetValue(T value);

        //重载运算符"=="
        public static bool operator ==(AbstractProperty<T> m, AbstractProperty<T> n)
        {
            if (ReferenceEquals(m, n)) { return true; }

            if ((object)m == null || (object)n == null) { return false; }
            return m.Value.Equals(n.Value);
        }

        public static bool operator ==(AbstractProperty<T> m, T n)
        {
            if (ReferenceEquals(m, n)) { return true; }

            if ((object)m == null || n == null) { return false; }
            return m.Value.Equals(n);
        }

        public static bool operator ==(T m, AbstractProperty<T> n)
        {
            if (ReferenceEquals(m, n)) { return true; }

            if (m == null || (object)n == null) { return false; }
            return m.Equals(n.Value);
        }

        public static bool operator !=(AbstractProperty<T> m, AbstractProperty<T> n)
        {
            return !(m == n);
        }

        public static bool operator !=(AbstractProperty<T> m, T n)
        {
            return !(m == n);
        }

        public static bool operator !=(T m, AbstractProperty<T> n)
        {
            return !(m == n);
        }

        public override bool Equals(object obj)
        {
            if (obj == null) { return false; }

            // 引用地址如果不同，则返回false
            if (!ReferenceEquals(this, obj)) { return false; }

            if (obj.GetType() == typeof(AbstractProperty<T>)) {
                AbstractProperty<T> abstractProperty = obj as AbstractProperty<T>;
                return Equals(abstractProperty);
            }

            // 判断类型Value是否一致
            if (obj.GetType() != typeof(T)) { return false; }
            return Value.Equals(obj);
        }

        public bool Equals(AbstractProperty<T> abstractProperty)
        {
            if ((object)abstractProperty == null) { return false; }
            return Value.Equals(abstractProperty.Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
