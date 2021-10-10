using System;

namespace IFramework.Core
{
    public class EventUnregister<T> : IDisposable
    {
        public Action<T> actions;

        public ITypeEvent typeEvent;

        public void Dispose()
        {
            typeEvent.UnRegisterEvent(actions);
        }
    }
}
