using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

namespace IFramework.Test.ActionKit
{
    public class UntilActionTest : MonoBehaviour
    {
        private int currentTime;
        private UntilAction untilAction;

        private void Awake()
        {
            untilAction = UntilAction.Allocate(() => currentTime == 1000, () => { "延迟了多少".LogInfo(); });

            this.Sequence()
                   .Until(() => Input.GetMouseButtonDown(0))
                   .Event(() => Debug.Log("鼠标按钮点击了"))
                   .Begin();
        }

        private void Update()
        {
            untilAction?.Execute();
            currentTime++;
        }
    }
}
