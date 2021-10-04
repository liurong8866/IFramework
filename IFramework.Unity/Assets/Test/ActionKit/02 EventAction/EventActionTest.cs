using IFramework.Core;
using IFramework.Engine;
using UnityEngine;

public class EventActionTest : MonoBehaviour
{
    private EventAction eventAction;

    private void Awake() { eventAction = EventAction.Allocate(() => { Log.Info("event 3 called"); }, () => { Log.Info("event 4 called"); }); }

    private void Start()
    {
        EventAction eventNode = EventAction.Allocate(() => { Log.Info("event 1 called"); }, () => { Log.Info("event 2 called"); });
        this.Execute(eventNode);
        EventAction eventNode2 = EventAction.Allocate();
        this.Execute(eventNode2);

        this.Action(() => { "hello world".LogInfo(); });
        this.Action();
    }

    private void Update()
    {
        if (eventAction != null && !eventAction.Finished && eventAction.Execute()) {
            Log.Info("eventNode2 执行完成");
        }
    }
}
