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
        [HideInInspector] public string Namespace;

        /// <summary>
        /// 脚本名称
        /// </summary>
        [HideInInspector] public string ScriptName;

        /// <summary>
        /// 脚本路径
        /// </summary>
        [HideInInspector] public string ScriptPath;
        
        /// <summary>
        /// 脚本名称是否作为路径的一部分
        /// </summary>
        [HideInInspector] public bool AsScriptSubPath = true;
        
        /// <summary>
        /// Prefab路径
        /// </summary>
        [HideInInspector] public string PrefabPath;
        
        /// <summary>
        /// 脚本名称是否作为路径的一部分
        /// </summary>
        [HideInInspector] public bool AsPrefabSubPath;
        
        /// <summary>
        /// 注释
        /// </summary>
        [HideInInspector] public string Comment = "";

        /// <summary>
        /// 脚本文件相对资源路径
        /// </summary>
        public string ScriptAssetsPath => Path.Combine("Assets", ScriptPath, AsScriptSubPath ? ScriptName : "");
        
        /// <summary>
        /// .cs 脚本全路径
        /// </summary>
        public string ScriptAssetsClassName => Path.Combine(ScriptAssetsPath, ScriptName + ".cs");
        
        /// <summary>
        /// .Designer.cs 脚本全路径
        /// </summary>
        public string ScriptAssetsDesignerName => Path.Combine(ScriptAssetsPath, ScriptName + ".Designer.cs");
        
        /// <summary>
        /// 预设文件相对资源路径
        /// </summary>
        public string PrefabAssetsPath => Path.Combine("Assets", PrefabPath, AsPrefabSubPath ? ScriptName : "");
                
    }
}
