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

namespace IFramework.Core
{
    /// <summary>
    /// MonoBehaviour单例特性，标记在需要单例的MonoBehaviour的类上
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class MonoSingletonAttribute : Attribute
    {
        // 在Hierarchy中的全路径
        private string pathInHierarchy;

        // 标记不销毁
        private bool dontDestroy = true;

        /// <summary>
        /// 单例特性构造函数
        /// </summary>
        /// <param name="pathInHierarchy">需要附加的GameObject在Hierarchy中的全路径</param>
        public MonoSingletonAttribute(string pathInHierarchy)
        {
            this.pathInHierarchy = pathInHierarchy;
        }

        // 获取Hierarchy中的全路径
        public string PathInHierarchy => pathInHierarchy;

        // 标记不销毁
        public bool DontDestroy {
            get => dontDestroy;
            set => dontDestroy = value;
        }
    }
}
