namespace IFramework.Engine
{
    public class RepeatNode : AbstractAction, INode
    {
        private IAction node;

        public int RepeatCount { get; set; }

        private int currentRepeatCount;

        public RepeatNode(IAction node, int repeatCount = -1)
        {
            RepeatCount = repeatCount;
            this.node = node;
        }

        /// <summary>
        /// 当前节点
        /// </summary>
        public IAction CurrentNode {
            get {
                IAction currentNode = node;
                return !(currentNode is INode node2) ? currentNode : node2.CurrentNode;
            }
        }

        protected override void OnExecute()
        {
            if (RepeatCount == -1) {
                if (node.Execute()) {
                    node.Reset();
                }
                return;
            }
            if (node.Execute()) {
                node.Reset();
                currentRepeatCount++;
            }
            if (currentRepeatCount == RepeatCount) {
                Finished = true;
            }
        }

        protected override void OnReset()
        {
            node?.Reset();
            currentRepeatCount = 0;
            Finished = false;
        }

        protected override void OnDispose()
        {
            // ReSharper disable once InvertIf
            if (null != node) {
                node.Dispose();
                node = null;
            }
        }
    }
}
