using System.IO;
using IFramework.Core;
using UnityEngine;
using UnityEngine.Networking;

namespace IFramework.Engine
{
    public class NetImageResource : AbstractNetResource
    {
        private string filePath;
        private string fileName;

        /// <summary>
        /// 从缓冲池获取对象
        /// </summary>
        public static NetImageResource Allocate(string path)
        {
            NetImageResource resource = ObjectPool<NetImageResource>.Instance.Allocate();
            if (resource != null) {
                resource.AssetName = path;
                resource.filePath = DirectoryUtils.CombinePath(Platform.PersistentData.ImagePath, Mathf.Abs(DirectoryUtils.GetPathByFullName(path).GetHashCode()) + "");
                resource.fileName = FileUtils.GetFileNameByPath(path);

                // 如果缓存已下载，则直接从缓存获取
                string requestUrl = FileUtils.Exists(resource.FullName) ? Platform.FilePathPrefix + resource.FullName : path.Substring(ResourcesUrlType.IMAGE.Length);
                resource.request = UnityWebRequestTexture.GetTexture(requestUrl);
            }
            return resource;
        }

        /// <summary>
        /// 保存路径
        /// </summary>
        protected override string FilePath => filePath;

        /// <summary>
        /// 保存文件名
        /// </summary>
        protected override string FileName => fileName;

        /// <summary>
        /// 获取对象
        /// </summary>
        protected override Object ResolveResult()
        {
            return ((DownloadHandlerTexture)request.downloadHandler).texture;
        }

        /// <summary>
        /// 回收资源到缓冲池
        /// </summary>
        public override void Recycle()
        {
            ObjectPool<NetImageResource>.Instance.Recycle(this);
        }
    }
}
