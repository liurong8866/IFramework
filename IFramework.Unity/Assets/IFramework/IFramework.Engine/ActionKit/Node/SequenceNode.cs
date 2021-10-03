using System.Collections.Generic;
using IFramework.Core;

namespace IFramework.Engine
{
    public class SequenceNode : AbstractAction, INode
    {
        // 记录原始节点状态，恢复默认值用
        protected List<IAction> originalNodes;
        // 真正执行的节点
        protected List<IAction> executeNodes;

        public SequenceNode()
        {
            // 从List对象池中分配
            originalNodes = ListPool<IAction>.Allocate();
            executeNodes = ListPool<IAction>.Allocate();
        }

        /// <summary>
        /// 总节点数量
        /// </summary>
        public int Count => executeNodes.Count;

        public IAction CurrentNode {
            get {
                IAction currentNode = executeNodes[0];
                // 如果当前节点是INode类型节点，也即是装饰节点，则取对应的属性CurrentNode， 否则直接返回
                return !(currentNode is INode node) ? currentNode : node.CurrentNode;
            }
        }

        protected override void OnReset()
        {
            executeNodes.Clear();

            // 通过原始节点恢复初始状态
            foreach (IAction node in originalNodes) {
                node.Reset();
                executeNodes.Add(node);
            }
        }
    }
}
