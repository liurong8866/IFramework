using System;
using System.Text;
using IFramework.Core;

namespace IFramework.Editor
{
    /// <summary>
    /// ViewController .designer.cs类生成模板
    /// </summary>
    public class ViewControllerDesignerTemplate : AbstractTemplate<ViewControllerDesignerTemplate>
    {
        private ViewControllerDesignerTemplate() { }

        /// <summary>
        /// 文件全名
        /// </summary>
        protected override string FullName => generateInfo.ScriptAssetsDesignerName;

        /// <summary>
        /// 拼接字符串
        /// </summary>
        protected override string BuildScript()
        {
            StringBuilder sb = new StringBuilder();
            // 确保每次生成文件都编译
            sb.AppendLine("/* 脚本自动生成于：{0} ，请勿修改！ */".Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            sb.AppendLine();
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using IFramework.Core;");
            sb.AppendLine("using IFramework.Engine;");
            sb.AppendLine();

            if (generateInfo.Namespace.Equals(Constant.UIKIT_DEFAULT_NAMESPACE)) {
                sb.AppendLine("// 请在菜单：IFramework/UIKit Config 中设置默认命名空间");
            }
            sb.AppendLine("namespace " + generateInfo.Namespace);
            sb.AppendLine("{");
            sb.AppendLine("\tpublic partial class " + generateInfo.ScriptName);
            sb.AppendLine("\t{");

            // 循环设置字段
            foreach (BindInfo bindInfo in elementInfo.BindInfoList) {
                if (bindInfo.BindScript.Comment.NotEmpty()) {
                    // 添加注释
                    sb.AppendLine("\t\t// " + bindInfo.BindScript.Comment);
                }
                sb.AppendLine($"\t\tpublic {bindInfo.BindScript.ComponentName} {bindInfo.Name};");
                sb.AppendLine();
            }
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
