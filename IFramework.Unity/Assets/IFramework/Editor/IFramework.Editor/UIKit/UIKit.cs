using System.Collections.Generic;
using System.Linq;
using IFramework.Core;
using IFramework.Engine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.VersionControl;
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
                if (go != null) {
                    AddScript<Bind>(go);
                }
            }
        }
        
        /// <summary>
        /// 绑定Bind脚本验证
        /// </summary>
        public static bool AddBindScriptValidate()
        {
            foreach (GameObject go in Selection.objects.OfType<GameObject>()) {
                if (go != null ) {
                    IBind bind = go.GetComponent<IBind>();
                    if (bind == null) {
                        return true;
                    }
                }
            }
            return false;
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

        public static void AddUIRootScript()
        {
            GameObject asset = Resources.Load<GameObject>("UIRoot");
            PrefabUtility.InstantiatePrefab(asset);
        }

        public static bool AddUIRootScriptValidate()
        {
            UIRoot uiRoot = GameObject.FindObjectOfType<UIRoot>();
            return uiRoot == null;
        }
        
        /// <summary>
        /// 打开UIKit设置窗口
        /// </summary>
        public static void OpenUIConfigWindow()
        {
            UIConfigWindow.Open();
        }

        /// <summary>
        /// 清除Miss
        /// </summary>
        public static void AssetRemoveMiss()
        {
            EditorUtils.ClearMissing(Selection.gameObjects);
        }
        
        // /// <summary>
        // /// 生成ViewController代码
        // /// </summary>
        // public static void ViewControllerGenerate()
        // {
        //     ViewControllerGenerator.GenerateCode(false);
        // }

        /// <summary>
        /// 生成UIPanel代码
        /// </summary>
        public static void UIPanelGenerate()
        {
            List<string> paths = EditorUtils.SelectedAssetsPaths();
            foreach (string path in paths) {
                // 如果没有标记AssetBundle，先标记
                if (!AssetBundleMark.CheckMarked(path)) {
                    AssetBundleMark.MarkAssetBundle(path);
                }
            }
            UIPanelGenerator.GenerateCode();
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
