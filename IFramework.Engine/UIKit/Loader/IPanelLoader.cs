using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// 面板加载器接口
    /// </summary>
    public interface IPanelLoader
    {
        /// <summary>
        /// 加载IPanel Prefab
        /// </summary>
        GameObject LoadPrefab(string assetName, string assetBundleName = null);

        /// <summary>
        /// 卸载资源
        /// </summary>
        void Unload();
    }
}
