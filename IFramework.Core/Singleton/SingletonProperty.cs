namespace IFramework.Core
{
    /// <summary>
    /// 属性单例类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class SingletonProperty<T> where T : class, ISingleton
    {
        // 静态实例
        private static volatile T instance;

        // 对象锁
        // ReSharper disable once StaticMemberInGenericType
        private static readonly object locker = new object();

        /// <summary>
        /// 双重锁，线程安全
        /// </summary>
        public static T Instance {
            get {
                if (instance == null) {
                    lock (locker) {
                        if (instance == null) {
                            instance = SingletonCreator.CreateSingleton<T>();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 资源释放
        /// </summary>
        public static void Dispose()
        {
            instance = null;
        }
    }
}
