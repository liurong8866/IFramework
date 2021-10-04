using System.Runtime.CompilerServices;
using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

public class SequenceNodeTest : MonoBehaviour
{
    
    
    private void Start()
    {
        SequenceNode sequenceNode = new SequenceNode();

        DelayAction delayAction = DelayAction.Allocate(3, () => { "等待3秒".LogInfo();});
        sequenceNode.Append(delayAction);
        sequenceNode.Execute();
        
        
        // this.Sequence()
        //     .Event(() => Debug.Log(Time.frameCount))
        //     .Delay(1.5f)
        //     .DelayFrame(2)
        //     .Event(() => Debug.Log(Time.frameCount))
        //     .Begin();
        // this.NextFrame(() => { Debug.Log(Time.frameCount); });
    }
    
    // private void Start() {
    //     this.Sequence()
    //         .Delay(1.0f)
    //         .Event(() => Log.Info("Sequence1 延时了 1s"))
    //         .Begin()
    //         .OnDisposed(() => { Log.Info("Sequence1 destroyed"); });
    //     
    //     var sequenceNode2 = new SequenceNode(DelayAction.Allocate(1.5f));
    //     sequenceNode2.Append(EventAction.Allocate(() => Log.Info("Sequence2 延时 1.5s")));
    //     sequenceNode2.Append(DelayAction.Allocate(0.5f));
    //     sequenceNode2.Append(EventAction.Allocate(() => Log.Info("Sequence2 延时 2.0s")));
    //     this.Execute(sequenceNode2);
    //     /* 这种方式需要自己手动进行销毁
    //     sequenceNode2.Dispose();
    //     sequenceNode2 = null;
    //     */
    //
    //     // 或者 OnDestroy 触发时进行销毁
    //     sequenceNode2.DisposeWhenGameObjectDestroyed(this);
    // }
    //
    // void OnLoginSucceed() { }
    //
    // private SequenceNode mSequenceNodeNode3 = new SequenceNode(
    //     DelayAction.Allocate(3.0f),
    //     EventAction.Allocate(() => { Log.Info("Sequence3 延时 3.0f"); }));
    //
    // private void Update() {
    //     if (mSequenceNodeNode3 != null && !mSequenceNodeNode3.Finished &&
    //         mSequenceNodeNode3.Execute()) {
    //         Log.Info("SequenceNode3 执行完成");
    //     }
    // }
    //
    // private void OnDestroy() {
    //     mSequenceNodeNode3.Dispose();
    //     mSequenceNodeNode3 = null;
    // }
}
