namespace IFramework.Core
{
    /// <summary>
    /// 有无参数构造函数的类实例工厂
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DefaultFactory<T> : IFactory<T> where T : class, new()
    {
        public T Create() { return new T(); }
    }
}
