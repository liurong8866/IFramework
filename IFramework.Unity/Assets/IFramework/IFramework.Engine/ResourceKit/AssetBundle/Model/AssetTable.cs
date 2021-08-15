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

using System.Collections.Generic;
using System.Linq;
using IFramework.Core;

namespace IFramework.Engine
{
    /// <summary>
    /// Asset缓存表
    /// </summary>
    public sealed class AssetTable : Table<AssetInfo>
    {
        /// <summary>
        /// 获取资源
        /// </summary>
        public AssetInfo GetAssetInfo(ResourceSearcher searcher)
        {
            // 获取资源
            string assetName = searcher.AssetName.ToLower();
            List<AssetInfo> assetInfoList = Get(assetName);

            // 过滤AssetBundleName
            if (searcher.AssetBundleName.IsNotNullOrEmpty())
            {
                assetInfoList = assetInfoList.Where(info => info.AssetBundleName == searcher.AssetBundleName).ToList();
            }
            
            // 过滤AssetType，
            if (searcher.AssetType.IsNotNullOrEmpty())
            {
                short code = searcher.AssetType.ToCode();
                if (code != 0)
                { 
                    List<AssetInfo> newInfo = assetInfoList.Where(info => info.AssetTypeCode == code).ToList();
                    
                    // 如果找到就用，找不到就忽略
                    if (newInfo.Any())
                    {
                        assetInfoList = newInfo;
                    }
                }
            }

            return assetInfoList.FirstOrDefault();
        }
    }
}