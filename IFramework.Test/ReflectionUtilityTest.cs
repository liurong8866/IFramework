using System;
using IFramework.Core;
using NUnit.Framework;

namespace IFramework.Test
{
    public class ReflectionUtilityTest
    {
        [Test]
        public void Test1()
        {
            // GenericA<string>.PrintA();
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(string) }, new object[] {100}, "PrintA", null, new Type []{}, null);
            
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(string) }, new object[] {100}, "PrintB", null, new Type []{}, null);
            
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(int) }, null, "PrintC", null, new Type []{typeof(int)}, new object[] { 100 });
            
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(string) }, null, "PrintD", null, new Type[] { typeof(int)},  new object[] {10 });
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(string) }, null, "PrintD", null, new Type[] { typeof(string)}, new object[] { "100" });
            
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(string) }, null, "PrintD", null, new Type[] { typeof(string), typeof(string)}, new object[] { "100", "hal" });
            
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(string) }, null, "PrintE", new Type[] {typeof(float)}, new Type []{}, null);
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(string) }, null, "PrintE", new Type[] {typeof(float)}, new Type []{typeof(int)},  new object[]{1});
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(string) }, null, "PrintE", new Type[] {typeof(float)}, new Type []{typeof(int), typeof(int)}, new object[]{1,2});
            
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(string) }, null, "PrintE", new Type[] {typeof(float)}, new Type[] { typeof(float),  typeof(string)}, new object[] { 1.2f, "hhh" });
            
            
            // GenericB.PrintB<string>();
            ReflectionUtility.Invoke(typeof(GenericB), null, null, "PrintA", new Type[] {typeof(string)}, new Type []{}, null);
            
            ReflectionUtility.Invoke(typeof(GenericB), null, null, "PrintB", new Type[] {typeof(string)}, new Type []{}, null);

            ReflectionUtility.Invoke(typeof(GenericB), null, null, "PrintC", new Type[] {typeof(string)}, new Type []{typeof(int)}, new object[]{ 10 });
            
            ReflectionUtility.Invoke(typeof(GenericB), null, null, "PrintC", new Type[] {typeof(string)}, new Type []{typeof(string)}, new object[]{ "10" });
            
            ReflectionUtility.Invoke(typeof(GenericB), null, null, "PrintC", new Type[] {typeof(string)}, new Type []{typeof(int), typeof(int)}, new object[]{ 10, 100 });
            
            ReflectionUtility.Invoke(typeof(GenericB), null, null, "PrintC", new Type[] {typeof(int), typeof(string)}, new Type []{typeof(int), typeof(string)}, new object[]{ 10, "100" });

            ReflectionUtility.Invoke(typeof(GenericB), null, null, "PrintD", null, new Type []{}, null);
            
            ReflectionUtility.Invoke(typeof(GenericB), null, null, "PrintD", null, new Type []{typeof(int)}, new object[]{100});
            
            // ReflectionUtility.Invoke(typeof(TypeEvent), null, null, "UnRegister", new Type[] {action}, new Type[] {}, null);
            
            // Invoke(Type type, Type[] classT, object[] constructorArgs, string method, Type[] methodT, Type[] methodArgType, object[] methodArgs)
        }

        [Test]
        public void Test2()
        {
           
            /*----------------------------- 反射调用 普通类-普通方法 -----------------------------*/
            ReflectionUtility.Invoke(typeof(GenericB), "PrintA");
            ReflectionUtility.Invoke(typeof(GenericB), "PrintA", new Type[]{typeof(int)}, new object[]{100});
            ReflectionUtility.Invoke(typeof(GenericB), new object[]{ 100}, "PrintA");
            ReflectionUtility.Invoke(typeof(GenericB), new object[]{200}, "PrintA", new Type[]{typeof(int)}, new object[]{100});
          
            /*----------------------------- 反射调用 普通类-泛型方法 -----------------------------*/
            ReflectionUtility.Invoke(typeof(GenericB), "PrintA", new Type[] {typeof(string)});
            ReflectionUtility.Invoke(typeof(GenericB), "PrintC", new Type[] {typeof(string)}, new Type[]{typeof(int)}, new object[]{1});
            ReflectionUtility.Invoke(typeof(GenericB), new object[]{ 100}, "PrintA", new Type[] {typeof(string)});
            ReflectionUtility.Invoke(typeof(GenericB), new object[]{ 100}, "PrintC", new Type[] {typeof(string)}, new Type[]{typeof(int)}, new object[]{1});

            /*----------------------------- 反射调用 泛型类-普通方法 -----------------------------*/
            ReflectionUtility.Invoke(typeof(GenericA<>), new Type[]{typeof(string)}, "PrintB");
            ReflectionUtility.Invoke(typeof(GenericA<>), new Type[]{typeof(string)}, "PrintC", new Type[]{typeof(int)}, new object[]{100});
            ReflectionUtility.Invoke(typeof(GenericA<>), new Type[]{typeof(string)}, new object[]{ 100}, "PrintB");
            ReflectionUtility.Invoke(typeof(GenericA<>), new Type[]{typeof(string)}, new object[]{200}, "PrintC", new Type[]{typeof(int)}, new object[]{100});
            
            /*----------------------------- 反射调用 泛型类-泛型方法 -----------------------------*/
            ReflectionUtility.Invoke(typeof(GenericA<>), new Type[]{typeof(string)}, "PrintE", new Type[]{typeof(string)});
            ReflectionUtility.Invoke(typeof(GenericA<>), new Type[]{typeof(string)}, "PrintE", new Type[]{typeof(string)}, new Type[]{typeof(int)}, new object[]{100});
            ReflectionUtility.Invoke(typeof(GenericA<>), new Type[]{typeof(string)}, new object[]{ 100}, "PrintE",new Type[]{typeof(string)});
            ReflectionUtility.Invoke(typeof(GenericA<>), new Type[]{typeof(string)}, new object[]{200}, "PrintE", new Type[]{typeof(string)}, new Type[]{typeof(int)}, new object[]{100});
            
        }
        
        [Test]
        public void Test3()
        {
            // GenericA<string>.PrintA();
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(string) }, new object[] {100}, "PrintA");
            
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(string) }, new object[] {100}, "PrintB");
            
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(int) }, "PrintC", new Type []{typeof(int)}, new object[] { 100 });
            
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(string) }, "PrintD", new Type[] { typeof(int)},  new object[] {10 });
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(string) }, "PrintD", new Type[] { typeof(string)}, new object[] { "100" });
            
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(string) }, "PrintD", new Type[] { typeof(string), typeof(string)}, new object[] { "100", "hal" });
            
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(string) }, "PrintE", new Type[] {typeof(float)});
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(string) }, "PrintE", new Type[] {typeof(float)}, new Type []{typeof(int)},  new object[]{1});
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(string) }, "PrintE", new Type[] {typeof(float)}, new Type []{typeof(int), typeof(int)}, new object[]{1,2});
            
            ReflectionUtility.Invoke(typeof(GenericA<>), new[] { typeof(string) }, "PrintE", new Type[] {typeof(float)}, new Type[] { typeof(float),  typeof(string)}, new object[] { 1.2f, "hhh" });
            
            
            // GenericB.PrintB<string>();
            ReflectionUtility.Invoke(typeof(GenericB), "PrintA", new Type[] {typeof(string)});
            
            ReflectionUtility.Invoke(typeof(GenericB), "PrintB", new Type[] {typeof(string)});

            ReflectionUtility.Invoke(typeof(GenericB), "PrintC", new Type[] {typeof(string)}, new Type []{typeof(int)}, new object[]{ 10 });
            
            ReflectionUtility.Invoke(typeof(GenericB), "PrintC", new Type[] {typeof(string)}, new Type []{typeof(string)}, new object[]{ "10" });
            
            ReflectionUtility.Invoke(typeof(GenericB), "PrintC", new Type[] {typeof(string)}, new Type []{typeof(int), typeof(int)}, new object[]{ 10, 100 });
            
            ReflectionUtility.Invoke(typeof(GenericB), "PrintC", new Type[] {typeof(int), typeof(string)}, new Type []{typeof(int), typeof(string)}, new object[]{ 10, "100" });

            ReflectionUtility.Invoke(typeof(GenericB), "PrintD");
            
            ReflectionUtility.Invoke(typeof(GenericB), "PrintD", new Type []{typeof(int)}, new object[]{100});
            
            Action<int> action = i => {
                int a = 1;
            };
            ReflectionUtility.Invoke(typeof(GenericB), "PrintE", new Type []{typeof(int)}, new Type []{typeof(Action<int>)}, new object[] {action});

                    
            // ReflectionUtility.Invoke(typeof(TypeEvent), null, null, "UnRegister", new Type[] {action}, new Type[] {}, null);
            
            // Invoke(Type type, Type[] classT, object[] constructorArgs, string method, Type[] methodT, Type[] methodArgType, object[] methodArgs)
        }
    }

    public class GenericA<T>
    {
        public GenericA()
        {
            int a = 1;
        }
        
        public GenericA(int b)
        {
            int a = 1;
        }
        
        public static void PrintA()
        {
            int a = 1;
        }
        
        public void PrintB()
        {
            int a = 1;
        }
        
        public void PrintC(int b)
        {
            int a = b;
        }
        
        // 这种方法与泛型方法重复的，导致 System.Reflection.AmbiguousMatchException Ambiguous match found.
        public void PrintD(int b)
        {
            int a = b;
        }
        
        public void PrintD(T t)
        {
            Type type = t.GetType();
        }
        
        public void PrintD(T t, string m)
        {
            Type type = t.GetType();
        }
        
        public void PrintE<k>()
        {
            int a = 1;
        }
        
        public void PrintE<K>(int a1)
        {
            int a = 1;
        }
        
        public void PrintE<K>(int a1, int b1)
        {
            int a = 1;
        }
        
        public void PrintE<K>(K k1,T t1)
        {
            int a = 1;
        }
    }

    public class GenericB
    {
        public GenericB()
        {
            int a = 1;
        }
        
        public GenericB(int b)
        {
            int a = 1;
        }

        public void PrintA()
        {
            int a = 1;
        }

        public void PrintA(int b)
        {
            int a = 1;
        }
        
        
        public static void PrintA<T>()
        {
            int a = 1;
        }
        
        public static void PrintB<T>()
        {
            int a = 1;
        }
        
        public void PrintC<K>(int a1)
        {
            int a = 1;
        }
        
        public void PrintC<K>(K a1)
        {
            int a = 1;
        }
        
        public void PrintC<K>(int a1, int b1)
        {
            int a = 1;
        }
        
        public void PrintC<K, T>(K k1,T t1)
        {
            int a = 1;
        }
        
        public void PrintD()
        {
            int a = 1;
        }

        public static void PrintD(int b)
        {
            int a = 1;
        }

        public void PrintE<T>(Action<T> action)
        {
            int a = 1;
        }
    }
}
