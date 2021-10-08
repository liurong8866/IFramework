using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace IFramework.Core
{
    public class TypeEvent : ITypeEvent
    {
        // 事件字典
        private readonly Dictionary<Type, ITypeEventRegister> typeEventDict = DictionaryPool<Type, ITypeEventRegister>.Allocate();

        /// <summary>
        /// 注册事件
        /// </summary>
        public IDisposable RegisterEvent<T>([NotNull] Action<T> action)
        {
            Type type = typeof(T);

            if (typeEventDict.TryGetValue(type, out ITypeEventRegister register)) {
                if (register is TypeEventRegister<T> reg) { reg.actions += action; }
            }
            else {
                TypeEventRegister<T> reg = new TypeEventRegister<T>();
                reg.actions = action;
                typeEventDict.Add(type, reg);
            }

            return new TypeEventUnregister<T> {
                actions = action,
                typeEvent = this
            };
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        public void UnRegisterEvent<T>(Action<T> action)
        {
            Type type = typeof(T);

            if (typeEventDict.TryGetValue(type, out ITypeEventRegister register)) {
                if (register is TypeEventRegister<T> reg) {
                    // ReSharper disable once DelegateSubtraction
                    reg.actions -= action;
                }
            }
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        public void SendEvent<T>() where T : new()
        {
            Type type = typeof(T);

            if (typeEventDict.TryGetValue(type, out ITypeEventRegister register)) {
                if (register is TypeEventRegister<T> reg) { reg.actions(new T()); }
            }
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        public void SendEvent<T>(T t)
        {
            Type type = typeof(T);

            if (typeEventDict.TryGetValue(type, out ITypeEventRegister register)) {
                if (register is TypeEventRegister<T> reg) { reg.actions(t); }
            }
        }

        /// <summary>
        /// 清空事件
        /// </summary>
        public void Clear()
        {
            foreach (KeyValuePair<Type, ITypeEventRegister> keyValue in typeEventDict) { keyValue.Value.Dispose(); }
            typeEventDict.Clear();
        }

        /// <summary>
        /// 回收方法
        /// </summary>
        public void Dispose() { }

        /*----------------------------*/
        /* 静态方法调用单例方法           */
        /*----------------------------*/

        // 全局注册事件
        private static readonly ITypeEvent eventer = new TypeEvent();

        /// <summary>
        /// 注册事件
        /// </summary>
        public static IDisposable Register<T>(Action<T> action)
        {
            return eventer.RegisterEvent(action);
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        public static void UnRegister<T>(Action<T> action)
        {
            eventer.UnRegisterEvent(action);
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
            eventer.SendEvent(t);
        }
    }
}
