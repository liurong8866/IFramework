using System;
using System.Collections;
using System.Threading;
using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

public class UntilActionTest : MonoBehaviour
{
    private int currentTime = 0;
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
