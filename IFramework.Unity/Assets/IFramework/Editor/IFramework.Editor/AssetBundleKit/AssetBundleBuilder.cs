using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IFramework.Core;
using IFramework.Engine;
using UnityEditor;
using UnityEngine;
using Environment = IFramework.Engine.Environment;

namespace IFramework.Editor
{
    public static class AssetBundleBuilder
    {
        public static void BuildAssetBundles()
        {
            Log.Clear();

            // 打包AssetBundle
            BuildAssetBundles(Platform.CurrentBuildPlatform);

            // 自动生成包名常量
            if (Configure.AutoGenerateName) {
                AssetBundleScript.GenerateConstScript();
            }
        }

        /// <summary>
        /// 打包 AssetBundle
        /// </summary>
        /// <param name="buildTarget">目标平台</param>
        public static void BuildAssetBundles(BuildTarget buildTarget)
        {
            string platformName = Environment.Instance.GetPlatformName(buildTarget);
            Log.Info("开始打包: [{0}]: 开始", platformName);
            AssetDatabase.RemoveUnusedAssetBundleNames();
            AssetDatabase.Refresh();

            // 默认包
            AssetBundlePackage defaultPackage = new AssetBundlePackage();

            // 子包
            List<AssetBundlePackage> subPackages = AssetBundlePackage.GetPackageList();

            // 划分默认包、子包
            AssetBundlePackage.SplitPackage(defaultPackage, subPackages);
            string outputPath = Path.Combine(Constant.ASSET_BUNDLE_PATH, platformName);
            Log.Info("正在打包: [{0}]: {1}", platformName, outputPath);
            DateTime start = DateTime.Now;

            // 打包
            Build(outputPath, defaultPackage, buildTarget);

            // 打包 - 子包
            foreach (AssetBundlePackage subPackage in subPackages) {
                string path = Path.Combine(outputPath, subPackage.NameSpace, subPackage.Name);
                Log.Info("正在打包: [{0}]: {1}", platformName, path);
                Build(path, subPackage, buildTarget);
            }
            AssetDatabase.Refresh();
            Log.Info("打包完毕: [{0}]: 共计{1}个主包，{2}个子包，耗时{3}秒", platformName, 1, subPackages.Count, (DateTime.Now - start).TotalSeconds);
        }

        /// <summary>
        /// 打包
        /// </summary>
        /// <param name="outputPath">输出目录</param>
        /// <param name="package">待打包资源</param>
        /// <param name="buildTarget">目标平台</param>
        private static void Build(string outputPath, AssetBundlePackage package, BuildTarget buildTarget)
        {
            // 没有则创建
            DirectoryUtils.Create(outputPath);

            // 打包 - 默认包
            BuildPipeline.BuildAssetBundles(outputPath, package.packages.ToArray(), BuildAssetBundleOptions.ChunkBasedCompression, buildTarget);

            // streamingAssets 路径
            string streamPath = Path.Combine(Application.streamingAssetsPath, outputPath);

            // 清空或创建目录
            DirectoryUtils.Clear(streamPath);

            // 覆盖目录
            FileUtil.ReplaceDirectory(outputPath, streamPath);

            // 保存配置文件
            BuildAssetConfigFile(package.packages.Select(b => b.assetBundleName).ToArray(), streamPath);
        }

        /// <summary>
        /// 构建AssetBundle 关系配置文件
        /// </summary>
        private static void BuildAssetConfigFile(string[] assetBundleNames, string outputPath = null)
        {
            if (assetBundleNames.IsNullOrEmpty()) return;

            AssetBundleConfig assetBundleConfig = new AssetBundleConfig();
            Environment.Instance.InitAssetBundleConfig(assetBundleConfig, assetBundleNames);
            string filePath = Path.Combine((outputPath ?? Platform.StreamingAssets.Root).Create(), Constant.ASSET_BUNDLE_CONFIG_FILE);
            assetBundleConfig.Save(filePath);
        }

        /// <summary>
        /// 强制清除所有AssetBundles
        /// </summary>
        public static void ForceClearAssetBundles()
        {
            DirectoryUtils.Clear(Constant.ASSET_BUNDLE_PATH);
            DirectoryUtils.Clear(Path.Combine(Application.streamingAssetsPath, Constant.ASSET_BUNDLE_PATH));
            AssetDatabase.Refresh();
        }
    }
}
