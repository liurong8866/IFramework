using System;
using System.Collections;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// 执行节点到基础抽象类
    /// </summary>
    public abstract class AbstractAction : Disposable, IAction
    {
        protected bool onBeginCalled;

        public Action OnBeganCallback;
        public Action OnEndedCallback;
        public Action OnDisposedCallback;

        /// <summary>
        /// 执行事件
        /// </summary>
        public bool Execute()
        {
            // 有可能被别的地方调用
            if (Finished) {
                return Finished;
            }

            if (!onBeginCalled) {
                onBeginCalled = true;
                OnBegin();
                OnBeganCallback.InvokeSafe();
            }

            if (!Finished) {
                OnExecute();
            }

            if (Finished) {
                OnEndedCallback.InvokeSafe();
                OnEnd();
            }
            return Finished || disposed;
        }

        /// <summary>
        /// 执行事件直到结束
        /// </summary>
        public T Execute<T>(T mono) where T : MonoBehaviour
        {
            mono.StartCoroutine(ExecuteAsync());
            return mono;
        }

        /// <summary>
        /// IAction 的扩展方法
        /// </summary>
        private IEnumerator ExecuteAsync()
        {
            if (Finished) Reset();

            while (!Execute()) {
                yield return null;
            }
        }

        /// <summary>
        /// 结束
        /// </summary>
        public virtual void Finish()
        {
            Finished = true;
        }

        /// <summary>
        /// 是否结束
        /// </summary>
        public bool Finished { get; protected set; }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            Finished = false;
            onBeginCalled = false;
            disposed = false;
            OnReset();
        }

        /// <summary>
        /// 托管资源：由CLR管理分配和释放的资源，即由CLR里new出来的对象；
        /// </summary>
        protected override void DisposeManaged()
        {
            OnBeganCallback = null;
            OnEndedCallback = null;
            OnDisposedCallback.InvokeSafe();
            OnDisposedCallback = null;
            OnDispose();
        }

        /*----------------------------- 虚拟方法 -----------------------------*/

        protected virtual void OnReset() { }

        protected virtual void OnBegin() { }

        protected virtual void OnExecute() { }

        protected virtual void OnEnd() { }

        protected virtual void OnDispose() { }
    }
}
