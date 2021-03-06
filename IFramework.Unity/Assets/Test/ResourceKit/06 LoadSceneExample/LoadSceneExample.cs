using IFramework.Engine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IFramework.Test.AssetResourceKit
{
    public class LoadSceneExample : MonoBehaviour
    {
        private ResourceLoader loader;

        private void Start()
        {
            loader = new ResourceLoader();

            // 同步
            // loader.Load("Chapter_01");
            // SceneManager.LoadScene("Chapter_01");

            // 异步
            // loader.AddToLoad("Chapter_01");
            // loader.LoadAsync(() => {SceneManager.LoadScene("Chapter_01");  });
            
            loader.LoadAsync("Chapter_01", (result, resource)=>{
                if (result) {
                    SceneManager.LoadScene("Chapter_01"); 
                }});
            
            // SceneManager.LoadScene("Chapter_01"); 
        }
    }
}
