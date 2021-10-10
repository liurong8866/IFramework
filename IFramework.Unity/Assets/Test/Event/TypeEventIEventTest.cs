using IFramework.Core;
using UnityEngine;

namespace IFramework.Test.Event
{
    public class TypeEventIEventTest : MonoBehaviour, IEvent<OnLeftMouseClickEvent>, IEvent<OnRightMouseClickEvent>
    {
        private void Start()
        {
            this.RegisterEvent<OnLeftMouseClickEvent>();
            this.RegisterEvent<OnRightMouseClickEvent>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0)) {
                TypeEvent.Send(new OnLeftMouseClickEvent());
            }
            else if (Input.GetMouseButton(1)) {
                TypeEvent.Send(new OnRightMouseClickEvent());
            }
        }

        public void OnEvent(OnLeftMouseClickEvent t)
        {
            "点击左键".LogInfo();
        }

        public void OnEvent(OnRightMouseClickEvent t)
        {
            "点击右键".LogInfo();
        }

        private void OnDisable()
        {
            this.UnRegisterEvent<OnLeftMouseClickEvent>();
            this.UnRegisterEvent<OnRightMouseClickEvent>();
        }
    }

    public struct OnLeftMouseClickEvent { }

    public struct OnRightMouseClickEvent { }
}
