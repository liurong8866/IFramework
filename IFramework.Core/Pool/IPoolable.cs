namespace IFramework.Core
{
    /// <summary>
    /// 可被用于对象池的接口
    /// </summary>
    public interface IPoolable
    {
        /// <summary>
        /// 回收对象时触发的事件
        /// </summary>
        void OnRecycled();

        /// <summary>
        /// 回收状态
        /// </summary>
        bool IsRecycled { get; set; }
    }
}
