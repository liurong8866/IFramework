using System;

namespace IFramework.Core
{
    /// <summary>
    /// MonoBehaviour单例特性，标记在需要单例的MonoBehaviour的类上
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MonoSingletonAttribute : Attribute
    {
        // 在Hierarchy中的全路径

        // 标记不销毁

        /// <summary>
        /// 单例特性构造函数
        /// </summary>
        /// <param name="pathInHierarchy">需要附加的GameObject在Hierarchy中的全路径</param>
        public MonoSingletonAttribute(string pathInHierarchy) { PathInHierarchy = pathInHierarchy; }

        // 获取Hierarchy中的全路径
        public string PathInHierarchy { get; }

        // 标记不销毁
        public bool DontDestroy { get; set; } = true;
    }
}
