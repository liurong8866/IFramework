using System;
using IFramework.Core;
using IFramework.Engine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class NetImageExample : MonoBehaviour {

    private VideoPlayer videoPlayer;
    ResourceLoader loader = new ResourceLoader();

    private void Awake() {
        videoPlayer = gameObject.GetComponent<VideoPlayer>();
    }

    void Start() {
        Image image = transform.Find("Image").GetComponent<Image>();
        loader.AddToLoad<Texture2D>(
            ResourcesUrlType.IMAGE + "https://img.3dmgame.com/uploads/images/news/20210929/1632876123_323945.jpg",
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
