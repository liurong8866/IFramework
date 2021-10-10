using UnityEngine;

namespace IFramework.Core
{
    /// <summary>
    /// 如果跳转到新的场景里已经有了实例，则销毁并创建新的单例
    /// </summary>
    public class ReplaceableMonoSingleton<T> : MonoBehaviour where T : Component
    {
        // 实例对象
        protected static volatile T instance;
        // 对象锁
        private static readonly object locker = new object();
        // 记录初始化时间
        public float InitializationTime;

        /// <summary>
        /// 单例构造器
        /// </summary>
        protected ReplaceableMonoSingleton() { }

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
                            if (instance == null) {
                                GameObject obj = new GameObject();
                                //整个对象的inspector面板在运行时都不可编辑
                                // test.hideFlags = HideFlags.NotEditable;
                                //对象上的某个属性在运行时不可编辑
                                // test.GetComponent<Transform>().hideFlags =HideFlags.NotEditable;
                                //运行时该对象不会出现在hierarchy面板上，但是scene视图和game视图上还能看到
                                // test.hideFlags = HideFlags.HideInHierarchy;
                                //在运行时该对象的inspector面板属性不可见
                                // test.hideFlags = HideFlags.HideInInspector;
                                //在运行时该对象在inspector的某个属性不可见
                                //test.GetComponent<Transform>().hideFlags =HideFlags.HideInInspector;
                                // 在运行时该对象在inspector的属性不可见且不保存
                                obj.hideFlags = HideFlags.HideAndDontSave;
                                instance = obj.AddComponent<T>();
                                ;
                            }
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

            // 记录初始化时间
            InitializationTime = Time.time;
            DontDestroyOnLoad(gameObject);
            T[] list = FindObjectsOfType<T>();

            foreach (T obj in list) {
                if (obj == this) continue;

                // 删除初始化之前创建的实例
                if (obj.GetComponent<ReplaceableMonoSingleton<T>>().InitializationTime < InitializationTime) {
                    // 销毁对象
                    Destroy(obj.gameObject);
                }
            }

            //如果当前对象是第一个，则设置为单例实例
            if (instance == null) {
                instance = this as T;
            }
        }
    }
}
