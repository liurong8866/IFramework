using System.Collections;
using System.Reflection;
using IFramework.Engine;
using UnityEngine;
using IFramework.Core;

namespace IFramework.Test.ResourceKit {
    public class ResourceTest : MonoBehaviour {

        private IResource resource;

        private void Start() {
            // ResourceManager.Init();
            resource = Resource.Allocate(ResourcesUrlType.RESOURCES + "Test");

            // resource.LoadSync();
            resource.LoadASync();
            StartCoroutine(MyMethod());
        }

        private IEnumerator MyMethod() {
            Debug.Log("Before Waiting 2 seconds");
            yield return new WaitForSeconds(2);
            resource.Asset.Instantiate();
        }

    }
}
