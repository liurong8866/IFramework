using System.Text;
using IFramework.Core;

namespace IFramework.Editor
{
    public class UIElementDesignerTemplate : AbstractTemplate<UIElementDesignerTemplate>
    {
        private UIElementDesignerTemplate() { }

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
            sb.AppendLine("using System;");
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using UnityEngine.UI;");
            sb.AppendLine("using IFramework.Core;");
            sb.AppendLine("using IFramework.Engine;");
            sb.AppendLine();
            sb.AppendLine("namespace " + generateInfo.Namespace);
            sb.AppendLine("{");
            sb.AppendFormat("\tpublic partial class " + generateInfo.ScriptName);
            sb.AppendLine("\t{");

            foreach (BindInfo markInfo in elementInfo.BindInfoList) {
                string strUIType = markInfo.BindScript.ComponentName;
                sb.AppendFormat("\t\t[SerializeField] public {0} {1};\r\n", strUIType, markInfo.Name);
            }
            sb.AppendLine();
            sb.Append("\t\t").AppendLine("public void Clear()");
            sb.Append("\t\t").AppendLine("{");

            foreach (BindInfo markInfo in elementInfo.BindInfoList) {
                sb.AppendFormat("\t\t\t{0} = null;\r\n", markInfo.Name);
            }
            sb.Append("\t\t").AppendLine("}");
            sb.AppendLine();
            sb.Append("\t\t").AppendLine("public override string ComponentName");
            sb.Append("\t\t").AppendLine("{");
            sb.Append("\t\t\t");
            sb.AppendLine("get { return \"" + elementInfo.BindInfo.BindScript.ComponentName + "\";}");
            sb.Append("\t\t").AppendLine("}");
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
