namespace IFramework.Engine
{
    public class RepeatNodeChain : AbstractActionChain
    {
        private RepeatNode repeatNode;
        private SequenceNode sequenceNode;

        protected override AbstractAction Node => repeatNode;

        public RepeatNodeChain(int repeatCount)
        {
            sequenceNode = new SequenceNode();
            repeatNode = new RepeatNode(sequenceNode, repeatCount);
        }

        public override IActionChain Append(IAction node)
        {
            sequenceNode.Append(node);
            return this;
        }

        protected override void OnDispose()
        {
            base.OnDispose();
            repeatNode?.Dispose();
            repeatNode = null;
            sequenceNode?.Dispose();
            sequenceNode = null;
        }
    }
}
