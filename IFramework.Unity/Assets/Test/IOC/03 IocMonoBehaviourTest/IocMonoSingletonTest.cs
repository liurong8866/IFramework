using System;
using IFramework.Core;

public class IocMonoSingletonTest : IocMonoSingleton<IocMonoSingletonTest>
{
    [Autowired("Student")]
    public IPerson Student;

    [Autowired("Teacher")]
    private IPerson Teacher { get; set; }
    
    
    protected override void Init()
    {
        Register<IPerson, Teacher>("Teacher");
        Register<IPerson, Student>("Student");
    }

    private void Start()
    {
        Student.print("IocMonoSingletonTest");
        Teacher.print("IocMonoSingletonTest");
    }
}
