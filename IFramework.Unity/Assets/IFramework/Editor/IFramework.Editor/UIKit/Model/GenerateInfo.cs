using System.IO;
using IFramework.Core;

namespace IFramework.Editor
{
    /// <summary>
    /// 生成信息
    /// </summary>
    public abstract class GenerateInfo
    {
        /// <summary>
        /// 命名空间
        /// </summary>
        public string Namespace;

        /// <summary>
        /// 脚本名称
        /// </summary>
        public string ScriptName;

        /// <summary>
        /// 脚本路径
        /// </summary>
        public string ScriptPath;

        /// <summary>
        /// Prefab路径
        /// </summary>
        public string PrefabPath;

        /// <summary>
        /// 注释
        /// </summary>
        public string Comment = "";

        /// <summary>
        /// 脚本文件相对资源路径
        /// </summary>
        public abstract string ScriptAssetsPath { get; }

        /// <summary>
        /// 预设文件相对资源路径
        /// </summary>
        public abstract string PrefabAssetsPath { get; }

        /// <summary>
        /// .cs 脚本全路径
        /// </summary>
        public string ScriptAssetsClassName => DirectoryUtils.CombinePath(ScriptAssetsPath, ScriptName + ".cs");

        /// <summary>
        /// .designer.cs 脚本全路径
        /// </summary>
        public string ScriptAssetsDesignerName => DirectoryUtils.CombinePath(ScriptAssetsPath, ScriptName + ".designer.cs");
    }

    /// <summary>
    /// ViewController的代码生成信息
    /// </summary>
    public class ViewControllerGenerateInfo : GenerateInfo
    {
        /// <summary>
        /// 脚本名称是否作为路径的一部分
        /// </summary>
        public bool AsScriptSubPath = true;

        /// <summary>
        /// 脚本名称是否作为路径的一部分
        /// </summary>
        public bool AsPrefabSubPath;

        /// <summary>
        /// 脚本文件相对资源路径
        /// </summary>
        public override string ScriptAssetsPath => DirectoryUtils.CombinePath("Assets", ScriptPath, AsScriptSubPath ? ScriptName : "");

        /// <summary>
        /// 预设文件相对资源路径
        /// </summary>
        public override string PrefabAssetsPath => DirectoryUtils.CombinePath("Assets", PrefabPath, AsPrefabSubPath ? ScriptName : "");

        public ViewControllerGenerateInfo(ViewController controller)
        {
            this.Namespace = controller.Namespace;
            this.ScriptName = controller.ScriptName;
            this.ScriptPath = controller.ScriptPath;
            this.AsScriptSubPath = controller.AsScriptSubPath;
            this.PrefabPath = controller.PrefabPath;
            this.AsPrefabSubPath = controller.AsPrefabSubPath;
            this.Comment = controller.Comment;
        }
    }

    /// <summary>
    /// UIPanel的代码生成信息
    /// </summary>
    public class UIPanelGenerateInfo : GenerateInfo
    {
        /// <summary>
        /// 脚本文件相对资源路径
        /// </summary>
        public override string ScriptAssetsPath => DirectoryUtils.CombinePath("Assets", ScriptPath, ScriptName);

        /// <summary>
        /// 预设文件相对资源路径
        /// </summary>
        public override string PrefabAssetsPath => DirectoryUtils.CombinePath("Assets", PrefabPath, ScriptName);
    }
}
