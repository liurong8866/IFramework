using System;
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
        public Type PanelType;

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
            PanelType = null;
            AssetBundleName = null;
            GameObjectName = null;
            Data = null;
            Panel = null;
        }

        public bool IsRecycled { get; set; }

        public override string ToString()
        {
            return $"PanelSearchKeys PanelType:{PanelType} "
                  + $"AssetBundleName:{AssetBundleName} "
                  + $"GameObjName:{GameObjectName} "
                  + $"Level:{Level} "
                  + $"UIData:{Data}";
        }

        public void Recycle()
        {
            ObjectPool<PanelSearcher>.Instance.Recycle(this);
        }
    }
}
