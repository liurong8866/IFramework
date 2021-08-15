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
        protected string assetBundleName;
        protected string[] assetBundleNames;
        protected AssetBundleRequest assetBundleRequest;

        public override string AssetBundleName
        {
            get => assetBundleName;
            set => assetBundleName = value;
        }
        
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
        
        // protected string AssetBundleName
        
        /// <summary>
        /// 同步加载资源
        /// </summary>
        public override bool LoadSync()
        {
            if (!IsLoadable) return false;
            
            if()
        }

        public override void LoadASync()
        {
            throw new NotImplementedException();
        }

        public override void Recycle()
        {
            throw new NotImplementedException();
        }

        public override IEnumerator LoadAsync(Action callback)
        {
            throw new NotImplementedException();
        }
    }
}