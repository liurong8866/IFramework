using System;
using System.Collections;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// 执行节点的基础接口
    /// </summary>
    public interface IAction : IResetable, IDisposable
    {
        /// <summary>
        /// 执行事件
        /// </summary>
        bool Execute();

        /// <summary>
        /// 执行事件直到结束
        /// </summary>
        T Execute<T>(T mono) where T : MonoBehaviour;
        
        /// <summary>
        /// 是否结束
        /// </summary>
        bool Finished { get; }
    }

    public static class IActionExtension
    {
        /// <summary>
        /// MonoBehaviour 的扩展方法
        /// </summary>
        public static T Execute<T>(this T self, IAction action) where T : MonoBehaviour
        {
            action.Execute(self);
            return self;
        }
    }
}
