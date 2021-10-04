using System;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// 链式调用接口
    /// </summary>
    public interface IActionChain : IAction
    {
        MonoBehaviour Executer { get; set; }

        IActionChain Append(IAction node);

        IDisposeWhen Begin();
    }

    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class IActionChainExtention
    {
        public static IActionChain Wait(this IActionChain self, float seconds)
        {
            return self.Append(DelayAction.Allocate(seconds));
        }

        public static IActionChain Event(this IActionChain self, params Action[] events)
        {
            return self.Append(EventAction.Allocate(events));
        }

        public static IActionChain OnBegin(this IActionChain self, Action<OnBeginAction> action)
        {
            return self.Append(OnBeginAction.Allocate(action));
        }

        public static IActionChain Sequence<T>(this T self) where T : MonoBehaviour
        {
            SequenceNodeChain chain = new SequenceNodeChain { Executer = self };
            chain.DisposeWhenGameObjectDestroyed(self);
            return chain;
        }

        public static IActionChain Repeat<T>(this T self, int count = -1) where T : MonoBehaviour
        {
            RepeatNodeChain retNodeChain = new RepeatNodeChain(count) { Executer = self };
            retNodeChain.DisposeWhenGameObjectDestroyed(self);
            return retNodeChain;
        }
    }
}
