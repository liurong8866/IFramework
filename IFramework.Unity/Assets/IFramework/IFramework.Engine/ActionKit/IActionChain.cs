/*****************************************************************************
 * MIT License
 * 
 * Copyright (c) 2021 liurong
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 *****************************************************************************/

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
        void OnDisposed(System.Action onDisposedEvent);

        IDisposeEventRegister OnFinished(System.Action onFinishedEvent);
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

        public static IActionChain Wait(this IActionChain self, float seconds) {
            return self.Append(DelayAction.Allocate(seconds));
        }

        public static IActionChain Event(this IActionChain self, params Action[] events) {
            return self.Append(EventAction.Allocate(events));
        }
    }
}
