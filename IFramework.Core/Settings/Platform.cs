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
using System.IO;
using UnityEditor;
using UnityEngine;

namespace IFramework.Core {
    /// <summary>
    /// 平台配置相关方法
    /// </summary>
    public static class Platform {

        /*----------------------------- 平台相关 -----------------------------*/

        /// <summary>
        /// 运行时平台名称
        /// </summary>
        public static string RuntimePlatformName => PlatformEnvironment.Instance.RuntimePlatformName;

        /// <summary>
        /// 是否为模拟模式
        /// </summary>
        public static bool IsSimulation => PlatformEnvironment.Instance.IsSimulation;

        /// <summary>
        /// 根据当前配置列表获取打包平台
        /// </summary>
        public static BuildTarget CurrentBuildPlatform {
            get {
                switch (Configure.CurrentPlatform.Value) {
                    case 0:
                        return BuildTarget.StandaloneWindows;
                    case 1:
                        return BuildTarget.StandaloneOSX;
                    case 2:
                        return BuildTarget.iOS;
                    case 3:
                        return BuildTarget.Android;
                    case 4:
                        return BuildTarget.WebGL;
                    case 5:
                        return BuildTarget.PS4;
                    case 6:
                        return BuildTarget.PS5;
                    case 7:
                        return BuildTarget.XboxOne;
                    default:
                        return BuildTarget.StandaloneWindows;
                }
            }
        }

        /// <summary>
        /// 运行时平台名称
        /// </summary>
        public static string GetPlatformName(RuntimePlatform platform) {
            switch (platform) {
                case RuntimePlatform.WindowsPlayer:
                    return "Windows";
                case RuntimePlatform.OSXPlayer:
                    return "MacOS";
                case RuntimePlatform.LinuxPlayer:
                    return "Linux";
                case RuntimePlatform.IPhonePlayer:
                    return "iOS";
                case RuntimePlatform.Android:
                    return "Android";
                case RuntimePlatform.WebGLPlayer:
                    return "WebGL";
                case RuntimePlatform.PS4:
                    return "PS4";
                case RuntimePlatform.PS5:
                    return "PS5";
                case RuntimePlatform.XboxOne:
                    return "XboxOne";
                case RuntimePlatform.WSAPlayerARM:
                case RuntimePlatform.WSAPlayerX64:
                case RuntimePlatform.WSAPlayerX86:
                    return "WSAPlayer";
                default:
                    return null;
            }
        }

        /*------------ 路径属性 Runtime开头的方法为携带 PlatformName ------------*/

        /// <summary>
        /// 运行时平台AssetBundle包路径，优先获取Persistent，其次获取Stream
        /// </summary>
        public static string RuntimeAssetBundlePath => Path.Combine(GetPersistentOrStreamPath(Constant.ASSET_BUNDLE_PATH), RuntimePlatformName);

        /// <summary>
        /// 内部目录 StreamingAssets文件夹路径
        /// </summary>
        public static string StreamingAssetsPath => Application.streamingAssetsPath;

        /// <summary>
        /// StreamingAssets文件夹下到AssetBundle包
        /// </summary>
        public static string StreamingAssetBundlePath => Path.Combine(StreamingAssetsPath, Constant.ASSET_BUNDLE_PATH);

        /// <summary>
        /// 运行时平台StreamAsset/AssetBundle/Platform包路径
        /// </summary>
        public static string RuntimeStreamAssetBundlePath => Path.Combine(StreamingAssetBundlePath, RuntimePlatformName);

        /// <summary>
        /// 外部目录 PersistentDataPath文件夹路径
        /// </summary>
        public static string PersistentDataPath => Application.persistentDataPath;
        
        
        /// <summary>
        /// 外部资源路径 PersistentDataPath/AssetBundle
        /// </summary>
        public static string PersistentAssetBundlePath {
            get {
                if (persistentAssetBundlePath == null) {
                    persistentAssetBundlePath = Path.Combine(PersistentDataPath, Constant.ASSET_BUNDLE_PATH);
                    DirectoryUtils.Create(persistentAssetBundlePath);
                }
                return persistentAssetBundlePath;
            }
        }
        
        /// <summary>
        /// 外部图片路径 PersistentDataPath/Resources/Images
        /// </summary>
        public static string PersistentImagePath => PersistentResourcePath(persistentImagePath, Constant.RESOURCE_IMAGE_PATH);

        /// <summary>
        /// 外部头像路径 PersistentDataPath/Resources/Images/Photo
        /// </summary>
        public static string PersistentPhotoPath => PersistentResourcePath(persistentPhotoPath, Constant.RESOURCE_PHOTO_PATH);

        /// <summary>
        /// 外部头像路径 PersistentDataPath/Resources/Video
        /// </summary>
        public static string PersistentVideoPath => PersistentResourcePath(persistentVideoPath, Constant.RESOURCE_VIDEO_PATH);

        /// <summary>
        /// 外部头像路径 PersistentDataPath/Resources/Audio
        /// </summary>
        public static string PersistentAudioPath => PersistentResourcePath(persistentAudioPath, Constant.RESOURCE_AUDIO_PATH);
        
        private static string PersistentResourcePath(string path, string folder) {
            if (path == null) {
                path = Path.Combine(PersistentDataPath, folder);
                DirectoryUtils.Create(path);
            }
            return path;
        }
        
        /// <summary>
        /// 文件路径前缀 file://
        /// </summary>
        public static string FilePathPrefix => PlatformEnvironment.Instance.FilePathPrefix;
        
        /*--------------------------- 资源名称相关 ---------------------------*/

        /// <summary>
        /// 根据路径获得资源名
        /// </summary>
        public static string AssetBundleNameByUrl(string url) {
            return url.Replace(RuntimeStreamAssetBundlePath + "/", "").Replace(PersistentAssetBundlePath + "/", "");
        }

        /// <summary>
        /// 根据资源名获得资源路径
        /// </summary>
        public static string AssetBundleNameToUrl(string name) {
            // 优先返回PersistentAsset路径
            string url = Path.Combine(PersistentAssetBundlePath, name);
            return File.Exists(url) ? url : Path.Combine(RuntimeStreamAssetBundlePath, name);
        }

        /// <summary>
        /// 获取资源名称，不包含扩展名
        /// </summary>
        public static string AssetPathToName(string assetPath) {
            var startIndex = assetPath.LastIndexOf("/", StringComparison.Ordinal) + 1;
            var endIndex = assetPath.LastIndexOf(".", StringComparison.Ordinal);
            if (endIndex > 0) {
                var length = endIndex - startIndex;
                return assetPath.Substring(startIndex, length).ToLower();
            }
            return assetPath.Substring(startIndex).ToLower();
        }

        /*--------------------------- 私有方法、变量 ---------------------------*/

        /// <summary>
        /// 先从外部资源获取，如果没有则返回内部资源路径
        /// </summary>
        private static string GetPersistentOrStreamPath(string relativePath) {
            string path = Path.Combine(PersistentDataPath, relativePath);
            if (File.Exists(path)) {
                return path;
            }
            return Path.Combine(StreamingAssetsPath, relativePath);
        }

        private static string persistentAssetBundlePath;
        private static string persistentImagePath;
        private static string persistentPhotoPath;
        private static string persistentVideoPath;
        private static string persistentAudioPath;

    }
}
