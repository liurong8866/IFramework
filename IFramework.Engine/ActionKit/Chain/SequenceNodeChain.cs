namespace IFramework.Engine
{
    /// <summary>
    /// 顺序执行序列 节点内当动作顺次执行，一个点的执行完毕，成功返回结果后执行下一节点
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
            sequenceNode.Append(node);
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
