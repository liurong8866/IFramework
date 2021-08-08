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
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// 单例实例创建类
    /// </summary>
    public class SingletonCreator
    {
        /// <summary>
        /// 通过反射创建普通类实例
        /// </summary>
        public static T CreateSingleton<T>() where T : class, ISingleton
        {
            Type type = typeof(T);
            Type monoBehaviourType = typeof(MonoBehaviour);

            // 如果是不是MonoBehaviour类型的类，则使用通用实例方法
            if (!monoBehaviourType.IsAssignableFrom(type))
            {
                T instance = ObjectFactory.Create<T>();
                instance.OnInit();
                return instance;
            }
            // 使用MonoBehaviour实例化方法
            else
            {
                return CreateMonoSingleton<T>();
            }
        }

        /// <summary>
        /// 通过反射创建MonoBehaviour类实例
        /// </summary>
        public static T CreateMonoSingleton<T>() where T : class, ISingleton
        {
            return null;
        }
        
        
        
        
        
    }
}