using System.IO;
using UnityEngine;

namespace IFramework.Core
{
    [AddComponentMenu("IFramework/ViewController")]
    public class ViewController : MonoBehaviour
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        [HideInInspector]
        public string Namespace;

        /// <summary>
        /// 脚本名称
        /// </summary>
        [HideInInspector]
        public string ScriptName;

        /// <summary>
        /// 脚本路径
        /// </summary>
        [HideInInspector]
        public string ScriptPath;

        /// <summary>
        /// 脚本名称是否作为路径的一部分
        /// </summary>
        [HideInInspector]
        public bool AsScriptSubPath = true;

        /// <summary>
        /// Prefab路径
        /// </summary>
        [HideInInspector]
        public string PrefabPath;

        /// <summary>
        /// 脚本名称是否作为路径的一部分
        /// </summary>
        [HideInInspector]
        public bool AsPrefabSubPath;

        /// <summary>
        /// 注释
        /// </summary>
        [HideInInspector]
        public string Comment = "";
    }
}
