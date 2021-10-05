using System;
using IFramework.Core;

namespace IFramework.Engine
{
    /// <summary>
    /// 条件执行节点，直到条件满足后触发动作并结束
    /// </summary>
    public class UntilAction : AbstractAction, IPoolable
    {
        private Action action;
        private Func<bool> condition;

        public static UntilAction Allocate(Func<bool> condition, Action action = null)
        {
            UntilAction untilAction = ObjectPool<UntilAction>.Instance.Allocate();
            untilAction.condition = condition;
            untilAction.action = action;
            return untilAction;
        }

        protected override void OnExecute()
        {
            Finished = condition.Invoke();
            // 满足条件时，执行
            if (Finished) action.InvokeSafe();
        }

        protected override void OnDispose()
        {
            ObjectPool<UntilAction>.Instance.Recycle(this);
        }

        public void OnRecycled()
        {
            Reset();
            condition = null;
        }

        public bool IsRecycled { get; set; }
    }
}
