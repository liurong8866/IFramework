using System.Collections;
using System.Collections.Generic;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Test.IOC
{
    public class IocTest : MonoBehaviour
    {
        // [Autowired] public A AObj;

        // Use this for initialization
        void Start()
        {
            IocContainer container = IocContainer.Instance;
            container.RegisterInstance(new A());
            container.Inject(this);
            container.Resolve<A>().HelloWorld();
        }

        public class A
        {
            public void HelloWorld()
            {
                "This is A obj".LogInfo();
            }
        }
    }
}