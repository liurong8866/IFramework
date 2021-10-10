using IFramework.Core;
using NUnit.Framework;

namespace IFramework.Test
{
    public class IocMonoTest
    {
        [Test]
        public void TestMono()
        {
            new IocMonoBehaviour().Register<>();
        }
    }
}
