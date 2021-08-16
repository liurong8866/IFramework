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
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IFramework.Core
{
    public class PlatformSetting
    {
        /// <summary>
        /// AssetBundle路径  AssetBundle/Platform
        /// </summary>
        public static string AssetBundlePath => Path.Combine(Constant.ASSET_BUNDLE_OUTPUT_PATH, EditorUserBuildSettings.activeBuildTarget.ToString());
        
        /// <summary>
        /// AssetBundle生成路径
        /// </summary>
        public static string AssetBundleBuildPath => Path.Combine(Constant.ASSET_BUNDLE_OUTPUT_PATH, GetPlatformForAssetBundles(CurrentBundlePlatform));
        
        /// <summary>
        /// StreamingAssets文件夹下到AssetBundle包
        /// </summary>
        public static string StreamingAssetBundlePath => Path.Combine(Application.streamingAssetsPath, AssetBundlePath);
        
        /// <summary>
        /// PersistentData 临时文件夹下到AssetBundle包
        /// </summary>
        public static string PersistentAssetBundlePath => Path.Combine(Application.persistentDataPath, AssetBundlePath);
        
        /// <summary>
        /// 获取Persistent 或者 Stream 路径
        /// </summary>
        public static string GetPersistentOrStreamPath(string relativePath)
        {
            string path = Path.Combine(Application.persistentDataPath, relativePath);

            if (File.Exists(path))
            {
                return path;
            }
            else
            {
                return Path.Combine(Application.streamingAssetsPath, relativePath);
            }
        }
        
        /// <summary>
        /// 运行时平台名称
        /// </summary>
        public static string GetPlatformForAssetBundles(RuntimePlatform platform)
        {
            switch (platform)
            {
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
        
        /// <summary>
        /// 编辑器模式下
        /// </summary>
        public static string GetPlatformForAssetBundles(BuildTarget target)
        {
            switch (target)
            { 
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneWindows64:
                    return "Windows";
                case BuildTarget.StandaloneOSX:
                    return "MacOS";
                case BuildTarget.StandaloneLinux64:
                    return "Linux";
                case BuildTarget.iOS:
                    return "iOS";
                case BuildTarget.Android:
                    return "Android";
                case BuildTarget.WebGL:
                    return "WebGL";
                case BuildTarget.PS4:
                    return "PS4";
                case BuildTarget.PS5:
                    return "PS5";
                case BuildTarget.XboxOne:
                    return "XboxOne";
                case BuildTarget.WSAPlayer:
                    return "WSAPlayer";
                default:
                    return null;
            }
        }
        
        /// <summary>
        /// 打包平台索引
        /// </summary>
        public static int GetCurrentPlatform()
        {
            int platformIndex = 0;
            
            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.StandaloneWindows:
                    platformIndex = 0;
                    break;
                case BuildTarget.StandaloneOSX:
                    platformIndex = 1;
                    break;
                case BuildTarget.iOS:
                    platformIndex = 2;
                    break;
                case BuildTarget.Android:
                    platformIndex = 3;
                    break;
                case BuildTarget.WebGL:
                    platformIndex = 4;
                    break;
                case BuildTarget.PS4:
                    platformIndex = 5;
                    break;
                case BuildTarget.PS5:
                    platformIndex = 6;
                    break;
                case BuildTarget.XboxOne:
                    platformIndex = 7;
                    break;
                default:
                    platformIndex = 0;
                    break;
            }

            return platformIndex;
        }
        
        /// <summary>
        /// 获得打包平台
        /// </summary>
        public static BuildTarget GetBuildTargetByIndex(int platformIndex)
        {
            switch (platformIndex)
            {
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
        
        /// <summary>
        /// 根据当前配置列表获取打包平台
        /// </summary>
        public static BuildTarget CurrentBundlePlatform => GetBuildTargetByIndex(Configure.CurrentPlatform.Value);

        /// <summary>
        /// 根据路径获得资源名
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string AssetBundleNameByUrl(string url)
        {
            string name = url.Replace(PlatformSetting.StreamingAssetBundlePath + "/", "")
                .Replace(PlatformSetting.PersistentAssetBundlePath + "/", "");
            return name;
        }
        
        /// <summary>
        /// 根据资源名获得资源路径
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string AssetBundleNameToUrl(string name)
        {
            // 优先返回PersistentAsset路径
            string url = PlatformSetting.PersistentAssetBundlePath + "/" + name;

            if (File.Exists(url))
            {
                return url;
            }
           
            return PlatformSetting.StreamingAssetBundlePath + "/" + name;
        }

        
    }
}