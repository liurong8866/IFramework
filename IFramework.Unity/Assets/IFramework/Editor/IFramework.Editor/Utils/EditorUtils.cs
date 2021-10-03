using System.IO;
using UnityEditor;
using UnityEngine;

namespace IFramework.Editor
{
    public static class EditorUtils
    {
        public static string GetSelectedPath()
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
    }
}
