using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace IFramework.Core
{
    public class TypeEvent : ITypeEvent
    {
        // 事件字典
        private readonly Dictionary<Type, IEventRegister> typeEventDict = DictionaryPool<Type, IEventRegister>.Allocate();

        /// <summary>
        /// 注册事件
        /// </summary>
        public IDisposable RegisterEvent<T>([NotNull] Action<T> action)
        {
            Type type = typeof(T);
            if (typeEventDict.TryGetValue(type, out IEventRegister register)) {
                if (register is EventRegister<T> reg) {
                    reg.actions += action;
                }
            }
            else {
                EventRegister<T> reg = new EventRegister<T>();
                reg.actions = action;
                typeEventDict.Add(type, reg);
            }
            return new EventUnregister<T> {
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
            if (typeEventDict.TryGetValue(type, out IEventRegister register)) {
                if (register is EventRegister<T> reg) {
                    // ReSharper disable once DelegateSubtraction
                    reg.actions -= action;

                    // 避免NullException
                    if (reg.actions == null) {
                        reg.actions = obj => { };
                    }
                }
            }
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        public void SendEvent<T>() where T : new()
        {
            Type type = typeof(T);
            if (typeEventDict.TryGetValue(type, out IEventRegister register)) {
                if (register is EventRegister<T> reg) {
                    reg.actions(new T());
                }
            }
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        public void SendEvent<T>(T param)
        {
            Type type = typeof(T);
            if (typeEventDict.TryGetValue(type, out IEventRegister register)) {
                if (register is EventRegister<T> reg) {
                    reg.actions(param);
                }
            }
        }

        /// <summary>
        /// 清空事件
        /// </summary>
        public void Clear()
        {
            foreach (KeyValuePair<Type, IEventRegister> keyValue in typeEventDict) {
                keyValue.Value.Dispose();
            }
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
        private static readonly ITypeEvent typeEvent = new TypeEvent();

        /// <summary>
        /// 注册事件
        /// </summary>
        public static IDisposable Register<T>(Action<T> action)
        {
            return typeEvent.RegisterEvent(action);
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        public static void UnRegister<T>(Action<T> action)
        {
            typeEvent.UnRegisterEvent(action);
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        public static void Send<T>() where T : new()
        {
            typeEvent.SendEvent<T>();
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        public static void Send<T>(T param)
        {
            typeEvent.SendEvent(param);
        }
    }
}
