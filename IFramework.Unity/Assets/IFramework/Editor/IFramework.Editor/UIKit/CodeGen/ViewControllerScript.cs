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
            // 生成脚本
            Log.Info("生成脚本: 开始");
            ViewController controller = go.GetComponent<ViewController>();
            
            RootControllerInfo rootControllerInfo = new RootControllerInfo {
                GameObjectName = controller.name
            };

            // 搜索所有绑定对象
            BindCollector.SearchBind(go.transform, "", rootControllerInfo);

            // 生成Controller层
            ViewControllerTemplate.Instance.Generate(controller);
            // 生成Model层
            ViewControllerDesignerTemplate.Instance.Generate(controller, rootControllerInfo);
            
            Log.Info("生成脚本: 完成");
        }
    }
}
