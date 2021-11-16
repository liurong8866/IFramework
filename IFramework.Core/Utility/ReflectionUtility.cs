using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine.UI;

namespace IFramework.Core
{
    /// <summary>
    /// C# 反射类扩展方法
    /// </summary>
    public static class ReflectionUtility
    {
        public static Assembly GetAssemblyCSharp()
        {
            return GetAssemblyCSharp("Assembly-CSharp,");
        }

        public static Assembly GetAssemblyCSharpEditor()
        {
            return GetAssemblyCSharp("Assembly-CSharp-Editor,");
        }

        private static Assembly GetAssemblyCSharp(string name)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly assembly in assemblies) {
                if (assembly.FullName.StartsWith(name)) {
                    return assembly;
                }
            }
            return null;
        }

        // /// <summary>
        // /// 通过反射方式调用函数
        // /// </summary>
        // public static object Invoke(object obj, string method, params object[] args)
        // {
        //     MethodInfo methodInfo = obj.GetType().GetMethod(method);
        //     if (methodInfo == null) {
        //         return null;
        //     }
        //     if (methodInfo.IsStatic) {
        //         return methodInfo.Invoke(null, args);
        //     }
        //     else {
        //         return methodInfo.Invoke(obj, args);
        //     }
        // }
        //
        // /// <summary>
        // /// 通过反射方式调用函数
        // /// </summary>
        // public static object Invoke(Type type, string method, params object[] args)
        // {
        //     MethodInfo methodInfo = type.GetMethod(method);
        //     if (methodInfo == null) {
        //         return null;
        //     }
        //     if (methodInfo.IsStatic) {
        //         return methodInfo.Invoke(null, args);
        //     }
        //     else {
        //         return methodInfo.Invoke(CreateInstance(type), args);
        //     }
        // }
        
        /*----------------------------- 反射调用 普通类-普通方法 -----------------------------*/
        
        /// <summary>
        /// 反射调用普通类普通方法
        /// </summary>
        public static object Invoke(Type type, string method)
        {
            return Invoke(type, null, method , null, new Type[] {}, null);
        }
        
        /// <summary>
        /// 反射调用普通类普通方法
        /// </summary>
        public static object Invoke(Type type, string method, Type[] methodArgType, object[] methodArgs)
        {
            return Invoke(type, null, null, method , null, methodArgType, methodArgs);
        }

        /// <summary>
        /// 反射调用普通类普通方法
        /// </summary>
        public static object Invoke(Type type, object[] constructorArgs, string method)
        {
            return Invoke(type, null, constructorArgs, method , null, new Type[] {}, null);
        }
        
        /// <summary>
        /// 反射调用普通类普通方法
        /// </summary>
        public static object Invoke(Type type, object[] constructorArgs, string method, Type[] methodArgType, object[] methodArgs)
        {
            return Invoke(type, null, constructorArgs, method , null, methodArgType, methodArgs);
        }

        /*----------------------------- 反射调用 普通类-泛型方法 -----------------------------*/
        /// <summary>
        /// 反射调用普通类泛型方法
        /// </summary>
        public static object Invoke(Type type, string method, Type[] methodT)
        {
            return Invoke(type, null, null, method , methodT, new Type[] {}, null);
        }

        /// <summary>
        /// 反射调用普通类泛型方法
        /// </summary>
        public static object Invoke(Type type, string method, Type[] methodT, Type[] methodArgType, object[] methodArgs)
        {
            return Invoke(type, null, null, method , methodT, methodArgType, methodArgs);
        }

        /// <summary>
        /// 反射调用普通类泛型方法
        /// </summary>
        public static object Invoke(Type type, object[] constructorArgs, string method, Type[] methodT)
        {
            return Invoke(type, null, constructorArgs, method , methodT, new Type[] {}, null);
        }
        
        /// <summary>
        /// 反射调用普通类泛型方法
        /// </summary>
        public static object Invoke(Type type, object[] constructorArgs, string method, Type[] methodT, Type[] methodArgType, object[] methodArgs)
        {
            return Invoke(type, null, constructorArgs, method , methodT, methodArgType, methodArgs);
        }
        
        /*----------------------------- 反射调用 泛型类-普通方法 -----------------------------*/
        
        /// <summary>
        /// 反射调用泛型类普通方法
        /// </summary>
        /// <param name="type">泛型类</param>
        /// <param name="classT">泛型类T></param>
        /// <param name="method">方法名</param>
        /// <returns></returns>
        public static object Invoke(Type type, Type[] classT, string method)
        {
            return Invoke(type, classT, null, method, null, new Type[] {}, null);
        }
        
        /// <summary>
        /// 反射调用泛型类普通方法
        /// </summary>
        /// <param name="type">泛型类</param>
        /// <param name="classT">泛型类T></param>
        /// <param name="method">方法名</param>
        /// <param name="methodArgType">方法参数类型</param>
        /// <param name="methodArgs">方法参数</param>
        /// <returns></returns>
        public static object Invoke(Type type, Type[] classT, string method, Type[] methodArgType, object[] methodArgs)
        {
            return Invoke(type, classT, null, method , null, methodArgType, methodArgs);
        }
        
        /// <summary>
        /// 反射调用泛型类普通方法
        /// </summary>
        /// <param name="type">泛型类</param>
        /// <param name="classT">泛型类T></param>
        /// <param name="constructorArgs">泛型类构造参数</param>
        /// <param name="method">方法名</param>
        /// <returns></returns>
        public static object Invoke(Type type, Type[] classT, object[] constructorArgs, string method)
        {
            return Invoke(type, classT, constructorArgs, method , null, new Type[] {}, null);
        }

        /// <summary>
        /// 反射调用泛型类普通方法
        /// </summary>
        /// <param name="type">泛型类</param>
        /// <param name="classT">泛型类T></param>
        /// <param name="constructorArgs">泛型类构造参数</param>
        /// <param name="method">方法名</param>
        /// <param name="methodArgType">方法参数类型</param>
        /// <param name="methodArgs">方法参数</param>
        /// <returns></returns>
        public static object Invoke(Type type, Type[] classT, object[] constructorArgs, string method, Type[] methodArgType, object[] methodArgs)
        {
            return Invoke(type, classT, constructorArgs, method , null, methodArgType, methodArgs);
        }
        
        /*----------------------------- 反射调用 泛型类-泛型方法 -----------------------------*/
        /// <summary>
        /// 反射调用泛型类泛型方法
        /// </summary>
        /// <param name="type">泛型类</param>
        /// <param name="classT">泛型类T></param>
        /// <param name="method">方法名</param>
        /// <param name="methodT">泛型方法T></param>
        /// <returns></returns>
        public static object Invoke(Type type, Type[] classT, string method, Type[] methodT)
        {
            return Invoke(type, classT, null, method , methodT, new Type[] {}, null);
        }
        
        /// <summary>
        /// 反射调用泛型类泛型方法
        /// </summary>
        /// <param name="type">泛型类</param>
        /// <param name="classT">泛型类T></param>
        /// <param name="method">方法名</param>
        /// <param name="methodT">泛型方法T></param>
        /// <param name="methodArgType">方法参数类型</param>
        /// <param name="methodArgs">方法参数</param>
        /// <returns></returns>
        public static object Invoke(Type type, Type[] classT, string method, Type[] methodT, Type[] methodArgType, object[] methodArgs)
        {
            return Invoke(type, classT, null, method , methodT, methodArgType, methodArgs);
        }

        /// <summary>
        /// 反射调用泛型类泛型方法
        /// </summary>
        /// <param name="type">泛型类</param>
        /// <param name="classT">泛型类T></param>
        /// <param name="constructorArgs">泛型类构造参数</param>
        /// <param name="method">方法名</param>
        /// <param name="methodT">泛型方法T></param>
        /// <returns></returns>
        public static object Invoke(Type type, Type[] classT, object[] constructorArgs,  string method, Type[] methodT)
        {
            return Invoke(type, classT, constructorArgs, method , methodT, new Type[] {}, null);
        }
        
        /// <summary>
        /// 反射调用泛型类泛型方法
        /// </summary>
        /// <param name="type">泛型类</param>
        /// <param name="classT">泛型类T></param>
        /// <param name="constructorArgs">泛型类构造参数</param>
        /// <param name="method">方法名</param>
        /// <param name="methodT">泛型方法T></param>
        /// <param name="methodArgType">方法参数类型</param>
        /// <param name="methodArgs">方法参数</param>
        /// <returns></returns>
        public static object Invoke(Type type, Type[] classT, object[] constructorArgs, string method, Type[] methodT, Type[] methodArgType, object[] methodArgs)
        {
            Type classType = type;

            // 如果是泛型类，则获取泛型类型
            if (type.IsGenericType) {
                classType = type.MakeGenericType(classT);
            }

            // 这个方法有局限性，重载的方法容易导致Exception（发现不明确的匹配）
            // MethodInfo methodInfo = methodArgType.Nothing() ? classType.GetMethod(method) : classType.GetMethod(method, methodArgType);
            MethodInfo methodInfo = GetMethodInfo(classType, method, methodT, methodArgType);
            if (methodInfo == null) {
                throw new Exception("未找到调用方法：" + method);
            }

            // 如果静态方法
            if (methodInfo.IsStatic) {
                return methodInfo.Invoke(null, methodArgs);
            }
            else {
                object instance = CreateInstance(classType, constructorArgs);
                return methodInfo.Invoke(instance, methodArgs);
            }
        }

        /// <summary>
        /// 获取方法信息
        /// </summary>
        private static MethodInfo GetMethodInfo(Type classType, string method, Type[] methodT, Type[] methodArgType)
        {
            MethodInfo[] methodInfos = classType.GetMethods();

            // 遍历所有方法
            foreach (MethodInfo info in methodInfos) {
                // 找到同名方法
                if (info.Name != method) continue;
                MethodInfo methodInfo = info;

                // 如果是泛型方法，则获取泛型方法
                if (methodInfo.IsGenericMethod) {
                    Type[] genericArguments = methodInfo.GetGenericArguments();
                    // MethodInfo genericMethodDefinition = methodInfo.GetGenericMethodDefinition();
                    if (genericArguments.Length != methodT.Length) continue;
                    methodInfo = methodInfo.MakeGenericMethod(methodT);
                }

                // 获得所有参数
                ParameterInfo[] parameterInfos = methodInfo.GetParameters();
                if (parameterInfos.Length == methodArgType.Length) {
                    bool isMatched = true;
                    // 判断参数匹配
                    for (int i = 0; i < parameterInfos.Length; i++) {
                        // 如果参数类型不同，则判断不同，退出。 出现 Action<T> 时，会有 System.Action`1的形式，FullName会有差异
                        // if (parameterInfos[i].ParameterType.FullName != methodArgType[i].FullName) {
                        if (parameterInfos[i].ParameterType.Name != methodArgType[i].Name) {
                            isMatched = false;
                            break;
                        }
                    }
                    // 参数完全批评，则为找到
                    if (isMatched) {
                        return methodInfo;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 创建实体类
        /// </summary>
        public static object CreateInstance(Type type, params object[] args)
        {
            object instance = args.NotEmpty() ? Activator.CreateInstance(type, args) : Activator.CreateInstance(type);
            
            if (instance != null) return instance;

            // 获取私有构造函数
            ConstructorInfo[] constructorInfos = typeof(Type).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            // 获取无参构造函数
            ConstructorInfo ctor = Array.Find(constructorInfos, c => c.GetParameters().Length == (args?.Length ?? 0));
            if (ctor == null) {
                throw new Exception("未找到无参私有构造函数: " + type.FullName);
            }
            instance = ctor.Invoke(args);
            return instance;
        }

        /*----------------------------- 获取属性、特性 -----------------------------*/
        /// <summary>
        /// 通过反射方式获取域值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName">域名</param>
        /// <returns></returns>
        public static object GetField(object obj, string fieldName)
        {
            FieldInfo fieldInfo = obj.GetType().GetField(fieldName);
            return fieldInfo == null ? null : fieldInfo.GetValue(obj);
        }

        /// <summary>
        /// 通过反射方式获取属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static object GetProperty(object obj, string propertyName, object[] index = null)
        {
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
            return propertyInfo == null ? null : propertyInfo.GetValue(obj, index);
        }

        /// <summary>
        /// 拥有特性
        /// </summary>
        /// <returns></returns>
        public static bool HasAttribute(this PropertyInfo prop, Type attributeType, bool inherit)
        {
            return prop.GetCustomAttributes(attributeType, inherit).Length > 0;
        }

        /// <summary>
        /// 拥有特性
        /// </summary>
        /// <returns></returns>
        public static bool HasAttribute(this FieldInfo field, Type attributeType, bool inherit)
        {
            return field.GetCustomAttributes(attributeType, inherit).Length > 0;
        }

        /// <summary>
        /// 拥有特性
        /// </summary>
        /// <returns></returns>
        public static bool HasAttribute(this Type type, Type attributeType, bool inherit)
        {
            return type.GetCustomAttributes(attributeType, inherit).Length > 0;
        }

        /// <summary>
        /// 拥有特性
        /// </summary>
        /// <returns></returns>
        public static bool HasAttribute(this MethodInfo method, Type attributeType, bool inherit)
        {
            return method.GetCustomAttributes(attributeType, inherit).Length > 0;
        }

        /// <summary>
        /// 获取第一个特性
        /// </summary>
        /// <param name="method"></param>
        /// <param name="inherit"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetFirstAttribute<T>(this MethodInfo method, bool inherit) where T : Attribute
        {
            T[] attrs = (T[])method.GetCustomAttributes(typeof(T), inherit);
            if (attrs.Length > 0) return attrs[0];
            return null;
        }

        /// <summary>
        /// 获取第一个特性
        /// </summary>
        public static T GetFirstAttribute<T>(this FieldInfo field, bool inherit) where T : Attribute
        {
            T[] attrs = (T[])field.GetCustomAttributes(typeof(T), inherit);
            if (attrs.Length > 0) return attrs[0];
            return null;
        }

        /// <summary>
        /// 获取第一个特性
        /// </summary>
        public static T GetFirstAttribute<T>(this PropertyInfo prop, bool inherit) where T : Attribute
        {
            T[] attrs = (T[])prop.GetCustomAttributes(typeof(T), inherit);
            if (attrs.Length > 0) return attrs[0];
            return null;
        }

        /// <summary>
        /// 获取第一个特性
        /// </summary>
        public static T GetFirstAttribute<T>(this Type type, bool inherit) where T : Attribute
        {
            T[] attrs = (T[])type.GetCustomAttributes(typeof(T), inherit);
            if (attrs.Length > 0) return attrs[0];
            return null;
        }
    }
}
