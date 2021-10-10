using System;
using System.Runtime.CompilerServices;

namespace IFramework.Core
{
    /// <summary>
    /// 游戏管理基础类
    /// </summary>
    /// <typeparam name="T">继承自ManagerBehaviour</typeparam>
    public abstract class ManagerBehaviour<T> : IocMonoSingleton<T>, IManager where T : ManagerBehaviour<T>
    {
        [Autowired] private ITypeEvent typeEvent;

        #region 代理实现

        public IDisposable RegisterEvent<T1>(Action<T1> action)
        {
            return typeEvent.RegisterEvent(action);
        }

        public void UnRegisterEvent<T1>(Action<T1> action)
        {
            typeEvent.UnRegisterEvent(action);
        }

        public void SendEvent<T1>() where T1 : new()
        {
            typeEvent.SendEvent<T1>();
        }

        public void SendEvent<T1>(T1 param)
        {
            typeEvent.SendEvent(param);
        }

        #endregion
    }
}
