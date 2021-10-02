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

namespace IFramework.Core
{
    /// <summary>
    /// 简单当计数器
    /// </summary>
    public class Countor : ICountor, IDisposable
    {
        public Countor()
        {
            Counter = 0;
        }

        public Countor(Action action)
        {
            Counter = 0;
            OnZero = action;
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int Counter { get; private set; } = 0;

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

        public virtual void Dispose()
        {
            OnZero = null;
        }

        public override string ToString()
        {
            return Counter.ToString();
        }
    }
}
