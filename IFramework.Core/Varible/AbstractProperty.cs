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
    /// 变量自定义基础类
    /// </summary>
    [Serializable]
    public abstract class AbstractProperty<T> : IDisposable
    {
        // 变量值
        protected T value;

        public AbstractProperty(){}
        
        public AbstractProperty(T value)
        {
            this.value = value;
        }
        
        // 解决因其他原因导致值未设置，而不触发事件问题
        protected bool setted = false;

        public T Value
        {
            get => GetValue();
            set => SetValue(value);
        }

        /// <summary>
        /// 判断是否值改变
        /// </summary>
        protected virtual bool IsValueChanged(T value)
        {
            return value == null || !value.Equals(this.value) || !this.setted;
        }
        
        public override string ToString()
        {
            return GetValue().ToString();
        }

        public virtual void Dispose() {}
        
        // 子类需要实现的抽象方法
        protected abstract T GetValue();
        protected abstract void SetValue(T value);
        
        
        //重载运算符"=="
        public static bool operator == (AbstractProperty<T> a, AbstractProperty<T> b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Value.Equals(b.Value);
        }
               
        public static bool operator == (AbstractProperty<T> a, T b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Value.Equals(b);
        }
     
        public static bool operator == (T a, AbstractProperty<T> b)
        {
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return a.Equals(b.Value);
        }

        public static bool operator != (AbstractProperty<T> a, AbstractProperty<T> b)
        {
            return !(a==b);
        }
 
        public static bool operator != (AbstractProperty<T> a, T b)
        {
            return !(a==b);
        }
   
        public static bool operator != (T a, AbstractProperty<T> b)
        {
            return !(a==b);
        }
        
        public override bool Equals(System.Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            // 引用地址如果不同，则返回false
            if (!ReferenceEquals(this, obj))
            {
                return false;
            }
            
            if(obj.GetType() == typeof(AbstractProperty<T>))
            {
                AbstractProperty<T> abstractProperty = obj as AbstractProperty<T>;
                return Equals(abstractProperty);
            }
            
            // 判断类型Value是否一致
            if (obj.GetType() != typeof(T))
            {
                return false;
            }
            
            return this.Value.Equals(obj) ;
        }
        
        public bool Equals(AbstractProperty<T> abstractProperty)
        {
            if ((object)abstractProperty == null)
            {
                return false;
            }

            return this.Value.Equals(abstractProperty.Value) ;
        }
        
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
}