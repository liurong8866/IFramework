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
using System.Collections.Generic;

namespace IFramework.Core
{
    /// <summary>
    /// 安全的计数器
    /// </summary>
    public class SafeCounter : ICounter, IDisposable
    {
        private readonly HashSet<object> owners = new HashSet<object>();

        public SafeCounter() { }
        
        public SafeCounter(Action onZero)
        {
            OnZero = onZero;
        }
        
        public int Count
        {
            get { return owners.Count; }
        }

        public Action OnZero { get; set; }

        public HashSet<object> Owners
        {
            get { return owners; }
        }

        public void Retain(object owner)
        {
            if (!owners.Add(owner))
            {
                "对象已经被记录".LogWarning();
            }
        }

        public void Release(object owner)
        {
            if (!owners.Remove(owner))
            {
                "没有找到要释放的对象".LogWarning();
            }
            else
            {
                if (Count == 0)
                {
                    OnZero.InvokeSafe();
                }
            }
        }
        
        public void Reset()
        {
            owners.Clear();
        }

        public void Dispose()
        {
            OnZero = null;
        }
    }
}