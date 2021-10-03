using IFramework.Core;
using IFramework.Test.Model;
using UnityEngine;

namespace IFramework.Test.RefCounter
{
    public class RefCounterTest : MonoBehaviour
    {
        private void Start()
        {
            // SimpleCounterTest();
            SafeCounterTest();
        }

        public void SimpleCounterTest()
        {
            Countor simpleRefCounter = new Countor();
            simpleRefCounter.Hold();
            simpleRefCounter.Hold();
            simpleRefCounter.Hold();
            simpleRefCounter.Counter.LogInfo();
            simpleRefCounter.UnHold();
            simpleRefCounter.UnHold();
            simpleRefCounter.UnHold();
            simpleRefCounter.Counter.LogInfo();
        }

        public void SafeCounterTest()
        {
            SafeCountor safeCounter = new SafeCountor();
            UserInfo user = new UserInfo();
            safeCounter.Hold(new UserInfo());
            safeCounter.Hold(new UserInfo());
            safeCounter.Hold(user);
            safeCounter.Hold(user);
        }
    }
}
