using System.IO;
using System.Text;
using IFramework.Core;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace IFramework.Editor
{
    public static class EditorUtils
    {
        /// <summary>
        /// 获取鼠标选择的路径
        /// </summary>
        public static string SelectedPath()
        {
            string path = string.Empty;

            foreach (Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets)) {
                path = AssetDatabase.GetAssetPath(obj);

                if (!string.IsNullOrEmpty(path) && File.Exists(path)) { return path; }
            }
            return path;
        }

        public static string CurrentSelectPath => Selection.activeObject == null ? null : AssetDatabase.GetAssetPath(Selection.activeObject);

        /// <summary>
        /// 获取父节点到当前节点的相对路径
        /// </summary>
        public static string PathToParent(Transform trans, string parentName)
        {
            StringBuilder sb = new StringBuilder(trans.name);

            // 循环直到父节点
            while (trans.parent != null) {
                if (trans.parent.name.Equals(parentName)) { break; }
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
            return PrefabUtility.SaveAsPrefabAssetAndConnect(gameObject, assetPath, InteractionMode.AutomatedAction);
        #else
            return PrefabUtility.CreatePrefab(assetPath, gameObject, ReplacePrefabOptions.ConnectToPrefab);
        #endif
        }
        
        /// <summary>
        /// 标记场景未保存
        /// </summary>
        public static void MarkCurrentSceneDirty() {
            EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }

        /// <summary>
        /// 
        /// </summary>
        public static void CombinFilePath()
        {
            
        }
        
        /// <summary>
        /// 判断是否 ViewController
        /// </summary>
        public static bool IsViewController(this Component component)
        {
            return component.GetComponent<ViewController>();
        }

    }
}
