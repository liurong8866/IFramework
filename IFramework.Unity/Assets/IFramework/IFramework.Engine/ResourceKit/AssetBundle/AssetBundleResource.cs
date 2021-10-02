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
using System.Collections.Generic;
using System.Linq;
using IFramework.Core;
using UnityEngine;
using UnityEngine.Networking;

namespace IFramework.Engine
{
    /// <summary>
    /// AssetBundle资源管理类
    /// </summary>
    public class AssetBundleResource : AbstractResource
    {
        private bool unloadFlag = true;

        private string[] dependResources;

        public AssetBundle AssetBundle {
            get => asset as AssetBundle;
            set => asset = value;
        }

        /// <summary>
        /// 分配实例
        /// </summary>
        public static AssetBundleResource Allocate(string assetName)
        {
            AssetBundleResource resource = ObjectPool<AssetBundleResource>.Instance.Allocate();
            resource.AssetName = assetName;
            resource.AssetType = typeof(AssetBundle);
            resource.InitAssetBundleName();
            return resource;
        }

        /// <summary>
        /// 初始化AssetBundleName
        /// </summary>
        private void InitAssetBundleName()
        {
            dependResources = AssetBundleConfig.ConfigFile.GetAllDependenciesByUrl(AssetName);
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        public override bool Load()
        {
            if (!IsLoadable) return false;

            State = ResourceState.Loading;

            // 如果不是模拟模式
            if (!Platform.IsSimulation) {
                string url = Platform.GetUrlByAssetBundleName(assetName);

                // 加载AssetBundle资源
                AssetBundle = AssetBundle.LoadFromFile(url);
                unloadFlag = true;

                if (AssetBundle == null) {
                    Log.Error("AssetBundle资源加载失败: " + assetName);
                    OnResourceLoadFailed();
                    return false;
                }
            }
            State = ResourceState.Ready;
            return true;
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public override void LoadASync()
        {
            if (!IsLoadable) return;

            State = ResourceState.Loading;
            ResourceManager.Instance.AddResourceLoadTask(this);
        }

        /// <summary>
        /// 异步加载
        /// </summary>
        public override IEnumerator LoadAsync(Action callback)
        {
            // 如果没有等待加载的资源，则退出
            if (Counter <= 0) {
                OnResourceLoadFailed();
                callback();
                yield break;
            }

            if (Platform.IsSimulation) {
                yield return null;
            }
            else {
                string url = Platform.GetUrlByAssetBundleName(assetName);

                if (Application.platform == RuntimePlatform.WebGLPlayer) {
                    // 加载AssetBundle资源
                    using UnityWebRequest unityWebRequest = UnityWebRequestAssetBundle.GetAssetBundle(url);
                    // 发起请求
                    UnityWebRequestAsyncOperation request = unityWebRequest.SendWebRequest();
                    // 返回并等待结果
                    yield return request;

                    // 如果处理结果完成
                    if (!request.isDone) {
                        Log.Error("AssetBundle加载失败: " + assetName);
                        OnResourceLoadFailed();
                        callback();
                        yield break;
                    }
                    AssetBundle = DownloadHandlerAssetBundle.GetContent(unityWebRequest);
                }
                else {
                    // 从文件中异步加载
                    AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(url);
                    // 返回并等待结果
                    yield return request;

                    // 如果处理结果完成
                    if (!request.isDone) {
                        Log.Error("AssetBundle加载失败: " + assetName);
                        OnResourceLoadFailed();
                        callback();
                        yield break;
                    }
                    AssetBundle = request.assetBundle;
                }
            }
            State = ResourceState.Ready;
            callback();
        }

        /// <summary>
        /// 获取依赖的资源
        /// </summary>
        public override List<string> GetDependResourceList()
        {
            return dependResources?.ToList();
        }

        /// <summary>
        /// 卸载图片资源
        /// </summary>
        public override bool UnloadImage {
            get => unloadFlag;
            set {
                if (AssetBundle != null) {
                    unloadFlag = value;
                }
            }
        }

        public override void Recycle()
        {
            ObjectPool<AssetBundleResource>.Instance.Recycle(this);
        }

        public override void OnRecycled()
        {
            base.OnRecycled();
            unloadFlag = true;
            dependResources = null;
        }

        protected override void OnReleaseResource()
        {
            if (AssetBundle != null) {
                AssetBundle.Unload(unloadFlag);
                AssetBundle = null;
            }
        }
    }
}
