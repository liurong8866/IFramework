namespace IFramework.Core
{
    /// <summary>
    /// 可持久化的一般类型抽象类
    /// </summary>
    public abstract class AbstractConfig<T> : AbstractProperty<T>, IPersistable<T>
    {
        protected string key;

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
                // this.value = value; 
                Save(value);
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
    }
}
