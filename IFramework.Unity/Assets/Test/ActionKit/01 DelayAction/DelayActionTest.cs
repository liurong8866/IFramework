using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

namespace IFramework.Test.ActionKit
{
    public class DelayActionTest : MonoBehaviour
    {
        private DelayAction mDelay4s;
        private DelayAction delay2s;

        private void Awake()
        {
            mDelay4s = DelayAction.Allocate(4.0f, () => { Log.Info("延时 4s"); });
            DelayAction delayAction = DelayAction.Allocate(1.0f, () => { Debug.Log("延时完毕"); });
            delayAction.OnBeginEvent = () => Debug.Log("开始延时");
            delayAction.OnEndEvent = () => Debug.Log("结束延时");
            this.Execute(delayAction);
        }

        private void Start()
        {
            this.Delay(1.0f, () => { Log.Info("延时 1s"); });
            delay2s = DelayAction.Allocate(2.0f, () => { Log.Info("延时 2s"); });
            delay2s.Execute();
            delay2s.Execute(this);
            DelayAction delay3s = DelayAction.Allocate(3.0f, () => { Log.Info("延时 3s"); });
            this.Execute(delay3s);
        }

        private void OnDestroy()
        {
            delay2s.Dispose();
            mDelay4s.Dispose();
        }

        private void Update()
        {
            if (mDelay4s != null && !mDelay4s.Finished && mDelay4s.Execute()) {
                Log.Info("Delay4s 执行完成");
            }
        }
    }
}
