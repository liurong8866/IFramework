using IFramework.Core;
using IFramework.Engine;
using UnityEngine;
using UnityEngine.UI;

namespace IFramework.Test.AssetResourceKit
{
    public class AssetBundleResExample : MonoBehaviour
    {
        private ResourceLoader loader;

        private void Start()
        {
            loader = ResourceLoader.Allocate();
            Image image = transform.Find("Image").GetComponent<Image>();
            RawImage rawImage = transform.Find("RawImage").GetComponent<RawImage>();
            RawImage rawImage2 = transform.Find("RawImage2").GetComponent<RawImage>();
            RawImage rawImage3 = transform.Find("RawImage3").GetComponent<RawImage>();
            RawImage rawImage4 = transform.Find("RawImage4").GetComponent<RawImage>();

            // Resource
            image.sprite = loader.LoadSprite(ResourcesUrlType.RESOURCES + "sprite/sword");
            rawImage.texture = loader.Load(ResourcesUrlType.RESOURCES + "sprite/CharCommunity_001") as Texture2D;
            rawImage2.texture = loader.Load<Texture2D>(ResourcesUrlType.RESOURCES + "sprite/CharCommunity_002");
            loader.AddToLoad(ResourcesUrlType.RESOURCES + "sprite/CharCommunity_003", (result, res) => { result.iif(() => rawImage3.texture = res.Asset as Texture2D); }).AddToLoad(ResourcesUrlType.RESOURCES + "sprite/CharCommunity_004", (result, res) => { result.iif(() => rawImage4.texture = res.Asset as Texture2D); }).LoadAsync();

            // AssetBundle
            // image.sprite = loader.LoadSprite("sword");
            // rawImage.texture = loader.Load("CharCommunity_001") as Texture2D;
            // rawImage2.texture = loader.Load<Texture2D>("CharCommunity_002");
            // rawImage3.texture = loader.Load<Texture2D>("CharCommunity_003");
            // rawImage4.texture = loader.Load<Texture2D>("CharCommunity_004");

            // AssetBundle异步加载
            // loader.AddToLoad("sword", (result, res) => {
            //     result.iif(() => {
            //         Texture2D texture = res.Asset as Texture2D;
            //         
            //         // 创建Sprite
            //         Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f);
            //         image.sprite =  sprite;
            //     });
            // });
            // loader.AddToLoad("CharCommunity_001", (result, res) => { result.iif(() => rawImage.texture = res.Asset as Texture2D); });
            // loader.AddToLoad("CharCommunity_002", (result, res) => { result.iif(() => rawImage2.texture = res.Asset as Texture2D); });
            // loader.AddToLoad("CharCommunity_003", (result, res) => { result.iif(() => rawImage3.texture = res.Asset as Texture2D); });
            // loader.AddToLoad("CharCommunity_004", (result, res) => { result.iif(() => rawImage4.texture = res.Asset as Texture2D); });
            //
            // loader.LoadAsync(
            //     ()=>{Log.Info("加载完毕");}
            // );

            // AssetBundle异步加载
            // loader.AddToLoad("CharCommunity_001", "sprite", (result, res) => { result.iif(() => rawImage.texture = res.Asset as Texture2D); });
            // loader.AddToLoad("CharCommunity_002", "sprite", (result, res) => { result.iif(() => rawImage2.texture = res.Asset as Texture2D); });
            // loader.AddToLoad("CharCommunity_003", "sprite", (result, res) => { result.iif(() => rawImage3.texture = res.Asset as Texture2D); });
            // loader.AddToLoad("CharCommunity_004", "sprite", (result, res) => { result.iif(() => rawImage4.texture = res.Asset as Texture2D); });
            //
            // loader.LoadAsync(
            //     ()=>{Log.Info("加载完毕");}
            // );
        }

        private void OnDestroy()
        {
            loader.Recycle();
            loader = null;
        }
    }
}
