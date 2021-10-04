using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// 链式调用接口
    /// </summary>
    public interface IActionChain : IAction
    {
        MonoBehaviour Executer { get; set; }

        IActionChain Append(IAction node);

        IDisposeWhen Begin();
    }
}
