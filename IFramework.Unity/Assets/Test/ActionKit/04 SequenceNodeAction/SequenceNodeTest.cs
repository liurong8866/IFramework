using IFramework.Engine;
using UnityEngine;

public class SequenceNodeTest : MonoBehaviour
{
    private void Start()
    {
        this.Sequence()
            .Event(() => Debug.Log(Time.frameCount))
            .Delay(3)
            .DelayFrame(2)
            .Event(() => Debug.Log(Time.frameCount))
            .Begin();
        this.NextFrame(() => { Debug.Log(Time.frameCount); });
    }
}
