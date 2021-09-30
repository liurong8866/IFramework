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
using Object = UnityEngine.Object;
using IFramework.Core;

namespace IFramework.Test.Extention {
    public delegate void Dd();

    public class ExtentionTest : MonoBehaviour {

        private void Start() {
            ObjectExtentionTest();

            // GameObjectExtentionTest();
            // TransformExtentiontest();
        }

        private void ObjectExtentionTest() {
            Object gameObject = new GameObject();
            gameObject
                .As<GameObject>()
                .Instantiate()
                .Name("testlist")
                .DontDestroyOnLoad()
                .transform.SetParent(this.gameObject.transform);
            // .DestroySelf();
            this.gameObject.DontDestroyOnLoad();
            Action action1 = () => { Debug.Log("hello world"); };
            action1.InvokeSafe();
            Action<int> action2 = (a) => { Debug.Log("hello world"); };
            action2.InvokeSafe<int>(2);
            Action<int, string> action3 = (a, b) => { Debug.Log("hello world"); };
            action3.InvokeSafe<int, string>(2, "cat");
            Dd dd = () => { Debug.Log("hello world"); };
            dd.InvokeSafe();
            Func<int> func = () => 1;
            func.InvokeSafe();
            
            Func<string, string> func2 = (name) => { return name;};
            Log.Info(func2.Invoke("liurong")); 
            
            Func<string, int, string> func3 = (name, age) => { return name + age;};
            Log.Info(func3.Invoke("liurong", 20)); 
            
            gameObject.InvokeAction<Object>((a) => { a.DestroySelf(); });
        }

        private void GameObjectExtentionTest() {
            var gameObject = new GameObject();
            gameObject.Name("testObject");
            var transform = gameObject.transform;
            var selfScript = gameObject.AddComponentSafe<AudioSource>();
            var boxCollider = gameObject.AddComponent<BoxCollider>();
            boxCollider = gameObject.AddComponentSafe<BoxCollider>();
            gameObject.Show(); // gameObject.SetActive(true)
            // selfScript.Show(); // this.gameObject.SetActive(true)
            // boxCollider.Show(); // boxCollider.gameObject.SetActive(true)
            // gameObject.transform.Show(); // transform.gameObject.SetActive(true)
            gameObject.Hide(); // gameObject.SetActive(false)
            // selfScript.Hide(); // this.gameObject.SetActive(false)
            // boxCollider.Hide(); // boxCollider.gameObject.SetActive(false)
            // transform.Hide(); // transform.gameObject.SetActive(false)
            //
            // selfScript.DestroyGameObject();
            // boxCollider.DestroyGameObject();
            // transform.DestroyGameObject();
            // selfScript.DestroySelf();
            // boxCollider.DestroySelf();
            // gameObject.DestroySelf();
            transform.DestroySelf();
            //
            // selfScript.DestroyGameObjectDelay(1.0f);
            // boxCollider.DestroyGameObjectDelay(1.0f);
            // transform.DestroySelf();
            //
            // gameObject.Layer(0);
            // selfScript.Layer(0);
            // boxCollider.Layer(0);
            // transform.Layer(0);
            //
            // gameObject.Layer("Default");
            // selfScript.Layer("Default");
            // boxCollider.Layer("Default");
            // transform.Layer("Default");
        }

        private void TransformExtentiontest() {
            GameObject obj = new GameObject();
            obj.transform
                // .Parent(transform.FindRecursion("AAA"))
                .Name("Hello")
                .LocalIdentity()
                .Identity(transform.FindRecursion("AAA"));
            Debug.Log(obj.transform.Path());
        }

    }
}
