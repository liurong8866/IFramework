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

using System.ComponentModel.Design;
using IFramework.Core;

namespace IFramework.Engine
{
    public class AssetBundleScene : AssetResource
    {
        public static AssetBundleScene Allocate(string name)
        {
            AssetBundleScene res = ObjectPool<AssetBundleScene>.Instance.Allocate();
            if (res != null)
            {
                res.AssetName = name;
                res.InitAssetBundleName();
            }
            return res;
        }
        
        public AssetBundleScene() {}
        public AssetBundleScene(string assetName) : base(assetName) {}

        public override bool Load()
        {
            if (!IsLoadable) return false;
            
            // 如果配置文件没有对应的Asset，则退出
            if (assetBundleNameConfig.IsNullOrEmpty()) return false;
            
            ResourceSearcher searcher = ResourceSearcher.Allocate(assetBundleNameConfig);
            AssetBundleResource resource = ResourceManager.Instance.GetResource<AssetBundleResource>(searcher);

            if (resource == null || resource.AssetBundle == null)
            {
                Log.Error("加载资源失败，没有找到AssetBundle：" + resource);
                return false;
            }

            state = ResourceState.Ready;
            return true;
        }

        public override void LoadASync()
        {
            Load();
        }

        public override void Recycle()
        {
            ObjectPool<AssetBundleScene>.Instance.Recycle(this);
        }
    }
}