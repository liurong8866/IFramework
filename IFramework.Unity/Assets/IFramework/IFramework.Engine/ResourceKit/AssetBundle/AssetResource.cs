using System;
using System.Collections;
using System.Collections.Generic;
using IFramework.Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IFramework.Engine
{
    /// <summary>
    /// AssetBundle包中的Asset资源管理类
    /// </summary>
    public class AssetResource : AbstractResource
    {
        protected string assetBundleNameConfig;

        /// <summary>
        /// 所属AssetBundle包名称
        /// </summary>
        public override string AssetBundleName { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public AssetResource() { }

        public AssetResource(string assetName) : base(assetName) { }

        /// <summary>
        /// 分配实例
        /// </summary>
        public static AssetResource Allocate(string assetName, string assetBundleName = null, Type assetType = null)
        {
            AssetResource resource = ObjectPool<AssetResource>.Instance.Allocate();

            if (resource != null) {
                resource.AssetName = assetName;
                resource.AssetBundleName = assetBundleName;
                resource.AssetType = assetType;
                resource.InitAssetBundleName();
            }
            return resource;
        }

        /// <summary>
        /// 初始化AssetBundleName
        /// </summary>
        protected void InitAssetBundleName()
        {
            assetBundleNameConfig = null;

            // 在config文件中查找资源
            using ResourceSearcher searcher = ResourceSearcher.Allocate(AssetName, AssetBundleName, AssetType);
            AssetInfo config = AssetBundleConfig.ConfigFile.GetAssetInfo(searcher);

            if (config == null) {
                Log.Error("未找到Asset的AssetInfo：{0}", assetName);
                return;
            }

            // 如果找到，则使用config的资源
            assetBundleNameConfig = config.AssetBundleName;

            if (assetBundleNameConfig.IsNullOrEmpty()) {
                Log.Error("未在配置文件中找到AssetBundle：{0}", config.AssetBundleIndex, AssetBundleName);
            }
        }

        /// <summary>
        /// 同步加载资源
        /// </summary>
        public override bool Load()
        {
            if (!IsLoadable || assetBundleNameConfig.IsNullOrEmpty()) return false;

            Object obj;

            // 如果是模拟模式，并且不是包信息资源
            if (Platform.IsSimulation && !assetName.Equals("assetbundlemanifest")) {
                using ResourceSearcher searcher = ResourceSearcher.Allocate(assetBundleNameConfig, null, typeof(AssetBundle));
                AssetBundleResource resource = ResourceManager.Instance.GetResource<AssetBundleResource>(searcher, true);
                // 根据包名+资源名获取资源路径
                string[] assetPaths = Environment.Instance.GetAssetPathsFromAssetBundleAndAssetName(resource.AssetName, assetName);

                if (assetPaths.IsNullOrEmpty()) {
                    Log.Error("AssetBundle资源加载失败: " + assetName);
                    OnResourceLoadFailed();
                    return false;
                }
                // 记录依赖资源
                HoldDependResource();
                state = ResourceState.Loading;
                obj = AssetType != null ? Environment.Instance.LoadAssetAtPath(assetPaths[0], AssetType) : Environment.Instance.LoadAssetAtPath<Object>(assetPaths[0]);
            }
            else {
                using ResourceSearcher searcher = ResourceSearcher.Allocate(assetBundleNameConfig, null, typeof(AssetBundle));
                AssetBundleResource resource = ResourceManager.Instance.GetResource<AssetBundleResource>(searcher);

                if (resource == null || !resource.AssetBundle) {
                    Log.Error("加载资源失败，未能找到AssetBundle: " + assetBundleNameConfig);
                    OnResourceLoadFailed();
                    return false;
                }
                // 记录依赖资源
                HoldDependResource();
                State = ResourceState.Loading;
                obj = AssetType != null ? resource.AssetBundle.LoadAsset(assetName, AssetType) : resource.AssetBundle.LoadAsset(assetName);
            }
            UnHoldDependResource();

            if (obj == null) {
                Log.Error("加载资源失败: {0} : {1} : {2}" + assetName, AssetType, assetBundleNameConfig);
                OnResourceLoadFailed();
                return false;
            }
            asset = obj;
            State = ResourceState.Ready;
            return true;
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public override void LoadASync()
        {
            if (!IsLoadable) return;

            // TODO 注释后支持直接AssetName引用，原代码必须含有AssetBundleName
            // if (AssetBundleName.IsNullOrEmpty()) return;
            State = ResourceState.Loading;
            ResourceManager.Instance.AddResourceLoadTask(this);
        }

        /// <summary>
        /// 异步加载资源
        /// </summary>
        public override IEnumerator LoadAsync(Action callback)
        {
            // 如果没有等待加载的资源，则退出
            if (Counter <= 0) {
                OnResourceLoadFailed();
                callback();
                yield break;
            }

            // 查询器
            using ResourceSearcher searcher = ResourceSearcher.Allocate(assetBundleNameConfig, null, typeof(AssetBundle));

            // 获取资源
            AssetBundleResource resource = ResourceManager.Instance.GetResource<AssetBundleResource>(searcher, true);

            // 如果是模拟模式，并且不是包信息资源
            if (Platform.IsSimulation && !assetName.Equals("assetbundlemanifest")) {
                string[] assetPaths = Environment.Instance.GetAssetPathsFromAssetBundleAndAssetName(resource.AssetName, assetName);

                if (assetPaths.IsNullOrEmpty()) {
                    Log.Error("加载资源失败: " + assetName);
                    OnResourceLoadFailed();
                    callback();
                    yield break;
                }

                // 记录依赖资源
                HoldDependResource();
                State = ResourceState.Loading;
                yield return new WaitForEndOfFrame();

                UnHoldDependResource();

                if (AssetType != null) {
                    asset = Environment.Instance.LoadAssetAtPath(assetPaths[0], AssetType);
                }
                else {
                    asset = Environment.Instance.LoadAssetAtPath<Object>(assetPaths[0]);
                }
            }
            else {
                // 等待帧结束，目的是解决AssetBundle不能为空到问题
                // yield return new WaitForEndOfFrame();

                if (resource == null || resource.AssetBundle == null) {
                    Log.Error("加载资源失败，未能找到AssetBundleImage: " + assetBundleNameConfig);
                    OnResourceLoadFailed();
                    callback();
                    yield break;
                }

                // 记录依赖资源
                HoldDependResource();
                State = ResourceState.Loading;
                AssetBundleRequest request;

                if (AssetType != null) {
                    request = resource.AssetBundle.LoadAssetAsync(assetName, AssetType);
                    yield return request;
                }
                else {
                    request = resource.AssetBundle.LoadAssetAsync(assetName);
                    yield return request;
                }
                UnHoldDependResource();

                if (request == null) {
                    Log.Error("加载资源失败: " + assetName);
                    OnResourceLoadFailed();
                    callback();
                    yield break;
                }
                asset = request.asset;
            }
            State = ResourceState.Ready;
            // 回调方法
            callback();
        }

        /// <summary>
        /// 获取资源依赖
        /// </summary>
        public override List<string> GetDependResourceList()
        {
            return assetBundleNameConfig != null ? new List<string> { assetBundleNameConfig } : null;
        }

        public override void Recycle()
        {
            ObjectPool<AssetResource>.Instance.Recycle(this);
        }

        public override void OnRecycled()
        {
            assetBundleNameConfig = null;
        }

        public override string ToString()
        {
            return $"Type:Asset\t {base.ToString()}\t FromAssetBundle:";
        }
    }
}
