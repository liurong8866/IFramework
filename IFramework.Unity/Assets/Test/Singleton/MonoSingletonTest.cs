using IFramework.Core;
using UnityEngine;

namespace IFramework.Test.Singelton
{
    public class MonoSingletonTest : MonoBehaviour
    {
        private void Start()
        {
            MonoSingletonTestDemo1 a = MonoSingletonTestDemo1.Instance;
            MonoSingletonTestDemo1 b = MonoSingletonTestDemo1.Instance;
            Debug.Log(a == b);
            Debug.Log(a.GetHashCode());
            Debug.Log(b.GetHashCode());
            MonoSingletonTestDemo1.Instance.Say();
        }
    }

    public class MonoSingletonTestDemo1 : MonoSingleton<MonoSingletonTestDemo1>
    {
        // 私有化构造函数，防止外部New创建
        private MonoSingletonTestDemo1() { }

        public void Say() { Debug.Log("hello world"); }

        public override void OnInit() { Debug.Log("这是单例初始化"); }
    }
}
