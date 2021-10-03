using System;
using System.Collections.Generic;

namespace IFramework.Core
{
    /// <summary>
    /// 安全的计数器
    /// </summary>
    public class SafeCountor : ICountor, IDisposable
    {
        public SafeCountor() { }

        public SafeCountor(Action action) { OnZero = action; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Counter => Owners.Count;

        /// <summary>
        /// 为 0 事件
        /// </summary>
        public Action OnZero { get; set; }

        public HashSet<object> Owners { get; } = new HashSet<object>();

        /// <summary>
        /// 记录
        /// </summary>
        public bool Hold(object owner)
        {
            if (!Owners.Add(owner)) {
                Log.Warning("对象已经被记录");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public bool UnHold(object owner)
        {
            if (!Owners.Remove(owner)) {
                Log.Warning("没有找到要释放的对象");
                return false;
            }

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
            Owners.Clear();

            if (invokeAction) {
                OnZero.InvokeSafe();
            }
        }

        public virtual void Dispose() { OnZero = null; }
    }
}
