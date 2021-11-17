using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.AccessControl;
using JetBrains.Annotations;
using UnityEngine;

namespace IFramework.Core
{
    /// <summary>
    /// Ioc 组件普通模式
    /// </summary>
    public abstract class IocMonoBehaviour : MonoBehaviour, IContainer
    {
        private Dictionary<Type, Delegate> dictionary;
        
        // 通过代理类集成Ioc
        private IContainer container;

        /// <summary>
        /// 处理自动注入，禁止子类调用，子类该用Init方法
        /// </summary>
        private void Awake()
        {
            dictionary = new Dictionary<Type, Delegate>();
            container = IocContainer.Instance;
            OnAwake();
            container.Inject(this);
        }

        /// <summary>
        /// 等同于Awake，如果需要，请在这里注册IOC
        /// </summary>
        protected virtual void OnAwake() { }

        #region 代理方法实现IOC

        public void Register<T>(string name = null)
        {
            container.Register<T>(name);
        }

        public void Register<TBase, TTarget>(string name = null)
        {
            container.Register<TBase, TTarget>(name);
        }

        public void Register(Type baseType, Type target, string name = null)
        {
            container.Register(baseType, target, name);
        }

        public void RegisterInstance<TBase>(TBase instance)
        {
            container.RegisterInstance(instance);
        }

        public void RegisterInstance<TBase>(TBase instance, bool injectNow)
        {
            container.RegisterInstance(instance, injectNow);
        }

        public void RegisterInstance<TBase>(TBase instance, string name, bool injectNow = true)
        {
            container.RegisterInstance(instance, name, injectNow);
        }

        public void RegisterInstance(Type baseType, object instance = null, bool injectNow = true)
        {
            container.RegisterInstance(baseType, instance, injectNow);
        }

        public void RegisterInstance(Type baseType, object instance = null, string name = null, bool injectNow = true)
        {
            container.RegisterInstance(baseType, instance, name, injectNow);
        }

        public void UnRegisterInstance<T>()
        {
            container.UnRegisterInstance<T>();
        }

        public T Resolve<T>(string name = null, bool require = false, params object[] args) where T : class
        {
            return container.Resolve<T>(name, require, args);
        }

        public object Resolve(Type baseType, string name = null, bool require = false, params object[] args)
        {
            return container.Resolve(baseType, name, require, args);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return container.ResolveAll<T>();
        }

        public IEnumerable<object> ResolveAll(Type type)
        {
            return container.ResolveAll(type);
        }

        public void Inject(object obj)
        {
            container.Inject(obj);
        }

        public void InjectAll()
        {
            container.InjectAll();
        }

        public void Clear()
        {
            container.Clear();
        }

        #endregion
        
        #region 代理实现事件
        
        /// <summary>
        /// 注册事件
        /// </summary>
        public IDisposable RegisterEvent<T>(Action<T> action)
        {
            Type type = typeof(T);
            if (dictionary.TryGetValue(type, out Delegate dValue)) {
                if (dValue is Action<T> actionValue) {
                    actionValue += action;
                }
            }
            else {
                dictionary.Add(typeof(T), action);
            }
            return TypeEvent.Register<T>(action);
        }

        /// <summary>
        /// 注销某类型的某事件
        /// </summary>
        public void UnRegisterEvent<T>(Action<T> action)
        {
            Type type = typeof(T);
            if (dictionary.TryGetValue(type, out Delegate dValue)) {
                if (dValue is Action<T> actionValue) {
                    actionValue -= action;
                }
                if (dValue == null) {
                    dictionary.Remove(type);
                }
            }
            else {
                dictionary.Add(typeof(T), action);
            }
            
            TypeEvent.UnRegister(action);
        }
        
        /// <summary>
        /// 注销某类型事件
        /// </summary>
        public void UnRegisterEvent<T>()
        {
            TypeEvent.UnRegister<T>();
            dictionary.Remove(typeof(T));
        }
        
        public void UnRegisterAllEvent()
        {
            foreach (KeyValuePair<Type,Delegate> keyValuePair in dictionary) {
                ReflectionUtility.Invoke(typeof(TypeEvent), "UnRegister", new Type[] {keyValuePair.Key}, new Type[] { typeof(Action<>) }, new object[]{keyValuePair.Value});
            }
        }
        
        /// <summary>
        /// 发送事件
        /// </summary>
        public void SendEvent<T>() where T : new()
        {
            TypeEvent.Send<T>();
        }

        /// <summary>
        /// 发送事件
        /// </summary>
        public void SendEvent<T>(T param)
        {
            TypeEvent.Send(param);
        }
        
        #endregion

        protected virtual void OnDestroy()
        {
            UnRegisterAllEvent();
        }
        
        /// <summary>
        /// 只销毁对象，不销毁IocContainer
        /// </summary>
        public virtual void Dispose()
        {
            UnRegisterAllEvent();
            Destroy(gameObject);
        }
    }
}
