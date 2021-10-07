using System.Text;
using IFramework.Core;

namespace IFramework.Editor
{
    public class ViewControllerTemplate : AbstractTemplate
    {
        /// <summary>
        /// 文件全名
        /// </summary>
        public override string FullPath => scriptPath + "/{0}.cs".Format(scriptName);

        /// <summary>
        /// 拼接字符串
        /// </summary>
        protected override string BuildScript()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using IFramework.Engine;");
            sb.AppendLine("/n");

            if (nameSpace.Equals(Configure.DefaultNameSpace.Value)) {
                sb.AppendLine("// 1.请在Unity3D菜单 IFramework/UIKit Config 里设置默认命名空间");
                sb.AppendLine("// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改");
            }

            sb.AppendLine("namespace " + nameSpace);
            sb.AppendLine("{");
            sb.AppendLine("\tpublic partial class {0} : ViewController".Format(scriptName));
            sb.AppendLine("\t{");
            sb.AppendLine("\t\tvoid Start()");
            sb.AppendLine("\t\t{");
            sb.AppendLine("\t\t\t// Code Here");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t}");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
