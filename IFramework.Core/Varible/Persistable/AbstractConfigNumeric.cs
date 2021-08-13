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
    /// 可持久化的数字类型抽象类
    /// </summary>
    public abstract class AbstractConfigNumeric<T> : AbstractPropertyNumeric<T>, IPersistable<T> where T : IConvertible, IComparable
    {
        protected string key;
        protected AbstractConfigNumeric(string key, T value)
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
                this.value = value;

                Save(value);
                
                setted = true;
            }
        }

        public abstract T Get();

        public abstract void Save(T value);
        
        
        //重载运算符"+"
        public static AbstractConfigNumeric<T> operator + (AbstractConfigNumeric<T> m, AbstractConfigNumeric<T> n)
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

            m.Value = result;
            
            return m;
        }
        
        public static AbstractConfigNumeric<T> operator + (AbstractConfigNumeric<T> m, T n)
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
            
            m.Value = result;
            
            return m;
        }
        
        public static AbstractConfigNumeric<T> operator + (T m, AbstractConfigNumeric<T> n)
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
            
            n.Value = result;
            
            return n;
        }
        
        //重载运算符"-"
        public static AbstractConfigNumeric<T> operator - (AbstractConfigNumeric<T> m, AbstractConfigNumeric<T> n)
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
            
            m.Value = result;
            
            return m;
        }
        
        public static AbstractConfigNumeric<T> operator - (AbstractConfigNumeric<T> m, T n)
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
            
            m.Value = result;
            
            return m;
        }
        
        public static AbstractConfigNumeric<T> operator - (T m, AbstractConfigNumeric<T> n)
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
           
            n.Value = result;
            
            return n;
        }
        
        //重载运算符"*"
        public static AbstractConfigNumeric<T> operator * (AbstractConfigNumeric<T> m, AbstractConfigNumeric<T> n)
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
            
            m.Value = result;
            
            return m;
        }
        
        public static AbstractConfigNumeric<T> operator * (AbstractConfigNumeric<T> m, T n)
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
            m.Value = result;
            
            return m;
        }
        
        public static AbstractConfigNumeric<T> operator * (T m, AbstractConfigNumeric<T> n)
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
            n.Value = result;
            
            return n;
        }
        
        //重载运算符"/"
        public static AbstractConfigNumeric<T> operator / (AbstractConfigNumeric<T> m, AbstractConfigNumeric<T> n)
        {
            T result;
            
            Type type = typeof(T);
            
            if (n.Value.ToInt() == 0) throw new Exception("除数不能为0！");
            
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
            
            m.Value = result;
            
            return m;
        }
        
        public static AbstractConfigNumeric<T> operator / (AbstractConfigNumeric<T> m, T n)
        {
            T result;
            
            Type type = typeof(T);
            
            if (n.ToInt() == 0) throw new Exception("除数不能为0！");
            
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
            m.Value = result;
            
            return m;
        }
        
        public static AbstractConfigNumeric<T> operator / (T m, AbstractConfigNumeric<T> n)
        {
            T result;
            
            Type type = typeof(T);

            if (n.Value.ToInt() == 0) throw new Exception("除数不能为0！");
            
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
            n.Value = result;
            
            return n;
        }
        
    }
}