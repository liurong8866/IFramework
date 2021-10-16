using System.Text;
using IFramework.Core;

namespace IFramework.Editor
{
    /// <summary>
    /// UIPanel .cs类生成模板
    /// </summary>
    public class UIPanelTemplate : AbstractTemplate<UIPanelTemplate>
    {
        private UIPanelTemplate() { }

        /// <summary>
        /// 文件全名
        /// </summary>
        protected override string FullName => generateInfo.ScriptAssetsClassName;

        /// <summary>
        /// 拼接字符串
        /// </summary>
        protected override string BuildScript()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using UnityEngine.UI;");
            sb.AppendLine("using IFramework.Core;");
            sb.AppendLine("using IFramework.Engine;");
            sb.AppendLine();
            if (generateInfo.Namespace.Equals(Constant.UIKIT_DEFAULT_NAMESPACE)) {
                sb.AppendLine("// 1.请在菜单 IFramework/UIKit Config 里设置默认命名空间");
                sb.AppendLine("// 2.用户逻辑代码不会被覆盖，如需重新生成，请手动删除当前代码文件");
            }
            sb.AppendLine("namespace " + generateInfo.Namespace);
            sb.AppendLine("{");
            sb.AppendLine($"\tpublic class {generateInfo.ScriptName}Data : UIPanelData {{ }}");
            sb.AppendLine();
            if (generateInfo.Comment.NotEmpty()) {
                sb.AppendLine("\t/// <summary>");
                sb.AppendLine("\t/// " + generateInfo.Comment);
                sb.AppendLine("\t/// </summary>");
            }
            sb.AppendLine($"\tpublic partial class {generateInfo.ScriptName} : UIPanel");
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tprotected override void OnInit(IData data = null)");
            sb.AppendLine("\t\t{");
            sb.AppendLine($"\t\t\tData = data as {generateInfo.ScriptName}Data ?? new {generateInfo.ScriptName}Data();");
            sb.AppendLine("\t\t}");
            sb.AppendLine();
            sb.AppendLine("\t\tprotected override void OnOpen(IData data = null) { }");
            sb.AppendLine();
            sb.AppendLine("\t\tprotected override void OnShow() { }");
            sb.AppendLine();
            sb.AppendLine("\t\tprotected override void OnHide() { }");
            sb.AppendLine();
            sb.AppendLine("\t\tprotected override void OnClose() { }");
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
