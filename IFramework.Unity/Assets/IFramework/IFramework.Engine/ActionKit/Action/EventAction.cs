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
    /// 事件执行动作
    /// </summary>
    public class EventAction : AbstractAction, IPoolable
    {
        private Action onExecuteEvent;

        /// <summary>
        /// 从对象池中申请对象
        /// </summary>
        public static EventAction Allocate(params Action[] actions)
        {
            EventAction eventAction = ObjectPool<EventAction>.Instance.Allocate();

            //如果有多个事件，则循环添加
            foreach (Action action in actions) {
                eventAction.onExecuteEvent += action;
            }
            return eventAction;
        }

        /// <summary>
        /// 执行事件
        /// </summary>
        protected override void OnExecute()
        {
            onExecuteEvent.InvokeSafe();
            Finished = true;
        }

        protected override void OnDispose()
        {
            ObjectPool<EventAction>.Instance.Recycle(this);
        }

        public void OnRecycled()
        {
            Reset();
            onExecuteEvent = null;
        }

        public bool IsRecycled { get; set; }
    }
    
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class EventActionExtensions
    {
        /// <summary>
        /// 执行某事件
        /// </summary>
        public static void Action<T>(this T self,  Action action) where T : MonoBehaviour
        {
            self.Execute(EventAction.Allocate(action));
        }
    }
}
