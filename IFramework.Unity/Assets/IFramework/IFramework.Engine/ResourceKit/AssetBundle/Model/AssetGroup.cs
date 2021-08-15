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
using IFramework.Core;

namespace IFramework.Engine
{
    /// <summary>
    /// Asset资源字典维护表
    /// </summary>
    public class AssetGroup
    {
        private string key;
        
        // 资源依赖关系列表
        private List<AssetDependence> assetDepends;
        // AssetName作为Key的字典
        private Dictionary<string, AssetInfo> assetNameMap;
        // AssetName+AssetBundleName 作为Key的字典
        private Dictionary<string, AssetInfo> assetBundleMap;

        /// <summary>
        /// 构造函数
        /// </summary>
        public AssetGroup(string key)
        {
            this.key = key;
        }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        public AssetGroup(SerializeData data)
        {
            this.key = data.Key;
            SetSerializeData(data);
        }
        
        /// <summary>
        /// 数据重置
        /// </summary>
        public void Reset()
        {
            assetDepends?.Clear();
            assetNameMap?.Clear();
        }

        /// <summary>
        /// 设置序列化数据
        /// </summary>
        private void SetSerializeData(SerializeData data)
        {
            if(data == null) return;
            
            assetDepends = new List<AssetDependence>(data.AssetDependencies);

            if (data.AssetInfos != null)
            {
                foreach (AssetInfo assetInfo in data.AssetInfos)
                {
                    AddAssetInfo(assetInfo);
                }
            }
        }

        /// <summary>
        /// 添加资源到字典表
        /// </summary>
        public bool AddAssetInfo(AssetInfo assetInfo)
        {
            assetNameMap ??= new Dictionary<string, AssetInfo>();
            assetBundleMap ??= new Dictionary<string, AssetInfo>();

            // 添加到AssetInfo字典
            if (assetNameMap.ContainsKey(key))
            {
                using ResourceSearcher searcher = ResourceSearcher.Allocate(assetInfo.AssetName);
                AssetInfo oldInfo = GetAssetInfo(searcher);

                if (oldInfo != null)
                {
                    Log.Warning("资源已存在: {0}\n旧包: {1}, 新包: {2}", 
                        assetInfo.AssetName, 
                        assetDepends[oldInfo.AssetBundleIndex].AssetBundleName, 
                        assetDepends[oldInfo.AssetBundleIndex].AssetBundleName
                    );
                }
            }
            else
            {
                assetNameMap.Add(key, assetInfo);
            }
            
            // 添加到AssetUUID字典
            
            key = assetInfo.UUID;
            
            if (assetBundleMap.ContainsKey(key))
            {
                using ResourceSearcher searcher = ResourceSearcher.Allocate(assetInfo.AssetName, assetInfo.AssetBundleName);
                AssetInfo oldInfo = GetAssetInfo(searcher);

                if (oldInfo != null)
                {
                    Log.Warning("资源已存在: {0}\n旧包: {1}, 新包: {2}", 
                        assetInfo.AssetName, 
                        assetDepends[oldInfo.AssetBundleIndex].AssetBundleName, 
                        assetDepends[oldInfo.AssetBundleIndex].AssetBundleName
                    );
                }
            }
            else
            {
                assetBundleMap.Add(key, assetInfo);
            }

            return true;
        }

        /// <summary>
        /// 从字典表获取资源信息
        /// </summary>
        public AssetInfo GetAssetInfo(ResourceSearcher searcher)
        {
            AssetInfo assetInfo = null;

            if (searcher.AssetBundleName != null)
            {
                assetBundleMap?.TryGetValue(searcher.AssetBundleName + searcher.AssetName, out assetInfo);
            }
            else
            {
                assetNameMap?.TryGetValue(searcher.AssetName, out assetInfo);
            }

            return assetInfo;
        }

        /// <summary>
        /// 添加资源关系信息
        /// </summary>
        public int AddAssetDependence(string assetName, string[] depends)
        {
            if (assetName.IsNullOrEmpty()) return -1;

            assetDepends ??= new List<AssetDependence>();

            ResourceSearcher searcher = ResourceSearcher.Allocate(assetName);

            AssetInfo assetInfo = GetAssetInfo(searcher);

            if (assetInfo != null)
            {
                return assetInfo.AssetBundleIndex;
            }

            assetDepends.Add(new AssetDependence(assetName, depends));

            int index = assetDepends.Count - 1;

            AddAssetInfo(new AssetInfo(assetName, null, index, ResourceLoadType.AssetBundle));

            return index;
        }
        
        /// <summary>
        /// 获取资源包信息
        /// </summary>
        public string GetAssetBundleName(string assetName, int index)
        {
            if (assetDepends.IsNullOrEmpty()) return "";

            if (index > assetDepends.Count) return "";

            if (assetNameMap.ContainsKey(assetName))
            {
                return assetDepends[index].AssetBundleName;
            }

            return "";
        }
        
        /// <summary>
        /// 获取依赖资源
        /// </summary>
        public bool GetAssetBundleDepends(string assetBundleName, out string[] result)
        {
            result = null;
            
            AssetDependence dependence = GetAssetDepend(assetBundleName);

            if (dependence != null)
            {
                result = dependence.Depends;
            }

            return true;
        }

        /// <summary>
        /// 获取依赖资源
        /// </summary>
        public AssetDependence GetAssetDepend(string assetName)
        {
            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetName);
            AssetInfo assetInfo = GetAssetInfo(searcher);

            if (assetInfo == null) return null;
            
            return assetDepends.IsNullOrEmpty() ? null : assetDepends[assetInfo.AssetBundleIndex];
        }
        
        public string Key => key;
        
        public List<AssetDependence> AssetDepends => assetDepends;
        
        public Dictionary<string, AssetInfo> AssetNameDict => assetNameMap;

    }
}