using System;
using System.Collections;

namespace IFramework.Engine
{
    /// <summary>
    /// 资源异步加载任务接口
    /// </summary>
    public interface IResourceLoadTask
    {
        /// <summary>
        /// 资源异步加载方法调用
        /// </summary>
        /// <param name="callback">异步加载完毕后，通知主线程回调</param>
        /// <returns></returns>
        IEnumerator LoadAsync(Action callback);
    }
}
