using System.IO;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Editor
{
    /// <summary>
    /// 代码模板抽象类
    /// </summary>
    public abstract class AbstractTemplate : ISingleton
    {
        protected ViewController controller;
        protected RootControllerInfo rootControllerInfo;

        public virtual void OnInit() { }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="controller">ViewController脚本信息路径</param>
        /// <param name="rootControllerInfo">面板代码信息</param>
        public void Generate(ViewController controller, RootControllerInfo rootControllerInfo = null)
        {
            this.controller = controller;
            this.rootControllerInfo = rootControllerInfo;

            // 格式化字符串
            this.controller.Namespace = controller.Namespace.Trim();
            this.controller.ScriptName = controller.ScriptName.Trim();
            this.controller.ScriptPath = controller.ScriptPath.Trim();

            // 设置全路径
            this.FullPath = Application.dataPath.CombinePath(controller.ScriptPath);

            // 如果文件不能覆盖，并且存在，则退出
            if (!IsOverwritten && FileUtils.Exists(FullName)) { return; }

            // 创建文件夹，如果有则忽略
            DirectoryUtils.Create(FullPath);

            // 写入文件
            FileUtils.Write(FullName, BuildScript());
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        protected string FullPath { get; private set; }

        /// <summary> 
        /// 文件全名
        /// </summary>
        public abstract string FullName { get; }

        /// <summary>
        /// 是否覆盖文件
        /// </summary>
        protected abstract bool IsOverwritten { get; }

        /// <summary>
        /// 拼接字符串
        /// </summary>
        protected abstract string BuildScript();
    }
}
