using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Test
{
	public class UISettingPanelData : UIPanelData { }

	public partial class UISettingPanel : UIPanel
	{
		protected override void OnInit()
		{
            EventBtn.onClick.AddListener(() => {
                TypeEvent.Send<UIMenuPanelEvent>(new UIMenuPanelEvent {
                    EventType = "ffffff"
                });
            });
            BackBtn.onClick.AddListener(() => { CloseSelf(); });
            
            
            
            
            // this.SendMsg(new AudioMusicMsg(
            //                                "GameBg",
            //                                loop: false,
            //                                allowMusicOff: false,
            //                                onMusicBeganCallback: () => { Debug.Log("Music Start"); },
            //                                onMusicEndedCallback: () => { Debug.Log("MusicEnd"); })
            //             );
            // this.SendMsg(new AudioMusicMsg("HomeBg"));
            //
		}

		protected override void OnOpen(IData data) { }

		protected override void OnShow() { }

		protected override void OnHide() { }

		protected override void OnClose() { }
	}
}
