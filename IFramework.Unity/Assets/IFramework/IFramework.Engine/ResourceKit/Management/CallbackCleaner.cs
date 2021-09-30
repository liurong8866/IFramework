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

namespace IFramework.Engine {
    /// <summary>
    /// 用于清理回调事件到载体
    /// </summary>
    public class CallbackCleaner {

        private readonly Action<bool, IResource> callbacks;

        private readonly IResource resource;

        public CallbackCleaner(IResource resource, Action<bool, IResource> callback) {
            this.resource = resource;
            this.callbacks = callback;
        }

        /// <summary>
        /// 释放监听的事件
        /// </summary>
        public void Release() {
            resource.UnRegisterOnLoadedEvent(callbacks);
        }

        /// <summary>
        /// 判断是否是当前资源
        /// </summary>
        public bool Is(IResource resource) {
            return this.resource.AssetName == resource.AssetName;
        }

    }
}
