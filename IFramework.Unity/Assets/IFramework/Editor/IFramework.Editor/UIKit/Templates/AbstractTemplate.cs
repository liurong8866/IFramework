using IFramework.Core;

namespace IFramework.Editor
{
    public abstract class AbstractTemplate : ISingleton
    {
        protected string nameSpace;
        protected string scriptName;
        protected string scriptPath;
        protected PanelCodeInfo panelCodeInfo;

        public virtual void OnInit() { }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="scriptName">脚本名称</param>
        /// <param name="scriptPath">脚本路径</param>
        /// <param name="panelCodeInfo">面板代码信息</param>
        public void Generate(string nameSpace, string scriptName, string scriptPath, PanelCodeInfo panelCodeInfo = null)
        {
            // 如果文件存在，则退出
            if (FileUtils.Exists(FullPath)) { return; }
            this.nameSpace = nameSpace.Trim();
            this.scriptName = scriptName.Trim();
            this.scriptPath = scriptPath.Trim();
            this.panelCodeInfo = panelCodeInfo;
            FileUtils.Write(FullPath, BuildScript());
        }

        /// <summary>
        /// 文件全名
        /// </summary>
        public abstract string FullPath { get; }

        /// <summary>
        /// 拼接字符串
        /// </summary>
        protected abstract string BuildScript();
    }
}
