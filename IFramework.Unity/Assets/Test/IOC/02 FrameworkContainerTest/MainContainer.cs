using IFramework.Core;

namespace IFramework.Test.IOC
{
    public class MainContainer : IocContainer, ISingleton
    {
        private MainContainer() { }

        public static IocContainer Container => SingletonProperty<MainContainer>.Instance;

        void ISingleton.OnInit()
        {
            // 注册网络服务模块
            RegisterInstance<INetworkExampleService>(new NetworkExampleService2());
        }
    }
}
