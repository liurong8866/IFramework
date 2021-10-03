using System;

namespace IFramework.Core
{
    /// <summary>
    /// 可绑定事件的一般类型接口
    /// </summary>
    public class Bindable<T> : AbstractProperty<T>, IBindable<T> where T : IConvertible, IComparable
    {
        public Bindable() { }

        public Bindable(T value) : base(value) { }

        // 绑定事件
        public Action<T> OnChange { get; set; }

        protected override T GetValue() { return value; }

        protected override void SetValue(T value)
        {
            if (IsValueChanged(value)) {
                this.value = value;
                OnChange?.Invoke(value);
                setted = true;
            }
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        public override void Dispose() { OnChange = null; }
    }
}
