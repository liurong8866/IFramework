using System.Collections;
using System.Collections.Generic;
using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

public class RepeatNodeActionTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ParallelNodeTest();
    }

    void ParallelNodeTest()
    {
        ParallelNode parallelNode = new ParallelNode();
        parallelNode.Append(DelayAction.Allocate(2, () => Log.Info("hello")));
        parallelNode.Append(DelayAction.Allocate(4, () => Log.Info("world")));
        parallelNode.Append(DelayAction.Allocate(1, () => Log.Info("liurong:")));
        parallelNode.Execute(this);
        // parallelNode.Append(DelayFrameAction.Allocate(1, () => Log.Info("action: hello" + Time.frameCount)));
        // parallelNode.Append(DelayFrameAction.Allocate(2, () => Log.Info("action: world"+ Time.frameCount)));
        // parallelNode.Append(DelayFrameAction.Allocate(3, () => Log.Info("action: liurong" + Time.frameCount)));
        // parallelNode.Execute(this);
    }

    void RepeatNodeTest()
    {
        RepeatNode repeatNode = new RepeatNode(DelayAction.Allocate(2, () => Log.Info("hello")), 2);
        repeatNode.Execute(this);
        RepeatNode repeatNode2 = new RepeatNode(DelayAction.Allocate(2, () => Log.Info("world")), 2);
        this.Execute(repeatNode2);

        this.Repeat(3)
               .Delay(3f)
               .Event(() => Debug.Log("序列：" + Time.frameCount))
               .DelayFrame(2)
               .Event(() => Debug.Log("序列：" + Time.frameCount))
               .NextFrame()
               .Begin();

        // 不填写则无限循环
        this.Repeat()
               .Delay(3f)
               .Event(() => Debug.Log("无限循环：" + Time.frameCount))
               .Begin();
    }
}
