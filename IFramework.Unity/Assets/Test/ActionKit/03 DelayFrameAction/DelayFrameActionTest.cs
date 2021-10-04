using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

public class DelayFrameActionTest : MonoBehaviour
{
    private void Start()
    {
        DelayFrameAction delayFrameAction = DelayFrameAction.Allocate(0, () => { Debug.Log(Time.frameCount); });
        this.Execute(delayFrameAction);
        
        DelayFrameAction delayFrameAction2 = DelayFrameAction.Allocate(2, () => { Debug.Log(Time.frameCount); });
        delayFrameAction2.Execute(this);

        
        this.DelayFrame(4, () => { Debug.Log(Time.frameCount); });
        this.DelayFrame(6, () => { Debug.Log(Time.frameCount); });
        
        "======= 以下为序列：顺序执行 =========".LogInfo();
        
        this.Sequence()
            .Event(() => Debug.Log("序列：" + Time.frameCount))
            .DelayFrame(2)
            .Event(() => Debug.Log("序列：" + Time.frameCount))
            .DelayFrame(4)
            .Event(() => Debug.Log("序列：" + Time.frameCount))
            .DelayFrame(6)
            .Event(() => Debug.Log("序列：" + Time.frameCount))
            .DelayFrame(8)
            .Event(() => Debug.Log("序列：" + Time.frameCount))
            .Begin();
        
        this.NextFrame(() => { Debug.Log("NextFrame："+Time.frameCount); });

    }
}
