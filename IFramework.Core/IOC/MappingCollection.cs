using System;
using System.Collections.Generic;

namespace IFramework.Core
{
    /// <summary>
    /// 元组
    /// </summary>
    public class Tuple<T1, T2>
    {
        public readonly T1 Item1;
        public readonly T2 Item2;

        public Tuple(T1 item1, T2 item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public override bool Equals(Object obj)
        {
            Tuple<T1, T2> p = obj as Tuple<T1, T2>;
            if (obj == null) return false;

            if (Item1 == null) {
                if (p.Item1 != null) return false;
            }
            else {
                if (p.Item1 == null || !Item1.Equals(p.Item1)) return false;
            }

            if (Item2 == null) {
                if (p.Item2 != null) return false;
            }
            else {
                if (p.Item2 == null || !Item2.Equals(p.Item2)) return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            if (Item1 != null) hash ^= Item1.GetHashCode();
            if (Item2 != null) hash ^= Item2.GetHashCode();
            return hash;
        }
    }
    
    /// <summary>
    /// 类型字典
    /// </summary>
    public class TypeMappingCollection : Dictionary<Tuple<Type, string>, Type>
    {
        public Type this[Type from, string name = null] {
            get {
                Tuple<Type, string> key = new Tuple<Type, string>(from, name);
                Type mapping = null;

                if (this.TryGetValue(key, out mapping)) {
                    return mapping;
                }
                return null;
            }
            set {
                Tuple<Type, string> key = new Tuple<Type, string>(from, name);
                this[key] = value;
            }
        }
    }

    /// <summary>
    /// 类型实例字典
    /// </summary>
    public class TypeInstanceCollection : Dictionary<Tuple<Type, string>, object>
    {
        public object this[Type from, string name = null] {
            get {
                Tuple<Type, string> key = new Tuple<Type, string>(from, name);
                object mapping = null;

                if (this.TryGetValue(key, out mapping)) {
                    return mapping;
                }
                return null;
            }
            set {
                Tuple<Type, string> key = new Tuple<Type, string>(from, name);
                this[key] = value;
            }
        }
    }

    /// <summary>
    /// 类型关系字典
    /// </summary>
    public class TypeRelationCollection : Dictionary<Tuple<Type, Type>, Type>
    {
        public Type this[Type from, Type to] {
            get {
                Tuple<Type, Type> key = new Tuple<Type, Type>(from, to);
                Type mapping = null;

                if (this.TryGetValue(key, out mapping)) {
                    return mapping;
                }
                return null;
            }
            set {
                Tuple<Type, Type> key = new Tuple<Type, Type>(from, to);
                this[key] = value;
            }
        }
    }
}
