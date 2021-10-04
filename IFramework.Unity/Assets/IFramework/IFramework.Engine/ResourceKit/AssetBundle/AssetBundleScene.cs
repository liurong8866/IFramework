using IFramework.Core;

namespace IFramework.Engine
{
    /// <summary>
    /// 打包成AssetBundle资源的场景资源管理类
    /// </summary>
    public class AssetBundleScene : AssetResource
    {
        public static AssetBundleScene Allocate(string name)
        {
            AssetBundleScene res = ObjectPool<AssetBundleScene>.Instance.Allocate();

            if (res != null) {
                res.AssetName = name;
                res.InitAssetBundleName();
            }
            return res;
        }

        public AssetBundleScene() { }

        public AssetBundleScene(string assetName) : base(assetName) { }

        public override bool Load()
        {
            if (!IsLoadable) return false;

            // 如果配置文件没有对应的Asset，则退出
            if (assetBundleNameConfig.IsNullOrEmpty()) return false;

            ResourceSearcher searcher = ResourceSearcher.Allocate(assetBundleNameConfig);
            AssetBundleResource resource = ResourceManager.Instance.GetResource<AssetBundleResource>(searcher);

            if (resource == null || resource.AssetBundle == null) {
                if (Platform.IsSimulation) {
                    Log.Warning("AssetBundle资源加载失败，模拟模式不支持动态加载场景: " + resource);
                }
                else {
                    Log.Error("AssetBundle资源加载失败: " + resource);
                }
                return false;
            }
            State = ResourceState.Ready;
            return true;
        }

        public override void LoadASync()
        {
            Load();
        }

        public override void Recycle()
        {
            ObjectPool<AssetBundleScene>.Instance.Recycle(this);
        }
    }
}
