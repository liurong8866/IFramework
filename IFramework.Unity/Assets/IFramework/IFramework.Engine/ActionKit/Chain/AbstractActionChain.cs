using System;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// 链式调用抽象类
    /// </summary>
    public abstract class AbstractActionChain : AbstractAction, IActionChain, IDisposeWhen
    {
        private bool disposeWhenCondition = false;
        private Func<bool> disposeCondition = null;
        private Action onDisposedEvent = null;

        /// <summary>
        /// 事件执行器
        /// </summary>
        public MonoBehaviour Executer { get; set; }

        /// <summary>
        /// 当前节点
        /// </summary>
        protected abstract AbstractAction Node { get; }

        public abstract IActionChain Append(IAction node);

        /// <summary>
        /// 执行事件
        /// </summary>
        protected override void OnExecute()
        {
            if (disposeWhenCondition && disposeCondition != null && disposeCondition.Invoke()) {
                Finish();
            }
            else {
                Finished = Node.Execute();
            }
        }

        /// <summary>
        /// 开始执行事件链
        /// </summary>
        public IDisposeWhen Begin()
        {
            Executer.Execute(this);
            return this;
        }

        protected override void OnEnd()
        {
            base.OnEnd();
            Dispose();
        }

        public void OnDisposed(Action action)
        {
            onDisposedEvent = action;
        }

        public IDisposeEventRegister OnFinished(Action action)
        {
            OnEndedCallback += action;
            return this;
        }

        public IDisposeEventRegister DisposeWhen(Func<bool> condition)
        {
            disposeWhenCondition = true;
            disposeCondition = condition;
            return this;
        }

        protected override void OnDispose()
        {
            Executer = null;
            disposeWhenCondition = false;
            disposeCondition = null;
            onDisposedEvent.InvokeSafe();
            onDisposedEvent = null;
        }
    }
}
