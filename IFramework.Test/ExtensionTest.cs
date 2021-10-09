using System;
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

        [Test]
        public void TestDateTime()
        {
            DateTime a = DateTime.Now;
            DateTime b = DateTimeExtention.StarTime;
            
            DateTime c = new DateTime(1970, 1, 1);
            DateTime d = new DateTime(1970, 1, 1, 0, 0, 0);
            DateTime e = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
            DateTime f = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime g = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Unspecified);

            DateTime c1 = c.ToLocalTime();
            DateTime d1 = d.ToLocalTime();
            DateTime e1 = e.ToLocalTime();
            DateTime f1 = f.ToLocalTime();
            DateTime g1 = g.ToLocalTime();
            
        }
        
        [Test]
        public void TestDateTime2()
        {
            DateTime a = DateTime.Now;
            DateTime a2 = DateTime.UtcNow;
            DateTime b = DateTimeExtention.StarTime;
            DateTime c = new DateTime(1970, 1, 1);
            DateTime d = b.ToLocalTime();
            DateTime e = b.ToUniversalTime();
            
            TimeSpan aa1 = a.Subtract(c);
            TimeSpan aa2 = a2.Subtract(c);
            
            DateTime f = b.ToLocalTime();
            DateTime g = f.ToLocalTime();
        }

        [Test]
        public void TestDateTime3()
        {
            DateTime a = DateTime.Now;
            DateTime b = DateTime.UtcNow;
            DateTime c = DateTimeExtention.StarTime;

            long a1 = a.ToUnixMilliseconds();
            long b1 = b.ToUnixMilliseconds();
            long c1 = c.ToUnixMilliseconds();
            
            long a2 = (long)a.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
            long b2 = (long)b.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
            long c2 = (long)c.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds;
            
            long a3 = (long)a.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            long b3 = (long)b.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            long c3 = (long)c.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds;
            
            long a4 = (long)a.Subtract(new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime()).TotalMilliseconds;
            long b4 = (long)b.Subtract(new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime()).TotalMilliseconds;
            long c4 = (long)c.Subtract(new DateTime(1970, 1, 1, 0, 0, 0).ToLocalTime()).TotalMilliseconds;

        }
        
    }
}
