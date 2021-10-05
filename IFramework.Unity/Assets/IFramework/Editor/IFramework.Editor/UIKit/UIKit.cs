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
        public static void BindScript()
        {
            foreach (GameObject o in Selection.objects.OfType<GameObject>()) {
                if (o) {
                    o.AddComponentSafe<Bind>();
                    EditorUtility.SetDirty(o);
                    EditorSceneManager.MarkSceneDirty(o.scene);
                }
            }
        }

        /// <summary>
        /// 绑定Bind脚本
        /// </summary>
        public static void OpenUIConfigWindow()
        {
            UIConfigWindow.Open();
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