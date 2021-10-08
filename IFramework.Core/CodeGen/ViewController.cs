using UnityEngine;

namespace IFramework.Core
{
    public class ViewController : MonoBehaviour
    {
        [HideInInspector] public string Namespace;

        [HideInInspector] public string ScriptName;

        [HideInInspector] public string ScriptPath;

        [HideInInspector] public string PrefabPath;
        
        [HideInInspector] public string Comment;
    }
}
