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
using UnityEngine;

namespace IFramework.Core
{
    /// <summary>
    /// 可持久化的数字类型抽象类
    /// </summary>
    public abstract class AbstractConfigNumeric<T> : AbstractPropertyNumeric<T>, IPersistable<T> where T : IConvertible, IComparable
    {
        protected string key;
        
        public AbstractConfigNumeric(string key, T value)
        {
            this.key = key;
            this.value = value;
        }

        protected override T GetValue()
        {
            return Get();
        }

        protected override void SetValue(T value)
        {
            if (IsValueChanged(value))
            {
                Value = value;

                Save(value);
                
                setted = true;
            }
        }

        public abstract T Get();

        public abstract void Save(T value);
        
        
        //重载运算符"+"
        public static AbstractConfigNumeric<T> operator + (AbstractConfigNumeric<T> a, AbstractConfigNumeric<T> b)
        {
            T result = default(T);
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.Value.ToInt() + b.Value.ToInt(), typeof(T));;
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.Value.ToShort() + b.Value.ToShort(), typeof(T));;
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.Value.ToLong() + b.Value.ToLong(), typeof(T));;
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.Value.ToFloat() + b.Value.ToFloat(), typeof(T));;
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.Value.ToDouble() + b.Value.ToDouble(), typeof(T));;
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.Value.ToDecimal() + b.Value.ToDecimal(), typeof(T));;
            }
            else
            {
                throw new Exception("未实现该类型的 \"+\" 运算符重载：" + typeof(T).Name);
            }

            a.value = result;
            
            return a;
        }
        
        public static AbstractConfigNumeric<T> operator + (AbstractConfigNumeric<T> a, T b)
        {
            T result = default(T);
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.Value.ToInt() + b.ToInt(), typeof(T));;
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.Value.ToShort() + b.ToShort(), typeof(T));;
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.Value.ToLong() + b.ToLong(), typeof(T));;
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.Value.ToFloat() + b.ToFloat(), typeof(T));;
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.Value.ToDouble() + b.ToDouble(), typeof(T));;
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.Value.ToDecimal() + b.ToDecimal(), typeof(T));;
            }
            else
            {
                throw new Exception("未实现该类型的 \"+\" 运算符重载：" + typeof(T).Name);
            }
            
            a.value = result;
            
            return a;
        }
        
        public static AbstractConfigNumeric<T> operator + (T a, AbstractConfigNumeric<T> b)
        {
            T result = default(T);
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.ToInt() + b.Value.ToInt(), typeof(T));;
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.ToShort() + b.Value.ToShort(), typeof(T));;
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.ToLong() + b.Value.ToLong(), typeof(T));;
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.ToFloat() + b.Value.ToFloat(), typeof(T));;
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.ToDouble() + b.Value.ToDouble(), typeof(T));;
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.ToDecimal() + b.Value.ToDecimal(), typeof(T));;
            }
            else
            {
                throw new Exception("未实现该类型的 \"+\" 运算符重载：" + typeof(T).Name);
            }
            
            b.value = result;
            
            return b;
        }
        
        //重载运算符"-"
        public static AbstractConfigNumeric<T> operator - (AbstractConfigNumeric<T> a, AbstractConfigNumeric<T> b)
        {
            T result = default(T);
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.Value.ToInt() - b.Value.ToInt(), typeof(T));;
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.Value.ToShort() - b.Value.ToShort(), typeof(T));;
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.Value.ToLong() - b.Value.ToLong(), typeof(T));;
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.Value.ToFloat() - b.Value.ToFloat(), typeof(T));;
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.Value.ToDouble() - b.Value.ToDouble(), typeof(T));;
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.Value.ToDecimal() - b.Value.ToDecimal(), typeof(T));;
            }
            else
            {
                throw new Exception("未实现该类型的 \"-\" 运算符重载：" + typeof(T).Name);
            }
            
            a.value = result;
            
            return a;
        }
        
        public static AbstractConfigNumeric<T> operator - (AbstractConfigNumeric<T> a, T b)
        {
            T result = default(T);
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.Value.ToInt() - b.ToInt(), typeof(T));;
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.Value.ToShort() - b.ToShort(), typeof(T));;
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.Value.ToLong() - b.ToLong(), typeof(T));;
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.Value.ToFloat() - b.ToFloat(), typeof(T));;
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.Value.ToDouble() - b.ToDouble(), typeof(T));;
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.Value.ToDecimal() - b.ToDecimal(), typeof(T));;
            }
            else
            {
                throw new Exception("未实现该类型的 \"-\" 运算符重载：" + typeof(T).Name);
            }
            
            a.value = result;
            
            return a;
        }
        
        public static AbstractConfigNumeric<T> operator - (T a, AbstractConfigNumeric<T> b)
        {
            T result = default(T);
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.ToInt() - b.Value.ToInt(), typeof(T));;
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.ToShort() - b.Value.ToShort(), typeof(T));;
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.ToLong() - b.Value.ToLong(), typeof(T));;
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.ToFloat() - b.Value.ToFloat(), typeof(T));;
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.ToDouble() - b.Value.ToDouble(), typeof(T));;
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.ToDecimal() - b.Value.ToDecimal(), typeof(T));;
            }
            else
            {
                throw new Exception("未实现该类型的 \"-\" 运算符重载：" + typeof(T).Name);
            }
           
            b.value = result;
            
            return b;
        }
        
        //重载运算符"*"
        public static AbstractConfigNumeric<T> operator * (AbstractConfigNumeric<T> a, AbstractConfigNumeric<T> b)
        {
            T result = default(T);
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.Value.ToInt() * b.Value.ToInt(), typeof(T));;
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.Value.ToShort() * b.Value.ToShort(), typeof(T));;
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.Value.ToLong() * b.Value.ToLong(), typeof(T));;
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.Value.ToFloat() * b.Value.ToFloat(), typeof(T));;
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.Value.ToDouble() * b.Value.ToDouble(), typeof(T));;
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.Value.ToDecimal() * b.Value.ToDecimal(), typeof(T));;
            }
            else
            {
                throw new Exception("未实现该类型的 \"*\" 运算符重载：" + typeof(T).Name);
            }
            
            a.value = result;
            
            return a;
        }
        
        public static AbstractConfigNumeric<T> operator * (AbstractConfigNumeric<T> a, T b)
        {
            T result = default(T);
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.Value.ToInt() * b.ToInt(), typeof(T));;
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.Value.ToShort() * b.ToShort(), typeof(T));;
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.Value.ToLong() * b.ToLong(), typeof(T));;
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.Value.ToFloat() * b.ToFloat(), typeof(T));;
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.Value.ToDouble() * b.ToDouble(), typeof(T));;
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.Value.ToDecimal() * b.ToDecimal(), typeof(T));;
            }
            else
            {
                throw new Exception("未实现该类型的 \"*\" 运算符重载：" + typeof(T).Name);
            }
            a.value = result;
            
            return a;
        }
        
        public static AbstractConfigNumeric<T> operator * (T a, AbstractConfigNumeric<T> b)
        {
            T result = default(T);
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.ToInt() * b.Value.ToInt(), typeof(T));;
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.ToShort() * b.Value.ToShort(), typeof(T));;
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.ToLong() * b.Value.ToLong(), typeof(T));;
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.ToFloat() * b.Value.ToFloat(), typeof(T));;
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.ToDouble() * b.Value.ToDouble(), typeof(T));;
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.ToDecimal() * b.Value.ToDecimal(), typeof(T));;
            }
            else
            {
                throw new Exception("未实现该类型的 \"*\" 运算符重载：" + typeof(T).Name);
            }
            b.value = result;
            
            return b;
        }
        
        //重载运算符"/"
        public static AbstractConfigNumeric<T> operator / (AbstractConfigNumeric<T> a, AbstractConfigNumeric<T> b)
        {
            T result = default(T);
            
            Type type = typeof(T);
            
            if (b.Value.ToInt() == 0) throw new Exception("除数不能为0！");
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.Value.ToInt() / b.Value.ToInt(), typeof(T));;
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.Value.ToShort() / b.Value.ToShort(), typeof(T));;
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.Value.ToLong() / b.Value.ToLong(), typeof(T));;
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.Value.ToFloat() / b.Value.ToFloat(), typeof(T));;
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.Value.ToDouble() / b.Value.ToDouble(), typeof(T));;
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.Value.ToDecimal() / b.Value.ToDecimal(), typeof(T));;
            }
            else
            {
                throw new Exception("未实现该类型的 \"/\" 运算符重载：" + typeof(T).Name);
            }
            
            a.value = result;
            
            return a;
        }
        
        public static AbstractConfigNumeric<T> operator / (AbstractConfigNumeric<T> a, T b)
        {
            T result = default(T);
            
            Type type = typeof(T);
            
            if (b.ToInt() == 0) throw new Exception("除数不能为0！");
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.Value.ToInt() / b.ToInt(), typeof(T));;
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.Value.ToShort() / b.ToShort(), typeof(T));;
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.Value.ToLong() / b.ToLong(), typeof(T));;
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.Value.ToFloat() / b.ToFloat(), typeof(T));;
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.Value.ToDouble() / b.ToDouble(), typeof(T));;
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.Value.ToDecimal() / b.ToDecimal(), typeof(T));;
            }
            else
            {
                throw new Exception("未实现该类型的 \"/\" 运算符重载：" + typeof(T).Name);
            }
            a.value = result;
            
            return a;
        }
        
        public static AbstractConfigNumeric<T> operator / (T a, AbstractConfigNumeric<T> b)
        {
            T result = default(T);
            
            Type type = typeof(T);

            if (b.Value.ToInt() == 0) throw new Exception("除数不能为0！");
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.ToInt() / b.Value.ToInt(), typeof(T));;
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.ToShort() / b.Value.ToShort(), typeof(T));;
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.ToLong() / b.Value.ToLong(), typeof(T));;
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.ToFloat() / b.Value.ToFloat(), typeof(T));;
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.ToDouble() / b.Value.ToDouble(), typeof(T));;
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.ToDecimal() / b.Value.ToDecimal(), typeof(T));;
            }
            else
            {
                throw new Exception("未实现该类型的 \"/\" 运算符重载：" + typeof(T).Name);
            }
            b.value = result;
            
            return b;
        }
        
    }
}