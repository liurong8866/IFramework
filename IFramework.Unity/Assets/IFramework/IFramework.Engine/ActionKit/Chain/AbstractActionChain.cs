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
        // TODO 观察注释后有无问题
        // private bool disposeWhenCondition;
        private Func<bool> disposeCondition;
        private Action onDisposedEvent;

        /// <summary>
        /// 事件执行器
        /// </summary>
        public MonoBehaviour Executer { get; set; }

        /// <summary>
        /// 当前节点
        /// </summary>
        protected abstract AbstractAction Node { get; }

        /// <summary>
        /// 添加事件节点
        /// </summary>
        public abstract IActionChain Append(IAction node);

        /// <summary>
        /// 执行事件
        /// </summary>
        protected override void OnExecute()
        {
            // if (disposeWhenCondition && disposeCondition != null && disposeCondition.Invoke()) {
            if (disposeCondition != null && disposeCondition.Invoke()) { Finish(); }
            else { Finished = Node.Execute(); }
        }

        /// <summary>
        /// 开始执行
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
            OnEndEvent += action;
            return this;
        }

        public IDisposeEventRegister DisposeWhen(Func<bool> condition)
        {
            // disposeWhenCondition = true;
            disposeCondition = condition;
            return this;
        }

        protected override void OnDispose()
        {
            Executer = null;
            // disposeWhenCondition = false;
            disposeCondition = null;
            onDisposedEvent.InvokeSafe();
            onDisposedEvent = null;
        }
    }
}
