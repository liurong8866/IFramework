using System;
using System.Text;
using IFramework.Core;

namespace IFramework.Editor
{
    /// <summary>
    /// UIPanel .designer.cs类生成模板
    /// </summary>
    public class UIPanelDesignerTemplate : AbstractTemplate<UIPanelDesignerTemplate>
    {
        private UIPanelDesignerTemplate() { }
        
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
            sb.AppendLine("/* auto generate at {0} */".Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            sb.AppendLine();
            sb.AppendLine("using System;");
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using UnityEngine.UI;");
            sb.AppendLine("using IFramework.Core;");
            sb.AppendLine("using IFramework.Engine;");
            sb.AppendLine();

            if (generateInfo.Namespace.Equals(Constant.UIKIT_DEFAULT_NAMESPACE)) {
                sb.AppendLine("// 请在菜单 IFramework/UIKit Config 里设置默认命名空间");
            }
            sb.AppendLine("namespace " + generateInfo.Namespace);
            sb.AppendLine("{");
            if (generateInfo.Comment.NotEmpty()) {
                sb.AppendLine("\t/// <summary>");
                sb.AppendLine("\t///" + generateInfo.Comment);
                sb.AppendLine("\t/// </summary>");
            }
            sb.AppendLine($"\tpublic partial class {generateInfo.ScriptName}");
            sb.AppendLine("\t{");
            sb.AppendLine($"\t\tpublic const string Name = \"{generateInfo.ScriptName}\";");
            sb.AppendLine();
            string privateData = $"{generateInfo.ScriptName.ToCamel()}Data";
            sb.AppendLine($"\t\tprivate {generateInfo.ScriptName}Data {privateData} = null;");
            sb.AppendLine();
            
            sb.AppendLine("\t\tprotected override void ClearUIComponents() { Data = null; }");
            sb.AppendLine($"\t\tpublic {generateInfo.ScriptName}Data Data");
            sb.AppendLine("\t\t{");
            sb.AppendLine($"\t\t\tget => {privateData} ??= new {generateInfo.ScriptName}Data();");
            sb.AppendLine("\t\t\tset { {privateData} = value; data = value; }  ");
            sb.AppendLine("\t\t}");
            sb.AppendLine();
            
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
