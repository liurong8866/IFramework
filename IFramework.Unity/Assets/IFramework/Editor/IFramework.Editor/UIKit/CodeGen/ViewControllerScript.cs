using System.Linq;
using IFramework.Core;
using UnityEditor;
using UnityEngine;

namespace IFramework.Editor
{
    public class ViewControllerScript
    {
        /// <summary>
        /// 生成脚本
        /// </summary>
        public static void CreateCode()
        {
            GameObject go = Selection.objects.First() as GameObject;

            if (!go) {
                Log.Warning("需要选择 GameObject");
                return;
            }

            // 如果当前是Bind类型，并且不包含ViewController，则认为是子节点，向上查找
            if (go.GetComponent<AbstractBind>() && !go.GetComponent<ViewController>()) {
                ViewController parentController = go.GetComponentInParent<ViewController>();

                // 如果找到ViewController，则
                if (parentController) { go = parentController.gameObject; }
            }
            Log.Info("生成脚本: 开始");
            ViewController controller = go.GetComponent<ViewController>();
            string scriptsPath = Application.dataPath + "/Scripts";

            if (controller) { scriptsPath = controller.ScriptsPath; }

            // 创建文件夹，如果有则忽略
            DirectoryUtils.Create(scriptsPath);

            PanelCodeInfo panelCodeInfo = new PanelCodeInfo {
                GameObjectName = controller.name
            };

            // 搜索所有绑定对象
            BindCollector.SearchBind(go.transform, "", panelCodeInfo);

            // 生成Controller层
            ViewControllerTemplate.Instance.Generate(controller.Namespace, controller.ScriptName, scriptsPath);
            // 生成Model层
            ViewControllerDesignerTemplate.Instance.Generate(controller.Namespace, controller.ScriptName, scriptsPath, panelCodeInfo);
            
            Log.Info("生成脚本: 完成");
        }
    }
}
