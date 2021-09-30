/*****************************************************************************
 * MIT License
 * 
 * Copyright (c) 2021 liurong
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *****************************************************************************/

using System;
using System.Reflection;

namespace IFramework.Core {
    /// <summary>
    /// C# 反射类扩展方法
    /// </summary>
    public static class ReflectionExtension {

        public static void Example() {
            // var selfType = ReflectionExtension.GetAssemblyCSharp().GetType("IFramework.Editor.ReflectionExtension");
            // selfType.LogInfo();
        }

        public static Assembly GetAssemblyCSharp() {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var a in assemblies) {
                if (a.FullName.StartsWith("Assembly-CSharp,")) {
                    return a;
                }
            }

//            Log.E(">>>>>>>Error: Can\'t find Assembly-CSharp.dll");
            return null;
        }

        public static Assembly GetAssemblyCSharpEditor() {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var a in assemblies) {
                if (a.FullName.StartsWith("Assembly-CSharp-Editor,")) {
                    return a;
                }
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
        public static object InvokeByReflect(this object obj, string methodName, params object[] args) {
            var methodInfo = obj.GetType().GetMethod(methodName);
            return methodInfo == null ? null : methodInfo.Invoke(obj, args);
        }

        /// <summary>
        /// 通过反射方式获取域值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="fieldName">域名</param>
        /// <returns></returns>
        public static object GetFieldByReflect(this object obj, string fieldName) {
            var fieldInfo = obj.GetType().GetField(fieldName);
            return fieldInfo == null ? null : fieldInfo.GetValue(obj);
        }

        /// <summary>
        /// 通过反射方式获取属性
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="propertyName"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static object GetPropertyByReflect(this object obj, string propertyName, object[] index = null) {
            var propertyInfo = obj.GetType().GetProperty(propertyName);
            return propertyInfo == null ? null : propertyInfo.GetValue(obj, index);
        }

        /// <summary>
        /// 拥有特性
        /// </summary>
        /// <returns></returns>
        public static bool HasAttribute(this PropertyInfo prop, Type attributeType, bool inherit) {
            return prop.GetCustomAttributes(attributeType, inherit).Length > 0;
        }

        /// <summary>
        /// 拥有特性
        /// </summary>
        /// <returns></returns>
        public static bool HasAttribute(this FieldInfo field, Type attributeType, bool inherit) {
            return field.GetCustomAttributes(attributeType, inherit).Length > 0;
        }

        /// <summary>
        /// 拥有特性
        /// </summary>
        /// <returns></returns>
        public static bool HasAttribute(this Type type, Type attributeType, bool inherit) {
            return type.GetCustomAttributes(attributeType, inherit).Length > 0;
        }

        /// <summary>
        /// 拥有特性
        /// </summary>
        /// <returns></returns>
        public static bool HasAttribute(this MethodInfo method, Type attributeType, bool inherit) {
            return method.GetCustomAttributes(attributeType, inherit).Length > 0;
        }

        /// <summary>
        /// 获取第一个特性
        /// </summary>
        /// <param name="method"></param>
        /// <param name="inherit"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetFirstAttribute<T>(this MethodInfo method, bool inherit) where T : Attribute {
            var attrs = (T[]) method.GetCustomAttributes(typeof(T), inherit);
            if (attrs.Length > 0)
                return attrs[0];
            return null;
        }

        /// <summary>
        /// 获取第一个特性
        /// </summary>
        public static T GetFirstAttribute<T>(this FieldInfo field, bool inherit) where T : Attribute {
            var attrs = (T[]) field.GetCustomAttributes(typeof(T), inherit);
            if (attrs.Length > 0)
                return attrs[0];
            return null;
        }

        /// <summary>
        /// 获取第一个特性
        /// </summary>
        public static T GetFirstAttribute<T>(this PropertyInfo prop, bool inherit) where T : Attribute {
            var attrs = (T[]) prop.GetCustomAttributes(typeof(T), inherit);
            if (attrs.Length > 0)
                return attrs[0];
            return null;
        }

        /// <summary>
        /// 获取第一个特性
        /// </summary>
        public static T GetFirstAttribute<T>(this Type type, bool inherit) where T : Attribute {
            var attrs = (T[]) type.GetCustomAttributes(typeof(T), inherit);
            if (attrs.Length > 0)
                return attrs[0];
            return null;
        }

    }
}
