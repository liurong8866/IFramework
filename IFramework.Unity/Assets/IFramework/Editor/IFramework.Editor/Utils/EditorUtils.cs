using System;
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
        public static string CurrentSelectPath => Selection.activeObject == null ? null : AssetDatabase.GetAssetPath(Selection.activeObject);

        /// <summary>
        /// 获取鼠标选择的路径
        /// </summary>
        public static string SelectedPath()
        {
            string path = string.Empty;
            foreach (Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets)) {
                path = AssetDatabase.GetAssetPath(obj);
                if (!string.IsNullOrEmpty(path) && File.Exists(path)) {
                    return path;
                }
            }
            return path;
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
        #if UNITY_2018_3_OR_NEWER
            GameObject go = PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, assetPath, InteractionMode.AutomatedAction);
            PrefabUtility.SavePrefabAsset(go);
            return go;
        #else
            return PrefabUtility.CreatePrefab(assetPath, gameObject, ReplacePrefabOptions.ConnectToPrefab);
        #endif
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
                    SerializedObject serializedObject = new SerializedObject(go);
                    bool hasMiss = false;

                    // 循环所有组件
                    for (int i = components.Length - 1; i >= 0; i--) {
                        // 丢失脚本
                        if (components[i] == null) {
                            GameObjectUtility.RemoveMonoBehavioursWithMissingScript(go);
                            hasMiss = true;
                        }
                    }
                    if (hasMiss) {
                        // 唤醒事件
                        onCleared.InvokeSafe(go);
                        // 提交修改
                        serializedObject.ApplyModifiedProperties();
                        EditorUtility.SetDirty(go);
                    }
                });
            }
        }

        /// <summary>
        /// 清除空格
        /// </summary>
        public static string FormatName(this string name)
        {
            return name.Replace(" ", "").Replace("　", "").Replace("-", "_");
        }

        /// <summary>
        /// 查找当前Bind所属的Element或者ViewController
        /// </summary>
        public static GameObject GetBindBelongsTo(AbstractBind bind) {
            Transform trans = bind.Transform;

            while (trans.parent != null) {
                if (trans.parent.IsViewController() || trans.parent.IsUIPanel()) {
                    return trans.parent.gameObject;
                }
                trans = trans.parent;
            }
            return bind.gameObject;
        }
    }
}
