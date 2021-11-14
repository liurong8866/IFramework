using System;
using IFramework.Core;

namespace IFramework.Test.IOC
{
    public class IocMonoEventTestData : IData
    {
        public string Name { get; set; }
    }
    public class IocMonoEventTest : IocMonoSingleton<IocMonoEventTest>
    {
        protected override void OnAwake()
        {
            base.OnAwake();

            TypeEvent.Clear();
            // TypeEvent.Register<IocMonoEventTestData>((data) => {
            //     data.Name.LogInfo();
            // });
            RegisterEvent<IocMonoEventTestData>(AA);
            
            RegisterEvent<IocMonoEventTestData>(obj => {
                Log.Info("这里是匿名函数");
            });
        }

        public void AA(IocMonoEventTestData data)
        {
            data.Name.LogInfo();
        }

        // private void OnDisable()
        // {
        //     // UnRegisterEvent<IocMonoEventTestData>();
        //     UnRegisterAllEvent();
        // }
    }
}
