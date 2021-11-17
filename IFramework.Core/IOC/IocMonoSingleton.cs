using System;

namespace IFramework.Core
{
    /// <summary>
    /// IOC 组件单例模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class IocMonoSingleton<T> : IocMonoBehaviour, ISingleton where T : IocMonoSingleton<T>
    {
        // 静态实例
        protected static volatile T instance;

        // 对象锁
        private static readonly object locker = new object();

        protected static bool isApplicationQuit;

        /// <summary>
        /// 获得实例
        /// </summary>
        public static T Instance {
            get {
                if (instance == null && !isApplicationQuit) {
                    lock (locker) {
                        if (instance == null && !isApplicationQuit) {
                            instance = SingletonCreator.CreateMonoSingleton<T>();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 单例初始化
        /// </summary>
        void ISingleton.OnInit() { }

        /// <summary>
        /// 应用程序退出：释放当前对象并销毁相关GameObject
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            isApplicationQuit = true;
            if (instance == null) return;
            Destroy(instance.gameObject);
            instance = null;
        }
        
        /// <summary>
        /// 释放当前对象
        /// </summary>
        protected override void OnDestroy()
        {
            base.OnDestroy();
            instance = null;
        }

        /// <summary>
        /// 判断当前应用程序是否退出
        /// </summary>
        public static bool IsApplicationQuit => isApplicationQuit;
    }
}
