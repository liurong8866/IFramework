using IFramework.Core;
using UnityEngine;

namespace IFramework.Test.Singelton
{
    public class MonoSingletonAttributeTest : MonoBehaviour
    {
        private void Start()
        {
            MonoSingletonAttributeTestDemo monoSingletonAttributeTestDemo = MonoSingletonAttributeTestDemo.Instance;
        }
    }

    [MonoSingleton("")]
    // [MonoSingleton("[UI]Root/StartGame")]
    public class MonoSingletonAttributeTestDemo : MonoSingleton<MonoSingletonAttributeTestDemo>
    {
        private MonoSingletonAttributeTestDemo() { }

        private void Start() { Debug.Log("我是自定义特性"); }
    }
}
