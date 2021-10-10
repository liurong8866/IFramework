using System;
using System.Text;
using IFramework.Core;

namespace IFramework.Editor
{
    public class ViewControllerDesignerTemplate : AbstractTemplate
    {
        private ViewControllerDesignerTemplate() { }

        // 属性单例
        public static ViewControllerDesignerTemplate Instance => SingletonProperty<ViewControllerDesignerTemplate>.Instance;

        /// <summary>
        /// 文件全名
        /// </summary>
        public override string FullName => controller.ScriptAssetsDesignerName;

        /// <summary>
        /// 拼接字符串
        /// </summary>
        protected override string BuildScript()
        {
            StringBuilder sb = new StringBuilder();
            // 确保每次生成文件都编译
            sb.AppendLine("/* auto generate at {0} */".Format(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            sb.AppendLine();
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine("using IFramework.Core;");
            sb.AppendLine("using IFramework.Engine;");
            sb.AppendLine();

            if (controller.Namespace.Equals(Constant.UIKIT_DEFAULT_NAMESPACE)) { sb.AppendLine("// 请在菜单：IFramework/UIKit Config 中设置默认命名空间"); }
            sb.AppendLine("namespace " + controller.Namespace);
            sb.AppendLine("{");
            sb.AppendLine("\tpublic partial class {0}".Format(controller.ScriptName));
            sb.AppendLine("\t{");

            // 循环设置字段
            foreach (BindInfo bindInfo in rootControllerInfo.BindInfoList) {
                if (bindInfo.BindScript.Comment.NotEmpty()) {
                    // 添加注释
                    sb.AppendLine("\t\t// " + bindInfo.BindScript.Comment);
                }
                sb.AppendLine("\t\tpublic {0} {1};".Format(bindInfo.BindScript.ComponentName, bindInfo.Name));
                sb.AppendLine();
            }
            sb.AppendLine("\t}");
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
