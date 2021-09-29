using System;
using IFramework.Engine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Test.ResourceKit._06_LoadSceneExample
{
    public class LoadSceneExample : MonoBehaviour
    {
        private ResourceLoader loader = null;
        private void Start()
        {
            loader = new ResourceLoader();
            
            loader.Load("Chapter_01");
            SceneManager.LoadScene("Chapter_01");
        }
    }
}