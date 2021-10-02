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

using IFramework.Core;
using UnityEngine;

namespace IFramework.Test.Event
{
    public class TypeEventIOnEventTest : MonoBehaviour, IOnEvent<OnLeftMouseClickEvent>, IOnEvent<OnRightMouseClickEvent>
    {
        private void Start()
        {
            this.RegisterEvent<OnLeftMouseClickEvent>();
            this.RegisterEvent<OnRightMouseClickEvent>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0)) {
                TypeEvent.Send(new OnLeftMouseClickEvent());
            }
            else if (Input.GetMouseButton(1)) {
                TypeEvent.Send(new OnRightMouseClickEvent());
            }
        }

        public void OnEvent(OnLeftMouseClickEvent t)
        {
            "点击左键".LogInfo();
        }

        public void OnEvent(OnRightMouseClickEvent t)
        {
            "点击右键".LogInfo();
        }

        private void OnDisable()
        {
            this.UnRegisterEvent<OnLeftMouseClickEvent>();
            this.UnRegisterEvent<OnRightMouseClickEvent>();
        }
    }

    public struct OnLeftMouseClickEvent { }

    public struct OnRightMouseClickEvent { }
}
