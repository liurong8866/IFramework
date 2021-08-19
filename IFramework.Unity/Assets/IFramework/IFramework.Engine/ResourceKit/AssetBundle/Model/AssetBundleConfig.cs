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

using System.Collections;
using IFramework.Core;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.Networking;

namespace IFramework.Engine
{
    /// <summary>
    /// 资源数据管理类
    /// </summary>
    public sealed class AssetBundleConfig
    {
        private AssetTable assetTable;

        public List<AssetBundleInfo> AssetBundleList { get; } = new List<AssetBundleInfo>();

        public void Reset()
        {
            foreach (AssetBundleInfo assetGroup in AssetBundleList)
            {
                assetGroup.Reset();
            }
            AssetBundleList.Clear();
            
            assetTable?.Dispose();

            assetTable = null;
        }
        
        /// <summary>
        /// 添加AssetBundle资源名
        /// </summary>
        public int AddAssetBundleInfo(string assetBundleName, string[] depends, out AssetBundleInfo assetBundleInfo)
        {
            assetBundleInfo = null;

            if (assetBundleName.IsNullOrEmpty()) return -1;
 
            // 如果是文件夹类型，去掉 /或\
            string key = assetBundleName.TrimEnd('/').TrimEnd('\\');

            if (key.IsNullOrEmpty()) return -1;
            
            // 根据Key获取AssetBundle
            assetBundleInfo = AssetBundleList.FirstOrDefault(item => item.Key.Equals(key));
            
            // 如果没有，添加
            if (assetBundleInfo == null)
            {
                assetBundleInfo = new AssetBundleInfo(key);
                AssetBundleList.Add(assetBundleInfo);
            }

            // 添加资源关系信息
            return assetBundleInfo.AddAssetDependence(assetBundleName, depends);
        }
        
        /// <summary>
        /// 通过URL找到所有依赖资源
        /// </summary>
        public string[] GetAllDependenciesByUrl(string url)
        {
            string assetBundleName = Platform.AssetBundleNameByUrl(url);

            string[] depends = null;
            
            foreach (AssetBundleInfo assetGroup in AssetBundleList)
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
                
                for (int i = AssetBundleList.Count - 1; i >= 0; --i)
                {
                    foreach (AssetInfo assetInfo in AssetBundleList[i].AssetInfos)
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
            AssetBundleDatas data = new AssetBundleDatas
            {
                AssetBundles = new AssetBundleData[AssetBundleList.Count]
            };

            for (int i = 0; i < AssetBundleList.Count; i++)
            {
                data.AssetBundles[i] = AssetBundleList[i].GetSerializeData();
            }
            
            SerializeUtils.SerializeToFile(path, data);
        }
        
        /// <summary>
        /// 从配置文件加载关系
        /// </summary>
        public void LoadFromFile(string path)
        {
            AssetBundleDatas bundles = SerializeUtils.DeserializeFromFile<AssetBundleDatas>(path);
            
            SetSerializeData(bundles);
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
            
            AssetBundleDatas bundles = SerializeUtils.DeserializeFromFile<AssetBundleDatas>(stream);
            
            if(bundles == null) yield break;
                
            SetSerializeData(bundles);
        }
        
        /// <summary>
        /// 加载后设置序列化数据
        /// </summary>
        private void SetSerializeData(AssetBundleDatas data)
        {
            if (data?.AssetBundles == null) return;

            // 添加AssetBundleList缓存
            for (int i = data.AssetBundles.Length - 1; i >= 0; i--)
            {
                AssetBundleList.Add(new AssetBundleInfo(data.AssetBundles[i]));
            }
            
            assetTable ??= new AssetTable();
            
            // 循环最外层AssetBundleDatas
            foreach (AssetBundleData assetBundleData in data.AssetBundles)
            {
                // 循环第二层AssetBundleData
                foreach (AssetInfo assetInfo in assetBundleData.AssetInfos)
                {
                    // 添加资源缓存
                    assetTable.Add(assetInfo.AssetName, assetInfo);
                }
            }
        }
        
        /// <summary>
        /// 获取自定义的 资源信息
        /// </summary>
        /// <returns></returns>
        public static AssetBundleConfig ConfigFile
        {
            get => configFile??= new AssetBundleConfig();
            set => configFile = value;
        }
        private static AssetBundleConfig configFile = null;
        
    }
}