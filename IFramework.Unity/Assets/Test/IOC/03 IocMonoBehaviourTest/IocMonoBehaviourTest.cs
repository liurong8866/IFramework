using IFramework.Core;

namespace IFramework.Test.IOC
{
    public class IocMonoBehaviourTest : IocMonoBehaviour
    {
        [Autowired("Teacher")]
        private readonly IPerson teacher = null;
        [Autowired("Student")]
        private readonly IPerson sutdent = null;

        [Autowired] private IPerson xiaoxuesheng1 { get; set; }
        [Autowired("Xiaoxuesheng")] private IPerson xiaoxuesheng2 { get; set; }

        [Autowired] private Xiaoxuesheng xiaoxuesheng3 { get; set; }
        [Autowired("Xiaoxuesheng")] private Xiaoxuesheng xiaoxuesheng4 { get; set; }

        protected override void OnAwake()
        {
            // Register<IPerson, Teacher>("Teacher");
            // Register<IPerson, Student>("Student");
            RegisterInstance<IPerson>(new Xiaoxuesheng());
            RegisterInstance<IPerson>(new Xiaoxuesheng(), "Xiaoxuesheng");
            RegisterInstance(new Xiaoxuesheng());
            RegisterInstance(new Xiaoxuesheng(), "Xiaoxuesheng");
        }

        private void Start()
        {
            teacher.print("IocMonoBehaviourTest");
            sutdent.print("IocMonoBehaviourTest");
            xiaoxuesheng1.print("IocMonoBehaviourTest 1");
            xiaoxuesheng2.print("IocMonoBehaviourTest 2");
            xiaoxuesheng3.print("IocMonoBehaviourTest 3");
            xiaoxuesheng4.print("IocMonoBehaviourTest 4");
        }
    }
}
