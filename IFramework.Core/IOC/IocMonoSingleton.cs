using System;
using UnityEngine;

namespace IFramework.Core
{
    public abstract class IocMonoSingleton<T> : IocMonoBehaviour, ISingleton where T : IocMonoSingleton<T>
    {
        // 静态实例
        protected static volatile T instance;

        // 对象锁
        private static readonly object locker = new object();

        protected static bool isApplicationQuit;

        /// <summary>
        /// 单例构造器
        /// </summary>
        protected IocMonoSingleton() { }
        
        /// <summary>
        /// 获得实例
        /// </summary>
        public static T Instance {
            get {
                if (instance == null && !isApplicationQuit) {
                    lock (locker) {
                        if (instance == null && !isApplicationQuit) { instance = SingletonCreator.CreateMonoSingleton<T>(); }
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
        protected virtual void OnDestroy()
        {
            instance = null;
        }

        /// <summary>
        /// 判断当前应用程序是否退出
        /// </summary>
        public static bool IsApplicationQuit => isApplicationQuit;
    }
}
