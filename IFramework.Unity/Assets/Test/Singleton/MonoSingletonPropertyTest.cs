using IFramework.Core;
using UnityEngine;

namespace IFramework.Test.Singelton
{
    public class MonoSingletonPropertyTest : MonoBehaviour
    {
        private void Start()
        {
            MonoSingletonPropertyTestDemo a = MonoSingletonPropertyTestDemo.Instance;
            MonoSingletonPropertyTestDemo b = MonoSingletonPropertyTestDemo.Instance;
            Debug.Log(a == b);
            Debug.Log(a.GetHashCode());
            Debug.Log(b.GetHashCode());
            MonoSingletonPropertyTestDemo.Instance.Say();
            MonoSingletonPropertyTestDemo.Instance.Say2();
        }
    }

    public class MonoSingletonPropertyTestDemo : Aaa, ISingleton
    {
        // 私有化构造函数，防止外部New创建
        private MonoSingletonPropertyTestDemo() { }

        public static MonoSingletonPropertyTestDemo Instance => MonoSingletonProperty<MonoSingletonPropertyTestDemo>.Instance;

        public void Say()
        {
            Debug.Log("hello world");
        }

        public void OnInit()
        {
            Debug.Log("这是单例初始化");
        }
    }

    public class Aaa : MonoBehaviour
    {
        public void Say2()
        {
            Debug.Log("我是AAA");
        }
    }
}
