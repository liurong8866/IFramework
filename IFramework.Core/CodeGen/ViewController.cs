using UnityEngine;

namespace IFramework.Core
{
    public class ViewController : MonoBehaviour
    {
        [HideInInspector] public string Namespace = string.Empty;

        [HideInInspector] public string ScriptName;

        [HideInInspector] public string ScriptsFolder = string.Empty;

        [HideInInspector] public bool GeneratePrefab;

        [HideInInspector] public string PrefabFolder = string.Empty;
    }
}
