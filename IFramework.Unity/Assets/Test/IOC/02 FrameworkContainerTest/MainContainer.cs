using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using IFramework.Core;
using UnityEngine;

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
