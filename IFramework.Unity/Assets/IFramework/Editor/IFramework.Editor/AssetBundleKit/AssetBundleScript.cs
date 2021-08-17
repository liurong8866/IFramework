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

using System.IO;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Editor
{
    public class AssetBundleScript
    {
        public static void GenerateConstScript()
        {
            Log.Info("生成脚本: [{0}]: 开始！", PlatformSetting.CurrentBundlePlatform);
            
            // 生成文件路径
            string path = Path.Combine(Application.dataPath, Constant.FRAMEWORK_PATH, Constant.ASSET_BUNDLE_SCRIPT_FILE);

            string content = Generate();
            
            SerializeUtils.SerializeToFile(path, content);
            
            Log.Info("生成脚本: [{0}]: 完成！", PlatformSetting.CurrentBundlePlatform);
        }

        public static string Generate()
        {
            
            return "haha";
        }
    }
}