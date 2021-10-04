using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

public class DelayActionTest : MonoBehaviour
{
    private DelayAction mDelay3s;

    private void Awake()
    {
        mDelay3s = DelayAction.Allocate(3.0f, () => { Log.Info("延时 3s"); });
    }

    private void Start()
    {
        this.Delay(1.0f, () => { Log.Info("延时 1s"); });
        
        DelayAction delay2s = DelayAction.Allocate(2.0f, () => { Log.Info("延时 2s"); });
        this.Execute(delay2s);

        DelayAction delay3 = new DelayAction();
        delay2s.Execute();
        
        delay2s.Dispose();
    }

    private void Update()
    {
        if (mDelay3s != null && !mDelay3s.Finished && mDelay3s.Execute()) {
            Log.Info("Delay3s 执行完成");
        }
    }
}
