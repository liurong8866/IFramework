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

using UnityEngine;

namespace IFramework.Engine
{
    public class Resource : AbstractResource
    {
        private string path;
        
        private ResourceRequest request;
        
        public static Resource Allocate(string name, ResourcesUrlType urlType)
        {
            var resource = ObjectPool<Resource>.Instance.Allocate();
            
            if (resource != null)
            {
                resource.AssetName = name;
            }

            if (urlType == ResourcesUrlType.Url)
            {
                resource.path = name.Substring("resources://".Length);
            }
            else
            {
                resource.path = name.Substring("Resources/".Length);
            }

            return resource;
        }
        
        
        public override bool LoadSync()
        {
            if (!CanLoadAble) return false;

            if (AssetName.IsNullOrEmpty()) return false;

            State = ResourceState.Loading;

            if (AssetType != null)
            {
                asset = Resources.Load(path, AssetType);
            }
            else
            {
                asset = Resources.Load(path);
            }

            if (asset == null)
            {
                ("资源加载失败：" + path).LogError();
                OnResourceLoadFailed();
                return false;
            }

            State = ResourceState.Ready;
            return true;
        }

        public override void LoadASync()
        {
            if (!CanLoadAble) return;

            if (AssetName.IsNullOrEmpty()) return;

            State = ResourceState.Loading;
            
            //TODO
        }

        public override void Recycle()
        {
            ObjectPool<Resource>.Instance.Recycle(this);
        }
    }
}