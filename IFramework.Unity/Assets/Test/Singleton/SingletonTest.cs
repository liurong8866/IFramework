using IFramework.Core;
using UnityEngine;

namespace IFramework.Test.Singleton
{
    public class SingletonTest : MonoBehaviour
    {
        private void Start()
        {
            SingletonTestDemo a = SingletonTestDemo.Instance;
            SingletonTestDemo b = SingletonTestDemo.Instance;
            Debug.Log(a == b);
            Debug.Log(a.GetHashCode());
            Debug.Log(b.GetHashCode());
            SingletonTestDemo.Instance.Say();
        }
    }

    public class SingletonTestDemo : Singleton<SingletonTestDemo>
    {
        // 私有化构造函数，防止外部New创建
        private SingletonTestDemo() { }

        public void Say()
        {
            Debug.Log("hello world");
        }

        public override void OnInit()
        {
            Debug.Log("这是单例初始化");
        }
    }
}
