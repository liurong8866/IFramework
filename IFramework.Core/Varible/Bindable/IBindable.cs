using System;

namespace IFramework.Core
{
    /// <summary>
    /// 可绑定事件的接口
    /// </summary>
    public interface IBindable<T>
    {
        Action<T> OnChange { get; set; }
    }
}
