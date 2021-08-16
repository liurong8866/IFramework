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
using UnityEditor;
using Object = UnityEngine.Object;

namespace IFramework.Core
{
    /// <summary>
    /// 环境类，用于编译条件的类，本身不编译，继承自IEnvironment接口，生成后放在Unity3D中
    /// </summary>
    public class Environment : IEnvironment
    {
        /// <summary>
        /// 获取当前平台名称
        /// </summary>
        public static string GetPlatformName()
        {
#if UNITY_EDITOR
            return EditorUserBuildSettings.activeBuildTarget.ToString();
#else
            return PlatformSetting.GetPlatformForAssetBundles(Application.platform);
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
    }
    
}