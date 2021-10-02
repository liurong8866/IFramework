
using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

public class DelayActionTest : MonoBehaviour
{
    void Start()
    {
        this.Delay(1.0f, () => { Log.Info("延时 1s"); });
        
        // DelayAction delay2s = DelayAction.Allocate(2.0f, () => { Log.Info("延时 2s"); });
        // this.ExecuteNode(delay2s);
        // delay2s.Dispose();
        
    }

}
