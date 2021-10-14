using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace IFramework.Core
{
    public abstract class AbstractContainer : IContainer
    {
        protected TypeMapping Mappings { get; set; } = new TypeMapping();

        protected TypeInstanceMapping Instances { get; set; } = new TypeInstanceMapping();

        /*----------------------------- Register -----------------------------*/

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <typeparam name="T">基础类</typeparam>
        public void Register<T>(string name = null)
        {
            Mappings[typeof(T), name] = typeof(T);
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="name">目标实现类名称，用于区别多个实现</param>
        /// <typeparam name="TBase">基础类</typeparam>
        /// <typeparam name="TTarget">目标类</typeparam>
        public void Register<TBase, TTarget>(string name = null)
        {
            Mappings[typeof(TBase), name] = typeof(TTarget);
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="baseType">基础类</param>
        /// <param name="target">目标类</param>
        /// <param name="name">目标实现类名称，用于区别多个实现</param>
        public void Register(Type baseType, Type target, string name = null)
        {
            Mappings[baseType, name] = target;
        }

        /// <summary>
        /// 注册实例
        /// </summary>
        /// <param name="instance">目标实例</param>
        /// <typeparam name="TBase">基础类</typeparam>
        public void RegisterInstance<TBase>(TBase instance)
        {
            RegisterInstance(instance, true);
        }

        /// <summary>
        /// 注册实例
        /// </summary>
        /// <param name="instance">目标实例</param>
        /// <param name="injectNow">是否立即注入</param>
        /// <typeparam name="TBase">基础类</typeparam>
        public void RegisterInstance<TBase>(TBase instance, bool injectNow)
        {
            RegisterInstance(instance, null, injectNow);
        }

        /// <summary>
        /// 注册实例
        /// </summary>
        /// <param name="instance">目标实例</param>
        /// <param name="name">目标实现类名称，用于区别多个实现</param>
        /// <param name="injectNow">是否立即注入</param>
        /// <typeparam name="TBase">基础类</typeparam>
        public void RegisterInstance<TBase>(TBase instance, string name, bool injectNow = true)
        {
            RegisterInstance(typeof(TBase), instance, name, injectNow);
        }

        /// <summary>
        /// 注册实例
        /// </summary>
        /// <param name="baseType">基础类</param>
        /// <param name="instance">目标实例</param>
        /// <param name="injectNow">是否立即注入</param>
        public void RegisterInstance(Type baseType, object instance = null, bool injectNow = true)
        {
            RegisterInstance(baseType, instance, null, injectNow);
        }

        /// <summary>
        /// 注册实例
        /// </summary>
        /// <param name="baseType">基础类</param>
        /// <param name="instance">目标实例</param>
        /// <param name="name">目标实现类名称，用于区别多个实现</param>
        /// <param name="injectNow">立即注入</param>
        public virtual void RegisterInstance(Type baseType, object instance = null, string name = null, bool injectNow = true)
        {
            Instances[baseType, name] = instance;
            if (injectNow) {
                Inject(instance);
            }
        }

        /// <summary>
        /// 取消注册
        /// </summary>
        /// <typeparam name="T">基础类</typeparam>
        public void UnRegisterInstance<T>()
        {
            Tuple<Type, string> key = Instances.Keys.SingleOrDefault(k => k.Item1 == typeof(T));
            if (key != null) {
                Instances.Remove(key);
            }
        }

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
        public T Resolve<T>(string name = null, bool require = false, params object[] args) where T : class
        {
            // 如果实例存在，则它将返回该实例，否则它将基于映射创建一个新的实例。
            return (T)Resolve(typeof(T), name, require, args);
        }

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
        public object Resolve(Type baseType, string name = null, bool require = false, params object[] args)
        {
            // 在实例中查找
            object item = Instances[baseType, name];
            if (item != null) {
                return item;
            }
            if (require) return null;

            // 在类型映射中查找
            Type namedMapping = Mappings[baseType, name];

            // 如果实例存在，则它将返回该实例，否则它将基于映射创建一个新的实例。
            return namedMapping != null ? CreateInstance(namedMapping, args) : null;
        }

        /// <summary>
        /// 解析所有实例
        /// </summary>
        /// <typeparam name="T">基础类</typeparam>
        /// <returns></returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            foreach (object obj in ResolveAll(typeof(T))) {
                yield return (T)obj;
            }
        }

        /// <summary>
        /// 解析所有类型、子类的实例
        /// </summary>
        /// <param name="type">基础类</param>
        /// <returns></returns>
        public IEnumerable<object> ResolveAll(Type type)
        {
            // 遍历实例字典
            foreach (KeyValuePair<Tuple<Type, string>, object> instance in Instances) {
                // 如果类型相同，并且名称不为空， 说明已解析完毕，返回并处理下一个
                if (instance.Key.Item1 == type && instance.Key.Item2.NotEmpty()) {
                    yield return instance.Value;
                }
            }

            // 遍历类字典
            foreach (KeyValuePair<Tuple<Type, string>, Type> entry in Mappings) {
                // 如果名称不是空
                bool condition = type.IsAssignableFrom(entry.Key.Item1);
                if (condition) {
                    object item = Activator.CreateInstance(entry.Value);
                    Inject(item);
                    yield return item;
                }
            }
        }

        /// <summary>
        /// 创建实例
        /// </summary>
        /// <param name="type"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private object CreateInstance(Type type, params object[] args)
        {
            // 如果有参数，则
            if (args != null && args.Length > 0) {
                object obj2 = Activator.CreateInstance(type, args);
                Inject(obj2);
                return obj2;
            }

            // 获取类型构造方法
            ConstructorInfo[] constructor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance);

            // 如果没有构造方法
            if (constructor.Length == 1) {
                object obj2 = Activator.CreateInstance(type);
                Inject(obj2);
                return obj2;
            }

            // 取第一个构造方法的参数
            ParameterInfo[] maxParameters = constructor.First().GetParameters();

            // 遍历所有构造方法，找到参数最多的
            foreach (ConstructorInfo c in constructor) {
                // 取构造方法的参数
                ParameterInfo[] parameters = c.GetParameters();
                if (parameters.Length > maxParameters.Length) {
                    maxParameters = parameters;
                }
            }

            // 解析所有参数实例
            object[] param = maxParameters.Select(p => {
                        // 如果参数是数组，则解析所有参数
                        if (p.ParameterType.IsArray) {
                            return ResolveAll(p.ParameterType);
                        }
                        // 解析类实例，如果为Null则加上实现类名称
                        return Resolve(p.ParameterType) ?? Resolve(p.ParameterType, p.Name);
                    })
                   .ToArray();

            // 创建实例
            object obj = Activator.CreateInstance(type, param);
            Inject(obj);
            return obj;
        }

        /// <summary>
        /// 将注册类型/映射注入对象
        /// </summary>
        /// <param name="obj">注入的实例</param>
        public void Inject(object obj)
        {
            if (obj == null) return;

            // 通过反射，从类型获取所有成员
            IEnumerable<MemberInfo> members = obj.GetType().GetMembers(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);

            // 遍历所有方法
            foreach (MemberInfo memberInfo in members) {
                // 找到属性
                AutowiredAttribute autowiredAttribute = memberInfo.GetCustomAttributes(typeof(AutowiredAttribute), true).FirstOrDefault() as AutowiredAttribute;

                // 如果存在AutowiredAttribute属性
                if (autowiredAttribute != null) {
                    // 如果是属性
                    if (memberInfo is PropertyInfo propertyInfo) {
                        propertyInfo.SetValue(obj, Resolve(propertyInfo.PropertyType, autowiredAttribute.Name), null);
                    }
                    // 如果是成员变量
                    else if (memberInfo is FieldInfo fieldInfo) {
                        fieldInfo.SetValue(obj, Resolve(fieldInfo.FieldType, autowiredAttribute.Name));
                    }
                }
            }
        }

        /// <summary>
        /// 注入所有实例
        /// </summary>
        public void InjectAll()
        {
            foreach (object instance in Instances.Values) {
                Inject(instance);
            }
        }

        /// <summary>
        /// 清除所有注册映射关系
        /// </summary>
        public void Clear()
        {
            Instances.Clear();
            Mappings.Clear();
        }

        public void Dispose()
        {
            Clear();
        }
    }
}
