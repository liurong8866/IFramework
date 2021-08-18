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
using Object = UnityEngine.Object;
using IFramework.Core;

namespace IFramework.Engine
{
    /// <summary>
    /// 环境类，用于编译条件的类，本身不编译，继承自IEnvironment接口，生成后放在Unity3D中
    /// </summary>
    public class Environment : IEnvironment
    {
        /// <summary>
        /// 获取当前平台名称
        /// </summary>
        public static string PlatformName
        {
#if UNITY_EDITOR
            get => GetPlatformForAssetBundles(EditorUserBuildSettings.activeBuildTarget);
#else
            get => PlatformSetting.GetPlatformForAssetBundles(Application.platform);
#endif
        }
        
#if UNITY_EDITOR
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
#endif
        
        /// <summary>
        /// 文件路径前缀file://
        /// </summary>
        public static string FilePathPrefix
        {
            get
            {
#if UNITY_EDITOR || UNITY_IOS
                return "file://";
#else
                return string.Empty;
#endif
            }
        }
        
        /// <summary>
        /// 是否模拟模式
        /// </summary>
        /// <summary>
        /// 是否模拟模式
        /// </summary>
#if UNITY_EDITOR
        public static bool IsSimulation
        {
            get { return Configure.IsSimulation.Value; }
            // ReSharper disable once ValueParameterNotUsed
            set { Configure.IsSimulation.Value = true; }
#else
            get { return false; }
            set { }
#endif
        }
        
        /// <summary>
        /// 根据资源名、包名获取的所有路径
        /// </summary>
        public static string[] GetAssetPathsFromAssetBundleAndAssetName(string assetName, string assetBundleName)
        {
#if UNITY_EDITOR
            return AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(assetName, assetBundleName);
#else
            return null;
#endif
        }
        
        /// <summary>
        /// 根据路径、类型获取资源
        /// </summary>
        public static Object LoadAssetAtPath(string assetPath, Type assetType)
        {
#if UNITY_EDITOR
            return AssetDatabase.LoadAssetAtPath(assetPath, assetType);
#else
            return null;
#endif
        }
        
        /// <summary>
        /// 根据路径获取资源
        /// </summary>
        public static T LoadAssetAtPath<T>(string assetPath) where T : Object
        {
#if UNITY_EDITOR
            return AssetDatabase.LoadAssetAtPath<T>(assetPath);
#else
            return null;
#endif
        }

        /// <summary>
        /// 将AssetBundle信息添加到关系配置表中
        /// </summary>
        /// <param name="assetBundleConfig"></param>
        /// <param name="assetBundleName"></param>
        public static void AddAssetBundleInfoToResourceData(AssetBundleConfig assetBundleConfig, string[] assetBundleName = null)
        {
#if UNITY_EDITOR
            
            AssetDatabase.RemoveUnusedAssetBundleNames();

            string[] assetBundleNames = assetBundleName ?? AssetDatabase.GetAllAssetBundleNames();
            
            foreach (string name in assetBundleNames)
            {
                string[] depends = AssetDatabase.GetAssetBundleDependencies(name, false);

                int index = assetBundleConfig.AddAssetDependence(name, depends, out AssetBundleInfo @assetBundleInfo);
                if (index < 0)
                {
                    continue;
                }
            
                string[] assets = AssetDatabase.GetAssetPathsFromAssetBundle(name);
                foreach (string asset in assets)
                {
                    Type type = AssetDatabase.GetMainAssetTypeAtPath(asset);
            
                    short code = type.ToCode();

                    string fileName = Path.GetFileName(asset);
                    
                    @assetBundleInfo.AddAssetInfo(asset.EndsWith(".unity")
                        ? new AssetInfo(fileName, name, index, ResourceLoadType.SCENE, code)
                        : new AssetInfo(fileName, name, index, ResourceLoadType.ASSET, code));
                }
            }
#endif
        }
        
    }
}