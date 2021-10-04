using System;

namespace IFramework.Core
{
    /// <summary>
    /// 可绑定事件的数字类型接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BindableNumeric<T> : AbstractPropertyNumeric<T>, IBindable<T> where T : struct, IConvertible, IComparable
    {
        protected BindableNumeric() { }

        protected BindableNumeric(T value)
        {
            this.value = value;
        }

        // 绑定事件
        public Action<T> OnChange { get; set; }

        protected override T GetValue()
        {
            return value;
        }

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
        public override void Dispose()
        {
            OnChange = null;
        }
    }
}
