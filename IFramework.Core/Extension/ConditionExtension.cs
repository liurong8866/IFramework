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

namespace IFramework.Core {
    public static class ConditionExtension {

        /// <summary>
        /// 如果为真，执行，并返回真
        /// </summary>
        public static bool IfTrue(this bool boolean, Action action) {
            if (boolean) {
                action.Invoke();
            }
            return boolean;
        }

        /// <summary>
        /// 如果为假，执行，并返回假
        /// </summary>
        public static bool IfFalse(this bool boolean, Action action) {
            if (!boolean) {
                action.Invoke();
            }
            return boolean;
        }

        /// <summary>
        /// 真假处理
        /// </summary>
        /// <param name="boolean">判断条件</param>
        /// <param name="trueAction">真处理</param>
        /// <param name="falseAction">假处理</param>
        // ReSharper disable once InconsistentNaming
        public static void iif(this bool boolean, Action trueAction, Action falseAction = null) {
            if (boolean) {
                trueAction.InvokeSafe();
            }
            else {
                falseAction.InvokeSafe();
            }
        }

        /// <summary>
        /// 真假处理
        /// </summary>
        /// <param name="boolean">判断条件</param>
        /// <param name="trueFunc">真处理</param>
        /// <param name="falseFunc">假处理</param>
        // ReSharper disable once InconsistentNaming
        public static TResult iif<TResult>(this bool boolean, Func<TResult> trueFunc, Func<TResult> falseFunc = null) {
            TResult result;
            if (boolean) {
                result = trueFunc.InvokeSafe();
            }
            else {
                result = falseFunc.InvokeSafe();
            }
            return result;
        }

    }
}
