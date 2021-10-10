using System;
using System.Xml.Serialization;

namespace IFramework.Core
{
    /// <summary>
    /// Ioc容器，单例模式
    /// </summary>
    public abstract class IocSingleton<T> : AbstractContainer, ISingleton where T : IocSingleton<T>
    {
        protected IocSingleton() { }

        public static T Instance => SingletonProperty<T>.Instance;
        
        public void OnInit()
        {
            Register();
            Inject(this);
        }

        /// <summary>
        /// 注册ioc
        /// </summary>
        public abstract void Register();
    }
}
