using IFramework.Core;
using NUnit.Framework;

namespace IFramework.Test
{
    public class ExtensionTest
    {
        [SetUp]
        public void Setup() { }

        [Test]
        public void TestCamal()
        {
            string a = "hello_world_nihao".ToCamel('_');
            string b = "hello_world_".ToCamel('_');
            string c = "_world_nihao".ToCamel('_');
            Assert.IsTrue("helloWorldNihao" == a);
            Assert.IsTrue("helloWorld" == b);
            Assert.IsTrue("worldNihao" == c);
        }

        [Test]
        public void TestPascal()
        {
            string a = "hello_world_nihao".ToPascal('_');
            string b = "hello_world_".ToPascal('_');
            string c = "_world_nihao".ToPascal('_');
            Assert.IsTrue("HelloWorldNihao" == a);
            Assert.IsTrue("HelloWorld" == b);
            Assert.IsTrue("WorldNihao" == c);
        }
    }
}
