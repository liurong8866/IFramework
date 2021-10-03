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
        public static bool IsNullOrEmpty(this string value) { return string.IsNullOrEmpty(value); }

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
            if (!value.IsNullOrEmpty()) {
                int len = value.Length;

                //如果大于截取长度则
                if (len > length) {
                    result = value.Substring(0, length);
                }
                else {
                    //greedy true:标识截取长度超出字符串则返回剩余长度。false:返回空
                    result = greedy ? value : "";
                }
            }
            else {
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
            if (!value.IsNullOrEmpty()) {
                int len = value.Length;

                //如果大于截取长度则
                if (len > length) {
                    result = value.Substring(len - length);
                }
                else {
                    //greedy true:标识截取长度超出字符串则返回剩余长度。false:返回空
                    result = greedy ? value : "";
                }
            }
            else {
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
            if (!value.IsNullOrEmpty()) {
                int len = value.Length;

                //如果开始位置不正确,返回""
                if (startIndex < 1 || startIndex > len) {
                    result = "";
                }
                else {
                    //如果大于截取长度则
                    if (len - startIndex > length) {
                        result = value.Substring(startIndex - 1, length);
                    }
                    else {
                        //greedy true:标识截取长度超出字符串则返回剩余长度。false:返回空
                        result = greedy ? value : "";
                    }
                }
            }
            else {
                result = "";
            }
            return result;
        }

        /// <summary>
        /// 取得骆驼命名方式
        /// </summary>
        public static string ToCamel(this string value, params char[] separator)
        {
            value = value.TrimStart(separator).TrimEnd(separator);
            if (value.IsNullOrEmpty()) return "";

            string[] array = value.Split(separator);

            // 首字母小写
            string result = array[0][0].ToString().ToLowerInvariant();
            result += array[0].Length > 1 ? array[0].Substring(1) : "";

            // 其余字母大写
            for (int i = 1; i < array.Length; i++) {
                result += array[i][0].ToString().ToUpperInvariant();
                result += array[i].Length > 1 ? array[i].Substring(1) : "";
            }
            return result;
        }

        /// <summary>
        /// 取得帕斯卡
        /// </summary>
        public static string ToPascal(this string value, params char[] separator)
        {
            string result = "";
            value = value.TrimStart(separator).TrimEnd(separator);
            if (value.IsNullOrEmpty()) return result;

            string[] array = value.Split(separator);

            // 全部字母大写
            foreach (string word in array) {
                if (word.IsNullOrEmpty()) continue;

                result += word[0].ToString().ToUpperInvariant();
                result += word.Length > 1 ? word.Substring(1) : "";
            }
            return result;
        }

        /// <summary>
        /// Windows转Linux回车换行符
        /// </summary>
        public static string ToUnixLineEndings(this string value) { return value.Replace("\r\n", "\n").Replace("\r", "\n"); }

        /// <summary>
        /// 添加后缀
        /// </summary>
        public static string Append(this string value, string content) { return new StringBuilder(value).Append(content).ToString(); }

        /// <summary>
        /// 添加前缀
        /// </summary>
        public static string AppendPrefix(this string value, string content) { return new StringBuilder(content).Append(value).ToString(); }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        public static string Format(this string value, params object[] args) { return string.Format(value, args); }

        /// <summary>
        /// 是否存在中文字符
        /// </summary>
        public static bool HasChinese(this string input) { return Regex.IsMatch(input, @"[\u4e00-\u9fa5]"); }

        /// <summary>
        /// 是否存在空格
        /// </summary>
        public static bool HasSpace(this string input) { return input.Contains(" "); }

        /// <summary>
        /// 删除特定字符
        /// </summary>
        public static string RemoveString(this string str, params string[] targets) { return targets.Aggregate(str, (current, t) => current.Replace(t, string.Empty)); }
    }
}
