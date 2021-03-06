using System;

namespace IFramework.Core
{
    /// <summary>
    /// TypeEvent接口
    /// </summary>
    public interface ITypeEvent : IDisposable
    {
        /// <summary>
        /// 注册事件
        /// </summary>
        IDisposable RegisterEvent<T>(Action<T> action);

        /// <summary>
        /// 注销某类型的某事件
        /// </summary>
        void UnRegisterEvent<T>(Action<T> action);

        /// <summary>
        /// 注销某类型事件
        /// </summary>
        void UnRegisterEvent<T>();
        
        /// <summary>
        /// 发送事件
        /// </summary>
        void SendEvent<T>() where T : new();

        /// <summary>
        /// 发送事件
        /// </summary>
        void SendEvent<T>(T param);

        /// <summary>
        /// 清空事件
        /// </summary>
        void ClearEvent();
    }
}
