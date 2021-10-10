using IFramework.Core;

namespace IFramework.Test.IOC
{
    public class MonoTest : IocMonoBehaviour
    {
        [Autowired("Student")]
        public IPerson Student;

        [Autowired("Teacher")] private IPerson Teacher { get; set; }

        // Start is called before the first frame update
        private void Start()
        {
            Student.print("MonoTest");
            Teacher.print("MonoTest");
        }
    }
}
