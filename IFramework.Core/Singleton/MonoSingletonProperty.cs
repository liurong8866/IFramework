using UnityEngine;

namespace IFramework.Core
{
    /// <summary>
    /// Mono属性单例，支持继承类的单例实例化
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class MonoSingletonProperty<T> where T : MonoBehaviour, ISingleton
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
                            instance = SingletonCreator.CreateMonoSingleton<T>();
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
            if (instance != null) {
                Object.Destroy(instance.gameObject);
                instance = null;
            }
        }
    }
}
