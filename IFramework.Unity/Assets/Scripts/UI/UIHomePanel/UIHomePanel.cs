using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Example.UI
{
    public class UIHomePanelData : UIPanelData { }

    public partial class UIHomePanel : UIPanel
    {
        protected override void OnInit(IData data = null)
        {
            this.data = data as UIHomePanelData ?? new UIHomePanelData();
            GameStart.onClick.AddListener(() => {
                Log.Info("开始游戏");
            });
            
        }

        protected override void OnOpen(IData data = null) { }

        protected override void OnShow() { }

        protected override void OnHide() { }

        protected override void OnClose() { }
    }
}
