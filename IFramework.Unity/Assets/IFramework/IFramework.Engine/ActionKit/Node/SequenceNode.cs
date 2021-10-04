using System.Collections.Generic;
using IFramework.Core;

namespace IFramework.Engine
{
    public class SequenceNode : AbstractAction, INode
    {
        // 记录原始节点状态，恢复默认值用
        protected List<IAction> originalNodes = ListPool<IAction>.Allocate();
        // 真正执行的节点
        protected List<IAction> executeNodes = ListPool<IAction>.Allocate();

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

        /// <summary>
        /// 添加节点
        /// </summary>
        public SequenceNode Append(IAction appendedNode)
        {
            originalNodes.Add(appendedNode);
            executeNodes.Add(appendedNode);
            return this;
        }

        /// <summary>
        /// 执行事件
        /// </summary>
        protected override void OnExecute()
        {
            if (Count > 0) {
                // 如果有异常，则进行销毁，不再进行下边的操作
                if (executeNodes[0].Disposed && !executeNodes[0].Finished) {
                    Dispose();
                    return;
                }

                while (executeNodes[0].Execute()) {
                    executeNodes.RemoveAt(0);
                    OnCurrentActionFinished();
                    if (Count == 0) break;
                }
            }
            Finished = Count == 0;
        }

        /// <summary>
        /// 当前事件结束触发的事件
        /// </summary>
        protected virtual void OnCurrentActionFinished() { }

        protected override void OnReset()
        {
            executeNodes.Clear();

            // 通过原始节点恢复初始状态
            foreach (IAction node in originalNodes) {
                node.Reset();
                executeNodes.Add(node);
            }
        }

        protected override void OnDispose()
        {
            if (originalNodes != null) {
                originalNodes.ForEach(node => node.Dispose());
                originalNodes.Clear();
                originalNodes.Recycle();
                originalNodes = null;
            }

            // ReSharper disable once InvertIf
            if (executeNodes != null) {
                executeNodes.Recycle();
                executeNodes = null;
            }
        }
    }
}
