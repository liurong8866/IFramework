using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.AccessControl;
using UnityEngine;

namespace IFramework.Core
{
    /// <summary>
    /// Ioc 组件普通模式
    /// </summary>
    public abstract class IocMonoBehaviour : MonoBehaviour, IContainer
    {
        private List<Type> actions;
        
        // 通过代理类集成Ioc
        private IContainer container;

        /// <summary>
        /// 处理自动注入，禁止子类调用，子类该用Init方法
        /// </summary>
        private void Awake()
        {
            actions = new List<Type>();
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
            actions.Add(typeof(T));
            return TypeEvent.Register(action);
        }

        /// <summary>
        /// 注销某类型的某事件
        /// </summary>
        public void UnRegisterEvent<T>(Action<T> action)
        {
            TypeEvent.UnRegister(action);
        }
        
        /// <summary>
        /// 注销某类型事件
        /// </summary>
        public void UnRegisterEvent<T>()
        {
            actions.Remove(typeof(T));
            TypeEvent.UnRegister<T>();
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
        
        /// <summary>
        /// 只销毁对象，不销毁IocContainer
        /// </summary>
        public void Dispose()
        {
            DisposeEvent();
            Destroy(gameObject);
        }

        private void DisposeEvent()
        {
            foreach (Type action in actions) {
                
                // 反射调用静态方法
                ReflectionUtility.Invoke(typeof(TypeEvent), null, null, "UnRegister", new Type[] {action}, new Type[] {}, null);
                // MethodInfo staticMethod = sampleClassType.GetMethod("UnRegister");
                // staticMethod.Invoke(null, null); // 静态方法调用不需要类实例，第一个参数为null
                // // 反射调用非静态方法
                // MethodInfo nonstaticMethod = sampleClassType.GetMethod("NonstaticMethod");
                // ConstructorInfo constructor = sampleClassType.GetConstructor(Type.EmptyTypes);
                // object obj = constructor.Invoke(null); // 构造SampleClass<>的实例
                // nonstaticMethod.Invoke(obj, null);     // 非静态方法调用需要类实例
                
            }
        }
    }
}
