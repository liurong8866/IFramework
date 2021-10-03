using System;
using UnityEngine;

namespace IFramework.Engine
{
    public interface IActionChain : IAction
    {
        MonoBehaviour Executer { get; set; }

        IActionChain Append(IAction node);

        IDisposeWhen Begin();
    }

    public interface IDisposeWhen : IDisposeEventRegister
    {
        IDisposeEventRegister DisposeWhen(Func<bool> condition);
    }

    public interface IDisposeEventRegister
    {
        void OnDisposed(Action onDisposedEvent);

        IDisposeEventRegister OnFinished(Action onFinishedEvent);
    }

    public static class IActionChainExtention
    {
        // public static IActionChain Repeat<T>(this T selfbehaviour, int count = -1) where T : MonoBehaviour {
        //     var retNodeChain = new RepeatNodeChain(count) { Executer = selfbehaviour };
        //     retNodeChain.DisposeWhenGameObjectDestroyed(selfbehaviour);
        //     return retNodeChain;
        // }
        //
        // public static IActionChain Sequence<T>(this T selfbehaviour) where T : MonoBehaviour {
        //     var retNodeChain = new SequenceNodeChain { Executer = selfbehaviour };
        //     retNodeChain.DisposeWhenGameObjectDestroyed(selfbehaviour);
        //     return retNodeChain;
        // }
        //
        // public static IActionChain OnlyBegin(this IActionChain selfChain, Action<OnlyBeginAction> onBegin) {
        //     return selfChain.Append(OnlyBeginAction.Allocate(onBegin));
        // }

        public static IActionChain Wait(this IActionChain self, float seconds) { return self.Append(DelayAction.Allocate(seconds)); }

        public static IActionChain Event(this IActionChain self, params Action[] events) { return self.Append(EventAction.Allocate(events)); }
    }
}
