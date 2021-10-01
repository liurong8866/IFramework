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

using IFramework.Core;
using UnityEngine;
using UnityEngine.Networking;

namespace IFramework.Engine {
    public class NetImageResource : AbstractNetResource {

        private string savePath;
        
        /// <summary>
        /// 从缓冲池获取对象
        /// </summary>
        public static NetImageResource Allocate(string url) {
            NetImageResource resource = ObjectPool<NetImageResource>.Instance.Allocate();
            if (resource != null) {
                resource.AssetName = url;
                resource.request = UnityWebRequestTexture.GetTexture(url.Substring(ResourcesUrlType.NET_IMAGE.Length));
            }
            return resource;
        }

        /// <summary>
        /// 保存路径
        /// </summary>
        protected override string SavePath => savePath;
        
        /// <summary>
        /// 获取对象
        /// </summary>
        protected override Object ResolveResult() {
            return ((DownloadHandlerTexture) request.downloadHandler).texture;
        }
        
        /// <summary>
        /// 回收资源到缓冲池
        /// </summary>
        public override void Recycle() {
            ObjectPool<NetImageResource>.Instance.Recycle(this);
        }
    }
}
