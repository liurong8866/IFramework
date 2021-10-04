using IFramework.Engine;
using UnityEngine;

namespace Test.ResourceKit._03_AssetBundleTest
{
    public class AssetBundleTest : MonoBehaviour
    {
        // private AssetResource assetResource;
        // private AssetBundleResource assetBundleResource;

        private void Start()
        {
            // 脱离了ResourceLoader，不可以直接调用，因为 Asset资源依赖AssetBundle，有依赖关系，单独不可以调用

            // assetBundleResource = AssetBundleResource.Allocate("malong-prefab");
            // assetBundleResource.Load();
            //
            // assetResource = AssetResource.Allocate("Malong");
            // assetResource.Load();
            // assetResource.Asset.Instantiate().Name("我是通过AssetResource 加载的 Malong");

            // assetResource = AssetResource.Allocate("AssetObj");
            // assetResource.Load();
            // assetResource.Asset.Instantiate().Name("我是通过AssetResource 加载的 AssetObj");
            //
            // assetResource = AssetResource.Allocate("AssetObj", "pack1");
            // assetResource.Load();
            // assetResource.Asset.Instantiate().Name("我是通过AssetResource 加载的 pack1");
            //
            // assetResource = AssetResource.Allocate("AssetObj", "pack2");
            // assetResource.Load();
            // assetResource.Asset.Instantiate().Name("我是通过AssetResource 加载的 pack2");
        }

        private void OnDestroy()
        {
            // assetResource.Dispose();
        }
    }
}
