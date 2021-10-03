/*****************************************************************************
 * MIT License
 * 
 * Copyright (c) 2021 liurong
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *****************************************************************************/

using System;
using IFramework.Core;

namespace IFramework.Engine
{
    /// <summary>
    /// 执行节点的基础抽象类
    /// </summary>
    public abstract class AbstractAction : Disposeble, IAction
    {
        public Action OnBeganCallback = null;
        public Action OnEndedCallback = null;
        public Action OnDisposedCallback = null;

        protected bool onBeginCalled = false;

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
        /// 结束
        /// </summary>
        public virtual void Finish() => Finished = true;

        /// <summary>
        /// 是否结束
        /// </summary>
        public bool Finished { get; protected set; }

        /// <summary>
        /// 是否释放
        /// </summary>
        public bool Disposed => disposed;

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
