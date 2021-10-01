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

namespace IFramework.Core {
    public struct Constant {

        // IFramework 目录
        public const string FRAMEWORK_NAME = "IFramework";
        
        // Environment 目录
        public const string ENVIRONMENT_PATH = "IFramework/Environment";
        
        // AssetBundles 生成的脚本文件名称
        public const string ASSET_BUNDLE_SCRIPT_FILE = "AssetsName.cs";

        // AssetBundle 生成目录
        public const string ASSET_BUNDLE_PATH = "AssetBundle";
        
        // Resources/Images
        public const string RESOURCE_IMAGE_PATH = "Resources/Images";
        
        // Resources/Images/Photo
        public const string RESOURCE_PHOTO_PATH = "Resources/Images/Photo";
        
        // Resources/Video
        public const string RESOURCE_VIDEO_PATH = "Resources/Video";
        
        // Resources/Audio
        public const string RESOURCE_AUDIO_PATH = "Resources/Audio";
        
        // AssetBundle 密钥
        public const string ASSET_BUNDLE_KEY = "iT5jM9h+7zT1rZ6x";

        // AssetBundle 配置文件名称
        public const string ASSET_BUNDLE_CONFIG_FILE = "asset-bundle-config.bin";

        // AssetBundle 配置文件密钥
        public const string ASSET_BUNDLE_CONFIG_FILE_KEY = "AoGI+h3OEA4TcJ1H";

        // Double类型数据比较 == 时 精度保留0.000001 有效，超过则视为可接受误差，判断为：相等
        public const double TOLERANCE = 1E-6;
        
        

    }
}
