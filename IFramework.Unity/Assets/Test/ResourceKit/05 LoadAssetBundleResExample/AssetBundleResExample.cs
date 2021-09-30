/*****************************************************************************
 * MIT License
 * 
 * Copyright (c) 2021 liurong
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *****************************************************************************/

using System;
using IFramework.Core;
using IFramework.Engine;
using UnityEngine;
using UnityEngine.UI;

namespace Test.ResourceKit._05_LoadAssetBundleResExample {
    public class AssetBundleResExample : MonoBehaviour {

        ResourceLoader loader;

        private void Start() {
            loader = ResourceLoader.Allocate();
            Image image = transform.Find("Image").GetComponent<Image>();
            RawImage rawImage = transform.Find("RawImage").GetComponent<RawImage>();
            RawImage rawImage2 = transform.Find("RawImage2").GetComponent<RawImage>();
            RawImage rawImage3 = transform.Find("RawImage3").GetComponent<RawImage>();
            RawImage rawImage4 = transform.Find("RawImage4").GetComponent<RawImage>();

            // Resource
            // image.sprite = loader.LoadSprite("resources://sprite/sword");
            // rawImage.texture = loader.Load("resources://sprite/CharCommunity_001") as Texture2D;
            // rawImage2.texture = loader.Load<Texture2D>("resources://sprite/CharCommunity_002");
            //
            // loader.AddToLoad("resources://sprite/CharCommunity_003", 
            //         (result, res) => { result.iif(() => rawImage3.texture = res.Asset as Texture2D); })
            //     .AddToLoad("resources://sprite/CharCommunity_004", 
            //         (result, res) => { result.iif(() => rawImage4.texture = res.Asset as Texture2D); })
            //     .LoadAsync();

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
            loader.AddToLoad("CharCommunity_001", "sprite", (result, res) => { result.iif(() => rawImage.texture = res.Asset as Texture2D); });
            loader.AddToLoad("CharCommunity_002", "sprite", (result, res) => { result.iif(() => rawImage2.texture = res.Asset as Texture2D); });
            loader.AddToLoad("CharCommunity_003", "sprite", (result, res) => { result.iif(() => rawImage3.texture = res.Asset as Texture2D); });
            loader.AddToLoad("CharCommunity_004", "sprite", (result, res) => { result.iif(() => rawImage4.texture = res.Asset as Texture2D); });
            
            loader.LoadAsync(
                ()=>{Log.Info("加载完毕");}
            );
        }

        private void OnDestroy() {
            loader.Recycle();
            loader = null;
        }

    }
}
