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

using System.Collections.Generic;
using System.IO;
using System.Linq;
using IFramework.Core;
using IFramework.Engine;
using UnityEditor.Build.Content;
using UnityEngine;

namespace IFramework.Editor
{
    public class AssetBundleScript
    {
        public static void GenerateConstScript()
        {
            Log.Info("生成脚本: [{0}]: 开始！", PlatformSetting.CurrentBundlePlatform);
            
            // 生成文件路径
            string path = Path.Combine(Application.dataPath, Constant.FRAMEWORK_NAME, Constant.ASSET_BUNDLE_SCRIPT_FILE);

            string content = Generate();
            
            FileUtils.Write(path, content);
            
            Log.Info("生成脚本: [{0}]: 完成！", PlatformSetting.CurrentBundlePlatform);
        }

        /// <summary>
        /// 生成脚本
        /// </summary>
        public static string Generate()
        {
            AssetDataConfig assetDataConfig = new AssetDataConfig();
            
            Environment.AddAssetBundleInfoToResourceData(assetDataConfig, null);

            List<AssetGroup> assetGroups = assetDataConfig.AssetGroups;

            List<AssetBundleScriptModel> assetBundleModels = new List<AssetBundleScriptModel>();

            // 循环加载所有资源
            foreach (AssetGroup assetGroup in assetGroups)
            {
                List<AssetDependence> depends = assetGroup.AssetDepends;

                foreach (AssetDependence depend in depends)
                {
                    AssetBundleScriptModel modle =new AssetBundleScriptModel(depend.AssetBundleName);

                    modle.assets = assetGroup.AssetInfos
                        .Where(info => info.AssetBundleName == depend.AssetBundleName)
                        .Select(info => info.AssetName)
                        .ToArray();
                    
                    assetBundleModels.Add(modle);
                }
            }

            return "";
        }
    }

    internal class AssetBundleScriptModel
    {
        public readonly string name = "";

        public AssetBundleScriptModel(string name)
        {
            this.name = name;
        }

        public string[] assets;
    }
}