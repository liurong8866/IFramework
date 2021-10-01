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

using System.IO;
using IFramework.Core;
using UnityEngine;
using UnityEngine.Networking;

namespace IFramework.Engine
{
    public class NetVideoResource : AbstractNetResource
    {
        private string filePath;
        private string fileName;

        /// <summary>
        /// 从缓冲池获取对象
        /// </summary>
        public static NetVideoResource Allocate(string path) {
            NetVideoResource resource = ObjectPool<NetVideoResource>.Instance.Allocate();

            if (resource != null) {
                resource.AssetName = path;
                resource.filePath = Path.Combine(Platform.PersistentData.VideoPath, Mathf.Abs(Platform.GetFilePathByPath(path).GetHashCode()) + "");
                resource.fileName = Platform.GetFileNameByPath(path);

                // 如果缓存已下载，则直接从缓存获取
                string requestUrl = File.Exists(resource.FullName)
                        ? Platform.FilePathPrefix + resource.FullName
                        : path.Substring(ResourcesUrlType.VIDEO.Length);
                resource.request = UnityWebRequest.Get(requestUrl);
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
        protected override Object ResolveResult() {
            // VideoClip videoClip = request.downloadHandler;
            // VideoClip videoClip = Resources.Load(Platform.FilePathPrefix + FullName, typeof(VideoClip)) as VideoClip;
            // return videoClip;
            //TODO
            return null;
        }

        /// <summary>
        /// 回收资源到缓冲池
        /// </summary>
        public override void Recycle() {
            ObjectPool<NetVideoResource>.Instance.Recycle(this);
        }
    }
}
