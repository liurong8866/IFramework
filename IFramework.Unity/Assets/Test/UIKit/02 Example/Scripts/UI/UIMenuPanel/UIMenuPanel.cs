using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Test
{
	public class UIMenuPanelData : UIPanelData { }

    public class UIMenuPanelEvent
    {
        public string EventType;
    }
    
	public partial class UIMenuPanel : UIPanel
	{
		protected override void OnInit()
		{
            ImageBg.color = "#abababab".HtmlStringToColor();
            this.Delay(0.1f, () => { UIKit.GetPanel<UIMenuPanel>().LogInfo(); });
            
            // 注册事件
            TypeEvent.Register<UIMenuPanelEvent>(data => {
                Log.Info("变更颜色" + data.EventType);
            });
            
            BtnPlay.onClick.AddListener(() => {
                UIKit.OpenPanel<UISectionPanel>(UILevel.Common);
                // this.DoTransition<UISectionPanel>(new FadeInOut(), UILevel.Common,
                // prefabName: "Resources/UISectionPanel");
            });
            
            BtnSetting.onClick.AddListener(() => {
                UIKit.OpenPanel<UISettingPanel>(UILevel.Popup);
            });
        }

        protected override void OnOpen(IData data) { }

		protected override void OnShow() { }

		protected override void OnHide() { }

		protected override void OnClose() { }
	}
}
