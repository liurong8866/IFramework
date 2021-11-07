using System;

namespace IFramework.Engine
{
    public interface IDisposeEventRegister
    {
        void OnDisposed(Action action);

        IDisposeEventRegister OnFinished(Action action);
    }
}
