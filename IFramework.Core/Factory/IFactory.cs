namespace IFramework.Core
{
    /// <summary>
    /// 对象工厂接口
    /// </summary>
    public interface IFactory<T>
    {
        /// <summary>
        /// 创建对象实例
        /// </summary>
        T Create();
    }
}
