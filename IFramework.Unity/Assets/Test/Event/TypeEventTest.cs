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
using IFramework.Core;

namespace IFramework.Test.Event
{
    public class TypeEventTest : MonoBehaviour
    {
        private void Start() {
            IDisposable disposable = TypeEvent.Register<GameStartEvent>(OnGameStartEvent);
            TypeEvent.Register<GameOverEvent>(OnGameOverEvent);
            TypeEvent.Register<ISkillEvent>(OnSkillEvent);
            TypeEvent.Send<GameStartEvent>();
            disposable.Dispose();
            TypeEvent.Send<GameStartEvent>();
            disposable.Dispose();

            TypeEvent.Send(new GameOverEvent() {
                score = 100
            });

            // 要把事件发送给父类
            TypeEvent.Send<ISkillEvent>(new PlayerSkillAEvent());
            TypeEvent.Send<ISkillEvent>(new PlayerSkillBEvent());
            gameObject.DestroySelf();
        }

        private void OnGameStartEvent(GameStartEvent gameStartEvent) {
            Debug.Log("游戏开始了");
        }

        private void OnGameOverEvent(GameOverEvent gameOverEvent) {
            Debug.LogFormat("游戏结束，分数:{0}", gameOverEvent.score);
        }

        private void OnSkillEvent(ISkillEvent skillEvent) {
            if (skillEvent is PlayerSkillAEvent) {
                Debug.Log("A 技能释放");
            }
            else if (skillEvent is PlayerSkillBEvent) {
                Debug.Log("B 技能释放");
            }
        }

        private void OnDestroy() {
            TypeEvent.UnRegister<GameStartEvent>(OnGameStartEvent);
            TypeEvent.UnRegister<GameOverEvent>(OnGameOverEvent);
            TypeEvent.UnRegister<ISkillEvent>(OnSkillEvent);
        }
    }

    #region 事件定义

    public class GameStartEvent
    { }

    public class GameOverEvent
    {
        // 可以携带参数
        public int score;
    }

    public interface ISkillEvent
    { }

    // 支持继承
    public class PlayerSkillAEvent : ISkillEvent
    { }

    public class PlayerSkillBEvent : ISkillEvent
    { }

    #endregion
}
