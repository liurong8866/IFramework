using System;
using System.Collections.Generic;
using System.Security.Claims;
using UnityEngine;

namespace IFramework.Core
{
    /// <summary>
    /// IocMonoBehaviour组件，自动注入字段、属性
    /// </summary>
    public abstract class IocMonoBehaviour : MonoBehaviour, IContainer
    {
        // 通过代理类实现Ioc
        private IContainer container;

        /// <summary>
        /// 处理自动注入，禁止子类调用，子类该用Init方法
        /// </summary>
        private void Awake()
        {
            container = IocContainer.Instance;
            Init();
            container.Inject(this);
        }

        /// <summary>
        /// 等同于Awake，如果需要，请在这里注册IOC
        /// </summary>
        protected virtual void Init(){}

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            container.Dispose();
            Destroy(gameObject);
        }

        #region 代理方法

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
    }
}
