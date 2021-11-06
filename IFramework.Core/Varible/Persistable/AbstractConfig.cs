using System;

namespace IFramework.Core
{
    /// <summary>
    /// 可持久化的一般类型抽象类
    /// </summary>
    public abstract class AbstractConfig<T> : AbstractProperty<T>, IPersistable<T>, IBindable<T>
    {
        protected string key;
        
        // 绑定事件
        public Action<T> OnChange { get; set; }
        
        protected AbstractConfig(string key, T value)
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
                OnChange?.Invoke(value);
                setted = true;
            }
        }

        /// <summary>
        /// 获取值
        /// </summary>
        public abstract T Get();

        /// <summary>
        /// 保存值
        /// </summary>
        public abstract void Save(T value);

        /// <summary>
        /// 清除缓存
        /// </summary>
        public abstract void Clear();
        
        /// <summary>
        /// 注销事件
        /// </summary>
        public override void Dispose()
        {
            OnChange = null;
        }
    }
}
