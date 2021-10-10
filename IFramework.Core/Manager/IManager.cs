using System;

namespace IFramework.Core
{
    public delegate void OnEvent(int key, params object[] param);
    
    public interface IManager
    {
        void Init();

        void RegisterEvent<T>(T msgId, OnEvent process) where T : IConvertible;

        void UnRegisterEvent<T>(T msgEvent, OnEvent process) where T : IConvertible;

        void SendEvent<T>(T eventId) where T : IConvertible;

        // void SendMsg(IMsg msg);
    }
}
