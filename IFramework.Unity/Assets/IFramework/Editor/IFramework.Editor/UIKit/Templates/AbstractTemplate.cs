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
        protected RootViewControllerInfo rootControllerInfo;

        public virtual void OnInit() { }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="controller">ViewController脚本信息路径</param>
        /// <param name="rootControllerInfo">面板代码信息</param>
        public void Generate(ViewController controller, RootViewControllerInfo rootControllerInfo = null, bool overwrite = false)
        {
            this.controller = controller;
            this.rootControllerInfo = rootControllerInfo;
            
            // 如果文件不能覆盖，并且存在，则退出
            if (!overwrite && FileUtils.Exists(FullName)) { return; }
            
            Log.Info("生成脚本: 正在生成脚本 " +  FullName);
            
            // 创建文件夹，如果有则忽略
            DirectoryUtils.Create(controller.ScriptAssetsPath);

            // 写入文件
            FileUtils.Write(FullName, BuildScript());
            
            
        }

        /// <summary> 
        /// 文件全名
        /// </summary>
        public abstract string FullName { get; }

        /// <summary>
        /// 拼接字符串
        /// </summary>
        protected abstract string BuildScript();
    }
}
