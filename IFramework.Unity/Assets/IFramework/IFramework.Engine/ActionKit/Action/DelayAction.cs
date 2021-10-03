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
    /// 延时执行动作
    /// </summary>
    [Serializable]
    public class DelayAction : AbstractAction, IPoolable
    {
        // 延迟时间
        [SerializeField] public float DelayTime;
        // 延迟事件
        public Action OnDelayFinish { get; set; }
        // 时间计数器
        private float currentSeconds;

        /// <summary>
        /// 从缓存池中申请对象
        /// </summary>
        public static DelayAction Allocate(float delayTime, Action action = null)
        {
            DelayAction delayAction = ObjectPool<DelayAction>.Instance.Allocate();
            delayAction.DelayTime = delayTime;
            delayAction.OnDelayFinish = action;
            return delayAction;
        }

        /// <summary>
        /// 执行事件
        /// </summary>
        protected override void OnExecute()
        {
            base.OnExecute();
            OnExecute(Time.deltaTime);
        }

        protected void OnExecute(float delta)
        {
            // 判断是否超时，过了时间视为延迟结束
            currentSeconds += delta;
            Finished = currentSeconds >= DelayTime;

            if (Finished) {
                OnDelayFinish.InvokeSafe();
            }
        }

        protected override void OnReset()
        {
            currentSeconds = 0.0f;
        }

        protected override void OnDispose()
        {
            ObjectPool<DelayAction>.Instance.Recycle(this);
        }

        public void OnRecycled()
        {
            OnDelayFinish = null;
            DelayTime = 0.0f;
            Reset();
        }

        public bool IsRecycled { get; set; }
    }

    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class DelayActionExtensions
    {
        /// <summary>
        /// 延迟N秒
        /// </summary>
        public static IActionChain Delay(this IActionChain self, float seconds)
        {
            return self.Append(DelayAction.Allocate(seconds));
        }
        
        /// <summary>
        /// 延迟N秒执行某事件
        /// </summary>
        public static void Delay<T>(this T self, float seconds, Action action) where T : MonoBehaviour
        {
            self.Execute(DelayAction.Allocate(seconds, action));
        }
    }
}
