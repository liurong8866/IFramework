using System;
using System.Collections;
using System.IO;
using IFramework.Core;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace IFramework.Engine
{
    public abstract class AbstractNetResource : AbstractResource
    {
        private bool disposed;
        protected UnityWebRequest request;

        /// <summary>
        /// 同步加载资源
        /// </summary>
        public override bool Load()
        {
            throw new NotImplementedException("请使用LoadASync方法加载网络资源！");
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public override void LoadASync()
        {
            if (!IsLoadable || AssetName.Nothing() || Counter <= 0) return;
            State = ResourceState.Loading;
            ResourceManager.Instance.AddResourceLoadTask(this);
        }

        /// <summary>
        /// 重写异步加载方法
        /// </summary>
        public override IEnumerator LoadAsync(Action callback)
        {
            if (Counter <= 0) {
                OnResourceLoadFailed();
                callback();
                yield break;
            }
            // request在子类中定义
            yield return request.SendWebRequest();
            if (!request.isDone) {
                Log.Error("资源加载失败：" + assetName);
                OnResourceLoadFailed();
                callback();
                yield break;
            }
            // 保存数据
            SaveData();
            // 处理数据
            asset = ResolveResult();
            // 销毁连接
            request.Dispose();
            request = null;
            State = ResourceState.Ready;
            callback();
        }

        /// <summary>
        /// 缓存数据
        /// </summary>
        private void SaveData()
        {
            // 如果文件不存在，则保存
            if (!FileUtils.Exists(FullName)) {
                try {
                    DirectoryUtils.Create(FilePath);
                    FileUtils.Write(FullName, request.downloadHandler.data);
                }
                catch (Exception e) {
                    Log.Error(e);
                }
            }
        }

        /// <summary>
        /// 处理对象
        /// </summary>
        protected abstract Object ResolveResult();

        /// <summary>
        /// 保存路径
        /// </summary>
        protected abstract string FilePath { get; }

        /// <summary>
        /// 保存文件名，带扩展名
        /// </summary>
        protected abstract string FileName { get; }

        /// <summary>
        /// 完整路径名
        /// </summary>
        protected string FullName => DirectoryUtils.CombinePath(FilePath, FileName);

        /// <summary>
        /// 析构函数，以备程序员忘记了显式调用Dispose方法
        /// </summary>
        ~AbstractNetResource()
        {
            //必须为false
            Dispose(false);
        }

        /// <summary>
        /// 实现IDisposable中的Dispose方法
        /// </summary>
        public override void Dispose()
        {
            base.Dispose();
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
            if (!disposed) {
                if (disposing) {
                    // 清理托管资源
                    Release();
                }
                // 清理非托管资源
                request.Dispose();
                request = null;
                disposed = true;
            }
        }
    }
}
