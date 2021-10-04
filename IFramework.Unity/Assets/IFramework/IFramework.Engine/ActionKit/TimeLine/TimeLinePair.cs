namespace IFramework.Engine.TimeLine
{
    public class TimeLinePair
    {
        public float Time;
        public IAction Node;

        public TimeLinePair(float time, IAction node)
        {
            Time = time;
            Node = node;
        }
    }
}
