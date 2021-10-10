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

    public static class IPanelExtention
    {
        /// <summary>
        /// 设置Panel层级
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="level"></param>
        public static void PanelLevel(this IPanel panel, UILevel level)
        {
            UIRoot.Instance.SetPanelLevel(panel, level);
        }
        
        /// <summary>
        /// 重置面板默认大小
        /// </summary>
        public static void ResetPanelSize(this IPanel panel)
        {
            UIKit.ResetPanelSize(panel);
        }
    }
}
