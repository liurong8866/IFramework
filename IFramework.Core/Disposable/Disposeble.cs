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

namespace IFramework
{
    /// <summary>
    /// Dispose模式
    /// </summary>
    public abstract class Disposeble : IDisposable
    {
        private bool disposed = false;
        
        /// <summary>
        /// 析构函数，以备程序员忘记了显式调用Dispose方法
        /// </summary>
        ~Disposeble()
        {
            //必须为false
            Dispose(false);
        }
        
        /// <summary>
        /// 实现IDisposable中的Dispose方法
        /// </summary>
        public void Dispose()
        {
            //必须为true
            Dispose(true);
            //通知垃圾回收机制不再调用终结器（析构器） 调用虚拟的Dispose方法。禁止Finalization（终结操作） 
            GC.SuppressFinalize(this);
        }
        
        /// <summary>
        /// 非密封类修饰用
        /// </summary>
        /// <param name="disposing">是否注销托管资源</param>
        protected virtual void Dispose(bool disposing)
        {
            // 不要多次处理 
            if (!disposed)
            {
                if (disposing)
                {
                    // 清理托管资源
                    DisposeManaged();
                }
                // 清理非托管资源
                UnDisposeManaged();
                
                disposed = true;
            }
        }

        /// <summary>
        /// 托管资源：由CLR管理分配和释放的资源，即由CLR里new出来的对象；
        /// </summary>
        protected abstract void DisposeManaged();
        
        /// <summary>
        /// 非托管资源：不受CLR管理的对象，windows内核对象，如文件、数据库连接、套接字、COM对象等；
        /// </summary>
        protected virtual void UnDisposeManaged(){}
        
    }
}