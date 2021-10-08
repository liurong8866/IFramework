using System.Text;
using IFramework.Core;

namespace IFramework.Editor
{
    public class ViewControllerTemplate : AbstractTemplate
    {
        private ViewControllerTemplate() { }

        // 属性单例
        public static ViewControllerTemplate Instance => SingletonProperty<ViewControllerTemplate>.Instance;

        /// <summary>
        /// 是否覆盖文件
        /// </summary>
        protected override bool IsOverwritten => false;

        /// <summary>
        /// 文件全名
        /// </summary>
        public override string FullName => FullPath + "/{0}.cs".Format(controller.ScriptName);

        /// <summary>
        /// 拼接字符串
        /// </summary>
        protected override string BuildScript()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using IFramework.Core;");
            sb.AppendLine("using IFramework.Engine;");

            if (controller.Namespace.Equals(Constant.UIKIT_DEFAULT_NAMESPACE)) {
                sb.AppendLine("// 1.请在菜单 IFramework/UIKit Config 里设置默认命名空间");
                sb.AppendLine("// 2.命名空间更改后，生成代码之后，需要把逻辑代码文件（非 Designer）的命名空间手动更改");
            }
            sb.AppendLine("namespace " + controller.Namespace);
            sb.AppendLine("{");
            sb.AppendLine("\tpublic partial class {0} : ViewController".Format(controller.ScriptName));
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
