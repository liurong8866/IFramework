using System;
using IFramework.Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IFramework.Test.Extention
{
    public delegate void Dd();

    public class ExtentionTest : MonoBehaviour
    {
        private void Start()
        {
            
            ObjectExtentionTest();

            // GameObjectExtentionTest();
            // TransformExtentiontest();
        }

        private void ObjectExtentionTest()
        {
            Object gameObject = new GameObject();
            gameObject.As<GameObject>().Instantiate().Name("testlist").DontDestroyOnLoad().transform.SetParent(this.gameObject.transform);
            // .DestroySelf();
            this.gameObject.DontDestroyOnLoad();
            Action action1 = () => { Debug.Log("hello world"); };
            action1.InvokeSafe();
            Action<int> action2 = a => { Debug.Log("hello world"); };
            action2.InvokeSafe(2);
            Action<int, string> action3 = (a, b) => { Debug.Log("hello world"); };
            action3.InvokeSafe(2, "cat");
            Dd dd = () => { Debug.Log("hello world"); };
            dd.InvokeSafe();
            Func<int> func = () => 1;
            func.InvokeSafe();
            Func<string, string> func2 = name => { return name; };
            Log.Info(func2.Invoke("liurong"));
            Func<string, int, string> func3 = (name, age) => { return name + age; };
            Log.Info(func3.Invoke("liurong", 20));
            gameObject.InvokeAction(a => { a.DestroySelf(); });
        }

        private void GameObjectExtentionTest()
        {
            GameObject gameObject = new GameObject();
            gameObject.Name("testObject");
            Transform transform = gameObject.transform;
            AudioSource selfScript = gameObject.AddComponentSafe<AudioSource>();
            BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
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

        private void TransformExtentiontest()
        {
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
