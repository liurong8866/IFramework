using IFramework.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace IFramework.Editor
{
    public class UIPanelScript
    {
        private static ScriptGenInfo mScriptKitInfo;
        
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
            GenerateUIPanelCode(obj, prefabPath, rootPanelInfo);
            CreateUIPanelDesignerCode(behaviourName, strFilePath, rootPanelInfo);
            UISerializer.StartAddComponent2PrefabAfterCompile(obj);
            HotScriptBind(obj);
            Object.DestroyImmediate(clone);
        }
        
        /// <summary>
        /// 生成UIPanel代码
        /// </summary>
        private void GenerateUIPanelCode(GameObject prefab, string prefabPath, RootPanelInfo rootPanelInfo) {
            
            if (null == prefab) return;

            string behaviourName = prefab.name;
            
            string filePath = EditorUtils.GenSourceFilePathFromPrefabPath(prefabPath, behaviourName);

            if (FileUtils.Exists(filePath) == false) {
                UIPanelTemplate.Write(behaviourName, filePath, UIKitSettingData.Load().Namespace, UIKitSettingData.Load());
            }
        }
        
        
        /// <summary>
        /// 热更新绑定
        /// </summary>
        private static void HotScriptBind(GameObject uiPrefab) {
            if (mScriptKitInfo.IsNotNull() && mScriptKitInfo.CodeBind.IsNotNull()) {
                mScriptKitInfo.CodeBind.Invoke(uiPrefab, mScriptKitInfo.HotScriptFilePath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
        }
    }
}
