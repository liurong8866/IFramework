using System;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class IActionChainExtention
    {
        /// <summary>
        /// 序列链表
        /// </summary>
        public static IActionChain Sequence<T>(this T self) where T : MonoBehaviour
        {
            SequenceNodeChain chain = new SequenceNodeChain { Executer = self };
            chain.DisposeWhenGameObjectDestroyed(self);
            return chain;
        }

        /// <summary>
        /// 循环链表
        /// </summary>
        public static IActionChain Repeat<T>(this T self, int count = -1) where T : MonoBehaviour
        {
            RepeatNodeChain chain = new RepeatNodeChain(count) { Executer = self };
            chain.DisposeWhenGameObjectDestroyed(self);
            return chain;
        }

        /// <summary>
        /// 延迟N秒
        /// </summary>
        public static IActionChain Delay(this IActionChain self, float seconds)
        {
            return self.Append(DelayAction.Allocate(seconds));
        }

        /// <summary>
        /// 延迟N帧
        /// </summary>
        public static IActionChain DelayFrame(this IActionChain self, int frameCount)
        {
            return self.Append(DelayFrameAction.Allocate(frameCount));
        }

        /// <summary>
        /// 延迟一帧
        /// </summary>
        public static IActionChain NextFrame(this IActionChain self)
        {
            return self.Append(DelayFrameAction.Allocate(1));
        }

        /// <summary>
        /// 事件
        /// </summary>
        public static IActionChain Event(this IActionChain self, params Action[] events)
        {
            return self.Append(EventAction.Allocate(events));
        }

        /// <summary>
        /// 开始事件
        /// </summary>
        public static IActionChain OnBegin(this IActionChain self, Action<OnBeginAction> action)
        {
            return self.Append(OnBeginAction.Allocate(action));
        }
    }
}
