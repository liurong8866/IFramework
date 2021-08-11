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

namespace IFramework.Engine
{
    public class ResourceSearchRule : IPoolable, IRecyclable
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
        /// 资源类型
        /// </summary>
        public Type AssetType { get; set; }

        /// <summary>
        /// 静态方法生成实例
        /// </summary>
        public static ResourceSearchRule Allocate(string assetName, string assetBundleName = null, Type assetType = null)
        {
            ResourceSearchRule searchRule = ObjectPool<ResourceSearchRule>.Instance.Allocate();

            searchRule.AssetName = assetName.ToLower();
            searchRule.AssetBundleName = assetBundleName == null ? null : assetBundleName.ToLower();
            searchRule.AssetType = assetType;
            
            return searchRule;
        }

        /// <summary>
        /// 查找资源，根据资源名称、类型、所属AssetBundle包名称判断
        /// </summary>
        public bool Match(IResource resource)
        {
            if (resource.AssetName == AssetName)
            {
                bool isMatch = true;

                if (AssetType != null)
                {
                    isMatch = resource.AssetType == AssetType;
                }

                if (AssetBundleName != null)
                {
                    isMatch = isMatch && resource.AssetBundleName == AssetBundleName;
                }

                return isMatch;
            }

            return false;
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
            ObjectPool<ResourceSearchRule>.Instance.Recycle(this);
        }
        
        public override string ToString()
        {
            return string.Format("AssetName:{0} AssetBundleName:{1} TypeName:{2}", AssetName, AssetBundleName,
                AssetType);
        }
        
    }
}