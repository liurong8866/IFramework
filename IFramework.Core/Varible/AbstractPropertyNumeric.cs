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

namespace IFramework.Core
{
    /// <summary>
    /// 数字类型的
    /// </summary>
    public abstract class AbstractPropertyNumeric<T> : AbstractProperty<T> where T : IConvertible, IComparable
    {
        public AbstractPropertyNumeric(){}
        
        public AbstractPropertyNumeric(T value)
        {
            this.value = value;
        }
        
        
        
        //"+"方法
        public static T Addition (AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n)
        {
            T result;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(m.Value.ToInt() + n.Value.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(m.Value.ToShort() + n.Value.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(m.Value.ToLong() + n.Value.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(m.Value.ToFloat() + n.Value.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(m.Value.ToDouble() + n.Value.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(m.Value.ToDecimal() + n.Value.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"+\" 运算符重载：" + typeof(T).Name);
            }
            
            return result;
        }
        
        public static T Addition (AbstractPropertyNumeric<T> m, T n)
        {
            T result;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(m.Value.ToInt() + n.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(m.Value.ToShort() + n.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(m.Value.ToLong() + n.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(m.Value.ToFloat() + n.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(m.Value.ToDouble() + n.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(m.Value.ToDecimal() + n.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"+\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }
        
        public static T Addition (T m, AbstractPropertyNumeric<T> n)
        {
            T result;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(m.ToInt() + n.Value.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(m.ToShort() + n.Value.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(m.ToLong() + n.Value.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(m.ToFloat() + n.Value.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(m.ToDouble() + n.Value.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(m.ToDecimal() + n.Value.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"+\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }
        
        //"-"方法
        public static T Subtraction (AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n)
        {
            T result;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(m.Value.ToInt() - n.Value.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(m.Value.ToShort() - n.Value.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(m.Value.ToLong() - n.Value.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(m.Value.ToFloat() - n.Value.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(m.Value.ToDouble() - n.Value.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(m.Value.ToDecimal() - n.Value.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"-\" 运算符重载：" + typeof(T).Name);
            }
            
            return result;
        }
        
        public static T Subtraction (AbstractPropertyNumeric<T> m, T n)
        {
            T result;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(m.Value.ToInt() - n.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(m.Value.ToShort() - n.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(m.Value.ToLong() - n.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(m.Value.ToFloat() - n.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(m.Value.ToDouble() - n.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(m.Value.ToDecimal() - n.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"-\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }
        
        public static T Subtraction (T m, AbstractPropertyNumeric<T> n)
        {
            T result;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(m.ToInt() - n.Value.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(m.ToShort() - n.Value.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(m.ToLong() - n.Value.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(m.ToFloat() - n.Value.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(m.ToDouble() - n.Value.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(m.ToDecimal() - n.Value.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"-\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }
        
        //"*"方法
        public static T Multiply (AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n)
        {
            T result;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(m.Value.ToInt() * n.Value.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(m.Value.ToShort() * n.Value.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(m.Value.ToLong() * n.Value.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(m.Value.ToFloat() * n.Value.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(m.Value.ToDouble() * n.Value.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(m.Value.ToDecimal() * n.Value.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"*\" 运算符重载：" + typeof(T).Name);
            }
            
            return result;
        }
        
        public static T Multiply (AbstractPropertyNumeric<T> m, T n)
        {
            T result;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(m.Value.ToInt() * n.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(m.Value.ToShort() * n.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(m.Value.ToLong() * n.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(m.Value.ToFloat() * n.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(m.Value.ToDouble() * n.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(m.Value.ToDecimal() * n.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"*\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }
        
        public static T Multiply (T m, AbstractPropertyNumeric<T> n)
        {
            T result;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(m.ToInt() * n.Value.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(m.ToShort() * n.Value.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(m.ToLong() * n.Value.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(m.ToFloat() * n.Value.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(m.ToDouble() * n.Value.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(m.ToDecimal() * n.Value.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"*\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }
        
        //"/"方法
        public static T Division (AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n)
        {
            T result;
            
            Type type = typeof(T);
            
            if (n.Value.ToDecimal() == 0) throw new Exception("除数不能为0！");
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(m.Value.ToInt() / n.Value.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(m.Value.ToShort() / n.Value.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(m.Value.ToLong() / n.Value.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(m.Value.ToFloat() / n.Value.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(m.Value.ToDouble() / n.Value.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(m.Value.ToDecimal() / n.Value.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"/\" 运算符重载：" + typeof(T).Name);
            }
            
            return result;
        }
        
        public static T Division (AbstractPropertyNumeric<T> m, T n)
        {
            T result;
            
            Type type = typeof(T);
            
            if (n.ToDecimal() == 0) throw new Exception("除数不能为0！");
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(m.Value.ToInt() / n.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(m.Value.ToShort() / n.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(m.Value.ToLong() / n.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(m.Value.ToFloat() / n.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(m.Value.ToDouble() / n.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(m.Value.ToDecimal() / n.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"/\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }
        
        public static T Division (T m, AbstractPropertyNumeric<T> n)
        {
            T result;
            
            Type type = typeof(T);

            if (n.Value.ToDecimal() == 0) throw new Exception("除数不能为0！");
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(m.ToInt() / n.Value.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(m.ToShort() / n.Value.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(m.ToLong() / n.Value.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(m.ToFloat() / n.Value.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(m.ToDouble() / n.Value.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(m.ToDecimal() / n.Value.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"/\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }
        
        //"%"方法
        public static T Module (AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n)
        {
            T result;
            
            Type type = typeof(T);
            
            if (n.Value.ToDecimal() == 0) throw new Exception("除数不能为0！");
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(m.Value.ToInt() % n.Value.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(m.Value.ToShort() % n.Value.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(m.Value.ToLong() % n.Value.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(m.ToFloat() % n.Value.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(m.ToDouble() % n.Value.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(m.ToDecimal() % n.Value.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"%\" 运算符重载：" + typeof(T).Name);
            }
            
            return result;
        }
        
        public static T Module (AbstractPropertyNumeric<T> m, T n)
        {
            T result;
            
            Type type = typeof(T);
            
            if (n.ToDecimal() == 0) throw new Exception("除数不能为0！");
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(m.Value.ToInt() % n.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(m.Value.ToShort() % n.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(m.Value.ToLong() % n.ToLong(), typeof(T));
            }
            else if (type == typeof(float) || type == typeof(double) || type ==typeof(decimal))
            {
                result = (T)Convert.ChangeType(m.Value.ToLong() / n.ToLong(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"%\" 运算符重载：" + typeof(T).Name);
            }
            
            return result;
        }
        
        public static T Module (T m, AbstractPropertyNumeric<T> n)
        {
            T result;
            
            Type type = typeof(T);
            
            if (n.Value.ToDecimal() == 0) throw new Exception("除数不能为0！");
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(m.ToInt() % n.Value.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(m.ToShort() % n.Value.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(m.ToLong() % n.Value.ToLong(), typeof(T));
            }
            else if (type == typeof(float) || type == typeof(double) || type ==typeof(decimal))
            {
                result = (T)Convert.ChangeType(m.ToLong() / n.Value.ToLong(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"%\" 运算符重载：" + typeof(T).Name);
            }
            
            return result;
        }
        

        //重载运算符"+"
        public static T operator + (AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n)
        {
            return Addition(m, n);
        }
        
        public static T operator + (AbstractPropertyNumeric<T> m, T n)
        {
            return Addition(m, n);
        }
        
        public static T operator + (T m, AbstractPropertyNumeric<T> n)
        {
            return Addition(m, n);
        }
        
        //重载运算符"-"
        public static T operator - (AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n)
        {
            return Subtraction(m, n);
        }
        
        public static T operator - (AbstractPropertyNumeric<T> m, T n)
        {
            return Subtraction(m, n);
        }
        
        public static T operator - (T m, AbstractPropertyNumeric<T> n)
        {
            return Subtraction(m, n);
        }
        
        //重载运算符"*"
        public static T operator * (AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n)
        {
            return Multiply(m, n);
        }
        
        public static T operator * (AbstractPropertyNumeric<T> m, T n)
        {
            return Multiply(m, n);
        }
        
        public static T operator * (T m, AbstractPropertyNumeric<T> n)
        {
            return Multiply(m, n);
        }
        
        //重载运算符"/"
        public static T operator / (AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n)
        {
            return Division(m, n);
        }
        
        public static T operator / (AbstractPropertyNumeric<T> m, T n)
        {
            return Division(m, n);
        }
        
        public static T operator / (T m, AbstractPropertyNumeric<T> n)
        {
            return Division(m, n);
        }
        
        //重载运算符"%"
        public static T operator % (AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n)
        {
            return Module(m, n);
        }
        
        public static T operator % (AbstractPropertyNumeric<T> m, T n)
        {
            return Module(m, n);
        }
        
        public static T operator % (T m, AbstractPropertyNumeric<T> n)
        {
            return Module(m, n);
        }
        

        //重载运算符"=="
        public static bool operator == (AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n)
        {
            if (ReferenceEquals(m, n))
            {
                return true;
            }

            if (((object)m == null) || ((object)n == null))
            {
                return false;
            }
            
            return Math.Abs(m.Value.ToDouble() - n.Value.ToDouble()) < Constant.TOLERANCE;
        }
        
        public static bool operator == (AbstractPropertyNumeric<T> m, object n)
        {
            if (ReferenceEquals(m, n))
            {
                return true;
            }

            if (((object)m == null) || (n == null))
            {
                return false;
            }
            
            return Math.Abs(m.ToDouble() - n.ToDouble()) < Constant.TOLERANCE;
        }
        
        public static bool operator == (object m, AbstractPropertyNumeric<T> n)
        {
            if (ReferenceEquals(m, n))
            {
                return true;
            }

            if ((m == null) || ((object)n == null))
            {
                return false;
            }
            
            return Math.Abs(m.ToDouble() - n.Value.ToDouble()) < Constant.TOLERANCE;
        }

        //重载运算符"!="
        public static bool operator != (AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n)
        {
            return !(m==n);
        }
        
        public static bool operator != (AbstractPropertyNumeric<T> m, object n)
        {
            return !(m==n);
        }
        
        public static bool operator != (object m, AbstractPropertyNumeric<T> n)
        {
            return !(m==n);
        }
        
        //重载运算符">"
        public static bool operator > (AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n)
        {
            return m.Value.ToDouble() > n.Value.ToDouble();
        }
        
        public static bool operator > (AbstractPropertyNumeric<T> m, object n)
        {
            return m.Value.ToDouble() > n.ToDouble();
        }
        
        public static bool operator > (object m, AbstractPropertyNumeric<T> n)
        {
            return m.ToDouble() > n.Value.ToDouble();
        }
        
        //重载运算符"<"
        public static bool operator < (AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n)
        {
            return m.Value.ToDouble() < n.Value.ToDouble();
        }
        
        public static bool operator < (AbstractPropertyNumeric<T> m, object n)
        {
            return m.Value.ToDouble() < n.ToDouble();
        }
        
        public static bool operator < (object m, AbstractPropertyNumeric<T> n)
        {
            return m.ToDouble() < n.Value.ToDouble();
        }
        
        //重载运算符">="
        public static bool operator >= (AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n)
        {
            return m.Value.ToDouble() >= n.Value.ToDouble();
        }
        
        public static bool operator >= (AbstractPropertyNumeric<T> m, object n)
        {
            return m.Value.ToDouble() >= n.ToDouble();
        }
        
        public static bool operator >= (object m, AbstractPropertyNumeric<T> n)
        {
            return m.ToDouble() >= n.Value.ToDouble();
        }
        
        //重载运算符"<="
        public static bool operator <= (AbstractPropertyNumeric<T> m, AbstractPropertyNumeric<T> n)
        {
            return m.Value.ToDouble() <= n.Value.ToDouble();
        }
        
        public static bool operator <= (AbstractPropertyNumeric<T> m, object n)
        {
            return m.Value.ToDouble() <= n.ToDouble();
        }
        
        public static bool operator <= (object m, AbstractPropertyNumeric<T> n)
        {
            return m.ToDouble() <= n.Value.ToDouble();
        }
        
        //重写Equals方法
        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            
            if(obj.GetType() == typeof(AbstractPropertyNumeric<T>))
            {
                AbstractPropertyNumeric<T> bindable = obj as AbstractPropertyNumeric<T>;
                return Equals(bindable);
            }
            
            // 判断类型Value是否一致
            if (obj.GetType() != typeof(T))
            {
                return false;
            }

            return Value.Equals(obj) ;
        }
        
        public bool Equals(AbstractPropertyNumeric<T> bindable)
        {
            if ((object)bindable == null)
            {
                return false;
            }
        
            return Value.Equals(bindable.Value) ;
        }
        
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}