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
using System.Collections.Generic;
using System.IO;
using IFramework.Core;
using UnityEditor;
using UnityEngine;

namespace IFramework.Editor
{
    public class AssetBundleBuilder
    {
        public static void BuildAssetBundles()
        {
            BuildAssetBundles(PlatformSettings.CurrentBundlePlatform);
        }

        /// <summary>
        /// 打包 AssetBundle
        /// </summary>
        /// <param name="buildTarget">目标平台</param>
        public static void BuildAssetBundles(BuildTarget buildTarget)
        {   
            Log.Info("开始打包: [{0}]:", buildTarget);
            AssetDatabase.RemoveUnusedAssetBundleNames();
            
            AssetDatabase.Refresh();
            
            // 默认包
            AssetBundlePackage defaultPackage = new AssetBundlePackage();

            // 子包
            List<AssetBundlePackage> subPackages = AssetBundlePackage.GetPackageList();

            // 划分默认包、子包
            AssetBundlePackage.SplitPackage(defaultPackage, subPackages);

            string outputPath = Path.Combine(AssetBundleKit.AssetBundleOutputPath, buildTarget.ToString());

            Log.Info("正在打包: [{0}]: {1}", buildTarget, outputPath);

            DateTime start = DateTime.Now;
            
            // 打包
            Build(outputPath, defaultPackage, buildTarget);
            
            // 打包 - 子包
            foreach (AssetBundlePackage subPackage in subPackages)
            {
                outputPath = Path.Combine(AssetBundleKit.AssetBundleOutputPath, subPackage.NameSpace, subPackage.Name, buildTarget.ToString());
                
                Log.Info("正在打包: [{0}]: {1}", buildTarget, outputPath);
                
                Build(outputPath, subPackage, buildTarget);
            }
            
            Log.Info("打包完毕: [{0}]，共计{1}个主包，{2}个子包，耗时{3}秒", buildTarget, 1, subPackages.Count,  (DateTime.Now - start).TotalSeconds );
            AssetDatabase.Refresh();
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
            BuildPipeline.BuildAssetBundles(outputPath, package.packages.ToArray(),
                BuildAssetBundleOptions.ChunkBasedCompression, buildTarget);

            // streamingAssets 路径
            string streamPath = Path.Combine(Application.streamingAssetsPath, outputPath);
            
            // 清空或创建目录
            DirectoryUtils.Clear(streamPath);
            
            // 覆盖目录
            FileUtil.ReplaceDirectory(outputPath, streamPath);
            
            //TODO 
            // AssetBundleExporter.BuildDataTable(defaultSubProjectData.Builds.Select(b => b.assetBundleName).ToArray());
        }
        
        /// <summary>
        /// 强制清除所有AssetBundles
        /// </summary>
        public static void ForceClearAssetBundles()
        {
            DirectoryUtils.Remove(AssetBundleKit.AssetBundleOutputPath);
            
            DirectoryUtils.Remove(Path.Combine(Application.streamingAssetsPath, AssetBundleKit.AssetBundleOutputPath));

            AssetDatabase.Refresh();
        }
    }
}