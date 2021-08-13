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
    public abstract class AbstractConfigNumeric<T> : AbstractPropertyNumeric<T>, IPersistable<T> where T : struct, IConvertible, IComparable
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
            m.Value =  Addition(m.Value, n.Value);
            return m;
        }
        
        public static AbstractConfigNumeric<T> operator + (AbstractConfigNumeric<T> m, T n)
        {
            m.Value =  Addition(m.Value, n);
            return m;
        }
        
        public static AbstractConfigNumeric<T> operator + (T m, AbstractConfigNumeric<T> n)
        {
            n.Value =  Addition(m, n.Value);
            return n;
        }
        
        //重载运算符"-"
        public static AbstractConfigNumeric<T> operator - (AbstractConfigNumeric<T> m, AbstractConfigNumeric<T> n)
        {
            n.Value =  Subtraction(m.Value, n.Value);
            return n;
        }
        
        public static AbstractConfigNumeric<T> operator - (AbstractConfigNumeric<T> m, T n)
        {
            m.Value =  Subtraction(m.Value, n);
            return m;
        }
        
        public static AbstractConfigNumeric<T> operator - (T m, AbstractConfigNumeric<T> n)
        {
            n.Value =  Subtraction(m, n.Value);
            return n;
        }
        
        //重载运算符"*"
        public static AbstractConfigNumeric<T> operator * (AbstractConfigNumeric<T> m, AbstractConfigNumeric<T> n)
        {
            m.Value =  Multiply(m.Value, n.Value);
            return m;
        }
        
        public static AbstractConfigNumeric<T> operator * (AbstractConfigNumeric<T> m, T n)
        {
            m.Value =  Multiply(m.Value, n);
            return m;
        }
        
        public static AbstractConfigNumeric<T> operator * (T m, AbstractConfigNumeric<T> n)
        {
            n.Value =  Multiply(m, n.Value);
            return n;
        }
        
        //重载运算符"/"
        public static AbstractConfigNumeric<T> operator / (AbstractConfigNumeric<T> m, AbstractConfigNumeric<T> n)
        {
            m.Value =  Division(m.Value, n.Value);
            return m;
        }
        
        public static AbstractConfigNumeric<T> operator / (AbstractConfigNumeric<T> m, T n)
        {
            m.Value =  Division(m.Value, n);
            return m;
        }
        
        public static AbstractConfigNumeric<T> operator / (T m, AbstractConfigNumeric<T> n)
        {
            n.Value =  Division(m, n.Value);
            return n;
        }
        
        //重载运算符"%"
        public static AbstractConfigNumeric<T> operator % (AbstractConfigNumeric<T> m, AbstractConfigNumeric<T> n)
        {
            m.Value =  Module(m.Value, n.Value);
            return m;
        }
        
        public static AbstractConfigNumeric<T> operator % (AbstractConfigNumeric<T> m, T n)
        {
            m.Value =  Module(m.Value, n);
            return m;
        }
        
        public static AbstractConfigNumeric<T> operator % (T m, AbstractConfigNumeric<T> n)
        {
            n.Value =  Module(m, n.Value);
            return n;
        }
        
    }
}