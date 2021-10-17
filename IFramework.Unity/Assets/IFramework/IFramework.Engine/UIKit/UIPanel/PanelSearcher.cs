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
        public string Keyword;

        public string PanelId;
        
        public string TypeName;

        public string AssetBundleName;

        public string GameObjectName;

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
            Keyword = null;
            PanelId = null;
            TypeName = null;
            AssetBundleName = null;
            GameObjectName = null;
            Data = null;
            Panel = null;
        }

        public bool IsRecycled { get; set; }

        public override string ToString()
        {
            return $"PanelSearchKeys Keyword:{Keyword} " + $"PanelId:{PanelId} " + $"TypeName:{TypeName} "  + $"AssetBundleName:{AssetBundleName} " + $"GameObjName:{GameObjectName} " + $"Level:{Level} " + $"UIData:{Data}";
        }

        public void Recycle()
        {
            ObjectPool<PanelSearcher>.Instance.Recycle(this);
        }
    }
}
