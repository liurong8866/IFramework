using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// 面板接口
    /// </summary>
    public interface IPanel
    {
        Transform Transform { get; }

        IPanelLoader Loader { get; set; }

        PanelInfo Info { get; set; }

        PanelState State { get; set; }

        void Init(IData data = null);

        void Open(IData data = null);

        void Show();

        void Hide();

        void Close(bool destroy = true);
    }
}
