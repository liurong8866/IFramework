using System;
using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

public class DelayFrameActionTest : MonoBehaviour
{
    void Start() {
        Debug.Log(Time.frameCount);
        var delayFrameAction = DelayFrameAction.Allocate(1, () => { Debug.Log(Time.frameCount); });
        this.Execute(delayFrameAction);
        this.DelayFrame(2, (() => { Debug.Log(Time.frameCount); }));
        this.DelayFrame(100, (() => { Debug.Log(Time.frameCount); }));

        // this.Sequence()
        //     .Event(() => Debug.Log(Time.frameCount))
        //     .DelayFrame(2)
        //     .Event(() => Debug.Log(Time.frameCount))
        //     .Begin();
        // this.NextFrame(() => { Debug.Log(Time.frameCount); });
    }
}
