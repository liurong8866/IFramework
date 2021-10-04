using System;
using System.Collections;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace IFramework.Core
{
    /// <summary>
    /// C# 基础类型扩展方法
    /// </summary>
    public static class BaseExtention
    {
        /// <summary>
        /// 判断是否为空
        /// </summary>
        public static bool IsNullOrEmpty(this object value)
        {
            bool result;

            if (value == null) {
                result = true;
            }
            else {
                result = string.IsNullOrEmpty(value.ToString());
            }
            return result;
        }

        /// <summary>
        /// 判断是否不为空
        /// </summary>
        public static bool IsNullOrEmpty(this ICollection value)
        {
            return value == null || value.Count == 0;
        }

        /// <summary>
        /// 判断是否不为空
        /// </summary>
        public static bool IsNotNullOrEmpty(this object value)
        {
            return !IsNullOrEmpty(value);
        }

        /// <summary>
        /// 判断是否不为空
        /// </summary>
        public static bool IsNotNullOrEmpty(this ICollection value)
        {
            return !IsNullOrEmpty(value);
        }

        /// <summary>
        /// 转换字符串
        /// </summary>
        public static string ToString(this object value)
        {
            string result = "";

            if (!value.IsNullOrEmpty()) {
                result = value.ToString();
            }
            return result;
        }

        /// <summary>
        /// 转换字符串
        /// </summary>
        public static string ToString(this object value, string defaultvalue)
        {
            string result = defaultvalue;

            if (!value.IsNullOrEmpty()) {
                result = value.ToString();
            }
            return result;
        }

        /// <summary>
        /// 对象转换Short
        /// </summary>
        public static short ToShort(this object value)
        {
            return value.IsNullOrEmpty() ? default : Convert.ToInt16(value);
        }

        /// <summary>
        /// 对象转换Int
        /// </summary>
        public static int ToInt(this object value)
        {
            return value.IsNullOrEmpty() ? default : Convert.ToInt32(value);
        }

        /// <summary>
        /// 对象转换Long
        /// </summary>
        public static long ToLong(this object value)
        {
            return value.IsNullOrEmpty() ? default : Convert.ToInt64(value);
        }

        /// <summary>
        /// 字符串转换Float
        /// </summary>
        public static float ToFloat(this object value)
        {
            return value.IsNullOrEmpty() ? default : Convert.ToSingle(value);
        }

        /// <summary>
        /// 字符串转换Double
        /// </summary>
        public static double ToDouble(this object value)
        {
            return value.IsNullOrEmpty() ? default : Convert.ToDouble(value);
        }

        /// <summary>
        /// 字符串转换Decimal
        /// </summary>
        public static decimal ToDecimal(this object value)
        {
            return value.IsNullOrEmpty() ? default : Convert.ToDecimal(value);
        }

        /// <summary>
        /// 转换为DateTime型
        /// </summary>
        public static DateTime ToDateTime(this object value)
        {
            return ToDateTime(value, DateTime.Parse("1970-1-1 00:00:01"));
        }

        /// <summary>
        /// 转换为DateTime型
        /// </summary>
        public static DateTime ToDateTime(this object value, DateTime defaultValue)
        {
            return value.IsNullOrEmpty() ? defaultValue : Convert.ToDateTime(value);
        }

        /// <summary>
        /// SQL 防止意外字符导致错误
        /// </summary>
        public static string ToSqlString(this object value)
        {
            string result;

            if (value == null) {
                result = "";
            }
            else {
                result = value.ToString().Trim().Replace("'", "''").Replace("]", "]]").Replace("%", "[%]").Replace("_", "[_]").Replace("^", "[^]");
            }
            return result;
        }

        /// <summary>
        /// SQL 防止意外字符导致错误
        /// </summary>
        public static string ToSqlString2(this object value)
        {
            string result;

            if (value == null) {
                result = "";
            }
            else {
                result = value.ToString().Trim().Replace("'", "’").Replace("]", "］").Replace("%", "％").Replace("_", "＿").Replace("^", "＾");
            }
            return result;
        }

        /// <summary>
        /// 是否为整型数值
        /// </summary>
        public static bool IsInteger(this object value)
        {
            return value is sbyte || value is short || value is int || value is long || value is byte || value is ushort || value is uint || value is ulong;
        }

        /// <summary>
        /// 是否为浮点型
        /// </summary>
        public static bool IsFloat(this object value)
        {
            return value is float | value is double | value is decimal;
        }

        /// <summary>
        /// 是否为数字
        /// </summary>
        public static bool IsNumeric(this object value)
        {
            if (!(value is byte || value is short || value is int || value is long || value is sbyte || value is ushort || value is uint || value is ulong || value is decimal || value is double || value is float)) return false;

            return true;
        }

        /// <summary>
        /// 取得介于min与max范围内的值，如果不在范围内，取最近的值
        /// </summary>
        public static T Between<T>(this T value, T min, T max) where T : struct, IComparable<T>
        {
            T result = value;

            if (value.CompareTo(min) < 0) {
                result = min;
            }
            else if (value.CompareTo(max) > 0) {
                result = max;
            }
            return result;
        }

        /// <summary>
        /// 取得介于min与max范围内的值,如果不在范围内，取默认值
        /// </summary>
        public static T Between<T>(this T value, T min, T max, T defaultvalue) where T : struct, IComparable<T>
        {
            T result = value;

            if (value.CompareTo(min) < 0) {
                result = defaultvalue;
            }
            else if (value.CompareTo(max) > 0) {
                result = defaultvalue;
            }
            return result;
        }

        /// <summary>
        /// 克隆对象
        /// </summary>
        public static T Clone<T>(this T value)
        {
            using (Stream objectStream = new MemoryStream()) {
                //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制  
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, value);
                objectStream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(objectStream);
            }
        }
    }
}
