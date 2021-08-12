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
    /// 可绑定事件的变量
    /// </summary>
    public class Bindable<T> : Property<T>
    {
        public Action<T> OnChange;
        
        public Bindable(){}
        
        public Bindable(T value) : base(value){ }
        
        protected override T GetValue()
        {
            return value;
        }
        
        protected override void SetValue(T value)
        {
            if (IsValueChanged(value))
            {
                this.value = value;

                OnChange?.Invoke(value);
                
                this.setted = true;
            }
        }

        /// <summary>
        /// 注销事件
        /// </summary>
        public override void Dispose()
        {
            OnChange = null;
        }
        
        //重载运算符"=="
        public static bool operator == (Bindable<T> a, Bindable<T> b)
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
               
        public static bool operator == (Bindable<T> a, T b)
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
     
        public static bool operator == (T a, Bindable<T> b)
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

        public static bool operator != (Bindable<T> a, Bindable<T> b)
        {
            return !(a==b);
        }
 
        public static bool operator != (Bindable<T> a, T b)
        {
            return !(a==b);
        }
   
        public static bool operator != (T a, Bindable<T> b)
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
            
            if(obj.GetType() == typeof(Bindable<T>))
            {
                Bindable<T> bindable = obj as Bindable<T>;
                return Equals(bindable);
            }
            
            // 判断类型Value是否一致
            if (obj.GetType() != typeof(T))
            {
                return false;
            }
            
            return this.Value.Equals(obj) ;
        }
        
        public bool Equals(Bindable<T> bindable)
        {
            if ((object)bindable == null)
            {
                return false;
            }

            return this.Value.Equals(bindable.Value) ;
        }
        
        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
    
    public class BindBool : Bindable<bool>
    {
        public BindBool(){}
        public BindBool(bool value) : base(value){ }
    }
    public class BindString : Bindable<string>
    {
        public BindString(){}
        public BindString(string value) : base(value){ }
    }
    public class BindChar : Bindable<char>
    {
        public BindChar(){}
        public BindChar(char value) : base(value){ }
    }
}