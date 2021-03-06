using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

namespace IFramework.Test.AssetResourceKit
{
    public class ResourceLoaderTest : MonoBehaviour
    {
        private ResourceLoader mResLoader = ResourceLoader.Allocate();

        private void Start()
        {
            mResLoader.Load<GameObject>(ResourcesUrlType.RESOURCES + "Jin").Instantiate().Name("这是使用 Resource 加载的对象");
            mResLoader.Load<GameObject>("AssetObj", "pack1").Instantiate().Name("这是使用通过 AssetName 加载的对象");
            mResLoader.Load<GameObject>("Liliy", "liliy-prefab").Instantiate().Name("这是使用通过 AssetName + AssetBundle 加载的对象Liliy");
            mResLoader.Load<GameObject>("Malong", "malong-prefab").Instantiate().Name("这是使用通过 AssetName + AssetBundle 加载的对象Malong");
        }

        private void OnDestroy()
        {
            mResLoader.Recycle();
            mResLoader = null;
        }
    }
}
