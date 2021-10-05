using System;
using IFramework.Core;
using JetBrains.Annotations;
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// 事件动作节点
    /// </summary>
    public class EventAction : AbstractAction, IPoolable
    {
        private Action action;

        /// <summary>
        /// 从对象池中申请对象
        /// </summary>
        public static EventAction Allocate(params Action[] actions)
        {
            EventAction eventAction = ObjectPool<EventAction>.Instance.Allocate();
            //如果有多个事件，则循环添加
            Array.ForEach(actions, action => { eventAction.action += action; });
            return eventAction;
        }

        /// <summary>
        /// 执行事件
        /// </summary>
        protected override void OnExecute()
        {
            action.InvokeSafe();
            Finished = true;
        }

        protected override void OnDispose()
        {
            ObjectPool<EventAction>.Instance.Recycle(this);
        }

        public void OnRecycled()
        {
            Reset();
            action = null;
        }

        public bool IsRecycled { get; set; }
    }

    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class EventActionExtensions
    {
        /// <summary>
        /// 执行某事件
        /// </summary>
        public static void Action<T>(this T self, [NotNull] params Action[] actions) where T : MonoBehaviour
        {
            self.Execute(EventAction.Allocate(actions));
        }
    }
}
