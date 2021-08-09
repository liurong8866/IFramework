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
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace IFramework.Engine
{
    public static class BaseExtention
    {
        #region object 扩展

        /// <summary>
        /// 转换字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultvalue">默认值</param>
        /// <returns></returns>
        public static string ToString(this object value)
        {
            string result = "";

            if (!value.IsNullOrEmpty())
            {
                result = value.ToString();
            }

            return result;
        }

        /// <summary>
        /// 转换字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultvalue">默认值</param>
        /// <returns></returns>
        public static string ToString(this object value, string defaultvalue)
        {
            string result = defaultvalue;

            if (!value.IsNullOrEmpty())
            {
                result = value.ToString();
            }

            return result;
        }

        /// <summary>
        /// 对象转换Short
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static short ToShort(this object value)
        {
            short result;

            if (!value.IsNullOrEmpty())
            {
                bool parse = Int16.TryParse(value.ToString(), out result);

                if (parse != true)
                {
                    result = default(short);
                }
            }
            else
            {
                result = default(short);
            }

            return result;
        }

        /// <summary>
        /// 对象转换Int
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int ToInt(this object value)
        {
            int result;

            if (!value.IsNullOrEmpty())
            {
                bool parse = Int32.TryParse(value.ToString(), out result);

                if (parse != true)
                {
                    result = default(int);
                }
            }
            else
            {
                result = default(int);
            }

            return result;
        }

        /// <summary>
        /// 对象转换Long
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static long ToLong(this object value)
        {
            long result;

            if (!value.IsNullOrEmpty())
            {
                bool parse = Int64.TryParse(value.ToString(), out result);

                if (parse != true)
                {
                    result = default(long);
                }
            }
            else
            {
                result = default(long);
            }

            return result;
        }

        /// <summary>
        /// 字符串转换Float
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static float ToFloat(this object value)
        {
            float result;

            if (!value.IsNullOrEmpty())
            {
                bool parse = float.TryParse(value.ToString(), out result);

                if (parse != true)
                {
                    result = default(float);
                }
            }
            else
            {
                result = default(float);
            }

            return result;
        }

        /// <summary>
        /// 字符串转换Float
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double ToDouble(this object value)
        {
            double result;

            if (!value.IsNullOrEmpty())
            {
                bool parse = double.TryParse(value.ToString(), out result);

                if (parse != true)
                {
                    result = default(double);
                }
            }
            else
            {
                result = default(double);
            }

            return result;
        }

        /// <summary>
        /// 转换为DateTime型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object value)
        {
            return ToDateTime(value, DateTime.Parse("1970-1-1 00:00:01"));
        }

        /// <summary>
        /// 转换为DateTime型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this object value, DateTime defaultValue)
        {
            DateTime result;

            if (!value.IsNullOrEmpty())
            {
                bool parse = DateTime.TryParse(value.ToString(), out result);

                if (parse != true)
                {
                    result = defaultValue;
                }
            }
            else
            {
                result = defaultValue;
            }

            return result;
        }

        /// <summary>
        /// SQL 防止意外字符导致错误
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToSQLString(this object value)
        {
            string result;

            if (value == null)
            {
                result = "";
            }
            else
            {
                result = value.ToString().Trim().Replace("'", "''").Replace("]", "]]").Replace("%", "[%]").Replace("_", "[_]").Replace("^", "[^]");
            }

            return result;
        }

        /// <summary>
        /// SQL 防止意外字符导致错误
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToSQLString2(this object value)
        {
            string result;

            if (value == null)
            {
                result = "";
            }
            else
            {
                result = value.ToString().Trim().Replace("'", "’").Replace("]", "］").Replace("%", "％").Replace("_", "＿").Replace("^", "＾");
            }

            return result;
        }

        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object value)
        {
            bool result;

            if (value == null)
            {
                result = true;
            }
            else
            {
                result = String.IsNullOrEmpty(value.ToString());
            }
            return result;
        }

        /// <summary>
        /// 判断是否不为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNotNullOrEmpty(this object value)
        {
            return !IsNullOrEmpty(value);
        }
        
        /// <summary>
        /// 是否为整型数值
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsInteger(this object value)
        {
            return (value is SByte || value is Int16 || value is Int32
                    || value is Int64 || value is Byte || value is UInt16
                    || value is UInt32 || value is UInt64);
        }

        /// <summary>
        /// 是否为浮点型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsFloat(this object value)
        {
            return (value is float | value is double | value is Decimal);
        }

        /// <summary>
        /// 是否为数字
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(this object value)
        {
            if (!(value is Byte ||
                    value is Int16 ||
                    value is Int32 ||
                    value is Int64 ||
                    value is SByte ||
                    value is UInt16 ||
                    value is UInt32 ||
                    value is UInt64 ||
                    value is Decimal ||
                    value is Double ||
                    value is Single))
                return false;
            else
                return true;
        }

        /// <summary>
        /// 取得介于min与max范围内的值，如果不在范围内，取最近的值
        /// </summary>
        public static T Between<T>(this T value, T min, T max) where T : struct,IComparable<T>
        {
            T result = value;

            if (value.CompareTo(min) < 0)
            {
                result = min;
            }
            else if (value.CompareTo(max) > 0)
            {
                result = max;
            }

            return result;
        }

        /// <summary>
        /// 取得介于min与max范围内的值,如果不在范围内，取默认值
        /// </summary>
        public static T Between<T>(this T value, T min, T max, T defaultvalue) where T : struct,IComparable<T>
        {
            T result = value;

            if (value.CompareTo(min) < 0)
            {
                result = defaultvalue;
            }
            else if (value.CompareTo(max) > 0)
            {
                result = defaultvalue;
            }

            return result;
        }

        /// <summary>
        /// 克隆对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T Clone<T>(this T value)
        {
            using (Stream objectStream = new MemoryStream())
            {
                //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制  
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, value);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(objectStream);
            }
        }

        #endregion
    }
}