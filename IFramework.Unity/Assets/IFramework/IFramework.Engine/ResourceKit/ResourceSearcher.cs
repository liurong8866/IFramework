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
using IFramework.Core;

namespace IFramework.Engine
{
    public class ResourceSearcher : Disposeble, IPoolable, IRecyclable
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        public string AssetName { get; set; }

        /// <summary>
        /// 所属AssetBundle包名称
        /// </summary>
        public string AssetBundleName { get; set; }

        /// <summary>
        /// 资源全称 AssetBundleName.AssetName
        /// </summary>
        public string FullName => AssetBundleName.IsNullOrEmpty() ? AssetName.ToLowerInvariant() : AssetBundleName.ToLowerInvariant() + "." + AssetName.ToLowerInvariant();

        /// <summary>
        /// 资源类型
        /// </summary>
        public Type AssetType { get; set; }

        public string OriginalAssetName { get; set; }

        /// <summary>
        /// 静态方法生成实例
        /// </summary>
        public static ResourceSearcher Allocate(string assetName, string assetBundleName = null, Type assetType = null)
        {
            ResourceSearcher searcher = ObjectPool<ResourceSearcher>.Instance.Allocate();
            searcher.AssetName = assetName.ToLowerInvariant();
            searcher.AssetBundleName = assetBundleName?.ToLowerInvariant();
            searcher.AssetType = assetType;
            searcher.OriginalAssetName = assetName;
            return searcher;
        }

        /// <summary>
        /// 匹配资源
        /// </summary>
        public bool Match(IResource resource)
        {
            // 判断名称不相同则退出
            if (resource.AssetName != AssetName) return false;

            bool isMatch = true;

            // 如果设置了类型，则判断类型相符
            if (AssetType != null) {
                isMatch = resource.AssetType == AssetType;
            }

            // 如果设置了包名，则判断包相符
            if (AssetBundleName != null) {
                isMatch = isMatch && resource.AssetBundleName == AssetBundleName;
            }
            return isMatch;
        }

        public void OnRecycled()
        {
            AssetName = null;
            AssetBundleName = null;
            AssetType = null;
        }

        public bool IsRecycled { get; set; }

        public void Recycle()
        {
            ObjectPool<ResourceSearcher>.Instance.Recycle(this);
        }

        public override string ToString()
        {
            return $"AssetName:{AssetName} AssetBundleName:{AssetBundleName} TypeName:{AssetType}";
        }

        protected override void DisposeManaged()
        {
            Recycle();
        }
    }
}
