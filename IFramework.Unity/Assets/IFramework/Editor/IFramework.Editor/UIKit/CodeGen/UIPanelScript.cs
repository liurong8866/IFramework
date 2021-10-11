using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace IFramework.Editor
{
    public class UIPanelScript
    {
        /// <summary>
        /// 生成脚本
        /// </summary>
        public static void GenerateCode()
        {
            Object[] objects = Selection.GetFiltered(typeof(GameObject), SelectionMode.Assets | SelectionMode.TopLevel);

            GenerateCode(objects);
        }

        public static void GenerateCode(Object[] objects)
        {
            
        }
    }
}
