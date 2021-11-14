using System;
using System.Collections;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Test.IOC
{
    public class IocEventSender : IocMonoBehaviour
    {
        private void Start()
        {
            // TypeEvent.Send(new IocMonoEventTestData() {
            //     Name = "liurong"
            // });
            
            // SendEvent(new IocMonoEventTestData() {
            //     Name = "liurong"
            // });

            StartCoroutine(BBB());
        }

        IEnumerator BBB()
        {
            while (true) {
                
                SendEvent(new IocMonoEventTestData() {
                    Name = "liurong"
                });
                yield return new WaitForSeconds(1);
            }
            
        }
    }
}
