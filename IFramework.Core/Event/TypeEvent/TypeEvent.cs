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
using System.Collections.Generic;
using UnityEngine;

namespace IFramework.Core
{
    public class TypeEvent : ITypeEvent
    {
        // 事件字典
        private readonly Dictionary<Type, ITypeEventRegister> typeEventDict = DictionaryPool<Type, ITypeEventRegister>.Get();

        /// <summary>
        /// 注册事件
        /// </summary>
        public IDisposable RegisterEvent<T>(Action<T> action)
        {
            Type type = typeof(T);
            
            if (typeEventDict.TryGetValue(type, out ITypeEventRegister register))
            {
                if (register is TypeEventRegister<T> reg)
                {
                    reg.Actions += action;
                }
            }
            else
            {
                TypeEventRegister<T> reg= new TypeEventRegister<T>();
                reg.Actions += action;
                typeEventDict.Add(type, reg);
            }
            return new TypeEventUnregister<T> {Actions = action, TypeEvent = this};
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        public void UnRegisterEvent<T>(Action<T> action)
        {
            Type type = typeof(T);
            
            if (typeEventDict.TryGetValue(type, out ITypeEventRegister register))
            {
                if (register is TypeEventRegister<T> reg)
                {
                    reg.Actions -= action;
                }
            }
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        public void SendEvent<T>() where T : new()
        {
            Type type = typeof(T);
            
            if (typeEventDict.TryGetValue(type, out ITypeEventRegister register))
            {
                if (register is TypeEventRegister<T> reg)
                {
                    reg.Actions(new T());
                }
            }
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        public void SendEvent<T>(T t)
        {
            Type type = typeof(T);
            
            if (typeEventDict.TryGetValue(type, out ITypeEventRegister register))
            {
                if (register is TypeEventRegister<T> reg)
                {
                    reg.Actions(t);
                }
            }
        }

        /// <summary>
        /// 清空事件
        /// </summary>
        public void Clear()
        {
            foreach (var keyValue in typeEventDict)
            {
                keyValue.Value.Dispose();
            }
            typeEventDict.Clear();
        }

        /// <summary>
        /// 回收
        /// </summary>
        public void Dispose(){ }
        
        
        /* 静态方法调用单例方法 */
        
        
        // 全局注册事件
        private static readonly ITypeEvent eventer = new TypeEvent();
        
        /// <summary>
        /// 注册事件
        /// </summary>
        public static IDisposable Register<T>(System.Action<T> action)
        {
            return eventer.RegisterEvent<T>(action);
        }
        
        /// <summary>
        /// 注销事件
        /// </summary>
        public static void UnRegister<T>(System.Action<T> action)
        {
            eventer.UnRegisterEvent<T>(action);
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        public static void Send<T>() where T : new()
        {
            eventer.SendEvent<T>();
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        public static void Send<T>(T t)
        {
            eventer.SendEvent<T>(t);
        }
        
    }
}