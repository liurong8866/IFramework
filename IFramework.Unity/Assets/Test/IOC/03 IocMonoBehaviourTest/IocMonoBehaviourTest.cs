using IFramework.Core;
using UnityEngine;

namespace IFramework.Test.IOC
{
    public class IocMonoBehaviourTest : IocMonoBehaviour
    {
        [Autowired("Teacher")] private IPerson teacher;
        [Autowired("Student")] private IPerson sutdent;

        // protected override void Init()
        // {
        //     Register<IPerson, Teacher>("Teacher");
        //     Register<IPerson, Student>("Student");
        // }

        private void Start()
        {
            teacher.print("IocMonoBehaviourTest");
            sutdent.print("IocMonoBehaviourTest");
        }

        // 可以把该方法放到单独的初始化类中，系统启动时加载，便于维护
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void RegisterIoc()
        {
            IocContainer container = IocContainer.Instance;

            container.Register<IPerson, Teacher>("Teacher");
            container.Register<IPerson, Student>("Student");
        }
    }
}