using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace IFramework.Engine.TimeLine
{
    public class TimeLine : AbstractAction
    {
        private float currentTime;

        public Action<string> OnReceivedEvent = null;

        private Queue<TimeLinePair> queue = new Queue<TimeLinePair>();

        public TimeLine(params TimeLinePair[] pairs)
        {
            foreach (TimeLinePair pair in pairs) { queue.Enqueue(pair); }
        }

        public void Append(TimeLinePair pair)
        {
            queue.Enqueue(pair);
        }

        public void Append(float time, IAction node)
        {
            queue.Enqueue(new TimeLinePair(time, node));
        }

        protected override void OnExecute()
        {
            OnExecute(Time.deltaTime);
        }

        protected void OnExecute(float delta)
        {
            currentTime += delta;
            IEnumerable<TimeLinePair> list = queue.Where(pair => pair.Time < currentTime && !pair.Node.Finished);

            foreach (TimeLinePair pair in list) {
                if (pair.Node.Execute()) { Finished = queue.Count(timelinePair => !timelinePair.Node.Finished) == 0; }
            }
        }

        protected override void OnReset()
        {
            currentTime = 0.0f;

            foreach (TimeLinePair timelinePair in queue) { timelinePair.Node.Reset(); }
        }

        protected override void OnDispose()
        {
            foreach (TimeLinePair timelinePair in queue) { timelinePair.Node.Dispose(); }
            queue.Clear();
            queue = null;
        }
    }
}
