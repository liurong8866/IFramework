using System;
using System.Text;
using IFramework.Core;

namespace IFramework.Editor
{
    public class ViewControllerDesignerTemplate : AbstractTemplate
    {
        private ViewControllerDesignerTemplate(){}
        
        // 属性单例
        public static ViewControllerDesignerTemplate Instance => SingletonProperty<ViewControllerDesignerTemplate>.Instance;

        /// <summary>
        /// 文件全名
        /// </summary>
        public override string FullPath => scriptPath + "/{0}.Designer.cs".Format(scriptName);

        /// <summary>
        /// 拼接字符串
        /// </summary>
        protected override string BuildScript()
        {
            StringBuilder sb = new StringBuilder();
            
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using IFramework.Core;");
            sb.AppendLine("using IFramework.Engine;");

            if (nameSpace.Equals(Constant.UIKIT_DEFAULT_NAMESPACE)) {
                sb.AppendLine("// 1.请在菜单：IFramework/UIKit Config 里设置默认命名空间");
                sb.AppendLine("// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改");
            }

            sb.AppendLine("namespace " + nameSpace);
            sb.AppendLine("{");
            sb.AppendLine("\tpublic partial class {0}".Format(scriptName));
            sb.AppendLine("\t{");

            foreach (BindInfo bindInfo in panelCodeInfo.BindInfoList) {
                sb.AppendLine("\t\tpublic {0} {1};".Format(bindInfo.BindScript.ComponentName, bindInfo.Name));
            }
            
            sb.AppendLine("\t}");
            sb.AppendLine("}");

            return sb.ToString();
        }
    }
}
