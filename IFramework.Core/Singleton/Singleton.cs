namespace IFramework.Core
{
    /// <summary>
    /// 单例模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> : ISingleton where T : Singleton<T>
    {
        // 静态实例
        protected static volatile T instance;

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
        /// 单例初始化
        /// </summary>
        public virtual void OnInit() { }

        /// <summary>
        /// 资源释放
        /// </summary>
        public virtual void Dispose()
        {
            instance = null;
        }
    }
}
