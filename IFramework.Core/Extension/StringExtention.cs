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
        /// 截取字符串左面
        /// </summary>
        /// <param name="value">数据源</param>
        /// <param name="length">截取长度</param>
        /// <param name="exacting">严格截取：截取长度超出字符串长度时，true: 返回空。false:返回实际截取长度</param>
        public static string Left(this string value, int length, bool exacting = false)
        {
            // 如果数据源是空，或者截取长度<1，返回空
            if (value.Nothing() || length < 1) return "";
            string result = exacting ? "" : value;

            //如果大于截取长度则
            if (value.Length > length) {
                result = value.Substring(0, length);
            }
            return result;
        }

        /// <summary>
        /// 截取第一次出现的字符串左边的字符串
        /// </summary>
        /// <param name="value">源字符串</param>
        /// <param name="before">要比对的字符串</param>
        /// <param name="include">是否包含before字符串，true：包含， false(默认)：不包含</param>
        /// <param name="exacting">严格截取：如果未找到匹配项，是否返回源字符串 true(默认)：返回， false：不返回</param>
        public static string Left(this string value, string before, bool include = false, bool exacting = false)
        {
            // 如果数据源是空，返回 ""
            if (value.Nothing()) return "";

            // 如果查询条件为空，则
            if (before.Nothing()) return exacting ? "" : value;
            string result = exacting ? "" : value;

            // 找到索引位置
            int index = value.IndexOf(before, StringComparison.Ordinal);

            // 如果找到字符串
            if (index >= 0) {
                // 如果包含字符串，就要加上before本身的长度偏移量
                if (include) index += before.Length;
                result = value.Substring(0, index);
            }
            return result;
        }

        /// <summary>
        /// 截取字符串右面
        /// </summary>
        /// <param name="value">数据源</param>
        /// <param name="length">截取长度</param>
        /// <param name="exacting">严格截取：截取长度超出字符串长度时，true: 返回空。false:返回实际截取长度</param>
        public static string Right(this string value, int length, bool exacting = false)
        {
            // 如果数据源是空，或者截取长度<1，返回空
            if (value.Nothing() || length < 1) return "";
            string result = exacting ? "" : value;

            //如果大于截取长度则
            if (value.Length > length) {
                result = value.Substring(value.Length - length);
            }
            return result;
        }

        /// <summary>
        /// 截取最后一次出现的字符串右边的字符串
        /// </summary>
        /// <param name="value">源字符串</param>
        /// <param name="after">要比对的字符串</param>
        /// <param name="include">是否包含after字符串，true：包含， false(默认)：不包含</param>
        /// <param name="exacting">严格截取：如果未找到匹配项，是否返回源字符串 true：返回， false(默认)：不返回</param>
        public static string Right(this string value, string after, bool include = false, bool exacting = false)
        {
            // 如果数据源是空，返回 ""
            if (value.Nothing()) return "";

            // 如果查询条件为空，则
            if (after.Nothing()) return exacting ? "" : value;
            string result = exacting ? "" : value;

            // 找到索引位置
            int index = value.LastIndexOf(after, StringComparison.Ordinal);

            // 如果找到字符串
            if (index >= 0) {
                // 如果不包含字符串，就要加上after本身的长度
                if (!include) index += after.Length;
                // 截取字符串
                result = value.Substring(index);
            }
            return result;
        }

        /// <summary>
        /// 截取字符串右面
        /// </summary>
        /// <param name="value">数据源</param>
        /// <param name="startIndex">开始位置()</param>
        /// <param name="length">截取长度</param>
        /// <param name="exacting">严格截取：截取长度超出字符串长度时，true: 返回空。false:返回实际截取长度</param>
        public static string Middle(this string value, int startIndex, int length, bool exacting = false)
        {
            // 如果数据源是空，或者截取长度<1，返回 ""
            if (value.Nothing() || length < 1) return "";

            //如果开始位置不正确,返回 ""
            if (startIndex < 1 || startIndex > value.Length) return "";
            string result = exacting ? "" : value;

            //如果大于截取长度则
            if (value.Length - startIndex > length) {
                result = value.Substring(startIndex - 1, length);
            }
            return result;
        }

        /// <summary>
        /// 取得骆驼命名方式
        /// </summary>
        public static string ToCamel(this string value, params char[] separator)
        {
            value = value.TrimStart(separator).TrimEnd(separator).Replace(" ", "").Replace("　", "");
            if (value.Nothing()) return "";
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
            value = value.TrimStart(separator).TrimEnd(separator).Replace(" ", "").Replace("　", "");
            if (value.Nothing()) return result;
            string[] array = value.Split(separator);

            // 全部字母大写
            foreach (string word in array) {
                if (word.Nothing()) continue;
                result += word[0].ToString().ToUpperInvariant();
                result += word.Length > 1 ? word.Substring(1) : "";
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
        public static StringBuilder AppendLine(this StringBuilder value, string content)
        {
            return value.Append("/r" + content);
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
