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

namespace IFramework.Core
{
    /// <summary>
    /// 属性单例类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class SingletonProperty<T> where T : class, ISingleton
    {
        // 静态实例
        private static volatile T instance;

        // 对象锁
        // ReSharper disable once StaticMemberInGenericType
        private static readonly object locker = new object();

        /// <summary>
        /// 双重锁，线程安全
        /// </summary>
        public static T Instance {
            get {
                if (instance == null) {
                    lock (locker) {
                        if (instance == null) {
                            instance = SingletonCreator.CreateSingleton<T>();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 资源释放
        /// </summary>
        public static void Dispose()
        {
            instance = null;
        }
    }
}
