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
    public class AssetResource : AbstractResource
    {
        protected AssetBundleRequest assetBundleRequest;

        /// <summary>
        /// 所属AssetBundle包名称
        /// </summary>
        public override string AssetBundleName { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AssetResource() : base() {}
        public AssetResource(string assetName) : base(assetName) {}
        
        /// <summary>
        /// 分配实例
        /// </summary>
        public static AssetResource Allocate(string assetName, string assetBundleName, Type assetType)
        {
            AssetResource resource = ObjectPool<AssetResource>.Instance.Allocate();
            if (resource != null)
            {
                resource.AssetName = assetName;
                resource.AssetBundleName = assetBundleName;
                resource.AssetType = assetType;
            }
            return resource;
        }
        
        /// <summary>
        /// 初始化
        /// </summary>
        protected void InitAssetBundleName()
        {
            // AssetBundleName = null;
            //
            // var resSearchKeys = ResSearchKeys.Allocate(mAssetName,mOwnerBundleName,AssetType);
            //
            // var config =  AssetBundleSettings.AssetBundleConfigFile.GetAssetData(resSearchKeys);
            //
            // resSearchKeys.Recycle2Cache();
            //
            // if (config == null)
            // {
            //     Log.E("Not Find AssetData For Asset:" + mAssetName);
            //     return;
            // }
            //
            // var assetBundleName = config.OwnerBundleName;
            //
            // if (string.IsNullOrEmpty(assetBundleName))
            // {
            //     Log.E("Not Find AssetBundle In Config:" + config.AssetBundleIndex + mOwnerBundleName);
            //     return;
            // }
            //
            // mAssetBundleArray = new string[1];
            // mAssetBundleArray[0] = assetBundleName;
        }
        
        /// <summary>
        /// 同步加载资源
        /// </summary>
        public override bool LoadSync()
        {
            if (!IsLoadable) return false;

            return false;
        }

        public override void LoadASync()
        {
            throw new NotImplementedException();
        }

        public override void Recycle()
        {
            ObjectPool<AssetResource>.Instance.Recycle(this);
        }

        public override IEnumerator LoadAsync(Action callback)
        {
            throw new NotImplementedException();
        }
        
        
        public override string ToString()
        {
            return $"Type:Asset\t {base.ToString()}\t FromAssetBundle:";
        }
        
        
        
    }
}