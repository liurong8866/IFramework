using UnityEngine;

namespace IFramework.Core
{
    public class DontDestroy: MonoBehaviour
    {
        void Awake() {
            DontDestroyOnLoad(this);
        }
    }
}
