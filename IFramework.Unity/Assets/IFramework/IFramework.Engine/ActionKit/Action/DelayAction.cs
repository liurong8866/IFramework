using System;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// 延时动作节点
    /// </summary>
    [Serializable]
    public class DelayAction : AbstractAction, IPoolable
    {
        // 延迟时间
        [SerializeField] public float DelayTime;
        // 时间计数器
        private float currentSeconds;
        // 延迟事件
        private Action action;

        /// <summary>
        /// 从缓存池中申请对象
        /// </summary>
        public static DelayAction Allocate(float delayTime, Action action = null)
        {
            DelayAction delayAction = ObjectPool<DelayAction>.Instance.Allocate();
            delayAction.DelayTime = delayTime;
            delayAction.action = action;
            return delayAction;
        }

        /// <summary>
        /// 执行事件
        /// </summary>
        protected override void OnExecute()
        {
            OnExecute(Time.deltaTime);
        }

        private void OnExecute(float delta)
        {
            // 判断是否超时，过了时间视为延迟结束
            currentSeconds += delta;
            Finished = currentSeconds >= DelayTime;

            if (Finished) { action.InvokeSafe(); }
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
            action = null;
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
        /// 延迟N秒执行某事件
        /// </summary>
        public static void Delay<T>(this T self, float seconds, Action action) where T : MonoBehaviour
        {
            self.Execute(DelayAction.Allocate(seconds, action));
        }

        /// <summary>
        /// 延迟N秒
        /// </summary>
        public static void Delay<T>(this T self, float seconds) where T : MonoBehaviour
        {
            self.Execute(DelayAction.Allocate(seconds));
        }
    }
}
