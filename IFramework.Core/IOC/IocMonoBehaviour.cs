using System;
using UnityEngine;

namespace IFramework.Core
{
    public abstract class IocMonoBehaviour : MonoBehaviour
    {
        private void Awake()
        {
            IocContainer.Instance<>()
            Registor();
            In(this);
        }
        
        protected abstract void AwakeBefore();
        protected abstract void AwakeAfter();
    }
}
