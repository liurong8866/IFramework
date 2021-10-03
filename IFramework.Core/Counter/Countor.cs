using System;

namespace IFramework.Core
{
    /// <summary>
    /// 简单当计数器
    /// </summary>
    public class Countor : ICountor, IDisposable
    {
        public Countor() { Counter = 0; }

        public Countor(Action action)
        {
            Counter = 0;
            OnZero = action;
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int Counter { get; private set; }

        /// <summary>
        /// 为 0 事件
        /// </summary>
        public Action OnZero { get; set; }

        /// <summary>
        /// 记录
        /// </summary>
        public bool Hold(object owner = null)
        {
            Counter++;
            return true;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public bool UnHold(object owner = null)
        {
            Counter--;

            if (Counter == 0) {
                OnZero.InvokeSafe();
            }
            return true;
        }

        /// <summary>
        /// 重置为0
        /// </summary>
        /// <param name="invokeAction">是否唤醒OnZero事件</param>
        public void Reset(bool invokeAction = false)
        {
            Counter = 0;

            if (invokeAction) {
                OnZero.InvokeSafe();
            }
        }

        public virtual void Dispose() { OnZero = null; }

        public override string ToString() { return Counter.ToString(); }
    }
}
