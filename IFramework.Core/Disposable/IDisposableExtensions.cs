using System;
using IFramework.Core.Trigger;
using UnityEngine;

namespace IFramework.Core
{
    public static class IDisposableExtensions
    {
        /// <summary>
        /// 给继承IDisposable接口的对象 拓展相关Add方法
        /// </summary>
        public static void AddToDisposeList(this IDisposable self, IDisposableList disposableList)
        {
            disposableList.Add(self);
        }

        /// <summary>
        /// 与 GameObject 绑定销毁
        /// </summary>
        public static void DisposeWhenGameObjectDestroyed(this IDisposable self, GameObject gameObject)
        {
            gameObject.AddComponentSafe<OnDestroyDisposeTrigger>().AddDispose(self);
        }

        /// <summary>
        /// 与 GameObject 绑定销毁
        /// </summary>
        public static void DisposeWhenGameObjectDestroyed(this IDisposable self, Component component)
        {
            component.gameObject.AddComponentSafe<OnDestroyDisposeTrigger>().AddDispose(self);
        }

        /// <summary>
        /// 与 GameObject 绑定销毁
        /// </summary>
        public static void DisposeWhenGameObjectDisabled(this IDisposable self, GameObject gameObject)
        {
            gameObject.AddComponentSafe<OnDisableDisposeTrigger>().AddDispose(self);
        }

        /// <summary>
        /// 与 GameObject 绑定销毁
        /// </summary>
        public static void DisposeWhenGameObjectDisabled(this IDisposable self, Component component)
        {
            component.gameObject.AddComponentSafe<OnDisableDisposeTrigger>().AddDispose(self);
        }
    }
}
