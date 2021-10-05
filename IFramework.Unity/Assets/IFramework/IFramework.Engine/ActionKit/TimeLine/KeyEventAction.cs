using IFramework.Core;

namespace IFramework.Engine.TimeLine
{
    public class KeyEventAction : AbstractAction, IPoolable
    {
        private TimeLine timeLine;
        private string eventName;

        public static KeyEventAction Allocate(string eventName, TimeLine timeLine)
        {
            KeyEventAction keyEventAction = ObjectPool<KeyEventAction>.Instance.Allocate();
            keyEventAction.eventName = eventName;
            keyEventAction.timeLine = timeLine;
            return keyEventAction;
        }

        protected override void OnBegin()
        {
            base.OnBegin();
            timeLine.OnReceivedEvent.InvokeSafe(eventName);
            Finish();
        }

        protected override void OnDispose()
        {
            ObjectPool<KeyEventAction>.Instance.Recycle(this);
        }

        public void OnRecycled()
        {
            timeLine = null;
            eventName = null;
        }

        public bool IsRecycled { get; set; }
    }
}
