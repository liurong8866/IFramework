using System.Collections.Generic;
using IFramework.Core;

namespace IFramework.Engine
{
    /// <summary>
    /// 并行执行节点 
    /// </summary>
    public class ParallelNode : AbstractAction
    {
        private int finishCount;
        protected List<AbstractAction> nodes = ListPool<AbstractAction>.Allocate();

        public ParallelNode(params AbstractAction[] nodes)
        {
            this.nodes.AddRange(nodes);

            foreach (AbstractAction node in nodes) {
                node.OnEndEvent += IncreaseFinishCount;
            }
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        public void Append(params AbstractAction[] nodes)
        {
            this.nodes.AddRange(nodes);

            foreach (AbstractAction node in nodes) {
                node.OnEndEvent += IncreaseFinishCount;
            }
        }

        /// <summary>
        /// 执行事件
        /// </summary>
        protected override void OnExecute()
        {
            for (int i = nodes.Count - 1; i >= 0; i--) {
                AbstractAction node = nodes[i];

                if (!node.Finished && node.Execute()) {
                    Finished = nodes.Count == finishCount;
                }
            }
        }

        protected override void OnReset()
        {
            nodes.ForEach(node => node.Reset());
            finishCount = 0;
        }

        public override void Finish()
        {
            for (int i = nodes.Count - 1; i >= 0; i--) {
                nodes[i].Finish();
            }
            base.Finish();
        }

        protected override void OnDispose()
        {
            foreach (AbstractAction node in nodes) {
                node.OnEndEvent -= IncreaseFinishCount;
                node.Dispose();
            }
            nodes.Recycle();
            nodes = null;
        }

        private void IncreaseFinishCount()
        {
            finishCount++;
        }
    }
}
