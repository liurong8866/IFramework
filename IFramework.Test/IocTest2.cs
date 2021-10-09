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
            A.Instance<A>().PrintA();
        }
    }
    
    
    public class A : IocContainer
    {
        [Autowired] private B b;

        protected A(){}
        
        [Test]
        public void PrintA()
        {
            b.Print1();
            b.Print2();
        }

        public override void Register()
        {
            RegisterInstance<B>(new B());
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
