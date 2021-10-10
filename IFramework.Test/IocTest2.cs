using System;
using IFramework.Core;
using NUnit.Framework;

namespace IFramework.Test
{
    public class IocTest2
    {
        [Test]
        public void Testa()
        {
            A.Instance.PrintA();
        }
    }

    public class A : IocSingleton<A>
    {
        [Autowired]
        private readonly B b = null;

        protected A() { }

        [Test]
        public void PrintA()
        {
            b.Print1();
            b.Print2();
        }

        public override void Init()
        {
            RegisterInstance(new B());
        }
    }

    public class B
    {
        public void Print1()
        {
            Console.Out.WriteLine("B - Print 1");
        }

        public void Print2()
        {
            Console.Out.WriteLine("B - Print 2");
        }
    }
}
