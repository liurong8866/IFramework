using IFramework.Core;
using UnityEngine;

namespace IFramework.Test.IOC
{
    public class IOCFrameworkExample : MonoBehaviour
    {
        [Autowired] public INetworkExampleService NetworkExampleService { get; set; }

        // Use this for initialization
        private void Start()
        {
            // 将模块注入 
            // 这种方式比较方便
            MainContainer.Container.Inject(this);
            NetworkExampleService.Request();

            // 或者 不通过注入，直接获得 实例
            // 这种方式性能更好
            INetworkExampleService networkExampleService = MainContainer.Container.Resolve<INetworkExampleService>();
            networkExampleService.Request();
        }
    }
}
