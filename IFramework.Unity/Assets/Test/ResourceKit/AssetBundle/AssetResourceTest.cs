using System.Collections;
using System.Collections.Generic;
using IFramework.Engine;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AssetResource resource = AssetResource.Allocate("Lili","Lili", typeof(GameObject));

        resource.Load();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
