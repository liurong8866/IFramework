using IFramework.Core;
using IFramework.Engine;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

namespace IFramework.Test.AssetResourceKit
{
    public class NetImageExample : MonoBehaviour
    {
        private VideoPlayer videoPlayer;
        private readonly ResourceLoader loader = new ResourceLoader();

        private void Awake()
        {
            videoPlayer = gameObject.GetComponent<VideoPlayer>();
        }

        private void Start()
        {
            Image image = transform.Find("Image").GetComponent<Image>();

            loader.AddToLoad<Texture2D>(ResourcesUrlType.IMAGE + "https://img.3dmgame.com/uploads/images/news/20210929/1632876123_323945.jpg",
                                        (b, res) => {
                                            if (b) {
                                                Texture2D texture = res.Asset as Texture2D;
                                                Sprite sprite = texture.CreateSprite();
                                                image.sprite = sprite;
                                                loader.DestroyOnRecycle(sprite);
                                            }
                                        });
            loader.LoadAsync();
        }
    }
}
