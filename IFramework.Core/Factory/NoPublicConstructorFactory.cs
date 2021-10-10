using System;
using System.Reflection;

namespace IFramework.Core
{
    /// <summary>
    /// 没有公共构造函数的类实例工厂
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NoPublicConstructorFactory<T> : IFactory<T> where T : class
    {
        public T Create()
        {
            // 找到所有私有构造函数
            ConstructorInfo[] constructors = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            // 找到无参数的私有构造函数
            ConstructorInfo constructor = Array.Find(constructors, c => c.GetParameters().Length == 0);

            if (constructor == null) {
                throw new Exception("未找到无参私有构造函数: " + typeof(T));
            }
            return constructor.Invoke(null) as T;
        }
    }
}
