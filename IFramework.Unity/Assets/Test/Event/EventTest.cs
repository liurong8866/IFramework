using IFramework.Core;
using IFramework.Test.Model;
using UnityEngine;

namespace IFramework.Test.Event
{
    public class EventTest : MonoBehaviour
    {
        private void Start()
        {
            UserInfo userInfo = new UserInfo {
                UserName = "liurong",
                Age = 20
            };
            Debug.Log("发送事件");
            DefaultEvent.Send(100, userInfo);
        }
    }
}
