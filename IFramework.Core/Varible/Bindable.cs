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
        /// 判断是否值改变
        /// </summary>
        protected virtual bool IsValueChanged(T value)
        {
            return value == null || !value.Equals(this.value) || !this.setted;
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
            return Compare(a, b);
        }
        public static bool operator != (Bindable<T> a, Bindable<T> b)
        {
            return !Compare(a, b);
        }
        private static bool Compare(Bindable<T> a, Bindable<T> b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            else if (a == null || b == null)
            {
                return false;
            }
            else
            {
                return a.Value.Equals(b.Value);
            }
        }
        
        
        public static bool operator == (Bindable<T> a, T b)
        {
            return Compare(a, b);
        }
        public static bool operator != (Bindable<T> a, T b)
        {
            return !Compare(a, b);
        }
        private static bool Compare(Bindable<T> a, T b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            else if (a == null || b == null)
            {
                return false;
            }
            else
            {
                return a.Value.Equals(b);
            }
        }
        
        
        public static bool operator == (T a, Bindable<T> b)
        {
            return Compare(a, b);
        }
        public static bool operator != (T a, Bindable<T> b)
        {
            return !Compare(a, b);
        }
        private static bool Compare(T a, Bindable<T> b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            else if (a == null || b == null)
            {
                return false;
            }
            else
            {
                return a.Equals(b.Value);
            }
        }

        public override bool Equals(object obj)
        {
            Type type = obj.GetType();
            
            if (obj == null) return false;

            if (type == typeof(Bindable<T>))
            {
                Bindable<T> compareObj = obj as Bindable<T>;
                return this.Value.Equals(compareObj.Value);
            }
            
            if (type == typeof(T))
            {
                T compareObj = (T) obj;
                return this.Value.Equals(compareObj);
            }
            
            return false;
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode();
        }
    }
    
    public class BindBool : Bindable<bool>{
        public BindBool(){}
        public BindBool(bool value) : base(value){ }
    }
    public class BindString : Bindable<string>{
        public BindString(){}
        public BindString(string value) : base(value){ }
    }
    public class BindChar : Bindable<char>{
        public BindChar(){}
        public BindChar(char value) : base(value){ }
    }
}