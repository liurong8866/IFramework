using System;
using System.Collections;
using System.Collections.Generic;
using IFramework.Engine;
using UnityEngine;
using IFramework.Core;

namespace IFramework.Test.ResourceKit
{

    public class ResourceTest : MonoBehaviour
    {
        private IResource resource;
        
        private void Start()
        {
            // ResourceManager.Init();

            resource = Resource.Allocate("Resources/Test", ResourcesUrlType.Folder);

            // resource.LoadSync();

            resource.LoadASync();

            StartCoroutine( MyMethod());


        }
        
        IEnumerator MyMethod() {
            Debug.Log("Before Waiting 2 seconds");
            yield return new WaitForSeconds(2);
            resource.Asset.Instantiate();
        }
    }
}