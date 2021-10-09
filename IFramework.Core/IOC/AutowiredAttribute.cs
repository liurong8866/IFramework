using System;

namespace IFramework.Core
{
    /// <summary>
    /// 注入属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class AutowiredAttribute : Attribute
    {
        public string Name { get; set; }
        
        public AutowiredAttribute(){}
                
        public AutowiredAttribute(string name)
        {
            Name = name;
        }
    }
}
