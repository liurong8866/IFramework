using System;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Test.Event
{
    public class TypeEventTest : MonoBehaviour
    {
        private void Start()
        {
            IDisposable disposable = TypeEvent.Register<GameStartEvent>(OnGameStartEvent);
            TypeEvent.Register<GameOverEvent>(OnGameOverEvent);
            TypeEvent.Register<ISkillEvent>(OnSkillEvent);
            TypeEvent.Send<GameStartEvent>();
            disposable.Dispose();
            TypeEvent.Send<GameStartEvent>();
            disposable.Dispose();
            TypeEvent.Send(new GameOverEvent { score = 100 });

            // 要把事件发送给父类
            TypeEvent.Send<ISkillEvent>(new PlayerSkillAEvent());
            TypeEvent.Send<ISkillEvent>(new PlayerSkillBEvent());
            gameObject.DestroySelf();
        }

        private void OnGameStartEvent(GameStartEvent gameStartEvent)
        {
            Debug.Log("游戏开始了");
        }

        private void OnGameOverEvent(GameOverEvent gameOverEvent)
        {
            Debug.LogFormat("游戏结束，分数:{0}", gameOverEvent.score);
        }

        private void OnSkillEvent(ISkillEvent skillEvent)
        {
            if (skillEvent is PlayerSkillAEvent) { Debug.Log("A 技能释放"); } else if (skillEvent is PlayerSkillBEvent) { Debug.Log("B 技能释放"); }
        }

        private void OnDestroy()
        {
            TypeEvent.UnRegister<GameStartEvent>(OnGameStartEvent);
            TypeEvent.UnRegister<GameOverEvent>(OnGameOverEvent);
            TypeEvent.UnRegister<ISkillEvent>(OnSkillEvent);
        }
    }

    #region 事件定义

    public class GameStartEvent { }

    public class GameOverEvent
    {
        // 可以携带参数
        public int score;
    }

    public interface ISkillEvent { }

    // 支持继承
    public class PlayerSkillAEvent : ISkillEvent { }

    public class PlayerSkillBEvent : ISkillEvent { }

    #endregion
}
