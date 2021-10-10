using System;
using IFramework.Core;
using NUnit.Framework;

namespace IFramework.Test
{
    public class IocTest
    {
        [Autowired] public IPerson Person { get; set; }

        public IocTest()
        {
            TestContainer.Instance.Inject(this);
        }
        
        [Test]
        public void TestIoc()
        {
            IPerson resolve = TestContainer.Instance.Resolve(typeof(IPerson)) as IPerson;
            // IPerson resolve = TestContainer.Instance.Resolve<IPerson>();
            Console.Out.WriteLine(resolve.name);
            Console.Out.WriteLine(resolve.age);
            resolve.print();
            
            Person.print();
            
        }
    }
    

    public class TestContainer : AbstractFrameworkContainer, ISingleton
    {
        private TestContainer() { }

        public static TestContainer Instance => SingletonProperty<TestContainer>.Instance;

        public void OnInit()
        {
            Register<IPerson, Student>();
        }
    }

    public interface IPerson
    {
        string name { get; }
        string age { get; }

        void print();
    }

    public class Teacher : IPerson
    {
        public string name { get; }
        public string age { get; }

        public void print()
        {
            Console.Out.WriteLine("我是老师");
        }

        public string clazz { get; }

        public Teacher() { }

        public Teacher(string name, string age, string clazz)
        {
            this.name = name;
            this.age = age;
            this.clazz = clazz;
        }
        
    }

    public class Student : IPerson
    {
        public string name { get; }
        public string age { get; }
        public string book { get; }
        public Teacher teacher { get;}

        public Student() { }

        public Student(string name, string age, string book, Teacher teacher)
        {
            
            this.name = name;
            this.age = age;
            this.book = book;
            this.teacher = teacher;
        }

        public void print()
        {
            Console.Out.WriteLine("我是学生");
        }

    }
}
