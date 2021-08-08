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
using UnityEngine;
using Object = UnityEngine.Object;

namespace IFramework.Engine
{
    public static class ObjectExtention
    {
        public static T As<T>(this object self) where T : class
        {
            return self as T;
        }
        
        public static T Instantiate<T>(this T selfObj) where T : Object
        {
            return Object.Instantiate(selfObj);
        }
        
        public static T Name<T>(this T selfObj, string name) where T : Object
        {
            selfObj.name = name;
            return selfObj;
        }
        

        #region Destroy/DontDestroy GameObject

        public static void DestroySelf<T>(this T selfObj) where T : Object
        {
            Object.Destroy(selfObj);
        }

        public static T DestroySelfGracefully<T>(this T selfObj) where T : Object
        {
            if (selfObj)
            {
                Object.Destroy(selfObj);
            }

            return selfObj;
        }
        
        public static T DestroySelfAfterDelay<T>(this T selfObj, float afterDelay) where T : Object
        {
            Object.Destroy(selfObj, afterDelay);
            return selfObj;
        }

        public static T DestroySelfAfterDelayGracefully<T>(this T selfObj, float delay) where T : Object
        {
            if (selfObj)
            {
                Object.Destroy(selfObj, delay);
            }

            return selfObj;
        }

        public static T DontDestroyOnLoad<T>(this T selfObj) where T : Object
        {
            Object.DontDestroyOnLoad(selfObj);
            return selfObj;
        }

        #endregion
        
        #region Invoke Action Delegate

        /// <summary>
        /// 功能：不为空则调用 Func
        /// 
        /// 示例:
        /// <code>
        /// Func<int> func = ()=> 1;
        /// var number = func.InvokeGracefully(); // 等价于 if (func != null) number = func();
        /// </code>
        /// </summary>
        /// <param name="selfFunc"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T InvokeGracefully<T>(this Func<T> selfFunc)
        {
            return null != selfFunc ? selfFunc() : default(T);
        }
        
        /// <summary>
        /// 功能：不为空则调用 Action
        /// 
        /// 示例:
        /// <code>
        /// System.Action action = () => Log.I("action called");
        /// action.InvokeGracefully(); // if (action != null) action();
        /// </code>
        /// </summary>
        /// <param name="selfAction"> action 对象 </param>
        /// <returns> 是否调用成功 </returns>
        public static bool InvokeGracefully(this Action selfAction)
        {
            if (null != selfAction)
            {
                selfAction();
                return true;
            }

            return false;
        }

        /// <summary>
        /// 不为空则调用 Action<T>
        /// 
        /// 示例:
        /// <code>
        /// System.Action<int> action = (number) => Log.I("action called" + number);
        /// action.InvokeGracefully(10); // if (action != null) action(10);
        /// </code>
        /// </summary>
        /// <param name="selfAction"> action 对象</param>
        /// <typeparam name="T">参数</typeparam>
        /// <returns> 是否调用成功</returns>
        public static bool InvokeGracefully<T>(this Action<T> selfAction, T t)
        {
            if (null != selfAction)
            {
                selfAction(t);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 不为空则调用 Action<T,K>
        ///
        /// 示例
        /// <code>
        /// System.Action<int,string> action = (number,name) => Log.I("action called" + number + name);
        /// action.InvokeGracefully(10,"qframework"); // if (action != null) action(10,"qframework");
        /// </code>
        /// </summary>
        /// <param name="selfAction"></param>
        /// <returns> call succeed</returns>
        public static bool InvokeGracefully<T, K>(this Action<T, K> selfAction, T t, K k)
        {
            if (null != selfAction)
            {
                selfAction(t, k);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 不为空则调用委托
        ///
        /// 示例：
        /// <code>
        /// // delegate
        /// TestDelegate testDelegate = () => { };
        /// testDelegate.InvokeGracefully();
        /// </code>
        /// </summary>
        /// <param name="selfAction"></param>
        /// <returns> call suceed </returns>
        public static bool InvokeGracefully(this Delegate selfAction, params object[] args)
        {
            if (null != selfAction)
            {
                selfAction.DynamicInvoke(args);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 调用自己
        /// </summary>
        public static T InvokeSelf<T>(this T selfObj, System.Action<T> toFunction) where T : Object
        {
            toFunction.InvokeGracefully(selfObj);
            return selfObj;
        }

        #endregion
    }
}