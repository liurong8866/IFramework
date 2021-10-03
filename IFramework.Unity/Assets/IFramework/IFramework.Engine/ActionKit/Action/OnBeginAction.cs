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
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// 开始时执行的动作
    /// </summary>
    public class OnBeginAction : AbstractAction, IPoolable, IRecyclable
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
            beginAction.InvokeSafe();
        }

        public void OnRecycled()
        {
            beginAction = null;
        }

        public bool IsRecycled { get; set; }

        public void Recycle()
        {
            ObjectPool<OnBeginAction>.Instance.Recycle(this);
        }
    }
}
