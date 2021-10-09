using System;
using System.Xml.Serialization;

namespace IFramework.Core
{
    public abstract class IocContainer : FrameworkContainer, ISingleton
    {
        protected IocContainer() { }

        public static IocContainer Instance => SingletonProperty<IocContainer>.Instance;

        public static T GetInstance<T>() where T : IocContainer
        {
            return SingletonProperty<T>.Instance;
        }

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
