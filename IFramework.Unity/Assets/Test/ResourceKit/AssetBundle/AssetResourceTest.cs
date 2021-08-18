using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

namespace IFramework.Test.AssetResourceKit
{
    public class AssetResourceTest : MonoBehaviour
    {
        private ResourceLoader mResLoader = ResourceLoader.Allocate();
        // Start is called before the first frame update
        private void Start()
        {
            
            mResLoader.Load<GameObject>("AssetObj")
                .Instantiate()
                .Name("这是使用通过 AssetName 加载的对象");
            
            // AssetResource resource = AssetResource.Allocate("Lili.pre","Lili", typeof(GameObject));
            //
            // resource.Load();
            //
            // resource.Asset.Instantiate().Name("hahah");

        }

        // Update is called once per frame
        private void Update()
        {
        
        }
    }
}

