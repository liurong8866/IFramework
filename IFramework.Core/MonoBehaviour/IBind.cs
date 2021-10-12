using UnityEngine;

namespace IFramework.Core
{
    public interface IBind
    {
        BindType BindType { get; }
        
        string ComponentName { get; }

        string Comment { get; }

        Transform Transform { get; }
    }

    public enum BindType
    {
        DefaultElement,
        Element,
        Component
    }
}
