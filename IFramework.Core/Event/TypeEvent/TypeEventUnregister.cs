using System;

namespace IFramework.Core
{
    public class TypeEventUnregister<T> : IDisposable
    {
        public Action<T> actions;

        public ITypeEvent typeEvent;

        public void Dispose()
        {
            typeEvent.UnRegisterEvent(actions);
        }
    }
}
