using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace IFramework.Core
{
    /// <summary>
    /// Ioc容器，单例模式，基于IContainer
    /// </summary>
    public abstract class IocSingleton<T> : IContainer, ISingleton where T : IocSingleton<T>
    {
        // 通过代理类实现Ioc
        private IContainer container;
        
        public static T Instance => SingletonProperty<T>.Instance;
        
        public void OnInit()
        {
            container = IocContainer.Instance;
            Init();
            container.Inject(this);
        }

        /// <summary>
        /// 注册ioc
        /// </summary>
        public abstract void Init();

        public void Dispose()
        {
            container.Dispose();
            container = null;
        }

        public void Register<T1>(string name = null)
        {
            container.Register<T1>(name);
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

        public void UnRegisterInstance<T1>()
        {
            container.UnRegisterInstance<T1>();
        }

        public T1 Resolve<T1>(string name = null, bool require = false, params object[] args) where T1 : class
        {
            return container.Resolve<T1>(name, require, args);
        }

        public object Resolve(Type baseType, string name = null, bool require = false, params object[] args)
        {
            return container.Resolve(baseType, name, require, args);
        }

        public IEnumerable<T1> ResolveAll<T1>()
        {
            return container.ResolveAll<T1>();
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
    }
}
