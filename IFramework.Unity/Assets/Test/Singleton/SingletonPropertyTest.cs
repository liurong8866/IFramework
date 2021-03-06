using IFramework.Core;
using UnityEngine;

namespace IFramework.Test.Singleton
{
    public class SingletonPropertyTest : MonoBehaviour
    {
        // Start is called before the first frame update
        private void Start()
        {
            SingletonPropertyTestDemo a = SingletonPropertyTestDemo.Instance;
            SingletonPropertyTestDemo b = SingletonPropertyTestDemo.Instance;
            Debug.Log(a == b);
            Debug.Log(a.GetHashCode());
            Debug.Log(b.GetHashCode());
            SingletonPropertyTestDemo.Instance.Say();
        }
    }

    public class SingletonPropertyTestDemo : MonoBehaviour, ISingleton
    {
        // 私有化构造函数，防止外部New创建
        private SingletonPropertyTestDemo() { }

        // Start is called before the first frame update
        private void Start()
        {
            Debug.Log("这是属性方法实现的单例 继承了MonoBehaviour");
        }

        public static SingletonPropertyTestDemo Instance => SingletonProperty<SingletonPropertyTestDemo>.Instance;

        public void OnInit()
        {
            Debug.Log("这是属性方法实现的单例初始化");
        }

        public void Say()
        {
            Debug.Log("hello world");
        }
    }
}
