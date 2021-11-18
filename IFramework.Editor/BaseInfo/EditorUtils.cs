using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using IFramework.Core;
using IFramework.Engine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace IFramework.Editor
{
    public static class EditorUtils
    {
        
        /// <summary>
        /// 根据当前配置列表获取打包平台
        /// </summary>
        public static BuildTarget CurrentBuildPlatform {
            get {
                switch (Configure.CurrentPlatform.Value) {
                    case 0: return BuildTarget.StandaloneWindows;
                    case 1: return BuildTarget.StandaloneOSX;
                    case 2: return BuildTarget.iOS;
                    case 3: return BuildTarget.Android;
                    case 4: return BuildTarget.WebGL;
                    case 5: return BuildTarget.PS4;
                    case 6: return BuildTarget.PS5;
                    case 7: return BuildTarget.XboxOne;
                    default: return BuildTarget.StandaloneWindows;
                }
            }
        }

        
        /// <summary>
        /// 获取鼠标选择的路径
        /// </summary>
        public static string SelectedAssetsPath()
        {
            string path = string.Empty;
            foreach (Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets)) {
                path = AssetDatabase.GetAssetPath(obj);
                if (path.NotEmpty() && FileUtils.Exists(path)) {
                    return path;
                }
            }
            return path;
        }
        
        public static List<string> SelectedAssetsPaths()
        {
            List<string> pathList = new List<string>();
            foreach (Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets)) {
                string path = AssetDatabase.GetAssetPath(obj);
                if (path.NotEmpty()) {
                    pathList.Add(path);
                }
            }
            return pathList;
        }

        /// <summary>
        /// 获取鼠标选择的资源
        /// </summary>
        public static Object SelectedAssetsObject()
        {
            foreach (Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets)) {
                string path = AssetDatabase.GetAssetPath(obj);
                if (path.NotEmpty() && FileUtils.Exists(path)) {
                    return obj;
                }
            }
            return null;
        }
        
        /// <summary>
        /// 获取鼠标选择的路径
        /// </summary>
        public static Object SelectedObject()
        {
            return Selection.activeObject;
        }

        /// <summary>
        /// 获取父节点到当前节点的相对路径
        /// </summary>
        public static string PathToParent(Transform trans, string parentName)
        {
            StringBuilder sb = new StringBuilder(trans.name);

            // 循环直到父节点
            while (trans.parent != null) {
                if (trans.parent.name.Equals(parentName)) {
                    break;
                }
                // 组成路径
                sb.Insert(0, trans.parent.name + "/");
                // 向上循环
                trans = trans.parent;
            }
            return sb.ToString();
        }

        /// <summary>
        /// 保存Prefab
        /// </summary>
        public static Object SavePrefab(GameObject gameObject, string assetPath)
        {
        // #if UNITY_2018_3_OR_NEWER
            GameObject go = PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, assetPath, InteractionMode.AutomatedAction);
            PrefabUtility.SavePrefabAsset(go);
            return go;
        // #else
        //     return PrefabUtility.CreatePrefab(assetPath, gameObject, ReplacePrefabOptions.ConnectToPrefab);
        // #endif
        }

        /// <summary>
        /// 标记场景未保存
        /// </summary>
        public static void MarkCurrentSceneDirty()
        {
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }

        /// <summary>
        /// 找到当前Scene所有跟节点
        /// </summary>
        public static GameObject[] GetRootGameObjects()
        {
            return SceneManager.GetActiveScene().GetRootGameObjects();
        }

        /// <summary>
        /// 判断是否 ViewController
        /// </summary>
        public static bool IsViewController(this Component component)
        {
            return component.GetComponent<ViewController>();
        }

        /// <summary>
        /// 判断是否 UIPanle
        /// </summary>
        public static bool IsUIPanel(this Component component)
        {
            return component.GetComponent<UIPanel>();
        }

        public static bool IsPrefab(this GameObject gameObject)
        {
            // 如果找到资源路径，说明是Prefab
            string assetPath = AssetDatabase.GetAssetPath(gameObject);
            return assetPath.NotEmpty();
        }

        /// <summary>
        /// 清空Missing脚本
        /// </summary>
        public static void ClearMissing(GameObject[] gameObjects, Action<GameObject> onCleared = null)
        {
            foreach (GameObject go in gameObjects) {
                // 递归查找所有子节点
                go.transform.ActionRecursion(trans => {
                    // 通过Component组件，找到Missing的组件
                    Component[] components = trans.gameObject.GetComponents<Component>();

                    // 创建SerializedObject对象，用于修改Component
                    SerializedObject serializedObject = new SerializedObject(trans);
                    bool hasMiss = false;

                    // 循环所有组件
                    for (int i = components.Length - 1; i >= 0; i--) {
                        // 丢失脚本
                        if (components[i] == null) {
                            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(trans.gameObject);
                            hasMiss = true;
                        }
                    }
                    if (hasMiss) {
                        // 唤醒事件
                        onCleared.InvokeSafe(trans);
                        // 提交修改
                        serializedObject.ApplyModifiedProperties();
                        EditorUtility.SetDirty(trans);
                    }
                });
            }
        }

        /// <summary>
        /// 清除空格
        /// </summary>
        public static string FormatName(this string value)
        {
            return value.Replace(" ", "").Replace("　", "").Replace("@", "_").Replace("!", "_").Replace("-", "_");
        }

        /// <summary>
        /// 查找当前Bind所属的Element或者ViewController
        /// </summary>
        public static GameObject GetBindBelongsTo(Bind bind) {
            Transform trans = bind.Transform;

            while (trans.parent != null) {
                if (trans.parent.IsViewController() || trans.parent.IsUIPanel()) {
                    return trans.parent.gameObject;
                }
                trans = trans.parent;
            }
            return bind.gameObject;
        }
        
        /// <summary>
        /// 保留N位小数
        /// </summary>
        public static Vector2 Round(Vector2 value, int decimalPoint = 0)
        {
            Vector2 value1 = new Vector2(value.x, value.y);
            
            int scale = 1;
            for (int i = 0; i < decimalPoint; i++) {
                scale *= 10;
            }
            value1 *= scale;
            value1.x = Mathf.RoundToInt(value1.x);
            value1.y = Mathf.RoundToInt(value1.y);
            return value1 / scale;
        }
        
        /// <summary>
        /// 保留N位小数
        /// </summary>
        public static Vector3 Round(Vector3 value, int decimalPoint = 0)
        {
            int scale = 1;
            for (int i = 0; i < decimalPoint; i++) {
                scale *= 10;
            }
            value *= scale;
            value.x = Mathf.RoundToInt(value.x);
            value.y = Mathf.RoundToInt(value.y);
            value.z = Mathf.RoundToInt(value.z);
            return value / scale;
        }
    }
}
