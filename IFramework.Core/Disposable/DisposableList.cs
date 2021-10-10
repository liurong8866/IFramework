using System;
using System.Collections.Generic;

namespace IFramework.Core
{
    /// <summary>
    /// 可回收List
    /// </summary>
    public class DisposableList : IDisposableList
    {
        /// <summary>
        /// 需要回收的List 对象
        /// </summary>
        private List<IDisposable> disposableList;

        public DisposableList()
        {
            disposableList = ListPool<IDisposable>.Allocate();
        }

        /// <summary>
        /// 给List添加相关对象
        /// </summary>
        /// <param name="disposable"></param>
        public void Add(IDisposable disposable)
        {
            disposableList.Add(disposable);
        }

        /// <summary>
        /// 释放相关对象
        /// </summary>
        public void Dispose()
        {
            foreach (IDisposable disposable in disposableList) {
                disposable.Dispose();
            }
            disposableList.Recycle();
            disposableList = null;
        }
    }
}
