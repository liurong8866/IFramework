using IFramework.Core;

namespace IFramework.Test.IOC
{
    public class IocMonoSingletonTest : IocMonoSingleton<IocMonoSingletonTest>
    {
        [Autowired("Student")]
        public IPerson Student;

        [Autowired("Teacher")] private IPerson Teacher { get; set; }

        [Autowired] private IPerson Xiaoxuesheng { get; set; }
        [Autowired("Xiaoxuesheng")] private IPerson Xiaoxuesheng2 { get; set; }

        [Autowired] private Xiaoxuesheng Xiaoxuesheng3 { get; set; }
        [Autowired("Xiaoxuesheng")] private Xiaoxuesheng Xiaoxuesheng4 { get; set; }

        protected override void OnAwake()
        {
            Register<IPerson, Xiaoxuesheng>();
            Register<IPerson, Xiaoxuesheng>("Xiaoxuesheng");
            Register<Xiaoxuesheng>();
            Register<Xiaoxuesheng>("Xiaoxuesheng");
        }

        private void Start()
        {
            Student.print("IocMonoSingletonTest");
            Teacher.print("IocMonoSingletonTest");
            Xiaoxuesheng.print("IocMonoSingletonTest 1");
            Xiaoxuesheng2.print("IocMonoSingletonTest 2");
            Xiaoxuesheng3.print("IocMonoSingletonTest 3");
            Xiaoxuesheng4.print("IocMonoSingletonTest 4");
        }
    }
}
