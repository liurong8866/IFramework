using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace IFramework.Core
{
    /// <summary>
    /// Unity Object 扩展方法
    /// </summary>
    public static class ObjectExtention
    {
        /* Example
        public delegate void DD();
        
        public static void Example()
        {
            Object gameObject = new GameObject();
            gameObject
                .As<Transform>()
                .Identity()
                .Name("test")
                .Instantiate()
                .DontDestroyOnLoad()
                .DestroySelf();
        
            Action action1 = () => { Debug.Log("hello world"); };
            action1.InvokeSafe();
            
            Action<int> action2 = (a) => { Debug.Log("hello world"); };
            action2.InvokeSafe<int>(2);
            
            Action<int, string> action3 = (a, b) => { Debug.Log("hello world"); };
            action3.InvokeSafe<int, string>( 2, "cat");
            
            DD dd = () => { Debug.Log("hello world"); };
            dd.InvokeSafe();
            
            Func<int> func = ()=> 1;
            func.InvokeSafe();
        
            gameObject.InvokeAction<Object>( (a)=>
            {
                a.DestroySelf();
            });
        
        }
        */

        /*----------------------------*/
        /* Basic Method               */
        /*----------------------------*/

        // 用于支持链式调用中类型转换
        public static T As<T>(this object self) where T : class
        {
            return self as T;
        }

        public static T Name<T>(this T self, string name) where T : Object
        {
            self.name = name;
            return self;
        }

        public static T Instantiate<T>(this T self) where T : Object
        {
            return Object.Instantiate(self);
        }

        /*----------------------------*/
        /* Invoke                     */
        /*----------------------------*/

        public static void InvokeSafe(this Action action)
        {
            action?.Invoke();
        }

        public static void InvokeSafe<T>(this Action<T> action, T param)
        {
            action?.Invoke(param);
        }

        public static void InvokeSafe<T, K>(this Action<T, K> action, T param1, K param2)
        {
            action?.Invoke(param1, param2);
        }

        public static void InvokeSafe(this Delegate action, params object[] param)
        {
            action?.DynamicInvoke(param);
        }

        public static T InvokeSafe<T>(this Func<T> function)
        {
            return function != null ? function() : default;
        }

        public static TResult InvokeSafe<T, TResult>(this Func<T, TResult> function, T param)
        {
            return function != null ? function.Invoke(param) : default;
        }

        public static TResult InvokeSafe<T, K, TResult>(this Func<T, K, TResult> function, T param1, K param2)
        {
            return function != null ? function.Invoke(param1, param2) : default;
        }

        public static T InvokeAction<T>(this T self, Action<T> action) where T : Object
        {
            action.InvokeSafe(self);
            return self;
        }

        /*----------------------------*/
        /* Destroy                    */
        /*----------------------------*/

        public static void DestroySelf<T>(this T self) where T : Object
        {
            if (self) {
                Object.Destroy(self);
            }
        }

        public static void DestroySelfImmediate<T>(this T self) where T : Object
        {
            if (self) {
                Object.DestroyImmediate(self);
            }
        }

        public static void DestroySelfDelay<T>(this T self, float delay) where T : Object
        {
            if (self) {
                Object.Destroy(self, delay);
            }
        }

        public static T DontDestroyOnLoad<T>(this T self) where T : Object
        {
            if (self) {
                Object.DontDestroyOnLoad(self);
            }
            return self;
        }

        /*----------------------------*/
        /* Json                       */
        /*----------------------------*/

        public static string ToJson<T>(this T self) where T : class
        {
            return JsonUtility.ToJson(self, true);
        }

        public static T FromJson<T>(this string json) where T : class
        {
            return JsonUtility.FromJson<T>(json);
        }
    }
}
