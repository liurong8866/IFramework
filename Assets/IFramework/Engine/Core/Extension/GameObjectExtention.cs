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
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace IFramework.Engine
{
    public static class GameObjectExtention
    {
        public static void Example()
        {
            var gameObject = new GameObject();
            var transform = gameObject.transform;
            var selfScript = gameObject.AddComponent<MonoBehaviour>();
            var boxCollider = gameObject.AddComponent<BoxCollider>();

            gameObject.Show(); // gameObject.SetActive(true)
            selfScript.Show(); // this.gameObject.SetActive(true)
            boxCollider.Show(); // boxCollider.gameObject.SetActive(true)
            gameObject.transform.Show(); // transform.gameObject.SetActive(true)

            gameObject.Hide(); // gameObject.SetActive(false)
            selfScript.Hide(); // this.gameObject.SetActive(false)
            boxCollider.Hide(); // boxCollider.gameObject.SetActive(false)
            transform.Hide(); // transform.gameObject.SetActive(false)

            selfScript.DestroyGameObject();
            boxCollider.DestroyGameObject();
            transform.DestroyGameObject();

            selfScript.DestroyGameObjGracefully();
            boxCollider.DestroyGameObjGracefully();
            transform.DestroyGameObjGracefully();

            selfScript.DestroyGameObjAfterDelay(1.0f);
            boxCollider.DestroyGameObjAfterDelay(1.0f);
            transform.DestroyGameObjAfterDelay(1.0f);

            selfScript.DestroyGameObjAfterDelayGracefully(1.0f);
            boxCollider.DestroyGameObjAfterDelayGracefully(1.0f);
            transform.DestroyGameObjAfterDelayGracefully(1.0f);

            gameObject.Layer(0);
            selfScript.Layer(0);
            boxCollider.Layer(0);
            transform.Layer(0);

            gameObject.Layer("Default");
            selfScript.Layer("Default");
            boxCollider.Layer("Default");
            transform.Layer("Default");
        }
        
        #region Show/Hide

        public static GameObject Show(this GameObject self)
        {
            self.SetActive(true);
            return self;
        }

        public static T Show<T>(this T self) where T : Component
        {
            self.gameObject.Show();
            return self;
        }
        
        public static GameObject Hide(this GameObject self)
        {
            self.SetActive(false);
            return self;
        }
        
        public static T Hide<T>(this T self) where T : Component
        {
            self.gameObject.Hide();
            return self;
        }
        
        public static T Enable<T>(this T selfBehaviour) where T : Behaviour
        {
            selfBehaviour.enabled = true;
            return selfBehaviour;
        }

        public static T Disable<T>(this T selfBehaviour) where T : Behaviour
        {
            selfBehaviour.enabled = false;
            return selfBehaviour;
        }
        
        #endregion

        #region Destroy

        public static void DestroyGameObject<T>(this T self) where T : Component
        {
            self.gameObject.DestroySelf();
        }
        
        public static void DestroyGameObjGracefully<T>(this T selfBehaviour) where T : Component
        {
            if (selfBehaviour && selfBehaviour.gameObject)
            {
                selfBehaviour.gameObject.DestroySelfGracefully();
            }
        }

        public static T DestroyGameObjAfterDelay<T>(this T selfBehaviour, float delay) where T : Component
        {
            selfBehaviour.gameObject.DestroySelfAfterDelay(delay);
            return selfBehaviour;
        }

        public static T DestroyGameObjAfterDelayGracefully<T>(this T selfBehaviour, float delay) where T : Component
        {
            if (selfBehaviour && selfBehaviour.gameObject)
            {
                selfBehaviour.gameObject.DestroySelfAfterDelay(delay);
            }

            return selfBehaviour;
        }

        #endregion

        #region Layer
        
        public static GameObject Layer(this GameObject selfObj, int layer)
        {
            selfObj.layer = layer;
            return selfObj;
        }

        public static T Layer<T>(this T selfComponent, int layer) where T : Component
        {
            selfComponent.gameObject.layer = layer;
            return selfComponent;
        }

        public static GameObject Layer(this GameObject selfObj, string layerName)
        {
            selfObj.layer = LayerMask.NameToLayer(layerName);
            return selfObj;
        }

        public static T Layer<T>(this T selfComponent, string layerName) where T : Component
        {
            selfComponent.gameObject.layer = LayerMask.NameToLayer(layerName);
            return selfComponent;
        }
        
        #endregion

        #region Component

        
        public static T GetOrAddComponent<T>(this GameObject selfComponent) where T : Component
        {
            var comp = selfComponent.gameObject.GetComponent<T>();
            return comp ? comp : selfComponent.gameObject.AddComponent<T>();
        }

        public static T GetOrAddComponent<T>(this Component component) where T : Component
        {
            return component.gameObject.GetOrAddComponent<T>();
        }

        public static Component GetOrAddComponent(this GameObject selfComponent, Type type)
        {
            var comp = selfComponent.gameObject.GetComponent(type);
            return comp ? comp : selfComponent.gameObject.AddComponent(type);
        }

        #endregion

        #region Camera

        // var screenshotTexture2D = Camera.main.CaptureCamera(new Rect(0, 0, Screen.width, Screen.height));
        // Log.I(screenshotTexture2D.width);
        public static Texture2D CaptureCamera(this Camera camera, Rect rect)
        {
            var renderTexture = new RenderTexture(Screen.width, Screen.height, 0);
            camera.targetTexture = renderTexture;
            camera.Render();

            RenderTexture.active = renderTexture;

            var screenShot = new Texture2D((int) rect.width, (int) rect.height, TextureFormat.RGB24, false);
            screenShot.ReadPixels(rect, 0, 0);
            screenShot.Apply();

            camera.targetTexture = null;
            RenderTexture.active = null;
            Object.Destroy(renderTexture);

            return screenShot;
        }

        #endregion

        #region Color

        // private static void Example()
        // {
        //     var color = "#C5563CFF".HtmlStringToColor();
        //     Log.I(color);
        //     
        //     var gameObject = new GameObject();
        //     var image = gameObject.AddComponent<Image>();
        //     var rawImage = gameObject.AddComponent<RawImage>();
        //
        //     // image.color = new Color(image.color.r,image.color.g,image.color.b,1.0f);
        //     image.ColorAlpha(1.0f);
        //     rawImage.ColorAlpha(1.0f);
        // }
        
        public static Color HtmlStringToColor(this string htmlString)
        {
            Color retColor;
            var parseSucceed = ColorUtility.TryParseHtmlString(htmlString, out retColor);
            return parseSucceed ? retColor : Color.black;
        }
        
        public static T ColorAlpha<T>(this T selfGraphic, float alpha) where T : Graphic
        {
            var color = selfGraphic.color;
            color.a = alpha;
            selfGraphic.color = color;
            return selfGraphic;
        }

        #endregion
    }
}