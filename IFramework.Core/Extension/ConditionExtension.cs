using System;
using System.Collections;

namespace IFramework.Core
{
    public static class ConditionExtension
    {
        /// <summary>
        /// 真假处理
        /// </summary>
        /// <param name="boolean">判断条件</param>
        /// <param name="trueAction">真处理</param>
        /// <param name="falseAction">假处理</param>
        // ReSharper disable once InconsistentNaming
        public static bool iif(this bool boolean, Action trueAction, Action falseAction = null)
        {
            if (boolean) { trueAction.InvokeSafe(); } else { falseAction.InvokeSafe(); }
            return boolean;
        }

        /// <summary>
        /// 真假处理
        /// </summary>
        /// <param name="boolean">判断条件</param>
        /// <param name="trueFunc">真处理</param>
        /// <param name="falseFunc">假处理</param>
        // ReSharper disable once InconsistentNaming
        public static T iif<T>(this bool boolean, Func<T> trueFunc, Func<T> falseFunc = null)
        {
            return boolean ? trueFunc.InvokeSafe() : falseFunc.InvokeSafe();
        }

        /// <summary>
        /// 如果对象为Null或""，执行TrueAction，否则执行FalseAction
        /// </summary>
        /// <param name="value">判断对象</param>
        /// <param name="trueAction">真处理</param>
        /// <param name="falseAction">假处理</param>
        public static bool IfNullOrEmpty(this object value, Action trueAction, Action falseAction = null)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString())) {
                trueAction.InvokeSafe();
                return true;
            }
            falseAction.InvokeSafe();
            return false;
        }

        /// <summary>
        /// 如果对象为Null或""，执行TrueAction，否则执行FalseAction
        /// </summary>
        /// <param name="value">判断对象</param>
        /// <param name="trueFunc">真处理</param>
        /// <param name="falseFunc">假处理</param>
        public static T IfNullOrEmpty<T>(this object value, Func<T> trueFunc, Func<T> falseFunc = null)
        {
            return value == null || string.IsNullOrEmpty(value.ToString()) ? trueFunc.InvokeSafe() : falseFunc.InvokeSafe();
        }

        /// <summary>
        /// 如果集合为Null或空，执行TrueAction，否则执行FalseAction
        /// </summary>
        /// <param name="value">判断对象</param>
        /// <param name="trueAction">真处理</param>
        /// <param name="falseAction">假处理</param>
        public static bool IfNullOrEmpty(this ICollection value, Action trueAction, Action falseAction = null)
        {
            if (value == null || value.Count == 0) {
                trueAction.InvokeSafe();
                return true;
            }
            falseAction.InvokeSafe();
            return false;
        }

        /// <summary>
        /// 如果集合为Null或空，执行TrueAction，否则执行FalseAction
        /// </summary>
        /// <param name="value">判断对象</param>
        /// <param name="trueFunc">真处理</param>
        /// <param name="falseFunc">假处理</param>
        public static T IfNullOrEmpty<T>(this ICollection value, Func<T> trueFunc, Func<T> falseFunc = null)
        {
            return value == null || value.Count == 0 ? trueFunc.InvokeSafe() : falseFunc.InvokeSafe();
        }
    }
}
