using System;
using IFramework.Core;

namespace IFramework.Engine
{
    /// <summary>
    /// 开始时动作节点
    /// </summary>
    public class OnBeginAction : AbstractAction, IPoolable
    {
        private Action<OnBeginAction> beginAction;

        public static OnBeginAction Allocate(Action<OnBeginAction> action)
        {
            OnBeginAction onBeginAction = ObjectPool<OnBeginAction>.Instance.Allocate();
            onBeginAction.beginAction = action;
            return onBeginAction;
        }

        protected override void OnBegin()
        {
            beginAction.InvokeSafe(this);
        }

        public bool IsRecycled { get; set; }

        protected override void OnDispose()
        {
            ObjectPool<OnBeginAction>.Instance.Recycle(this);
        }

        public void OnRecycled()
        {
            Reset();
            beginAction = null;
        }
    }
}
