using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

namespace QFramework.Example
{
    public class BasicUsageTest : MonoBehaviour
    {
        private ResourceLoader mResLoader = ResourceLoader.Allocate();

        private void Start()
        {
            mResLoader.Load<GameObject>(ResourcesUrlType.RESOURCES + "GameObject").Instantiate().Name("这是使用 ResKit 加载的对象");
            mResLoader.Load<GameObject>(AssetsName.Pack1.ASSETOBJ).Instantiate().Name("这是使用通过 AssetName  加载的对象");
            mResLoader.Load<GameObject>("AssetObj", "pack1").Instantiate().Name("这是使用通过 AssetName  和 AssetBundle-pack1  加载的对象");
            mResLoader.Load<GameObject>("AssetObj", "pack2").Instantiate().Name("这是使用通过 AssetName  和 AssetBundle-pack2 加载的对象");
        }

        private void OnDestroy()
        {
            mResLoader.Recycle();
            mResLoader = null;
        }
    }
}
