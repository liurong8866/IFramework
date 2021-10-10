using System;

namespace IFramework.Core
{
    /// <summary>
    /// 
    /// </summary>
    public class ManagerBehaviour : IocMonoSingleton<ManagerBehaviour>, IManager
    {
        public void Init()
        {
            throw new NotImplementedException();
        }

        public void RegisterEvent<T>(T msgId, OnEvent process) where T : IConvertible
        {
            throw new NotImplementedException();
        }

        public void UnRegisterEvent<T>(T msgEvent, OnEvent process) where T : IConvertible
        {
            throw new NotImplementedException();
        }

        public void SendEvent<T>(T eventId) where T : IConvertible
        {
            throw new NotImplementedException();
        }
    }
}
