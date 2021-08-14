
using System.Collections;
using IFramework.Engine;
using UnityEngine;
using IFramework.Core;
using UnityEngine.UI;

namespace IFramework.Test.ResourceKit
{

    public class ResourceLoaderTest : MonoBehaviour
    {
        public Image imag;
        
        private void Start()
        {
            ResourceLoader resourceLoader = ResourceLoader.Allocate();

            // Object resource = resourceLoader.Load("Resources://Test");
            // resource.Instantiate().Name("这是Resource.Load加载");
            //
            // Object resource2 = resourceLoader.Load("Resources://Test");
            // resource2.Instantiate().Name("这是Resource加载");
            //
            // Sprite resource3 = resourceLoader.LoadSprite("Resources://icon");
            // imag.sprite = resource3;
            
            resourceLoader.AddToLoad("Resources://Test", (result, resource) =>
            {
                if (result)
                {
                    resource.Asset.Instantiate().Name("我是动态加载的");
                }
            });
            resourceLoader.AddToLoad("Resources://Test", (result, resource) =>
            {
                if (result)
                {
                    resource.Asset.Instantiate().Name("我是动态加载的");
                }
            });
            resourceLoader.AddToLoad("Resources://Test", (result, resource) =>
            {
                if (result)
                {
                    resource.Asset.Instantiate().Name("我是动态加载的");
                }
            });

            Object resource4 = resourceLoader.Load("Resources://Test");
            resource4.Instantiate().Name("这是Resource.Load加载");
        }
        
    }
}