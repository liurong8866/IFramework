
using System.Collections;
using IFramework.Engine;
using UnityEngine;
using IFramework.Core;

namespace IFramework.Test.ResourceKit
{

    public class ResourceLoaderTest : MonoBehaviour
    {
        private void Start()
        {
            ResourceLoader resourceLoader = ResourceLoader.Allocate();

            GameObject resource = resourceLoader.Load("Resources/Test") as GameObject;

            resource.Instantiate();
        }
        
    }
}