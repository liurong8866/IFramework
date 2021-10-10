using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

namespace IFramework.Test.ActionKit
{
    public class EventActionTest : MonoBehaviour
    {
        private EventAction eventAction;

        private void Awake()
        {
            eventAction = EventAction.Allocate(() => { Log.Info("event 4 called"); }, () => { Log.Info("event 5 called"); });
        }

        private void Start()
        {
            // 方法调用
            EventAction eventNode = EventAction.Allocate(() => { Log.Info("event 1 called"); }, () => { Log.Info("event 2 called"); });
            eventNode.Execute(this);

            // IAction 扩展方法调用
            EventAction eventNode2 = EventAction.Allocate();
            this.Execute(eventNode2);

            // 扩展方法调用
            this.Action(() => { "event 3 called".LogInfo(); });
        }

        private void Update()
        {
            if (eventAction != null && !eventAction.Finished && eventAction.Execute()) {
                Log.Info("eventNode  执行完成");
            }
        }
    }
}