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

namespace IFramework.Core
{
    /// <summary>
    /// C# 字符串扩展方法
    /// </summary>
    public static class StringExtention
    {
        /// <summary>
        /// 判断是否为空
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// 截取字符串左面
        /// </summary>
        /// <param name="value">数据源</param>
        /// <param name="length">截取长度</param>
        /// <returns></returns>
        public static string Left(this string value, int length)
        {
            //返回非贪婪数据
            return Left(value, length, false);
        }

        /// <summary>
        /// 截取字符串左面
        /// </summary>
        /// <param name="value">数据源</param>
        /// <param name="length">截取长度</param>
        /// <param name="greedy">是否贪婪(true:标识截取长度超出字符串则返回剩余长度。false:返回空)</param>
        /// <returns></returns>
        public static string Left(this string value, int length, bool greedy)
        {
            string result = "";

            //如果不为空则
            if (!value.IsNullOrEmpty())
            {
                int len = value.Length;

                //如果大于截取长度则
                if (len > length)
                {
                    result = value.Substring(0, length);
                }
                else
                {
                    //greedy true:标识截取长度超出字符串则返回剩余长度。false:返回空
                    result = greedy ? value : "";
                }
            }
            else
            {
                result = "";
            }

            return result;
        }

        /// <summary>
        /// 截取字符串右面
        /// </summary>
        /// <param name="value">数据源</param>
        /// <param name="length">截取长度</param>
        /// <returns></returns>
        public static string Right(this string value, int length)
        {
            //返回非贪婪数据
            return Right(value, length, false);
        }

        /// <summary>
        /// 截取字符串右面
        /// </summary>
        /// <param name="value">数据源</param>
        /// <param name="length">截取长度</param>
        /// <param name="greedy">是否贪婪(true:标识截取长度超出字符串则返回剩余长度。false:返回空)</param>
        /// <returns></returns>
        public static string Right(this string value, int length, bool greedy)
        {
            string result = "";

            //如果不为空则
            if (!value.IsNullOrEmpty())
            {
                int len = value.Length;

                //如果大于截取长度则
                if (len > length)
                {
                    result = value.Substring(len - length);
                }
                else
                {
                    //greedy true:标识截取长度超出字符串则返回剩余长度。false:返回空
                    result = greedy ? value : "";
                }
            }
            else
            {
                result = "";
            }

            return result;
        }

        /// <summary>
        /// 截取字符串中间
        /// </summary>
        /// <param name="value">数据源</param>
        /// <param name="startIndex">开始位置(从1开始)</param>
        /// <param name="length">截取长度</param>
        /// <returns></returns>
        public static string Middle(this string value, int startIndex, int length)
        {
            //返回非贪婪数据
            return Middle(value, startIndex, length, false);
        }

        /// <summary>
        /// 截取字符串右面
        /// </summary>
        /// <param name="value">数据源</param>
        /// <param name="startIndex">开始位置()</param>
        /// <param name="length">截取长度</param>
        /// <param name="greedy">是否贪婪(true:标识截取长度超出字符串则返回剩余长度。false:返回空)</param>
        /// <returns></returns>
        public static string Middle(this string value, int startIndex, int length, bool greedy)
        {
            string result = "";

            //如果不为空则
            if (!value.IsNullOrEmpty())
            {
                int len = value.Length;

                //如果开始位置不正确,返回""
                if (startIndex < 1 || startIndex > len)
                {
                    result = "";
                }
                else
                {
                    //如果大于截取长度则
                    if (len - startIndex > length)
                    {
                        result = value.Substring((startIndex - 1), length);
                    }
                    else
                    {
                        //greedy true:标识截取长度超出字符串则返回剩余长度。false:返回空
                        result = greedy ? value : "";
                    }
                }
            }
            else
            {
                result = "";
            }

            return result;
        }
        
        /// <summary>
        /// 取得骆驼命名方式
        /// </summary>
        public static string ToCamel(this string value)
        {
            string result = "";

            //如果不为空则
            if (!value.IsNullOrEmpty())
            {
                result = value[0].ToString().ToLower() + value.Substring(1);
            }
            else
            {
                result = "";
            }

            return result;
        }

        /// <summary>
        /// 取得帕斯卡
        /// </summary>
        public static string ToPascal(this string value)
        {
            string result = "";

            //如果不为空则
            if (!value.IsNullOrEmpty())
            {
                result = value[0].ToString().ToUpper() + value.Substring(1);
            }
            else
            {
                result = "";
            }

            return result;
        }

        /// <summary>
        /// Windows转Linux回车换行符
        /// </summary>
        public static string ToUnixLineEndings(this string value)
        {
            return value.Replace("\r\n", "\n").Replace("\r", "\n");
        }
        
        /// <summary>
        /// 添加后缀
        /// </summary>
        public static string Append(this string value, string content)
        {
            return new StringBuilder(value).Append(content).ToString();
        }

        /// <summary>
        /// 添加前缀
        /// </summary>
        public static string AppendPrefix(this string value, string content)
        {
            return new StringBuilder(content).Append(value).ToString();
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        public static string Format(this string value, params object[] args)
        {
            return string.Format(value, args);
        }

        /// <summary>
        /// 是否存在中文字符
        /// </summary>
        public static bool HasChinese(this string input)
        {
            return Regex.IsMatch(input, @"[\u4e00-\u9fa5]");
        }

        /// <summary>
        /// 是否存在空格
        /// </summary>
        public static bool HasSpace(this string input)
        {
            return input.Contains(" ");
        }

        /// <summary>
        /// 删除特定字符
        /// </summary>
        public static string RemoveString(this string str, params string[] targets)
        {
            return targets.Aggregate(str, (current, t) => current.Replace(t, string.Empty));
        }
    }
}