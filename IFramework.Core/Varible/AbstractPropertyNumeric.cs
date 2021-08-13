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
        
        //重载运算符"+"
        public static T operator + (AbstractPropertyNumeric<T> a, AbstractPropertyNumeric<T> b)
        {
            T result;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.Value.ToInt() + b.Value.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.Value.ToShort() + b.Value.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.Value.ToLong() + b.Value.ToLong(), typeof(T));;
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.Value.ToFloat() + b.Value.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.Value.ToDouble() + b.Value.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.Value.ToDecimal() + b.Value.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"+\" 运算符重载：" + typeof(T).Name);
            }
            
            return result;
        }
        
        public static T operator + (AbstractPropertyNumeric<T> a, T b)
        {
            T result;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.Value.ToInt() + b.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.Value.ToShort() + b.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.Value.ToLong() + b.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.Value.ToFloat() + b.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.Value.ToDouble() + b.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.Value.ToDecimal() + b.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"+\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }
        
        public static T operator + (T a, AbstractPropertyNumeric<T> b)
        {
            T result;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.ToInt() + b.Value.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.ToShort() + b.Value.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.ToLong() + b.Value.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.ToFloat() + b.Value.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.ToDouble() + b.Value.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.ToDecimal() + b.Value.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"+\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }
        
        //重载运算符"-"
        public static T operator - (AbstractPropertyNumeric<T> a, AbstractPropertyNumeric<T> b)
        {
            T result;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.Value.ToInt() - b.Value.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.Value.ToShort() - b.Value.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.Value.ToLong() - b.Value.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.Value.ToFloat() - b.Value.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.Value.ToDouble() - b.Value.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.Value.ToDecimal() - b.Value.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"-\" 运算符重载：" + typeof(T).Name);
            }
            
            return result;
        }
        
        public static T operator - (AbstractPropertyNumeric<T> a, T b)
        {
            T result;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.Value.ToInt() - b.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.Value.ToShort() - b.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.Value.ToLong() - b.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.Value.ToFloat() - b.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.Value.ToDouble() - b.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.Value.ToDecimal() - b.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"-\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }
        
        public static T operator - (T a, AbstractPropertyNumeric<T> b)
        {
            T result;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.ToInt() - b.Value.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.ToShort() - b.Value.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.ToLong() - b.Value.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.ToFloat() - b.Value.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.ToDouble() - b.Value.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.ToDecimal() - b.Value.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"-\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }
        
        //重载运算符"*"
        public static T operator * (AbstractPropertyNumeric<T> a, AbstractPropertyNumeric<T> b)
        {
            T result;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.Value.ToInt() * b.Value.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.Value.ToShort() * b.Value.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.Value.ToLong() * b.Value.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.Value.ToFloat() * b.Value.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.Value.ToDouble() * b.Value.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.Value.ToDecimal() * b.Value.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"*\" 运算符重载：" + typeof(T).Name);
            }
            
            return result;
        }
        
        public static T operator * (AbstractPropertyNumeric<T> a, T b)
        {
            T result;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.Value.ToInt() * b.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.Value.ToShort() * b.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.Value.ToLong() * b.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.Value.ToFloat() * b.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.Value.ToDouble() * b.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.Value.ToDecimal() * b.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"*\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }
        
        public static T operator * (T a, AbstractPropertyNumeric<T> b)
        {
            T result;
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.ToInt() * b.Value.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.ToShort() * b.Value.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.ToLong() * b.Value.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.ToFloat() * b.Value.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.ToDouble() * b.Value.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.ToDecimal() * b.Value.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"*\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }
        
        //重载运算符"/"
        public static T operator / (AbstractPropertyNumeric<T> a, AbstractPropertyNumeric<T> b)
        {
            T result;
            
            Type type = typeof(T);
            
            if (b.Value.ToInt() == 0) throw new Exception("除数不能为0！");
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.Value.ToInt() / b.Value.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.Value.ToShort() / b.Value.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.Value.ToLong() / b.Value.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.Value.ToFloat() / b.Value.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.Value.ToDouble() / b.Value.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.Value.ToDecimal() / b.Value.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"/\" 运算符重载：" + typeof(T).Name);
            }
            
            return result;
        }
        
        public static T operator / (AbstractPropertyNumeric<T> a, T b)
        {
            T result;
            
            Type type = typeof(T);
            
            if (b.ToInt() == 0) throw new Exception("除数不能为0！");
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.Value.ToInt() / b.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.Value.ToShort() / b.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.Value.ToLong() / b.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.Value.ToFloat() / b.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.Value.ToDouble() / b.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.Value.ToDecimal() / b.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"/\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }
        
        public static T operator / (T a, AbstractPropertyNumeric<T> b)
        {
            T result;
            
            Type type = typeof(T);

            if (b.Value.ToInt() == 0) throw new Exception("除数不能为0！");
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.ToInt() / b.Value.ToInt(), typeof(T));
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.ToShort() / b.Value.ToShort(), typeof(T));
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.ToLong() / b.Value.ToLong(), typeof(T));
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.ToFloat() / b.Value.ToFloat(), typeof(T));
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.ToDouble() / b.Value.ToDouble(), typeof(T));
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.ToDecimal() / b.Value.ToDecimal(), typeof(T));
            }
            else
            {
                throw new Exception("未实现该类型的 \"/\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }
        
        
        //重载运算符"=="
        public static bool operator == (AbstractPropertyNumeric<T> a, AbstractPropertyNumeric<T> b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            
            return Math.Abs(a.Value.ToDouble() - b.Value.ToDouble()) < Constant.TOLERANCE;
        }
        
        public static bool operator == (AbstractPropertyNumeric<T> a, object b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || (b == null))
            {
                return false;
            }
            
            return Math.Abs(a.ToDouble() - b.ToDouble()) < Constant.TOLERANCE;
        }
        
        public static bool operator == (object a, AbstractPropertyNumeric<T> b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if ((a == null) || ((object)b == null))
            {
                return false;
            }
            
            return Math.Abs(a.ToDouble() - b.Value.ToDouble()) < Constant.TOLERANCE;
        }

        //重载运算符"!="
        public static bool operator != (AbstractPropertyNumeric<T> a, AbstractPropertyNumeric<T> b)
        {
            return !(a==b);
        }
        
        public static bool operator != (AbstractPropertyNumeric<T> a, object b)
        {
            return !(a==b);
        }
        
        public static bool operator != (object a, AbstractPropertyNumeric<T> b)
        {
            return !(a==b);
        }
        
        //重载运算符">"
        public static bool operator > (AbstractPropertyNumeric<T> a, AbstractPropertyNumeric<T> b)
        {
            return a.Value.ToDouble() > b.Value.ToDouble();
        }
        
        public static bool operator > (AbstractPropertyNumeric<T> a, object b)
        {
            return a.Value.ToDouble() > b.ToDouble();
        }
        
        public static bool operator > (object a, AbstractPropertyNumeric<T> b)
        {
            return a.ToDouble() > b.Value.ToDouble();
        }
        
        //重载运算符"<"
        public static bool operator < (AbstractPropertyNumeric<T> a, AbstractPropertyNumeric<T> b)
        {
            return a.Value.ToDouble() < b.Value.ToDouble();
        }
        
        public static bool operator < (AbstractPropertyNumeric<T> a, object b)
        {
            return a.Value.ToDouble() < b.ToDouble();
        }
        
        public static bool operator < (object a, AbstractPropertyNumeric<T> b)
        {
            return a.ToDouble() < b.Value.ToDouble();
        }
        
        //重载运算符">="
        public static bool operator >= (AbstractPropertyNumeric<T> a, AbstractPropertyNumeric<T> b)
        {
            return a.Value.ToDouble() >= b.Value.ToDouble();
        }
        
        public static bool operator >= (AbstractPropertyNumeric<T> a, object b)
        {
            return a.Value.ToDouble() >= b.ToDouble();
        }
        
        public static bool operator >= (object a, AbstractPropertyNumeric<T> b)
        {
            return a.ToDouble() >= b.Value.ToDouble();
        }
        
        //重载运算符"<="
        public static bool operator <= (AbstractPropertyNumeric<T> a, AbstractPropertyNumeric<T> b)
        {
            return a.Value.ToDouble() <= b.Value.ToDouble();
        }
        
        public static bool operator <= (AbstractPropertyNumeric<T> a, object b)
        {
            return a.Value.ToDouble() <= b.ToDouble();
        }
        
        public static bool operator <= (object a, AbstractPropertyNumeric<T> b)
        {
            return a.ToDouble() <= b.Value.ToDouble();
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