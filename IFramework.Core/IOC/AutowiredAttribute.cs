using System;

namespace IFramework.Core
{
    /// <summary>
    /// 注入属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class AutowiredAttribute : Attribute
    {
        /// <summary>
        /// 指定注入的实现类名称
        /// </summary>
        public string Name { get; set; }

        public AutowiredAttribute() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">指定注入的实现类名称</param>
        public AutowiredAttribute(string name)
        {
            Name = name;
        }
    }
}
