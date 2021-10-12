using IFramework.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace IFramework.Editor
{
    /// <summary>
    /// UIPanel 代码生成器
    /// </summary>
    public class UIPanelGenerator
    {
        /// <summary>
        /// 生成脚本
        /// </summary>
        public static void GenerateCode()
        {
            Object[] objects = Selection.GetFiltered(typeof(GameObject), SelectionMode.Assets | SelectionMode.TopLevel);
            GenerateCode(objects);
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        public static void GenerateCode(Object[] objects)
        {
            bool displayProgress = objects.Length > 1;

            if (displayProgress) {
                EditorUtility.DisplayProgressBar("", "正在生成 UI Code ...", 0);
            }

            for (int i = 0; i < objects.Length; i++) {
                GenerateCode(objects[i] as GameObject, AssetDatabase.GetAssetPath(objects[i]));

                if (displayProgress) {
                    EditorUtility.DisplayProgressBar("", "正在生成 UI Code ...", (float)(i + 1) / objects.Length);
                }
            }
            AssetDatabase.Refresh();

            if (displayProgress) {
                EditorUtility.ClearProgressBar();
            }
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        private static void GenerateCode(GameObject obj, string prefabPath)
        {
            if (obj == null) return;

            // 如果不是Prefab，则退出
            PrefabAssetType prefabType = PrefabUtility.GetPrefabAssetType(obj);

            if (prefabType == PrefabAssetType.NotAPrefab) {
                return;
            }

            // 实例化Prefab
            GameObject clone = PrefabUtility.InstantiatePrefab(obj) as GameObject;
            if (clone == null) return;

            RootPanelInfo rootPanelInfo = new RootPanelInfo {
                GameObjectName = clone.name.Replace("(clone)", string.Empty)
            };
            // 查询所有Bind
            BindCollector.SearchBind(clone.transform, "", rootPanelInfo);

            // 根据Prefab路径获取Script生成路径
            UIPanelGenerateInfo panelGenerateInfo = new UIPanelGenerateInfo() {
                Namespace = Configure.DefaultNameSpace.Value,
                ScriptName = obj.name,
                ScriptPath = prefabPath.Left(),
            };
            // 如果存在文件，则不覆盖
            UIPanelTemplate.Instance.Generate(panelGenerateInfo, rootPanelInfo, false);
            
            // 生成 designer.cs
            UIPanelDesignerTemplate.Instance.Generate(panelGenerateInfo, rootPanelInfo, true);
            
            // UISerializer.StartAddComponent2PrefabAfterCompile(obj);
            Object.DestroyImmediate(clone);
        }

        /// <summary>
        /// 生成UIPanel代码
        /// </summary>
        private static void GenerateUIPanelCode(GameObject prefab, string prefabPath, RootPanelInfo rootPanelInfo)
        {
            
        }
    }
}
