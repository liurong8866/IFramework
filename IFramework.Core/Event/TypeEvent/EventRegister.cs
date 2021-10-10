using System;

namespace IFramework.Core
{
    /// <summary>
    /// 消息注册类
    /// </summary>
    public interface IEventRegister : IDisposable { }

    /// <summary>
    /// 消息注册类
    /// </summary>
    public class EventRegister<T> : IEventRegister
    {
        // 委托本身就可以一对多注册
        public Action<T> actions = obj => { };

        public void Dispose()
        {
            actions = null;
        }
    }
}
