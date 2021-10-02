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

namespace IFramework.Core
{
    public class ObjectFactory
    {
        /// <summary>
        /// 无参数构造函数类实例
        /// </summary>
        /// <returns></returns>
        public static T Create<T>() where T : class
        {
            return Activator.CreateInstance(typeof(T)) as T;
        }

        /// <summary>
        /// 有参数构造函数类实例
        /// </summary>
        /// <param name="args">需要实例化的类构造函数的参数，根据参数不同调用不同构造函数</param>
        /// <typeparam name="T">要实例化的类</typeparam>
        /// <returns></returns>
        public static T Create<T>(params object[] args) where T : class
        {
            return Activator.CreateInstance(typeof(T), args) as T;
        }

        /// <summary>
        /// 动态创建类的实例：创建无参/私有的构造函数  泛型扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T CreateNoPublicConstructor<T>() where T : class
        {
            // 获取私有构造函数
            var constructorInfos = typeof(T).GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            // 获取无参构造函数
            var ctor = Array.Find(constructorInfos, c => c.GetParameters().Length == 0);

            if (ctor == null) {
                throw new Exception("未找到无参私有构造函数: " + typeof(T));
            }
            return ctor.Invoke(null) as T;
        }
    }
}
