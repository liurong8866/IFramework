using System;
using UnityEngine;

namespace IFramework.Core
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
            if (self) {
                self.SetActive(true);
            }
            return self;
        }

        public static GameObject Hide(this GameObject self)
        {
            if (self) {
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
            if (self && self.gameObject) {
                self.gameObject.DestroySelf();
            }
        }

        public static void DestroyGameObjectImmediate<T>(this T self) where T : Component
        {
            if (self && self.gameObject) {
                self.gameObject.DestroySelfImmediate();
            }
        }

        public static T DestroyGameObjectDelay<T>(this T self, float delay) where T : Component
        {
            if (self && self.gameObject) {
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
            T component = self.gameObject.GetComponent<T>();
            return component ? component : self.gameObject.AddComponent<T>();
        }

        public static T AddComponentSafe<T>(this Component component) where T : Component { return component.gameObject.AddComponentSafe<T>(); }

        public static Component AddComponentSafe(this GameObject self, Type type)
        {
            Component component = self.gameObject.GetComponent(type);
            return component ? component : self.gameObject.AddComponent(type);
        }
    }
}
