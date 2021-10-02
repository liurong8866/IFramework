using System;
using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

public class DelayActionTest : MonoBehaviour
{
    void Start()
    {
        this.Delay(1.0f, () => { Log.Info("延时 1s"); });
        DelayAction delay2s = DelayAction.Allocate(2.0f, () => { Log.Info("延时 2s"); });
        this.ExecuteNode(delay2s);
        delay2s.Dispose();
    }

    private DelayAction mDelay3s = DelayAction.Allocate(3.0f, () => { Log.Info("延时 3s"); });

    private void Update()
    {
        if (mDelay3s != null && !mDelay3s.Finished && mDelay3s.Execute(Time.deltaTime)) {
            Log.Info("Delay3s 执行完成");
        }
    }
}
