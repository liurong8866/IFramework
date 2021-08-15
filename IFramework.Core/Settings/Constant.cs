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

namespace IFramework.Core
{
    public struct Constant
    {
        // IFramework 目录
        public const string FRAMEWORK_PATH = "Assets/IFramework";
        
        // AssetBundles 生成目录
        public const string ASSET_BUNDLE_OUTPUT_PATH = "AssetBundles";
        
        // AssetBundles 配置文件文件名称
        public const string ASSET_BUNDLE_CONFIG_FILE = "asset_bundle_config.bin";
        
        // AssetBundles 生成的脚本文件名称
        public const string ASSET_BUNDLE_SCRIPT_FILE = "AssetBundleName.cs";
        
        
        // double类型数据 == 时 精度
        public const double TOLERANCE = 1E-6;
    }
}