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
    /// <summary>
    /// GameObject 扩展方法
    /// </summary>
    public static class GameObjectExtention
    {
        /* Example
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

            selfScript.DestroyGameObjectDelay(1.0f);
            boxCollider.DestroyGameObjectDelay(1.0f);
            transform.DestroySelf();

            gameObject.Layer(0);
            selfScript.Layer(0);
            boxCollider.Layer(0);
            transform.Layer(0);

            gameObject.Layer("Default");
            selfScript.Layer("Default");
            boxCollider.Layer("Default");
            transform.Layer("Default");
        }
        */
        
        /*-----------------------------*/
        /* Show & Hide                 */
        /*-----------------------------*/

        public static GameObject Show(this GameObject self)
        {
            if (self)
            {
                self.SetActive(true);
            }
            return self;
        }

        public static GameObject Hide(this GameObject self)
        {
            if (self)
            {
                self.SetActive(false);
            }
            
            return self;
        }
        
        public static Behaviour Enable(this Behaviour self) 
        {
            self.enabled = true;
            return self;
        }

        public static Behaviour Disable(this Behaviour self)
        {
            self.enabled = false;
            return self;
        }

        public static T Show<T>(this T self) where T : Component
        {
            self.gameObject.Show();
            return self;
        }

        public static T Hide<T>(this T self) where T : Component
        {
            self.gameObject.Hide();
            return self;
        }
        
        public static T Enable<T>(this T self) where T : Behaviour
        {
            self.enabled = true;
            return self;
        }

        public static T Disable<T>(this T self) where T : Behaviour
        {
            self.enabled = false;
            return self;
        }
         
        /*----------------------------*/
        /* Destroy                    */
        /*----------------------------*/
        
        public static void DestroyGameObject<T>(this T self) where T : Component
        {
            if (self && self.gameObject)
            {
                self.gameObject.DestroySelf();
            }
        }
        
        public static void DestroyGameObjectImmediate<T>(this T self) where T : Component
        {
            if (self && self.gameObject)
            {
                self.gameObject.DestroySelfImmediate();
            }
        }

        public static T DestroyGameObjectDelay<T>(this T self, float delay) where T : Component
        {
            if (self && self.gameObject)
            {
                self.gameObject.DestroySelfDelay(delay);
            }

            return self;
        }

        /*----------------------------*/
        /* Layer                      */
        /*----------------------------*/
        
        public static GameObject Layer(this GameObject self, int layer)
        {
            self.layer = layer;
            return self;
        }

        public static GameObject Layer(this GameObject self, string layerName)
        {
            self.layer = LayerMask.NameToLayer(layerName);
            return self;
        }

        public static T Layer<T>(this T self, int layer) where T : Component
        {
            self.gameObject.layer = layer;
            return self;
        }

        public static T Layer<T>(this T self, string layerName) where T : Component
        {
            self.gameObject.layer = LayerMask.NameToLayer(layerName);
            return self;
        }
        
        /*----------------------------*/
        /* Component                  */
        /*----------------------------*/

        public static T AddComponentSafe<T>(this GameObject self) where T : Component
        {
            var component = self.gameObject.GetComponent<T>();
            return component ? component : self.gameObject.AddComponent<T>();
        }

        public static T AddComponentSafe<T>(this Component component) where T : Component
        {
            return component.gameObject.AddComponentSafe<T>();
        }

        public static Component AddComponentSafe(this GameObject self, Type type)
        {
            var component = self.gameObject.GetComponent(type);
            return component ? component : self.gameObject.AddComponent(type);
        }

    }
}