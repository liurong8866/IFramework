using UnityEngine;

namespace IFramework.Test.IOC
{
    public interface INetworkExampleService
    {
        void Request();
    }

    public class NetworkExampleService1 : INetworkExampleService
    {
        public void Request()
        {
            Debug.Log("请求服务器1");
        }
    }

    public class NetworkExampleService2 : INetworkExampleService
    {
        public void Request()
        {
            Debug.Log("请求服务器2");
        }
    }
}
