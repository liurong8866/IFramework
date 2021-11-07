using IFramework.Core;

namespace IFramework.Engine
{
    public enum PanelOpenType
    {
        Single,
        Multiple
    }

    public class PanelSearcher : IPoolable, IRecyclable
    {
        public string Key;

        public string PanelId;
        
        public string PanelName;

        public string AssetBundleName;

        public UILevel Level = UILevel.Common;

        public IData Data;

        public IPanel Panel;

        public PanelOpenType OpenType = PanelOpenType.Single;

        public static PanelSearcher Allocate()
        {
            return ObjectPool<PanelSearcher>.Instance.Allocate();
        }

        public void OnRecycled()
        {
            Key = null;
            PanelId = null;
            PanelName = null;
            AssetBundleName = null;
            Data = null;
            Panel = null;
        }

        public bool IsRecycled { get; set; }

        public override string ToString()
        {
            return $"PanelSearchKeys Key:{Key} " + $"PanelId:{PanelId} " + $"TypeName:{PanelName} "  + $"AssetBundleName:{AssetBundleName} " + $"Level:{Level} " + $"UIData:{Data}";
        }

        public void Recycle()
        {
            ObjectPool<PanelSearcher>.Instance.Recycle(this);
        }
    }
}
