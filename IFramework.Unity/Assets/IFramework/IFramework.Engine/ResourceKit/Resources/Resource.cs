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
using System.Collections;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// Resources文件夹下的资源管理类
    /// </summary>
    public sealed class Resource : AbstractResource
    {
        private string path;
        
        /// <summary>
        /// 从缓冲池获取对象
        /// </summary>
        public static Resource Allocate(string name, ResourcesUrlType urlType)
        {
            Resource resource = ObjectPool<Resource>.Instance.Allocate();

            if (resource != null)
            {
                resource.AssetName = name;

                resource.path = name.Substring(urlType == ResourcesUrlType.Url ? "resources://".Length : "Resources/".Length);
            }
            
            return resource;
        }
        
        /// <summary>
        /// 同步加载资源
        /// </summary>
        public override bool Load()
        {
            if (!IsLoadable) return false;

            if (AssetName.IsNullOrEmpty()) return false;

            State = ResourceState.Loading;

            asset = AssetType != null ? Resources.Load(path, AssetType) : Resources.Load(path);

            if (asset == null)
            {
                Log.Error("资源加载失败：" + path);
                OnResourceLoadFailed();
                return false;
            }

            State = ResourceState.Ready;
            
            return true;
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public override void LoadASync()
        {
            if (!IsLoadable) return;

            if (AssetName.IsNullOrEmpty()) return;

            State = ResourceState.Loading;

            ResourceManager.Instance.AddResourceLoadTask(this);
        }
        
        /// <summary>
        /// 重写异步加载方法
        /// </summary>
        public override IEnumerator LoadAsync(Action callback)
        {
            ResourceRequest request;

            request = AssetType != null ? Resources.LoadAsync(path, AssetType) : Resources.LoadAsync(path);

            yield return request;

            if (!request.isDone)
            {
                Log.Error("资源加载失败：" + assetName);
                OnResourceLoadFailed();
                callback();
                yield break;
            }

            asset = request.asset;

            State = ResourceState.Ready;
            
            callback();
        }

        /// <summary>
        /// 回收资源到缓冲池
        /// </summary>
        public override void Recycle()
        {
            ObjectPool<Resource>.Instance.Recycle(this);
        }
    }
}