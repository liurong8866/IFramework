using System.Collections;
using System.Collections.Generic;
using IFramework.Core;
using UnityEngine;

public class Init
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void RegisterIoc()
    {
        IocContainer container = IocContainer.Instance;
        
        container.Register<IPerson, Teacher>("Teacher");
        container.Register<IPerson, Student>("Student");
    }
}
