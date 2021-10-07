using System.IO;
using System.Text;
using IFramework.Core;
using UnityEditor;
using UnityEngine;

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
                sb.Insert(0, trans.parent.name.Append("/"));
                // 向上循环
                trans = trans.parent;
            }
            return sb.ToString();
        }

        /// <summary>
        /// 判断是否 ViewController
        /// </summary>
        public static bool IsViewController(this Component component)
        {
            if (!component.GetComponent<ViewController>()) return false;

            return true;
        }
    }
}
