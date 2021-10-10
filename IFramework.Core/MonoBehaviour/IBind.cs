using UnityEngine;

namespace IFramework.Core
{
    public interface IBind
    {
        string ComponentName { get; set; }

        string Comment { get; set; }

        Transform Transform { get; }

        BindType BindType { get; set; }
    }

    public enum BindType
    {
        DefaultElement,
        Element,
        Component
    }
}
