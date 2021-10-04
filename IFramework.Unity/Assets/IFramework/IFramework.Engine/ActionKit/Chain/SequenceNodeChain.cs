namespace IFramework.Engine
{
    /// <summary>
    /// 序列节点链
    /// </summary>
    public class SequenceNodeChain : AbstractActionChain
    {
        private SequenceNode sequenceNode;

        protected override AbstractAction Node => sequenceNode;

        public SequenceNodeChain()
        {
            sequenceNode = new SequenceNode();
        }

        public override IActionChain Append(IAction node)
        {
            this.sequenceNode.Append(node);
            return this;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            sequenceNode.Dispose();
            sequenceNode = null;
        }
    }
}
