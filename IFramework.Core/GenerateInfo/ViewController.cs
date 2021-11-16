using IFramework.Core;
using UnityEditor;
using UnityEngine;

namespace IFramework.Core
{
    [AddComponentMenu("IFramework/ViewController")]
    public class ViewController : MonoBehaviour, IBind
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

        // /// <summary>
        // /// 序列化的字段
        // /// </summary>
        // [HideInInspector]
        // public Serialized SerializedFiled;
        //
        // /// <summary>
        // /// 用于在Inspector自定义面板中更改时触发修改，保存Prefab
        // /// </summary>
        // public class Serialized
        // {
        //     public SerializedProperty Namespace;
        //     public SerializedProperty ScriptName;
        //     public SerializedProperty ScriptPath;
        //     public SerializedProperty AsScriptSubPath;
        //     public SerializedProperty PrefabPath;
        //     public SerializedProperty AsPrefabSubPath;
        //     public SerializedProperty Comment;
        //
        //     public Serialized(SerializedObject serializedObject)
        //     {
        //         Namespace = serializedObject.FindProperty("Namespace");
        //         ScriptName = serializedObject.FindProperty("ScriptName");
        //         ScriptPath = serializedObject.FindProperty("ScriptPath");
        //         AsScriptSubPath = serializedObject.FindProperty("AsScriptSubPath");
        //         PrefabPath = serializedObject.FindProperty("PrefabPath");
        //         AsPrefabSubPath = serializedObject.FindProperty("AsPrefabSubPath");
        //         Comment = serializedObject.FindProperty("Comment");
        //     }
        // }

        /// <summary>
        /// 实现IBind接口
        /// </summary>
        public Transform Transform => transform;
        public BindType BindType => BindType.Element;
        public string ComponentName => ScriptName;
        string IBind.Comment => this.Comment;
    }
}
