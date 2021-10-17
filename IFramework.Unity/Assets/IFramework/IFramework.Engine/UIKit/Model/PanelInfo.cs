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
        public string Key { get; set; }
        
        /// <summary>
        /// 面板ID
        /// </summary>
        public string PanelId { get; set; }
        
        /// <summary>
        /// 类名称(短)
        /// </summary>
        public string PanelName { get; set; }

        /// <summary>
        /// AssetBundle名称
        /// </summary>
        public string AssetBundleName { get; set; }

        /// <summary>
        /// 面板级别
        /// </summary>
        public UILevel Level { get; set; } = UILevel.Common;
        
        /// <summary>
        /// 打开类型
        /// </summary>
        public PanelOpenType OpenType { get; set; }
        
        /// <summary>
        /// 面板扩展数据
        /// </summary>
        public IData Data { get; set; }
        
        /// <summary>
        /// 从缓冲池申请对象
        /// </summary>
        public static PanelInfo Allocate(PanelSearcher searcher)
        {
            PanelInfo panelInfo = ObjectPool<PanelInfo>.Instance.Allocate();
            panelInfo.Key = searcher.Keyword;
            panelInfo.PanelId = searcher.PanelId;
            panelInfo.PanelName = searcher.TypeName;
            panelInfo.Level = searcher.Level;
            panelInfo.AssetBundleName = searcher.AssetBundleName;
            panelInfo.OpenType = searcher.OpenType;
            panelInfo.Data = searcher.Data;
            return panelInfo;
        }

        public void OnRecycled()
        {
            Key = null;
            PanelId = null;
            PanelName = null;
            AssetBundleName = null;
            AssetBundleName = null;
        }

        public bool IsRecycled { get; set; }

        public void Recycle()
        {
            ObjectPool<PanelInfo>.Instance.Recycle(this);
        }
    }
}
