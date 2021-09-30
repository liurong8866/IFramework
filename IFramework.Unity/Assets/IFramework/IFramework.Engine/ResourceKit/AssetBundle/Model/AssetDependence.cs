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

namespace IFramework.Engine {
    /// <summary>
    /// 描述资源依赖关系类
    /// </summary>
    [Serializable]
    public class AssetDependence {

        public string AssetBundleName;

        public string[] Depends;

        public AssetDependence(string assetBundleName, string[] depends) {
            this.AssetBundleName = assetBundleName;
            if (depends.IsNotNullOrEmpty()) {
                this.Depends = depends;
            }
        }

        public override string ToString() {
            string result = "AssetName: " + AssetBundleName;
            if (Depends != null) {
                foreach (string depend in Depends) {
                    result += "#: " + depend;
                }
            }
            return result;
        }

    }
}
