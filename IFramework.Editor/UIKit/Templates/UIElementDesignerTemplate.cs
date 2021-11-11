using System.Text;

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
            sb.AppendLine("\tpublic partial class " + generateInfo.ScriptName);
            sb.AppendLine("\t{");
            foreach (BindInfo bindInfo in elementInfo.BindInfoList) {
                sb.AppendLine($"\t\t[SerializeField] public {bindInfo.BindScript.ComponentName.Replace("UnityEngine.UI.","").Replace("UnityEngine.","")} {bindInfo.Name};");
            }
            sb.AppendLine();
            sb.AppendLine($"\t\tpublic override string ComponentName => \"{elementInfo.BindInfo.BindScript.ComponentName}\";");
            sb.AppendLine();
            sb.AppendLine("\t\tpublic void OnDisable()");
            sb.AppendLine("\t\t{");
            foreach (BindInfo bindInfo in elementInfo.BindInfoList) {
                sb.AppendLine($"\t\t\t{bindInfo.Name} = null;");
            }
            sb.AppendLine("\t\t}");
            sb.AppendLine();
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
