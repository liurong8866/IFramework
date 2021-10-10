using System;
using System.Runtime.CompilerServices;

namespace IFramework.Core
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class ManagerBehaviour : IocMonoSingleton<ManagerBehaviour>, IManager
    {
        [Autowired]
        private ITypeEvent typeEvent;
        
        #region 代理实现
        
        public IDisposable RegisterEvent<T>(Action<T> action)
        {
            return typeEvent.RegisterEvent(action);
        }

        public void UnRegisterEvent<T>(Action<T> action)
        {
            typeEvent.UnRegisterEvent(action);
        }

        public void SendEvent<T>() where T : new()
        {
            typeEvent.SendEvent<T>();
        }

        public void SendEvent<T>(T param)
        {
            typeEvent.SendEvent(param);
        }
        
        #endregion
        
    }
}
