using System;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// 延迟帧动作节点
    /// </summary>
    [Serializable]
    public class DelayFrameAction : AbstractAction, IPoolable, IResetable
    {
        // 延迟帧数
        [SerializeField] public int FrameCount;
        // 延迟事件
        private Action action;
        // 帧数计数器
        private int startFrame;

        /// <summary>
        /// 从对象池中申请对象
        /// </summary>
        public static DelayFrameAction Allocate(int frameCount, Action acton = null)
        {
            DelayFrameAction delayFrameAction = ObjectPool<DelayFrameAction>.Instance.Allocate();
            delayFrameAction.FrameCount = frameCount;
            delayFrameAction.action = acton;
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
                action.InvokeSafe();
            }
        }

        protected override void OnReset()
        {
            startFrame = Time.frameCount;
        }

        protected override void OnDispose()
        {
            ObjectPool<DelayFrameAction>.Instance.Recycle(this);
        }

        public void OnRecycled()
        {
            action = null;
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
        /// 延迟N帧执行某事件
        /// </summary>
        public static void DelayFrame<T>(this T self, int frameCount, Action action) where T : MonoBehaviour
        {
            self.Execute(DelayFrameAction.Allocate(frameCount, action));
        }

        /// <summary>
        /// 延迟N帧执行某事件
        /// </summary>
        public static void DelayFrame<T>(this T self, int frameCount) where T : MonoBehaviour
        {
            self.Execute(DelayFrameAction.Allocate(frameCount));
        }

        /// <summary>
        /// 下一帧执行某事件
        /// </summary>
        public static void NextFrame<T>(this T self, Action action) where T : MonoBehaviour
        {
            self.Execute(DelayFrameAction.Allocate(1, action));
        }

        /// <summary>
        /// 下一帧执行某事件
        /// </summary>
        public static void NextFrame<T>(this T self) where T : MonoBehaviour
        {
            self.Execute(DelayFrameAction.Allocate(1));
        }
    }
}
