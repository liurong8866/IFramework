using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// 链式调用接口
    /// </summary>
    public interface IActionChain : IAction
    {
        /// <summary>
        /// 
        /// </summary>
        MonoBehaviour Executer { get; set; }

        /// <summary>
        /// 添加节点
        /// </summary>
        IActionChain Append(IAction node);

        /// <summary>
        /// 开始执行
        /// </summary>
        IDisposeWhen Begin();
    }
}
