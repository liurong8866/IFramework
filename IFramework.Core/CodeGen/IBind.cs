using UnityEngine;

namespace IFramework.Core
{
    public interface IBind
    {
        string ComponentName { get; }

        string Comment { get; }

        Transform Transform { get; }

        BindType BindType { get; set; }
    }
}
