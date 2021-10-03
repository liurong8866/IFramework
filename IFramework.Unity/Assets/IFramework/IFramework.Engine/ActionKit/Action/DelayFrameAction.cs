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
    /// 延迟帧动作
    /// </summary>
    [Serializable]
    public class DelayFrameAction : AbstractAction, IPoolable, IResetable
    {
        // 延迟帧数
        [SerializeField] public int FrameCount;
        // 延迟事件
        public Action OnDelayFrameFinish { get; set; }
        // 帧数计数器
        private int startFrame;

        /// <summary>
        /// 从对象池中申请对象
        /// </summary>
        public static DelayFrameAction Allocate(int frameCount, Action acton = null)
        {
            DelayFrameAction delayFrameAction = ObjectPool<DelayFrameAction>.Instance.Allocate();
            delayFrameAction.FrameCount = frameCount;
            delayFrameAction.OnDelayFrameFinish = acton;
            return delayFrameAction;
        }

        /// <summary>
        /// 开始事件
        /// </summary>
        protected override void OnBegin()
        {
            base.OnBegin();
            // 记录当前帧
            startFrame = Time.frameCount;
        }

        /// <summary>
        /// 执行事件
        /// </summary>
        protected override void OnExecute()
        {
            Finished = Time.frameCount - startFrame >= FrameCount;

            if (Finished) {
                OnDelayFrameFinish.InvokeSafe();
            }
        }

        protected override void OnReset()
        {
            base.OnReset();
            startFrame = Time.frameCount;
        }

        protected override void OnDispose()
        {
            ObjectPool<DelayFrameAction>.Instance.Recycle(this);
        }

        public void OnRecycled()
        {
            OnDelayFrameFinish = null;
            FrameCount = 0;
            Reset();
        }

        public bool IsRecycled { get; set; }
    }

    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class DelayFrameActionExtensions
    {
        /// <summary>
        /// 延迟N帧
        /// </summary>
        public static IActionChain DelayFrame(this IActionChain self, int frameCount)
        {
            return self.Append(DelayFrameAction.Allocate(frameCount));
        }

        /// <summary>
        /// 延迟N帧执行某事件
        /// </summary>
        public static void DelayFrame<T>(this T self, int frameCount, Action action) where T : MonoBehaviour
        {
            self.Execute(DelayFrameAction.Allocate(frameCount, action));
        }

        /// <summary>
        /// 延迟一帧
        /// </summary>
        public static IActionChain NextFrame(this IActionChain self)
        {
            return self.Append(DelayFrameAction.Allocate(1));
        }

        /// <summary>
        /// 下一帧执行某事件
        /// </summary>
        public static void NextFrame<T>(this T self, Action action) where T : MonoBehaviour
        {
            self.Execute(DelayFrameAction.Allocate(1, action));
        }
    }
}
