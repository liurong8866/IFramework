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
    [Serializable]
    public class AssetInfo
    {

        /// <summary>
        /// 资源名称
        /// </summary>
        public string AssetName;

        /// <summary>
        /// 所属AssetBundle包名称
        /// </summary>
        public string AssetBundleName;

        /// <summary>
        /// 所属AssetBundle包索引
        /// </summary>
        public int AssetBundleIndex;

        /// <summary>
        /// 资源类型
        /// </summary>
        public short AssetType;

        /// <summary>
        /// 资源类型编码
        /// </summary>
        public short AssetTypeCode;

        /// <summary>
        /// UUID
        /// </summary>
        public string Uuid => AssetBundleName.IsNullOrEmpty() ? AssetName : AssetBundleName + AssetName;

        public AssetInfo() { }

        public AssetInfo(string assetName, string assetBundleName, int assetBundleIndex, short assetType, short assetTypeCode = 0)
        {
            AssetName = assetName;
            AssetBundleName = assetBundleName;
            AssetBundleIndex = assetBundleIndex;
            AssetType = assetType;
            AssetTypeCode = assetTypeCode;
        }
    }
}