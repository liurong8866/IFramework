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
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;

namespace IFramework.Engine {
    public class NetVideoResource : AbstractNetResource {
        
        /// <summary>
        /// 从缓冲池获取对象
        /// </summary>
        public static NetVideoResource Allocate(string name) {
            NetVideoResource resource = ObjectPool<NetVideoResource>.Instance.Allocate();
            if (resource != null) {
                resource.AssetName = name;
                resource.request = UnityWebRequest.Get(name.Substring(ResourcesUrlType.NET_VIDEO.Length));
            }
            return resource;
        }

        /// <summary>
        /// 回收资源到缓冲池
        /// </summary>
        public override void Recycle() {
            ObjectPool<NetVideoResource>.Instance.Recycle(this);
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        protected override Object ResolveResult() {
            // byte[] data = ((DownloadHandlerBuffer) request.downloadHandler).data;
            // string path = Platform.PersistentDataPath + "/Resources/";
            // DirectoryUtils.Create(path);
            // FileUtils.Write(path + "myvideo.mp4", data);
            // VideoClip videoClip = Resources.L("file://"+path + "myvideo") as VideoClip;
            return null;
        }

        protected override string SavePath { get; }

    }
}
