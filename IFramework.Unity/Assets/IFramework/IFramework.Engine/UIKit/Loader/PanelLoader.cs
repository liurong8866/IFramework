using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    public class PanelLoader : IPanelLoader
    {
        private ResourceLoader loader = ResourceLoader.Allocate();

        /// <summary>
        /// 加载Panel
        /// </summary>
        public IPanel LoadPanel(PanelSearcher searcher)
        {
            PanelLoader panelLoader = new PanelLoader();
            // 加载PanelPrefab
            GameObject panelPrefab = panelLoader.LoadPrefab(searcher.TypeName, searcher.AssetBundleName);
            // 实例化Prefab
            GameObject obj = Object.Instantiate(panelPrefab);
            // 获取UIPanel组件
            UIPanel panel = obj.GetComponent<UIPanel>();
            // 设置类加载器
            panel.As<IPanel>().Loader = panelLoader;
            return panel;
        }

        /// <summary>
        /// 加载Prefab
        /// </summary>
        public GameObject LoadPrefab(string assetName, string assetBundleName = null)
        {
            return assetBundleName.NotEmpty() ? loader.Load<GameObject>(assetName, assetBundleName) : loader.Load<GameObject>(assetName);
        }

        /// <summary>
        /// 卸载资源
        /// </summary>
        public void Unload()
        {
            loader.Recycle();
            loader = null;
        }
    }
}
