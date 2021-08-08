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
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IFramework.Engine
{
    public static class StringExtention
    {
        public static void Example()
        {
            var emptyStr = string.Empty;
            emptyStr.IsNotNullAndEmpty();
            emptyStr.IsNullOrEmpty();
            emptyStr = emptyStr.Append("appended").Append("1").ToString();
            emptyStr.IsNullOrEmpty();
        }

        /// <summary>
        /// Check Whether string is null or empty
        /// </summary>
        /// <param name="selfStr"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string selfStr)
        {
            return string.IsNullOrEmpty(selfStr);
        }

        /// <summary>
        /// Check Whether string is null or empty
        /// </summary>
        /// <param name="selfStr"></param>
        /// <returns></returns>
        public static bool IsNotNullAndEmpty(this string selfStr)
        {
            return !string.IsNullOrEmpty(selfStr);
        }

        /// <summary>
        /// Check Whether string trim is null or empty
        /// </summary>
        /// <param name="selfStr"></param>
        /// <returns></returns>
        public static bool IsTrimNotNullAndEmpty(this string selfStr)
        {
            return selfStr != null && !string.IsNullOrEmpty(selfStr.Trim());
        }

        public static bool IsTrimNullOrEmpty(this string selfStr)
        {
            return selfStr == null || string.IsNullOrEmpty(selfStr.Trim());
        }

        /// <summary>
        /// 缓存
        /// </summary>
        private static readonly char[] mCachedSplitCharArray = {'.'};

        /// <summary>
        /// Split
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="splitSymbol"></param>
        /// <returns></returns>
        public static string[] Split(this string selfStr, char splitSymbol)
        {
            mCachedSplitCharArray[0] = splitSymbol;
            return selfStr.Split(mCachedSplitCharArray);
        }

        /// <summary>
        /// 首字母大写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UppercaseFirst(this string str)
        {
            return char.ToUpper(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// 首字母小写
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string LowercaseFirst(this string str)
        {
            return char.ToLower(str[0]) + str.Substring(1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUnixLineEndings(this string str)
        {
            return str.Replace("\r\n", "\n").Replace("\r", "\n");
        }

        /// <summary>
        /// 转换成 CSV
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string ToCSV(this string[] values)
        {
            return string.Join(", ", values
                .Where(value => !string.IsNullOrEmpty(value))
                .Select(value => value.Trim())
                .ToArray()
            );
        }

        public static string[] ArrayFromCSV(this string values)
        {
            return values
                .Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries)
                .Select(value => value.Trim())
                .ToArray();
        }

        public static string ToSpacedCamelCase(this string text)
        {
            var sb = new StringBuilder(text.Length * 2);
            sb.Append(char.ToUpper(text[0]));
            for (var i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                {
                    sb.Append(' ');
                }

                sb.Append(text[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 有点不安全,编译器不会帮你排查错误。
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string FillFormat(this string selfStr, params object[] args)
        {
            return string.Format(selfStr, args);
        }

        /// <summary>
        /// 添加前缀
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="toAppend"></param>
        /// <returns></returns>
        public static StringBuilder Append(this string selfStr, string toAppend)
        {
            return new StringBuilder(selfStr).Append(toAppend);
        }

        /// <summary>
        /// 添加后缀
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="toPrefix"></param>
        /// <returns></returns>
        public static string AddPrefix(this string selfStr, string toPrefix)
        {
            return new StringBuilder(toPrefix).Append(selfStr).ToString();
        }

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="toAppend"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static StringBuilder AppendFormat(this string selfStr, string toAppend, params object[] args)
        {
            return new StringBuilder(selfStr).AppendFormat(toAppend, args);
        }

        /// <summary>
        /// 最后一个单词
        /// </summary>
        /// <param name="selfUrl"></param>
        /// <returns></returns>
        public static string LastWord(this string selfUrl)
        {
            return selfUrl.Split('/').Last();
        }

        /// <summary>
        /// 解析成数字类型
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="defaulValue"></param>
        /// <returns></returns>
        public static int ToInt(this string selfStr, int defaulValue = 0)
        {
            var retValue = defaulValue;
            return int.TryParse(selfStr, out retValue) ? retValue : defaulValue;
        }

        /// <summary>
        /// 解析到时间类型
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this string selfStr, DateTime defaultValue = default(DateTime))
        {
            var retValue = defaultValue;
            return DateTime.TryParse(selfStr, out retValue) ? retValue : defaultValue;
        }


        /// <summary>
        /// 解析 Float 类型
        /// </summary>
        /// <param name="selfStr"></param>
        /// <param name="defaulValue"></param>
        /// <returns></returns>
        public static float ToFloat(this string selfStr, float defaulValue = 0)
        {
            var retValue = defaulValue;
            return float.TryParse(selfStr, out retValue) ? retValue : defaulValue;
        }

        /// <summary>
        /// 是否存在中文字符
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool HasChinese(this string input)
        {
            return Regex.IsMatch(input, @"[\u4e00-\u9fa5]");
        }

        /// <summary>
        /// 是否存在空格
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool HasSpace(this string input)
        {
            return input.Contains(" ");
        }

        /// <summary>
        /// 删除特定字符
        /// </summary>
        /// <param name="str"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string RemoveString(this string str, params string[] targets)
        {
            return targets.Aggregate(str, (current, t) => current.Replace(t, string.Empty));
        }
    }
}