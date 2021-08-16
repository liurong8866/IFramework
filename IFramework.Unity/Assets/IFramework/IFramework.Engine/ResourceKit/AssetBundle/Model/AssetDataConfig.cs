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
using System.Collections;
using IFramework.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace IFramework.Engine
{
    /// <summary>
    /// 资源数据管理类
    /// </summary>
    public sealed class AssetDataConfig
    {
        private AssetTable assetTable;

        private readonly List<AssetGroup> assetGroupList= new List<AssetGroup>();
        
        public List<AssetGroup> AssetGroups => assetGroupList;

        public void Reset()
        {
            foreach (AssetGroup assetGroup in assetGroupList)
            {
                assetGroup.Reset();
            }
            assetGroupList.Clear();
            
            assetTable?.Dispose();

            assetTable = null;
        }
        
        /// <summary>
        /// 添加AssetBundle资源名
        /// </summary>
        public int AddAssetDependence(string assetName, string[] depends, out AssetGroup group)
        {
            group = null;

            if (assetName.IsNullOrEmpty()) return -1;
 
            string key = assetName.TrimEnd('/').TrimEnd('\\');

            if (key.IsNullOrEmpty()) return -1;
            
            // 根据Key获取group
            group = assetGroupList.FirstOrDefault(item => item.Key.Equals(key));
            
            // 如果没有group，就新增
            if (group == null)
            {
                group = new AssetGroup(key);
                assetGroupList.Add(group);
            }

            // 添加资源关系信息到group
            return group.AddAssetDependence(assetName, depends);
        }
        
        /// <summary>
        /// 通过URL找到所有依赖资源
        /// </summary>
        public string[] GetAllDependenciesByUrl(string url)
        {
            string assetBundleName = PlatformSetting.AssetBundleNameByUrl(url);

            string[] depends = null;
            
            foreach (AssetGroup assetGroup in assetGroupList)
            {
                depends = assetGroup.GetAssetBundleDepends(assetBundleName);
                if (depends != null)
                {
                    break;
                }
            }
            return depends;
        }
        
        /// <summary>
        /// 获取资源信息
        /// </summary>
        public AssetInfo GetAssetInfo(ResourceSearcher searcher)
        {
            // 如果assetTable为空，则初始化AssetTable
            if (assetTable == null)
            {
                assetTable = new AssetTable();
                
                for (int i = assetGroupList.Count - 1; i >= 0; --i)
                {
                    foreach (AssetInfo assetInfo in assetGroupList[i].AssetInfos)
                    {
                        assetTable.Add(assetInfo.AssetName, assetInfo);
                    }
                }
            }
            // 从 AssetTable中获取数据
            return assetTable.GetAssetInfo(searcher);
        }

        /// <summary>
        /// 保存数据关系
        /// </summary>
        public void Save(string path)
        {
            AssetGroupDatas data = new AssetGroupDatas
            {
                AssetGroups = new AssetGroupData[assetGroupList.Count]
            };

            for (int i = 0; i < assetGroupList.Count; i++)
            {
                data.AssetGroups[i] = assetGroupList[i].GetSerializeData();
            }
            
            SerializeUtils.SerializeToFile(path, data);
        }
        
        /// <summary>
        /// 从配置文件加载关系
        /// </summary>
        public void LoadFromFile(string path)
        {
            AssetGroupDatas groups = SerializeUtils.DeserializeFromFile<AssetGroupDatas>(path);
            
            SetSerializeData(groups);
        }
        
        /// <summary>
        /// 从配置文件加载关系
        /// </summary>
        public IEnumerator LoadFromFileAsync(string path)
        {
            using UnityWebRequest webRequest = new UnityWebRequest(path);

            yield return webRequest.SendWebRequest();
            
            // 如果成功
            if (!webRequest.isDone)
            {
                Log.Error("AssetBundle配置资源加载失败: " + path);
                yield break;
            }
            
            MemoryStream stream = new MemoryStream(webRequest.downloadHandler.data);
                
            AssetGroupDatas groups = SerializeUtils.DeserializeFromFile<AssetGroupDatas>(path);
                
            if(groups == null) yield break;
                
            SetSerializeData(groups);
        }
        
        /// <summary>
        /// 加载后设置序列化数据
        /// </summary>
        private void SetSerializeData(AssetGroupDatas data)
        {
            if (data?.AssetGroups == null) return;

            for (int i = data.AssetGroups.Length - 1; i >= 0; i--)
            {
                assetGroupList.Add(new AssetGroup(data.AssetGroups[i]));
            }

            assetTable ??= new AssetTable();
        }
        
        
        /// <summary>
        /// 获取自定义的 资源信息
        /// </summary>
        /// <returns></returns>
        public static AssetDataConfig ConfigFile
        {
            get => configFile??= new AssetDataConfig();
            set => configFile = value;
        }

        private static AssetDataConfig configFile = null;
        
//         /// <summary>
//         /// 将AssetBundle信息添加到关系配置表中
//         /// </summary>
//         /// <param name="assetDataConfig"></param>
//         /// <param name="assetBundleName"></param>
//         public static void AddAssetBundleInfoToResourceData(AssetDataConfig assetDataConfig, string[] assetBundleName = null)
//         {
// #if UNITY_EDITOR
//             
//             AssetDatabase.RemoveUnusedAssetBundleNames();
//
//             string[] assetBundleNames = assetBundleName ?? AssetDatabase.GetAllAssetBundleNames();
//             
//             foreach (string name in assetBundleNames)
//             {
//                 string[] depends = AssetDatabase.GetAssetBundleDependencies(name, false);
//
//                 int index = assetDataConfig.AddAssetDependence(name, depends, out AssetGroup @group);
//                 if (index < 0)
//                 {
//                     continue;
//                 }
//             
//                 string[] assets = AssetDatabase.GetAssetPathsFromAssetBundle(name);
//                 foreach (string asset in assets)
//                 {
//                     Type type = AssetDatabase.GetMainAssetTypeAtPath(asset);
//             
//                     short code = type.ToCode();
//
//                     string fileName = Path.GetFileName(asset);
//                     
//                     @group.AddAssetInfo(asset.EndsWith(".unity")
//                         ? new AssetInfo(fileName, name, index,  ResourceLoadType.Scene,code)
//                         : new AssetInfo(fileName, name, index,  ResourceLoadType.Asset,code));
//                 }
//             }
// #endif
//         }
    }
}