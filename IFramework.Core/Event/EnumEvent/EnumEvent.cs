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

namespace IFramework.Core
{
    // 事件代理
    public delegate void OnEvent(int key, params object[] param);
    
    /// <summary>
    /// 基于字典的消息事件
    /// </summary>
    public class EnumEvent : Singleton<EnumEvent>, IPoolable
    {
        // 全局监听事件字典
        private readonly Dictionary<int, EventListener> listenerMap = new Dictionary<int, EventListener>(50);
        
        // 单例私有构造函数
        private EnumEvent() {}

        /// <summary>
        /// 注册事件
        /// </summary>
        public bool RegisterEvent<T>(T key, OnEvent action) where T : IConvertible
        {
            var keyValue = key.ToInt32(null);

            if (!listenerMap.TryGetValue(keyValue, out var listener))
            {
                listener = new EventListener();
                listenerMap.Add(keyValue, listener);
            }

            return listener.Add(action);
        }

        /// <summary>
        /// 取消注册某一事件
        /// </summary>
        public void UnRegisterEvent<T>(T key, OnEvent action) where T : IConvertible
        {
            if (listenerMap.TryGetValue(key.ToInt32(null), out var listener))
            {
                listener?.Remove(action);
            }
        }
        
        /// <summary>
        /// 取消注册某一类型事件
        /// </summary>
        public void UnRegisterEvent<T>(T key) where T : IConvertible
        {
            var keyValue = key.ToInt32(null);
            
            if (listenerMap.TryGetValue(keyValue, out var listener))
            {
                listener?.Clear();
                listener = null;
                
                listenerMap.Remove(keyValue);
            }
        }
        
        /// <summary>
        /// 发送消息
        /// </summary>
        public bool SendEvent<T>(T key, params object[] param) where T : IConvertible
        {
            int keyValue = key.ToInt32(null);
            
            if (listenerMap.TryGetValue(keyValue, out var listener))
            {
                if (listener != null)
                {
                    return listener.Invoke(keyValue, param);
                }
            }
            return false;
        }
        
        public void OnRecycled()
        {
            listenerMap.Clear();
        }

        public bool IsRecycled { get; set; }
        
        /* 静态方法调用单例方法 */
        
        public static bool Send<T>(T key, params object[] param) where T : IConvertible
        {
            return Instance.SendEvent(key, param);
        }

        public static bool Register<T>(T key, OnEvent action) where T : IConvertible
        {
            return Instance.RegisterEvent(key, action);
        }

        public static void UnRegister<T>(T key, OnEvent action) where T : IConvertible
        {
            Instance.UnRegisterEvent(key, action);
        }
        
        public static void UnRegister<T>(T key) where T : IConvertible
        {
            Instance.UnRegisterEvent(key);
        }
    }

    /// <summary>
    /// 事件监听消息
    /// </summary>
    class EventListener
    {
        private LinkedList<OnEvent> eventList;

        // 调用方法
        public bool Invoke(int key, params object[] param)
        {
            if (eventList == null || eventList.Count == 0)
            {
                return false;
            }

            OnEvent action = null;
            LinkedListNode<OnEvent> next = eventList.First;
            LinkedListNode<OnEvent> nextCache = null;

            // 依次执行所有监听的方法
            while (next != null)
            {
                // 取得当前事件
                action = next.Value;
                // 先于事件执行，记录下一级事件，避免在运行事件时取消注册事件（猜测）
                nextCache = next.Next;
                // 执行事件
                action(key, param);
                // 如果next事件丢失，可以使用缓存的指针指向该事件
                next = next.Next ?? nextCache;
            }

            return true;
        }
        
        // 添加监听消息
        public bool Add(OnEvent listener)
        {
            if (eventList == null)
            {
                eventList = new LinkedList<OnEvent>();
            }

            if (eventList.Contains(listener))
            {
                return false;
            }
            
            eventList.AddLast(listener);
            return true;
        }

        // 移除监听消息
        public void Remove(OnEvent listener)
        {
            if (eventList != null && eventList.Count > 0)
            {
                eventList.Remove(listener);
            }
        }
        
        // 清空所有监听消息
        public void Clear()
        {
            if (eventList != null && eventList.Count > 0)
            {
                eventList.Clear();
            }
        }
    }
}