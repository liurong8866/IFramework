using System.Linq;
using IFramework.Core;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace IFramework.Editor
{
    public class UIKit
    {
        /// <summary>
        /// 绑定Bind脚本
        /// </summary>
        public static void AddBindScript()
        {
            foreach (GameObject go in Selection.objects.OfType<GameObject>()) {
                if (go != null) { AddScript<Bind>(go); }
            }
        }

        /// <summary>
        /// 绑定ViewController
        /// </summary>
        public static void AddViewScript()
        {
            // 取选中对象的第一个
            GameObject go = Selection.objects.First() as GameObject;

            if (go == null) {
                Log.Warning("请选择 GameObject");
                return;
            }
            AddScript<ViewController>(go);
        }

        private static void AddScript<T>(GameObject go) where T : Component
        {
            go.AddComponentSafe<T>();
            EditorUtility.SetDirty(go);
            EditorSceneManager.MarkSceneDirty(go.scene);
        }

        /// <summary>
        /// 打开UIKit设置窗口
        /// </summary>
        public static void OpenUIConfigWindow()
        {
            UIConfigWindow.Open();
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        public static void ViewControllerGenerate()
        {
            ViewControllerScript.GenerateCode();
        }

        /// <summary>
        /// 恢复默认设置
        /// </summary>
        public static void ResetConfig()
        {
            // 生成脚本默认命名空间
            Configure.DefaultNameSpace.Value = Constant.UIKIT_DEFAULT_NAMESPACE;

            // UI脚本生成路径
            Configure.UIScriptPath.Value = Constant.UIKIT_UI_SCRIPT_PATH;

            // UI Prefab 生成路径
            Configure.UIPrefabPath.Value = Constant.UIKIT_UI_PREFAB_PATH;

            // ViewController 脚本生成路径
            Configure.ViewControllerScriptPath.Value = Constant.UIKIT_UI_VC_SCRIPT_PATH;

            // ViewController Prefab 生成路径
            Configure.ViewControllerPrefabPath.Value = Constant.UIKIT_UI_VC_PREFAB_PATH;
        }
    }
}
