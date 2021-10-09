using System;
using System.Collections.Generic;

namespace IFramework.Core
{
    /// <summary>
    /// IOC容器接口
    /// </summary>
    public interface IFrameworkContainer : IDisposable
    {
        // public TypeMapping Mappings { get; set; }
        //
        // public TypeInstanceMapping Instances { get; set; }

        /*----------------------------- Register -----------------------------*/

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="T">基础类</typeparam>
        public void Register<T>(string name = null);

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="name">目标实现类名称，用于区别多个实现</param>
        /// <typeparam name="TBase">基础类</typeparam>
        /// <typeparam name="TTarget">目标类</typeparam>
        public void Register<TBase, TTarget>(string name = null);
        
        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="baseType">基础类</param>
        /// <param name="target">目标类</param>
        /// <param name="name">目标实现类名称，用于区别多个实现</param>
        public void Register(Type baseType, Type target, string name = null);

        /// <summary>
        /// 注册实例
        /// </summary>
        /// <param name="instance">目标实例</param>
        /// <typeparam name="TBase">基础类</typeparam>
        public void RegisterInstance<TBase>(TBase instance);

        /// <summary>
        /// 注册实例
        /// </summary>
        /// <param name="instance">目标实例</param>
        /// <param name="injectNow">是否立即注入</param>
        /// <typeparam name="TBase">基础类</typeparam>
        public void RegisterInstance<TBase>(TBase instance, bool injectNow);

        /// <summary>
        /// 注册实例
        /// </summary>
        /// <param name="instance">目标实例</param>
        /// <param name="name">目标实现类名称，用于区别多个实现</param>
        /// <param name="injectNow">是否立即注入</param>
        /// <typeparam name="TBase">基础类</typeparam>
        public void RegisterInstance<TBase>(TBase instance, string name, bool injectNow = true);

        /// <summary>
        /// 注册实例
        /// </summary>
        /// <param name="baseType">基础类</param>
        /// <param name="instance">目标实例</param>
        /// <param name="injectNow">是否立即注入</param>
        public void RegisterInstance(Type baseType, object instance = null, bool injectNow = true);

        /// <summary>
        /// 注册实例
        /// </summary>
        /// <param name="baseType">基础类</param>
        /// <param name="instance">目标实例</param>
        /// <param name="name">目标实现类名称，用于区别多个实现</param>
        /// <param name="injectNow">立即注入</param>
        public void RegisterInstance(Type baseType, object instance = null, string name = null, bool injectNow = true);

        /// <summary>
        /// 取消注册
        /// </summary>
        /// <typeparam name="T">基础类</typeparam>
        public void UnRegisterInstance<T>();
        
        /*----------------------------- Resolve -----------------------------*/

        /// <summary>
        /// 解析实例
        /// </summary>
        /// <param name="name"></param>
        /// <param name="require">
        ///     <value>true：未注册时返回null</value>
        ///     <value>false: 未注册时创建实例</value>
        /// </param>
        /// <param name="args">构造函数参数</param>
        /// <typeparam name="T">基础类</typeparam>
        /// <returns></returns>
        public T Resolve<T>(string name = null, bool require = false, params object[] args) where T : class;

        /// <summary>
        /// 解析实例
        /// </summary>
        /// <param name="baseType">基础类</param>
        /// <param name="name">目标实现类名称</param>
        /// <param name="require">
        ///     <value>true：未注册时返回null</value>
        ///     <value>false: 未注册时创建实例</value>
        /// </param>
        /// <param name="args">构造函数参数</param>
        public object Resolve(Type baseType, string name = null, bool require = false, params object[] args);

        /// <summary>
        /// 解析所有实例
        /// </summary>
        /// <typeparam name="T">基础类</typeparam>
        /// <returns></returns>
        public IEnumerable<T> ResolveAll<T>();

        /// <summary>
        /// 解析所有类型、子类的实例
        /// </summary>
        /// <param name="type">基础类</param>
        /// <returns></returns>
        public IEnumerable<object> ResolveAll(Type type);
        
        /// <summary>
        /// 将注册类型/映射注入对象
        /// </summary>
        /// <param name="obj">注入的实例</param>
        public void Inject(object obj);

        /// <summary>
        /// 注入所有实例
        /// </summary>
        public void InjectAll();

        /// <summary>
        /// 清除所有注册映射关系
        /// </summary>
        public void Clear();
    }
}
