using System;
using IFramework.Core;

namespace IFramework.Test.IOC
{
    public class IocMonoEventTestData : IData
    {
        public string Name { get; set; }
    }
    public class IocMonoEventTest : IocMonoBehaviour
    {
        protected override void OnAwake()
        {
            base.OnAwake();

            // TypeEvent.Register<IocMonoEventTestData>((data) => {
            //     data.Name.LogInfo();
            // });
            RegisterEvent<IocMonoEventTestData>(AA);
        }

        public void AA(IocMonoEventTestData data)
        {
            data.Name.LogInfo();
        }

        private void OnDisable()
        {
            UnRegisterEvent<IocMonoEventTestData>(AA);
        }
    }
}
