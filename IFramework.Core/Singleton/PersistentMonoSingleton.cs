using UnityEngine;

namespace IFramework.Core
{
    /// <summary>
    /// 如果跳转到新的场景里已经有了实例，则不创建新的单例（或者创建新的单例后会销毁掉新的单例）
    /// </summary>
    public class PersistentMonoSingleton<T> : MonoBehaviour where T : Component
    {
        // 实例对象
        protected static volatile T instance;
        // 对象锁
        private static readonly object locker = new object();

        /// <summary>
        /// 获取实例
        /// </summary>
        public static T Instance {
            get {
                if (instance == null) {
                    lock (locker) {
                        if (instance == null) {
                            // 查找当前类实例
                            instance = FindObjectOfType<T>();

                            // 如果最未找到，再添加
                            if (instance == null) { instance = new GameObject().AddComponent<T>(); }
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 在Awake时检查对象，如果存在则销毁
        /// </summary>
        protected virtual void Awake()
        {
            if (!Application.isPlaying) return;

            //如果当前对象是第一个，则设置为单例实例
            if (instance == null) {
                instance = this as T;
                DontDestroyOnLoad(transform.gameObject);
            }
            else {
                // 如果当前对象不为空，并且不是自己则销毁
                if (this != instance) { Destroy(gameObject); }
            }
        }
    }
}
