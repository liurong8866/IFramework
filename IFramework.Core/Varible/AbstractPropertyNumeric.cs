/*****************************************************************************
 * MIT License
 * 
 * Copyright (c) 2021 liurong
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *****************************************************************************/

using System;

namespace IFramework.Core {
    /// <summary>
    /// 数字类型的
    /// </summary>
    public abstract class AbstractPropertyNumeric<T> : AbstractProperty<T> where T : struct, IConvertible, IComparable {

        public int ToInt() {
            return Value.ToInt();
        }

        public long ToLong() {
            return Value.ToLong();
        }

        public short ToShort() {
            return Value.ToShort();
        }

        public float ToFloat() {
            return Value.ToFloat();
        }

        public double ToDouble() {
            return Value.ToDouble();
        }

        public decimal ToDecimal() {
            return Value.ToDecimal();
        }

        //"+"方法
        public static T Addition(object m, object n) {
            T result;
            Type type = typeof(T);
            if (type == typeof(int)) {
                result = (T) Convert.ChangeType(m.ToInt() + n.ToInt(), typeof(T));
            }
            else if (type == typeof(short)) {
                result = (T) Convert.ChangeType(m.ToShort() + n.ToShort(), typeof(T));
            }
            else if (type == typeof(long)) {
                result = (T) Convert.ChangeType(m.ToLong() + n.ToLong(), typeof(T));
            }
            else if (type == typeof(float)) {
                result = (T) Convert.ChangeType(m.ToFloat() + n.ToFloat(), typeof(T));
            }
            else if (type == typeof(double)) {
                result = (T) Convert.ChangeType(m.ToDouble() + n.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal)) {
                result = (T) Convert.ChangeType(m.ToDecimal() + n.ToDecimal(), typeof(T));
            }
            else {
                throw new Exception("未实现该类型的 \"+\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }

        //"-"方法
        public static T Subtraction(object m, object n) {
            T result;
            Type type = typeof(T);
            if (type == typeof(int)) {
                result = (T) Convert.ChangeType(m.ToInt() - n.ToInt(), typeof(T));
            }
            else if (type == typeof(short)) {
                result = (T) Convert.ChangeType(m.ToShort() - n.ToShort(), typeof(T));
            }
            else if (type == typeof(long)) {
                result = (T) Convert.ChangeType(m.ToLong() - n.ToLong(), typeof(T));
            }
            else if (type == typeof(float)) {
                result = (T) Convert.ChangeType(m.ToFloat() - n.ToFloat(), typeof(T));
            }
            else if (type == typeof(double)) {
                result = (T) Convert.ChangeType(m.ToDouble() - n.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal)) {
                result = (T) Convert.ChangeType(m.ToDecimal() - n.ToDecimal(), typeof(T));
            }
            else {
                throw new Exception("未实现该类型的 \"-\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }

        //"*"方法
        public static T Multiply(object m, object n) {
            T result;
            Type type = typeof(T);
            if (type == typeof(int)) {
                result = (T) Convert.ChangeType(m.ToInt() * n.ToInt(), typeof(T));
            }
            else if (type == typeof(short)) {
                result = (T) Convert.ChangeType(m.ToShort() * n.ToShort(), typeof(T));
            }
            else if (type == typeof(long)) {
                result = (T) Convert.ChangeType(m.ToLong() * n.ToLong(), typeof(T));
            }
            else if (type == typeof(float)) {
                result = (T) Convert.ChangeType(m.ToFloat() * n.ToFloat(), typeof(T));
            }
            else if (type == typeof(double)) {
                result = (T) Convert.ChangeType(m.ToDouble() * n.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal)) {
                result = (T) Convert.ChangeType(m.ToDecimal() * n.ToDecimal(), typeof(T));
            }
            else {
                throw new Exception("未实现该类型的 \"*\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }

        //"/"方法
        public static T Division(object m, object n) {
            T result;
            Type type = typeof(T);
            if (n.ToDecimal() == 0) throw new Exception("除数不能为0！");
            if (type == typeof(int)) {
                result = (T) Convert.ChangeType(m.ToInt() / n.ToInt(), typeof(T));
            }
            else if (type == typeof(short)) {
                result = (T) Convert.ChangeType(m.ToShort() / n.ToShort(), typeof(T));
            }
            else if (type == typeof(long)) {
                result = (T) Convert.ChangeType(m.ToLong() / n.ToLong(), typeof(T));
            }
            else if (type == typeof(float)) {
                result = (T) Convert.ChangeType(m.ToFloat() / n.ToFloat(), typeof(T));
            }
            else if (type == typeof(double)) {
                result = (T) Convert.ChangeType(m.ToDouble() / n.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal)) {
                result = (T) Convert.ChangeType(m.ToDecimal() / n.ToDecimal(), typeof(T));
            }
            else {
                throw new Exception("未实现该类型的 \"/\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }

        //"%"方法
        public static T Module(object m, object n) {
            T result;
            Type type = typeof(T);
            if (n.ToDecimal() == 0) throw new Exception("除数不能为0！");
            if (type == typeof(int)) {
                result = (T) Convert.ChangeType(m.ToInt() % n.ToInt(), typeof(T));
            }
            else if (type == typeof(short)) {
                result = (T) Convert.ChangeType(m.ToShort() % n.ToShort(), typeof(T));
            }
            else if (type == typeof(long)) {
                result = (T) Convert.ChangeType(m.ToLong() % n.ToLong(), typeof(T));
            }
            else if (type == typeof(float)) {
                result = (T) Convert.ChangeType(m.ToFloat() % n.ToFloat(), typeof(T));
            }
            else if (type == typeof(double)) {
                result = (T) Convert.ChangeType(m.ToDouble() % n.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal)) {
                result = (T) Convert.ChangeType(m.ToDecimal() % n.ToDecimal(), typeof(T));
            }
            else {
                throw new Exception("未实现该类型的 \"%\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }

        //重载运算符"+"
        public static T operator +(AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n) {
            return Addition(m.Value, n.Value);
        }

        public static T operator +(AbstractPropertyNumeric<T> m, T n) {
            return Addition(m.Value, n);
        }

        public static T operator +(T m, AbstractPropertyNumeric<T> n) {
            return Addition(m, n.Value);
        }

        //重载运算符"-"
        public static T operator -(AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n) {
            return Subtraction(m.Value, n.Value);
        }

        public static T operator -(AbstractPropertyNumeric<T> m, T n) {
            return Subtraction(m.Value, n);
        }

        public static T operator -(T m, AbstractPropertyNumeric<T> n) {
            return Subtraction(m, n.Value);
        }

        //重载运算符"*"
        public static T operator *(AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n) {
            return Multiply(m.Value, n.Value);
        }

        public static T operator *(AbstractPropertyNumeric<T> m, T n) {
            return Multiply(m.Value, n);
        }

        public static T operator *(T m, AbstractPropertyNumeric<T> n) {
            return Multiply(m, n.Value);
        }

        //重载运算符"/"
        public static T operator /(AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n) {
            return Division(m.Value, n.Value);
        }

        public static T operator /(AbstractPropertyNumeric<T> m, T n) {
            return Division(m.Value, n);
        }

        public static T operator /(T m, AbstractPropertyNumeric<T> n) {
            return Division(m, n.Value);
        }

        //重载运算符"%"
        public static T operator %(AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n) {
            return Module(m.Value, n.Value);
        }

        public static T operator %(AbstractPropertyNumeric<T> m, T n) {
            return Module(m.Value, n);
        }

        public static T operator %(T m, AbstractPropertyNumeric<T> n) {
            return Module(m, n.Value);
        }

        //重载运算符"++"
        public static AbstractPropertyNumeric<T> operator ++(AbstractPropertyNumeric<T> self) {
            self.Value = Addition(self.Value, 1);
            return self;
        }

        //重载运算符"--"
        public static AbstractPropertyNumeric<T> operator --(AbstractPropertyNumeric<T> self) {
            self.Value = Addition(self.Value, -1);
            return self;
        }

        //重载运算符"=="
        public static bool operator ==(AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n) {
            if (ReferenceEquals(m, n)) {
                return true;
            }
            if (((object) m == null) || ((object) n == null)) {
                return false;
            }
            return Math.Abs(m.Value.ToDouble() - n.Value.ToDouble()) < Constant.TOLERANCE;
        }

        public static bool operator ==(AbstractPropertyNumeric<T> m, object n) {
            if (ReferenceEquals(m, n)) {
                return true;
            }
            if (((object) m == null) || (n == null)) {
                return false;
            }
            return Math.Abs(m.ToDouble() - n.ToDouble()) < Constant.TOLERANCE;
        }

        public static bool operator ==(object m, AbstractPropertyNumeric<T> n) {
            if (ReferenceEquals(m, n)) {
                return true;
            }
            if ((m == null) || ((object) n == null)) {
                return false;
            }
            return Math.Abs(m.ToDouble() - n.Value.ToDouble()) < Constant.TOLERANCE;
        }

        //重载运算符"!="
        public static bool operator !=(AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n) {
            return !(m == n);
        }

        public static bool operator !=(AbstractPropertyNumeric<T> m, object n) {
            return !(m == n);
        }

        public static bool operator !=(object m, AbstractPropertyNumeric<T> n) {
            return !(m == n);
        }

        //重载运算符">"
        public static bool operator >(AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n) {
            return m.Value.ToDouble() > n.Value.ToDouble();
        }

        public static bool operator >(AbstractPropertyNumeric<T> m, object n) {
            return m.Value.ToDouble() > n.ToDouble();
        }

        public static bool operator >(object m, AbstractPropertyNumeric<T> n) {
            return m.ToDouble() > n.Value.ToDouble();
        }

        //重载运算符"<"
        public static bool operator <(AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n) {
            return m.Value.ToDouble() < n.Value.ToDouble();
        }

        public static bool operator <(AbstractPropertyNumeric<T> m, object n) {
            return m.Value.ToDouble() < n.ToDouble();
        }

        public static bool operator <(object m, AbstractPropertyNumeric<T> n) {
            return m.ToDouble() < n.Value.ToDouble();
        }

        //重载运算符">="
        public static bool operator >=(AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n) {
            return m.Value.ToDouble() >= n.Value.ToDouble();
        }

        public static bool operator >=(AbstractPropertyNumeric<T> m, object n) {
            return m.Value.ToDouble() >= n.ToDouble();
        }

        public static bool operator >=(object m, AbstractPropertyNumeric<T> n) {
            return m.ToDouble() >= n.Value.ToDouble();
        }

        //重载运算符"<="
        public static bool operator <=(AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n) {
            return m.Value.ToDouble() <= n.Value.ToDouble();
        }

        public static bool operator <=(AbstractPropertyNumeric<T> m, object n) {
            return m.Value.ToDouble() <= n.ToDouble();
        }

        public static bool operator <=(object m, AbstractPropertyNumeric<T> n) {
            return m.ToDouble() <= n.Value.ToDouble();
        }

        //重写Equals方法
        public override bool Equals(Object obj) {
            if (obj == null) {
                return false;
            }
            if (obj.GetType() == typeof(AbstractPropertyNumeric<T>)) {
                AbstractPropertyNumeric<T> bindable = obj as AbstractPropertyNumeric<T>;
                return Equals(bindable);
            }

            // 判断类型Value是否一致
            if (obj.GetType() != typeof(T)) {
                return false;
            }
            return Value.Equals(obj);
        }

        public bool Equals(AbstractPropertyNumeric<T> bindable) {
            if ((object) bindable == null) {
                return false;
            }
            return Value.Equals(bindable.Value);
        }

        public override int GetHashCode() {
            return Value.GetHashCode();
        }

    }
}
