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

using UnityEngine;

namespace IFramework.Engine
{
    public abstract class MonoSingleton<T> : MonoBehaviour, ISingleton where T : MonoSingleton<T>
    {
        // 静态实例
        protected static T instance;
        
        // 对象锁
        static object locker = new object();

        protected static bool isApplicationQuit = false;

        /// <summary>
        /// 双重锁，线程安全
        /// </summary>
        public static T Instance
        {
            get
            {
                if (instance == null && !isApplicationQuit)
                {
                    lock (locker)
                    {
                        if (instance == null && !isApplicationQuit)
                        {
                            instance = SingletonCreator.CreateMonoSingleton<T>();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// 单例初始化
        /// </summary>
        public virtual void OnInit() { }

        /// <summary>
        /// 资源释放
        /// </summary>
        public virtual void Dispose()
        {
            instance = null;
            Destroy(gameObject);
        }

        /// <summary>
        /// 应用程序退出：释放当前对象并销毁相关GameObject
        /// </summary>
        protected virtual void OnApplicationQuit()
        {
            isApplicationQuit = true;
            
            if(instance == null) return;
            
            Destroy(instance.gameObject);
            
            instance = null;
        }
        
        /// <summary>
        /// 释放当前对象
        /// </summary>
        protected virtual void OnDestroy()
        {
            instance = null;
        }

        /// <summary>
        /// 判断当前应用程序是否退出
        /// </summary>
        public static bool IsApplicationQuit
        {
            get => isApplicationQuit;
        }
    }
}