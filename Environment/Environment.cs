using System;
using UnityEditor;
using Object = UnityEngine.Object;
using IFramework.Core;

#if !UNITY_EDITOR
using UnityEngine;
#endif

namespace IFramework.Engine
{
    /// <summary>
    /// 环境类，用于编译条件的类，本身不编译，继承自IEnvironment接口，生成后放在Unity3D中
    /// </summary>
    public sealed class Environment : Singleton<Environment>, IEnvironment
    {
        private Environment() { }

        /// <summary>
        /// 获取当前平台名称
        /// </summary>
        public string RuntimePlatformName {
        #if UNITY_EDITOR
            get => GetPlatformName((int)EditorUserBuildSettings.activeBuildTarget);
        #else
            get => Platform.GetPlatformName(Application.platform);
        #endif
        }


        /// <summary>
        /// 编辑器模式下
        /// </summary>
        public string GetPlatformName(int target) {
        #if UNITY_EDITOR
            switch ((BuildTarget)target) {
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
        #else
            return null;
        #endif
        }
        
        /// <summary>
        /// 是否模拟模式
        /// </summary>
        public bool IsSimulation {
        #if UNITY_EDITOR
            get { return Configure.ResourceKit.IsSimulation.Value; }
            // ReSharper disable once ValueParameterNotUsed
            set { Configure.ResourceKit.IsSimulation.Value = true; }
        #else
            get { return false; }
            set { }
        #endif
        }

        /// <summary>
        /// 文件路径前缀file://
        /// </summary>
        public string FilePathPrefix {
            get {
            #if UNITY_EDITOR || UNITY_IOS
                return "file://";
            #else
                return string.Empty;
            #endif
            }
        }
        
        // public static readonly string PathURL =
        //             #if UNITY_ANDROID
        //     "jar:file://" + Application.dataPath + "!/assets/";
        //             #elif UNITY_IPHONE
        //     Application.dataPath + "/Raw/";
        //             #elif UNITY_STANDALONE_WIN || UNITY_EDITOR
        //                 "file://" + Application.dataPath + "/StreamingAssets/";
        //     #else
        //     string.Empty;
        //     #endif

        /// <summary>
        /// 根据资源名、包名获取的所有路径
        /// </summary>
        public string[] GetAssetPathsFromAssetBundleAndAssetName(string assetName, string assetBundleName) {
        #if UNITY_EDITOR
            return AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(assetName, assetBundleName);
        #else
            return null;
        #endif
        }

        /// <summary>
        /// 根据路径、类型获取资源
        /// </summary>
        public Object LoadAssetAtPath(string assetPath, Type assetType) {
        #if UNITY_EDITOR
            return AssetDatabase.LoadAssetAtPath(assetPath, assetType);
        #else
            return null;
        #endif
        }

        /// <summary>
        /// 根据路径获取资源
        /// </summary>
        public T LoadAssetAtPath<T>(string assetPath) where T : Object {
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
        /// <param name="assetBundleNames"></param>
        public void InitAssetBundleConfig(IAssetBundleConfig assetBundleConfig, string[] assetBundleNames = null) {
        #if UNITY_EDITOR
            AssetDatabase.RemoveUnusedAssetBundleNames();

            // 如果没有传入Name，则获取全部AssetBundleName
            string[] assetBundleNameArray = assetBundleNames ?? AssetDatabase.GetAllAssetBundleNames();

            // 循环处理所有AssetBundleName
            foreach (string assetBundleName in assetBundleNameArray) {
                // 获取AssetBundleName是否有依赖，比如 Secne 包含了多个资源
                string[] depends = AssetDatabase.GetAssetBundleDependencies(assetBundleName, false);

                // 添加AssetBundleName信息到缓存
                int index = assetBundleConfig.AddAssetBundleInfo(assetBundleName, depends, out IAssetBundleInfo @assetBundleInfo);
                AssetBundleInfo @assetBundleInfo2 = assetBundleInfo as AssetBundleInfo;
                if (index < 0) {
                    continue;
                }

                // 获取该AssetBundleName下所有资源
                string[] assets = AssetDatabase.GetAssetPathsFromAssetBundle(assetBundleName);

                foreach (string asset in assets) {
                    // 取得资源类型
                    Type type = AssetDatabase.GetMainAssetTypeAtPath(asset);
                    short code = type.ToCode();

                    // 取得资源名，小写无扩展名
                    string assetName = FileUtils.GetFileNameByPath(asset, false).ToLowerInvariant();

                    // 添加资源到缓存
                    @assetBundleInfo2.AddAssetInfo(asset.EndsWith(".unity")
                                                            ? new AssetInfo(assetName, assetBundleName, index, ResourceLoadType.ASSET_BUNDLE_SCENE, code)
                                                            : new AssetInfo(assetName, assetBundleName, index, ResourceLoadType.ASSET_BUNDLE_ASSET, code));
                }
            }
        #endif
        }
    }
}
