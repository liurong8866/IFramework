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

        public override bool Equals(object obj)
        {
            if (!(obj is Tuple<T1, T2> tuple)) return false;

            if (Item1 == null) {
                if (tuple.Item1 != null) return false;
            }
            else {
                if (tuple.Item1 == null || !Item1.Equals(tuple.Item1)) return false;
            }

            if (Item2 == null) {
                if (tuple.Item2 != null) return false;
            }
            else {
                if (tuple.Item2 == null || !Item2.Equals(tuple.Item2)) return false;
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
    /// Key: Type string
    /// Value: Type
    /// </summary>
    public class TypeMapping : Dictionary<Tuple<Type, string>, Type>
    {
        public Type this[Type from, string name = null] {
            get {
                Tuple<Type, string> key = new Tuple<Type, string>(from, name);
                return TryGetValue(key, out Type mapping) ? mapping : null;
            }
            set {
                Tuple<Type, string> key = new Tuple<Type, string>(from, name);
                this[key] = value;
            }
        }
    }

    /// <summary>
    /// 类型实例字典
    /// Key: Type string
    /// Value: object
    /// </summary>
    public class TypeInstanceMapping : Dictionary<Tuple<Type, string>, object>
    {
        public object this[Type from, string name = null] {
            get {
                Tuple<Type, string> key = new Tuple<Type, string>(from, name);
                return TryGetValue(key, out object mapping) ? mapping : null;
            }
            set {
                Tuple<Type, string> key = new Tuple<Type, string>(from, name);
                this[key] = value;
            }
        }
    }

    /// <summary>
    /// 类型关系字典
    /// Key: Type Type
    /// Value: Type
    /// </summary>
    public class TypeRelationMapping : Dictionary<Tuple<Type, Type>, Type>
    {
        public Type this[Type from, Type to] {
            get {
                Tuple<Type, Type> key = new Tuple<Type, Type>(from, to);
                return TryGetValue(key, out Type mapping) ? mapping : null;
            }
            set {
                Tuple<Type, Type> key = new Tuple<Type, Type>(from, to);
                this[key] = value;
            }
        }
    }
}
