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
        /// 数据
        /// </summary>
        public IData Data { get; set; }

        /// <summary>
        /// 面板级别
        /// </summary>
        public UILevel Level { get; set; } = UILevel.Common;

        /// <summary>
        /// 面板类型
        /// </summary>
        public Type PanelType;

        /// <summary>
        /// 游戏对象名称
        /// </summary>
        public string GameObjectName;

        /// <summary>
        /// AssetBundle名称
        /// </summary>
        public string AssetBundleName { get; set; }

        /// <summary>
        /// 申请获取PanelInfo
        /// </summary>
        /// <param name="gameObjectName">游戏对象名称</param>
        /// <param name="level">面板层级</param>
        /// <param name="data">面板数据</param>
        /// <param name="panelType">面板类型</param>
        /// <param name="assetBundleName">AssetBundle资源名称</param>
        public static PanelInfo Allocate(string gameObjectName, UILevel level, IData data, Type panelType, string assetBundleName)
        {
            PanelInfo panelInfo = ObjectPool<PanelInfo>.Instance.Allocate();
            panelInfo.GameObjectName = gameObjectName;
            panelInfo.Level = level;
            panelInfo.Data = data;
            panelInfo.PanelType = panelType;
            panelInfo.AssetBundleName = assetBundleName;
            return panelInfo;
        }

        public void OnRecycled()
        {
            Data = null;
            AssetBundleName = null;
            GameObjectName = null;
            PanelType = null;
        }

        public bool IsRecycled { get; set; }

        public void Recycle()
        {
            ObjectPool<PanelInfo>.Instance.Recycle(this);
        }
    }
}
