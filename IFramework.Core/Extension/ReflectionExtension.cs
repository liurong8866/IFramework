using System;
using System.Reflection;

namespace IFramework.Core
{
    /// <summary>
    /// C# 反射类扩展方法
    /// </summary>
    public static class ReflectionExtension
    {
        public static void Example()
        {
            // var selfType = ReflectionExtension.GetAssemblyCSharp().GetType("IFramework.Editor.ReflectionExtension");
            // selfType.LogInfo();
        }

        public static Assembly GetAssemblyCSharp()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly a in assemblies) {
                if (a.FullName.StartsWith("Assembly-CSharp,")) { return a; }
            }

            //            Log.E(">>>>>>>Error: Can\'t find Assembly-CSharp.dll");
            return null;
        }

        public static Assembly GetAssemblyCSharpEditor()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly a in assemblies) {
                if (a.FullName.StartsWith("Assembly-CSharp-Editor,")) { return a; }
            }

            //            Log.E(">>>>>>>Error: Can\'t find Assembly-CSharp-Editor.dll");
            return null;
        }

        /// <summary>
        /// 通过反射方式调用函数
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="methodName">方法名</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static object InvokeByReflect(this object obj, string methodName, params object[] args)
        {
            MethodInfo methodInfo = obj.GetType().GetMethod(methodName);
            return methodInfo == null ? null : methodInfo.Invoke(obj, args);
        }

        /// <summary>
        /// 通过反射方式获取域值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName">域名</param>
        /// <returns></returns>
        public static object GetFieldByReflect(this object obj, string fieldName)
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
        public static object GetPropertyByReflect(this object obj, string propertyName, object[] index = null)
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
