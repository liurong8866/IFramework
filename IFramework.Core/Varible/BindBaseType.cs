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
using System.Globalization;
using UnityEditor;

namespace IFramework.Core
{
    public abstract class BindBaseType<T> : Bindable<T> where T : IConvertible, IComparable
    {
        public BindBaseType(){}
        
        public BindBaseType(T value) : base(value){ }
        
        //重载运算符"+"
        public static T operator + (BindBaseType<T> a, BindBaseType<T> b)
        {
            T result = default(T);
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.Value.ToInt() > b.Value.ToInt(), typeof(T));;
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.Value.ToShort() > b.Value.ToShort(), typeof(T));;
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.Value.ToLong() > b.Value.ToLong(), typeof(T));;
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.Value.ToFloat() > b.Value.ToFloat(), typeof(T));;
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.Value.ToDouble() > b.Value.ToDouble(), typeof(T));;
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.Value.ToDecimal() > b.Value.ToDecimal(), typeof(T));;
            }
            else
            {
                throw new Exception("未实现该类型的 \"+\" 运算符重载：" + typeof(T).Name);
            }
            
            return result;
        }
        
        public static T operator + (BindBaseType<T> a, T b)
        {
            T result = default(T);
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.Value.ToInt() > b.ToInt(), typeof(T));;
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.Value.ToShort() > b.ToShort(), typeof(T));;
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.Value.ToLong() > b.ToLong(), typeof(T));;
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.Value.ToFloat() > b.ToFloat(), typeof(T));;
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.Value.ToDouble() > b.ToDouble(), typeof(T));;
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.Value.ToDecimal() > b.ToDecimal(), typeof(T));;
            }
            else
            {
                throw new Exception("未实现该类型的 \"+\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }
        
        public static T operator + (T a, BindBaseType<T> b)
        {
            T result = default(T);
            
            Type type = typeof(T);
            
            if (type == typeof(int))
            {
                result = (T)Convert.ChangeType(a.ToInt() > b.Value.ToInt(), typeof(T));;
            }
            else if (type == typeof(short))
            {
                result = (T)Convert.ChangeType(a.ToShort() > b.Value.ToShort(), typeof(T));;
            }
            else if (type == typeof(long))
            {
                result = (T)Convert.ChangeType(a.ToLong() > b.Value.ToLong(), typeof(T));;
            }
            else if (type == typeof(float))
            {
                result = (T)Convert.ChangeType(a.ToFloat() > b.Value.ToFloat(), typeof(T));;
            }
            else if (type == typeof(double))
            {
                result = (T)Convert.ChangeType(a.ToDouble() > b.Value.ToDouble(), typeof(T));;
            }
            else if (type == typeof(decimal))
            {
                result = (T)Convert.ChangeType(a.ToDecimal() > b.Value.ToDecimal(), typeof(T));;
            }
            else
            {
                throw new Exception("未实现该类型的 \"+\" 运算符重载：" + typeof(T).Name);
            }
            return result;
        }
        
        //重载运算符"-"
        public static T operator - (BindBaseType<T> a, BindBaseType<T> b)
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
            
            return result;
        }
        
        public static T operator - (BindBaseType<T> a, T b)
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
            return result;
        }
        
        public static T operator - (T a, BindBaseType<T> b)
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
            return result;
        }
        
        //重载运算符"*"
        public static T operator * (BindBaseType<T> a, BindBaseType<T> b)
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
            
            return result;
        }
        
        public static T operator * (BindBaseType<T> a, T b)
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
            return result;
        }
        
        public static T operator * (T a, BindBaseType<T> b)
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
            return result;
        }
        
        //重载运算符"/"
        public static T operator / (BindBaseType<T> a, BindBaseType<T> b)
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
            
            return result;
        }
        
        public static T operator / (BindBaseType<T> a, T b)
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
            return result;
        }
        
        public static T operator / (T a, BindBaseType<T> b)
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
            return result;
        }
        
        //重载运算符">"
        public static bool operator > (BindBaseType<T> a, BindBaseType<T> b)
        {
            return a.Value.ToDouble() > b.Value.ToDouble();
        }
        
        public static bool operator > (BindBaseType<T> a, T b)
        {
            return a.Value.ToDouble() > b.ToDouble();
        }
        
        public static bool operator > (T a, BindBaseType<T> b)
        {
            return a.ToDouble() > b.Value.ToDouble();
        }
        
        //重载运算符"<"
        public static bool operator < (BindBaseType<T> a, BindBaseType<T> b)
        {
            return a.Value.ToDouble() < b.Value.ToDouble();
        }
        
        public static bool operator < (BindBaseType<T> a, T b)
        {
            return a.Value.ToDouble() < b.ToDouble();
        }
        
        public static bool operator < (T a, BindBaseType<T> b)
        {
            return a.ToDouble() < b.Value.ToDouble();
        }
        
        //重载运算符">="
        public static bool operator >= (BindBaseType<T> a, BindBaseType<T> b)
        {
            return a.Value.ToDouble() >= b.Value.ToDouble();
        }
        
        public static bool operator >= (BindBaseType<T> a, T b)
        {
            return a.Value.ToDouble() >= b.ToDouble();
        }
        
        public static bool operator >= (T a, BindBaseType<T> b)
        {
            return a.ToDouble() >= b.Value.ToDouble();
        }
        
        //重载运算符"<="
        public static bool operator <= (BindBaseType<T> a, BindBaseType<T> b)
        {
            return a.Value.ToDouble() <= b.Value.ToDouble();
        }
        
        public static bool operator <= (BindBaseType<T> a, T b)
        {
            return a.Value.ToDouble() <= b.ToDouble();
        }
        
        public static bool operator <= (T a, BindBaseType<T> b)
        {
            return a.ToDouble() <= b.Value.ToDouble();
        }
    }
    

    // 基础类型扩展
    
    public class BindInt : BindBaseType<int>
    {
        public BindInt(){}
        public BindInt(int value) : base(value){ }
    }
    public class BindShort : BindBaseType<short> {
        public BindShort(){}
        public BindShort(short value) : base(value){ }
    }
    public class BindLong : BindBaseType<long>{
        public BindLong(){}
        public BindLong(long value) : base(value){ }
    }
    public class BindFloat : BindBaseType<float>{
        public BindFloat(){}
        public BindFloat(float value) : base(value){ }
    }
    public class BindDouble : BindBaseType<double>{
        public BindDouble(){}
        public BindDouble(double value) : base(value){ }
    }
    public class BindDecimal : BindBaseType<decimal>{
        public BindDecimal(){}
        public BindDecimal(decimal value) : base(value){ }
    }
    
}