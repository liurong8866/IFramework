using System.IO;
using IFramework.Core;
using UnityEngine;

namespace IFramework.Editor
{
    public abstract class AbstractTemplate : ISingleton
    {
        protected ViewController controller;
        protected PanelCodeInfo panelCodeInfo;

        public virtual void OnInit() { }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="controller">ViewController脚本信息路径</param>
        /// <param name="panelCodeInfo">面板代码信息</param>
        public void Generate(ViewController controller, PanelCodeInfo panelCodeInfo = null)
        {
            this.controller = controller;
            this.controller.Namespace = controller.Namespace.Trim();
            this.controller.ScriptName = controller.ScriptName.Trim();
            this.controller.ScriptsPath = Application.dataPath.CombinePath(controller.ScriptsPath.Trim());
            this.panelCodeInfo = panelCodeInfo;

            // 如果文件不能覆盖，并且存在，则退出
            if (!IsOverwritten && FileUtils.Exists(FullPath)) { return; }

            // 创建文件夹，如果有则忽略
            DirectoryUtils.Create(this.controller.ScriptsPath);
            
            // 写入文件
            FileUtils.Write(FullPath, BuildScript());
        }

        /// <summary>
        /// 文件全名
        /// </summary>
        public abstract string FullPath { get; }
        
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
