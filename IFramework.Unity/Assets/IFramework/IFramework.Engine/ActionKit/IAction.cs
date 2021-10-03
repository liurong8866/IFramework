using System;
using System.Collections;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// 执行节点的基础接口
    /// </summary>
    public interface IAction : IDisposable, IResetable
    {
        /// <summary>
        /// 执行事件
        /// </summary>
        bool Execute();

        /// <summary>
        /// 结束
        /// </summary>
        void Finish();

        /// <summary>
        /// 是否结束
        /// </summary>
        bool Finished { get; }

        /// <summary>
        /// 是否释放
        /// </summary>
        bool Disposed { get; }
    }

    public static class IActionExtension
    {
        /// <summary>
        /// MonoBehaviour 的扩展方法
        /// </summary>
        public static T Execute<T>(this T self, IAction command) where T : MonoBehaviour
        {
            self.StartCoroutine(command.ExecuteAction());
            return self;
        }

        /// <summary>
        /// IAction 的扩展方法
        /// </summary>
        public static IEnumerator ExecuteAction(this IAction self)
        {
            if (self.Finished) self.Reset();

            while (!self.Execute()) {
                yield return null;
            }
        }
    }
}
