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
using System.Collections.Generic;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    public class AssetResource : AbstractResource
    {
        protected string assetBundleNameConfig;
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
                resource.InitAssetBundleName();
            }
            return resource;
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        public override bool LoadSync()
        {
            if (!IsLoadable) return false;

            // 如果配置文件没有对应的Asset，则退出
            if (assetBundleNameConfig.IsNullOrEmpty()) return false;

            // 如果是模拟模式，并且不是包信息资源
            if (Configure.IsSimulation.Value && !assetName.Equals("assetbundlemanifest"))
            {
                using ResourceSearcher searcher = ResourceSearcher.Allocate(assetBundleNameConfig, null, typeof(AssetBundle));

                ResourceManager.Instance.GetResource<AssetBundleResource>(searcher);
                

            }
            else
            {
                
            }
            return false;
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public override void LoadASync()
        {
            if (!IsLoadable) return;
            
            if(AssetBundleName.IsNullOrEmpty()) return;

            state = ResourceState.Loading;
            
            ResourceManager.Instance.AddResourceLoadTask(this);
        }

        public override IEnumerator LoadAsync(Action callback)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取资源依赖
        /// </summary>
        public override List<string> GetDependResourceList()
        {
            return new List<string>() { AssetBundleName };
        }

        /// <summary>
        /// 初始化AssetBundleName
        /// </summary>
        protected void InitAssetBundleName()
        {
            assetBundleNameConfig = null;
            
            // 在config文件中查找资源
            using ResourceSearcher searcher = ResourceSearcher.Allocate(AssetName, AssetBundleName, AssetType);
            AssetInfo config = ResourceDataConfig.ConfigFile.GetAssetInfo(searcher);

            if (config == null)
            {
                Log.Error("未找到Asset的AssetInfo：{0}", assetName);
                return;
            }
            
            // 如果找到，则使用config的资源
            assetBundleNameConfig = config.AssetBundleName;

            if (assetBundleNameConfig.IsNullOrEmpty())
            {
                Log.Error("未在配置文件中找到AssetBundle：{0}", config.AssetBundleIndex, AssetBundleName);
            }
        }
        
        public override void Recycle()
        {
            ObjectPool<AssetResource>.Instance.Recycle(this);
        }

        public override string ToString()
        {
            return $"Type:Asset\t {base.ToString()}\t FromAssetBundle:";
        }
        
    }
}