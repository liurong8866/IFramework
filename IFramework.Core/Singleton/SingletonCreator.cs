using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IFramework.Core
{
    /// <summary>
    /// 单例实例创建类
    /// </summary>
    public static class SingletonCreator
    {
        /// <summary>
        /// 通过反射创建普通类实例
        /// </summary>
        public static T CreateSingleton<T>() where T : class, ISingleton
        {
            // bool res = {TypeA}.IsAssignableFrom({TypeB}) ;
            // 如果TypeA和TypeB类型一样则返回true；
            // 如果TypeA是TypeB的父类则返回true;
            // 如果TypeB实现了接口TypeA则返回true;

            // 如果是不是MonoBehaviour类型的类，则为通用实例方法
            if (!typeof(MonoBehaviour).IsAssignableFrom(typeof(T))) {
                T instance = ObjectFactory.CreateNoPublicConstructor<T>();
                instance.OnInit();
                return instance;
            }
            // 使用MonoBehaviour实例化方法
            return CreateMonoSingleton<T>();
        }

        /// <summary>
        /// 通过反射创建MonoBehaviour类实例
        /// </summary>
        public static T CreateMonoSingleton<T>() where T : class, ISingleton
        {
            T instance = null;
            Type type = typeof(T);

            // 判断T实例存在的条件是否满足
            if (!Application.isPlaying) return null;

            // 1、判断当前场景中是否存在T实例，有则返回
            instance = Object.FindObjectOfType(type) as T;

            if (instance != null) {
                instance.OnInit();
                return instance;
            }

            // 2、判断是否为自定义单例特性，获取T类型 自定义属性，并找到相关路径属性，利用该属性创建T实例
            object[] customAttributes = type.GetCustomAttributes(true);

            foreach (object customAttribute in customAttributes) {
                if (customAttribute is MonoSingletonAttribute custom) {
                    instance = AttachComponent<T>(custom.PathInHierarchy, custom.DontDestroy);
                    break;
                }
            }

            // 3、如果还是无法找到instance  则主动去创建同名Obj 并挂载相关脚本 组件
            if (instance == null) {
                GameObject gameObject = new GameObject(typeof(T).Name);
                instance = gameObject.AddComponent(typeof(T)) as T;
            }

            // 调用初始化方法
            instance?.OnInit();
            return instance;
        }

        /// <summary>
        /// 在GameObject上附加T组件（脚本）
        /// </summary>
        /// <param name="path">组件路径（应该就是Hierarchy下的树结构路径）</param>
        /// <param name="dontDestroy">不要销毁 标签</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private static T AttachComponent<T>(string path, bool dontDestroy) where T : class
        {
            //找到需要附加的GameObject
            GameObject gameObject = FindGameObject(path, dontDestroy);

            // 如果没有找到路径的GameObject，那么就建一个
            if (gameObject == null) {
                gameObject = new GameObject(typeof(T).Name + "[Singleton]");

                if (dontDestroy) {
                    Object.DontDestroyOnLoad(gameObject);
                }
            }

            // 附加组件并返回
            return gameObject.AddComponent(typeof(T)) as T;
        }

        /// <summary>
        /// 根据路径查找GameObject
        /// </summary>
        /// <param name="path">组件路径（应该就是Hierarchy下的树结构路径）</param>
        /// <param name="dontDestroy">不要销毁 标签</param>
        /// <returns></returns>
        private static GameObject FindGameObject(string path, bool dontDestroy)
        {
            // 如果路径为空，返回NULL
            if (string.IsNullOrEmpty(path)) return null;

            string[] subPath = path.Split('/');
            if (subPath != null && subPath.Length == 0) return null;

            return FindGameObject(null, subPath, 0, true, dontDestroy);
        }

        /// <summary>
        /// 查找Obj（一个递归查找Obj的过程）
        /// </summary>
        /// <param name="root">父节点</param>
        /// <param name="subPath">拆分后的路径节点</param>
        /// <param name="index">子路径数组索引</param>
        /// <param name="build">true</param>
        /// <param name="dontDestroy">不要销毁 标签</param>
        /// <returns></returns>
        private static GameObject FindGameObject(GameObject root, string[] subPath, int index, bool build, bool dontDestroy)
        {
            GameObject client = null;

            // 如果没有父节点，说明是路径第一个节点，直接查找自路径
            if (root == null) {
                client = GameObject.Find(subPath[index]);

                // 在根节点标记 DontDestroy 标记
                if (client != null && dontDestroy) {
                    Object.DontDestroyOnLoad(client);
                }
            }
            // 如果是二级、三级等子路径，则在父路径下查找
            else {
                Transform childe = root.transform.Find(subPath[index]);

                if (childe != null) {
                    client = childe.gameObject;
                }
            }

            // 如果未找到节点，并且系统需要自动创建，则自动创建该节点
            if (client == null) {
                if (build) {
                    client = new GameObject(subPath[index]);

                    // 如果是二级路径，则把创建好等节点添加到其父节点上，也就是逐层创建GameObject
                    if (root != null) {
                        client.transform.SetParent(root.transform);
                    }

                    // 在根节点标记 DontDestroy 标记
                    if (dontDestroy && index == 0) {
                        Object.DontDestroyOnLoad(client);
                    }
                }
            }

            // 如果client为空，或者已到达数组末尾，则返回
            if (client == null || index == subPath.Length - 1) {
                return client;
            }

            // 递归调用，当前节点作为父节点，查询
            return FindGameObject(client, subPath, ++index, build, dontDestroy);
        }
    }
}
