using System;
using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

public class DelayFrameActionTest : MonoBehaviour
{
    private void Start()
    {
        // DelayFrameAction delayFrameAction = DelayFrameAction.Allocate(0, () => { Debug.Log(Time.frameCount); });
        // this.Execute(delayFrameAction);
        //
        // DelayFrameAction delayFrameAction2 = DelayFrameAction.Allocate(2, () => { Debug.Log(Time.frameCount); });
        // delayFrameAction2.Execute(this);
        //
        //
        // this.DelayFrame(4, () => { Debug.Log(Time.frameCount); });
        // this.DelayFrame(6, () => { Debug.Log(Time.frameCount); });
        //
        //
        // this.NextFrame(() => { Debug.Log("NextFrame："+Time.frameCount); });

        OnBeginAction onBeginAction = OnBeginAction.Allocate(aa);
        
        onBeginAction.Execute();
    }

    public void aa(OnBeginAction a)
    {
        Log.Info("hello world");
    }
}
