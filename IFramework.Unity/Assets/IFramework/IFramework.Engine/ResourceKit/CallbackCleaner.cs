using System;

namespace IFramework.Engine
{
    /// <summary>
    /// 用于清理回调事件的载体
    /// </summary>
    public class CallbackCleaner
    {
        private readonly Action<bool, IResource> callbacks;

        private readonly IResource resource;

        public CallbackCleaner(IResource resource, Action<bool, IResource> callback)
        {
            this.resource = resource;
            callbacks = callback;
        }

        /// <summary>
        /// 释放监听的事件
        /// </summary>
        public void Release()
        {
            resource.UnRegisterOnLoadedEvent(callbacks);
        }

        /// <summary>
        /// 判断是否是当前资源
        /// </summary>
        public bool Is(IResource resource)
        {
            return this.resource.AssetName == resource.AssetName;
        }
    }
}
