using System;

namespace IFramework.Core
{
    /// <summary>
    /// 消息注册类
    /// </summary>
    public interface ITypeEventRegister : IDisposable { }

    /// <summary>
    /// 消息注册类
    /// </summary>
    public class TypeEventRegister<T> : ITypeEventRegister
    {
        // 委托本身就可以一对多注册
        public Action<T> actions = obj => { };

        public void Dispose()
        {
            actions = null;
        }
    }
}
