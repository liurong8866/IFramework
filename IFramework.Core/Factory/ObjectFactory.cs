using System;
using System.Reflection;

namespace IFramework.Core
{
    public class ObjectFactory
    {
        /// <summary>
        /// 无参数构造函数类实例
        /// </summary>
        /// <returns></returns>
        public static T Create<T>() where T : class { return Activator.CreateInstance(typeof(T)) as T; }

        /// <summary>
        /// 有参数构造函数类实例
        /// </summary>
        /// <param name="args">需要实例化的类构造函数的参数，根据参数不同调用不同构造函数</param>
        /// <typeparam name="T">要实例化的类</typeparam>
        /// <returns></returns>
        public static T Create<T>(params object[] args) where T : class { return Activator.CreateInstance(typeof(T), args) as T; }

        /// <summary>
        /// 动态创建类的实例：创建无参/私有的构造函数  泛型扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateNoPublicConstructor<T>() where T : class
        {
            // 获取私有构造函数
            ConstructorInfo[] constructorInfos = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            // 获取无参构造函数
            ConstructorInfo ctor = Array.Find(constructorInfos, c => c.GetParameters().Length == 0);

            if (ctor == null) {
                throw new Exception("未找到无参私有构造函数: " + typeof(T));
            }
            return ctor.Invoke(null) as T;
        }
    }
}
