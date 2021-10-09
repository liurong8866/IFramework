using System;
using System.Collections.Generic;

namespace IFramework.Core
{
    /// <summary>
    /// IOC容器接口
    /// </summary>
    public interface IFrameworkContainer : IDisposable
    {
        TypeMappingCollection Mappings { get; set; }
        TypeInstanceCollection Instances { get; set; }
        TypeRelationCollection RelationshipMappings { get; set; }

        /// <summary>
        /// 注入 已注册的 类型/映射 到一个对象中
        /// </summary>
        void Inject(object obj);

        /// <summary>
        /// 注入一次所有已注册过的类型实例
        /// </summary>
        void InjectAll();

        /// <summary>
        /// 注册依赖
        /// </summary>
        /// <typeparam name="TSource">父类型（抽象类型）</typeparam>
        /// <typeparam name="TTarget">子类型（具体类型）</typeparam>
        void Register<TSource, TTarget>(string name = null);

        /// <summary>
        /// 注册关系
        /// </summary>
        /// <typeparam name="TFor"></typeparam>
        /// <typeparam name="TBase"></typeparam>
        /// <typeparam name="TConcrete"></typeparam>
        void RegisterRelation<TFor, TBase, TConcrete>();

        void RegisterRelation(Type tfor, Type tbase, Type tconcrete);

        /// <summary>
        /// 为一个类型注册一个实例
        /// </summary>
        /// <typeparam name="TBase"></typeparam>
        /// <param name="default"></param>
        /// <param name="injectNow"></param>
        /// <returns></returns>
        void RegisterInstance<TBase>(TBase @default, bool injectNow);

        /// <summary>
        /// 为一个类型注册一个示例
        /// </summary>
        /// <param name="type"></param>
        /// <param name="default"></param>
        /// <param name="injectNow"></param>
        /// <returns></returns>
        void RegisterInstance(Type type, object @default, bool injectNow);

        /// <summary>
        /// 为一个类型注册一个示例
        /// </summary>
        /// <param name="baseType">The type to register the instance for.</param>
        /// <param name="name">The name for the instance to be resolved.</param>
        /// <param name="instance">The instance that will be resolved be the name</param>
        /// <param name="injectNow">Perform the injection immediately</param>
        void RegisterInstance(Type baseType, object instance = null, string name = null, bool injectNow = true);

        void RegisterInstance<TBase>(TBase instance);

        /// <summary>
        /// 为一个类型注册一个示例
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="name"></param>
        /// <param name="injectNow"></param>
        /// <typeparam name="TBase"></typeparam>
        void RegisterInstance<TBase>(TBase instance, string name, bool injectNow = true);

        /// <summary>
        /// 取消注册
        /// </summary>
        /// <typeparam name="TBase"></typeparam>
        void UnRegisterInstance<TBase>();

        /// <summary>
        ///  If an instance of T exist then it will return that instance otherwise it will create a new one based off mappings.
        /// </summary>
        /// <typeparam name="T">The type of instance to resolve</typeparam>
        /// <returns>The/An instance of 'instanceType'</returns>
        T Resolve<T>(string name = null, bool requireInstance = false, params object[] args) where T : class;

        TBase ResolveRelation<TBase>(Type tfor, params object[] arg);

        TBase ResolveRelation<TFor, TBase>(params object[] arg);

        /// <summary>
        /// Resolves all instances of TType or subclasses of TType.  Either named or not.
        /// </summary>
        /// <typeparam name="TType">The Type to resolve</typeparam>
        /// <returns>List of objects.</returns>
        IEnumerable<TType> ResolveAll<TType>();

        //IEnumerable<object> ResolveAll(Type type);
        void Register(Type source, Type target, string name = null);

        /// <summary>
        /// Resolves all instances of TType or subclasses of TType.  Either named or not.
        /// </summary>
        /// <returns>List of objects.</returns>
        IEnumerable<object> ResolveAll(Type type);

        /// <summary>
        /// If an instance of instanceType exist then it will return that instance otherwise it will create a new one based off mappings.
        /// </summary>
        /// <param name="baseType">The type of instance to resolve</param>
        /// <param name="name">The type of instance to resolve</param>
        /// <param name="requireInstance">If true will return null if an instance isn't registered.</param>
        /// <returns>The/An instance of 'instanceType'</returns>
        object Resolve(Type baseType, string name = null, bool requireInstance = false, params object[] constructorArgs);

        object ResolveRelation(Type tfor, Type tbase, params object[] arg);

        
        object CreateInstance(Type type, params object[] args);
        
        /// <summary>
        /// 清除掉所有的已经注册的实例和类型
        /// </summary>
        void Clear();

    }
}
