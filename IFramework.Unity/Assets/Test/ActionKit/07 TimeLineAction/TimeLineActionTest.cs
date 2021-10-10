using System.Runtime.CompilerServices;
using IFramework.Core;
using IFramework.Engine;
using IFramework.Engine.TimeLine;
using UnityEngine;

namespace IFramework.Test.ActionKit
{
    public class TimeLineActionTest : MonoBehaviour
    {
        void Start()
        {
            TimeLine timelineNode = new TimeLine();

            // 第一秒输出 HelloWorld
            timelineNode.Append(1.0f, EventAction.Allocate(() => Debug.Log("HelloWorld")));

            // 第二秒输出 延时了 2 秒
            timelineNode.Append(2.0f, EventAction.Allocate(() => Debug.Log("延时了 2 秒")));

            // 第三秒发送 一个事件
            timelineNode.Append(3.0f, KeyEventAction.Allocate("someEventA", timelineNode));

            // 第四秒发送 一个事件
            timelineNode.Append(4.0f, KeyEventAction.Allocate("someEventB", timelineNode));

            // 监听 timeline 的 key 事件
            timelineNode.OnReceivedEvent = keyEvent => Debug.Log(keyEvent);

            // 执行 timeline
            this.Execute(timelineNode);
        }
    }
}