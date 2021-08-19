using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

namespace IFramework.Test.AssetResourceKit
{
    public class ResourceLoaderTest : MonoBehaviour
    {
        ResourceLoader mResLoader = ResourceLoader.Allocate();
        private void Start()
        {
             
            mResLoader.Load<GameObject>("resources://Jin")
                .Instantiate()
                .Name("这是使用 Resource 加载的对象");
            
            mResLoader.Load<GameObject>("AssetObj")
                .Instantiate()
                .Name("这是使用通过 AssetName 加载的对象");
            
            
            mResLoader.Load<GameObject>("AssetObj", "assetobj-prefab")
                .Instantiate()
                .Name("这是使用通过 AssetName + AssetBundle 加载的对象");

        }

        
        private void OnDestroy()
        {
            mResLoader.Recycle();
            mResLoader = null;
        }
    }
}

