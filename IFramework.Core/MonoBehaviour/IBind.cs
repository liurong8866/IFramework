using UnityEngine;

namespace IFramework.Core
{
    public interface IBind
    {
        Transform Transform { get; }
        
        BindType BindType { get; }
        
        string ComponentName { get; }

        string Comment { get; }
    }

    public enum BindType
    {
        DefaultElement,
        Element,
        Component
    }
}
