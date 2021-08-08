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

namespace IFramework.Engine
{
    public class ObjectFactory
    {
        /// <summary>
        /// 无参数构造函数类实例
        /// </summary>
        /// <param name="type">要实例化的类</param>
        /// <returns></returns>
        public static T Create<T>(Type type) where T : class
        {
            return Activator.CreateInstance(type) as T;
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
    }
}