using System;
using IFramework.Core;

namespace IFramework.Engine
{
    /// <summary>
    /// 面板信息类
    /// </summary>
    public class PanelInfo : IPoolable, IRecyclable
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 类名称(短)
        /// </summary>
        public string PanelName { get; set; }

        /// <summary>
        /// 游戏对象名称
        /// </summary>
        public string GameObjectName { get; set; }

        /// <summary>
        /// AssetBundle名称
        /// </summary>
        public string AssetBundleName { get; set; }

        /// <summary>
        /// 面板级别
        /// </summary>
        public UILevel Level { get; set; } = UILevel.Common;

        /// <summary>
        /// 数据
        /// </summary>
        public IData Data { get; set; }

        /// <summary>
        /// 从缓冲池申请对象
        /// </summary>
        /// <param name="id">唯一标识</param>
        /// <param name="panelName">面板名称</param>
        /// <param name="gameObjectName">游戏对象名称</param>
        /// <param name="level">面板层级</param>
        /// <param name="data">面板数据</param>
        /// <param name="assetBundleName">AssetBundle资源名称</param>
        public static PanelInfo Allocate(string id, string panelName, string gameObjectName, UILevel level, IData data, string assetBundleName)
        {
            PanelInfo panelInfo = ObjectPool<PanelInfo>.Instance.Allocate();
            panelInfo.Id = id;
            panelInfo.PanelName = panelName;
            panelInfo.GameObjectName = gameObjectName;
            panelInfo.Level = level;
            panelInfo.Data = data;
            panelInfo.AssetBundleName = assetBundleName;
            return panelInfo;
        }

        public void OnRecycled()
        {
            Id = null;
            PanelName = null;
            GameObjectName = null;
            AssetBundleName = null;
            Data = null;
            AssetBundleName = null;
        }

        public bool IsRecycled { get; set; }

        public void Recycle()
        {
            ObjectPool<PanelInfo>.Instance.Recycle(this);
        }
    }
}
