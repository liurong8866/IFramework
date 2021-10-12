using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    public abstract class UIElement : IocMonoBehaviour, IBind
    {
        public virtual BindType BindType => BindType.Element;

        public abstract string ComponentName { get; }
        
        public string Comment { get; set; }
        
        public Transform Transform => transform;

        public IManager Manager => UIManager.Instance;
    }
}
