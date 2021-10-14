using System.Text;
using IFramework.Core;

namespace IFramework.Editor
{
    public class UIElementTemplate : AbstractTemplate<UIElementTemplate>
    {
        private UIElementTemplate() { }

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
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Collections.Generic;");
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using UnityEngine.UI;");
            sb.AppendLine("using IFramework.Core;");
            sb.AppendLine("using IFramework.Engine;");
            sb.AppendLine();
            sb.AppendLine("namespace " + generateInfo.Namespace);
            sb.AppendLine("{");
            sb.AppendLine("\tpublic partial class {0} : {1}".Format(generateInfo.ScriptName, elementInfo.BindInfo.BindScript.BindType == BindType.Component ? "UIComponent" : "UIElement"));
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tprivate void Awake() { }");
            sb.AppendLine();
            sb.Append("\t\tprotected override void BeforeDestroy() { }");
            sb.AppendLine("\t}");
            sb.Append("}");
            return sb.ToString();
        }
    }
}
