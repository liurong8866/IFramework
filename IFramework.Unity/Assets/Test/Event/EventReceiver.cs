using IFramework.Core;
using UnityEngine;

namespace IFramework.Test.Event
{
    public class EventReceiver : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("注册事件");
            NumberEvent.Register(100, Action);
        }

        private void Action(int key, params object[] param)
        {
            switch (key) {
                case 100:
                    Debug.Log(param);
                    break;
            }
        }

        private void OnDestroy()
        {
            NumberEvent.UnRegister(100, Action);
        }
    }
}
