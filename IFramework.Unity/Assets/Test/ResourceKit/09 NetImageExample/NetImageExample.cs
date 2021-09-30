using IFramework.Core;
using IFramework.Engine;
using UnityEngine;
using UnityEngine.UI;

public class NetImageExample : MonoBehaviour {

    ResourceLoader loader = new ResourceLoader();

    void Start() {
        Image image = transform.Find("Image").GetComponent<Image>();
        loader.AddToLoad<Texture2D>(
            ResourcesUrlType.NET_VIDEO + "https://vd3.bdstatic.com/mda-ka5ayxd86t7z2h1r/mda-ka5ayxd86t7z2h1r.mp4?pd=22",
            // ResourcesUrlType.NET_IMAGE + "https://img.3dmgame.com/uploads/images/news/20210929/1632876123_323945.jpg",
            (b, res) => {
                if (b) {
                    var texture = res.Asset as Texture2D;
                    var sprite = texture.CreateSprite();
                    image.sprite = sprite;
                    loader.DestroyOnRecycle(sprite);
                }
            });
        loader.LoadAsync();
    }

}
