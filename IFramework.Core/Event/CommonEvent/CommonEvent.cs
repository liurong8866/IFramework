using System;
using System.Collections.Generic;

namespace IFramework.Core
{
    /// <summary>
    /// 基于字典的消息事件
    /// </summary>
    public class CommonEvent<TEvent> : Singleton<CommonEvent<TEvent>>, IPoolable where TEvent : Delegate
    {
        // 全局监听事件字典
        private readonly Dictionary<int, EventListener<TEvent>> listenerMap = new Dictionary<int, EventListener<TEvent>>(50);

        // 单例私有构造函数
        protected CommonEvent() { }

        /// <summary>
        /// 注册事件
        /// </summary>
        public bool RegisterEvent<T>(T key, TEvent action) where T : IConvertible
        {
            int keyValue = key.ToInt32(null);

            if (!listenerMap.TryGetValue(keyValue, out EventListener<TEvent> listener)) {
                listener = new EventListener<TEvent>();
                listenerMap.Add(keyValue, listener);
            }
            return listener.Add(action);
        }

        /// <summary>
        /// 取消注册某一事件
        /// </summary>
        public void UnRegisterEvent<T>(T key, TEvent action) where T : IConvertible
        {
            if (listenerMap.TryGetValue(key.ToInt32(null), out EventListener<TEvent> listener)) {
                listener?.Remove(action);
            }
        }

        /// <summary>
        /// 取消注册某一类型事件
        /// </summary>
        public void UnRegisterEvent<T>(T key) where T : IConvertible
        {
            int keyValue = key.ToInt32(null);

            if (listenerMap.TryGetValue(keyValue, out EventListener<TEvent> listener)) {
                listener?.Clear();
                listener = null;
                listenerMap.Remove(keyValue);
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public bool SendEvent<T>(T key) where T : IConvertible
        {
            int keyValue = key.ToInt32(null);

            if (listenerMap.TryGetValue(keyValue, out EventListener<TEvent> listener)) {
                if (listener != null) {
                    return listener.Invoke(keyValue);
                }
            }
            return false;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        public bool SendEvent<T>(T key, params object[] param) where T : IConvertible
        {
            int keyValue = key.ToInt32(null);

            if (listenerMap.TryGetValue(keyValue, out EventListener<TEvent> listener)) {
                if (listener != null) {
                    return listener.Invoke(keyValue, param);
                }
            }
            return false;
        }

        /// <summary>
        /// 回收资源
        /// </summary>
        public void OnRecycled()
        {
            listenerMap.Clear();
        }

        public bool IsRecycled { get; set; }

        /*----------------------------*/
        /* 静态方法调用单例方法           */
        /*----------------------------*/

        /// <summary>
        /// 发送无参数消息
        /// </summary>
        public static bool Send<T>(T key) where T : IConvertible
        {
            return Instance.SendEvent(key);
        }

        /// <summary>
        /// 发送有参数消息
        /// </summary>
        public static bool Send<T>(T key, params object[] param) where T : IConvertible
        {
            return Instance.SendEvent(key, param);
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        public static bool Register<T>(T key, TEvent action) where T : IConvertible
        {
            return Instance.RegisterEvent(key, action);
        }

        /// <summary>
        /// 取消注册某一事件
        /// </summary>
        public static void UnRegister<T>(T key, TEvent action) where T : IConvertible
        {
            Instance.UnRegisterEvent(key, action);
        }

        /// <summary>
        /// 取消注册某一类型事件
        /// </summary>
        public static void UnRegister<T>(T key) where T : IConvertible
        {
            Instance.UnRegisterEvent(key);
        }
    }

    /// <summary>
    /// 事件监听消息
    /// </summary>
    internal sealed class EventListener<T> where T : Delegate
    {
        private LinkedList<T> eventList;

        // 调用方法
        public bool Invoke(int key, params object[] param)
        {
            if (eventList == null || eventList.Count == 0) {
                return false;
            }
            LinkedListNode<T> next = eventList.First;

            // 依次执行所有监听的方法
            while (next != null) {
                // 取得当前事件
                T action = next.Value;

                // 先于事件执行，记录下一级事件，避免在运行事件时取消注册事件（猜测）
                LinkedListNode<T> nextCache = next.Next;

                // 执行事件
                (param.Length == 0).iif(() => action.InvokeSafe(key), () => action.InvokeSafe(key, param));

                // 如果next事件丢失，可以使用缓存的指针指向该事件
                next = next.Next ?? nextCache;
            }
            return true;
        }

        // 添加监听消息
        public bool Add(T listener)
        {
            eventList ??= new LinkedList<T>();
            if (eventList.Contains(listener)) return false;

            eventList.AddLast(listener);
            return true;
        }

        // 移除监听消息
        public void Remove(T listener)
        {
            if (eventList != null && eventList.Count > 0) {
                eventList.Remove(listener);
            }
        }

        // 清空所有监听消息
        public void Clear()
        {
            if (eventList != null && eventList.Count > 0) {
                eventList.Clear();
            }
        }
    }
}
