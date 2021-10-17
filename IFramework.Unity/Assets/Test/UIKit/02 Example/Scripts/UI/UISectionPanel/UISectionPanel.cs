using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Test
{
	public class UISectionPanelData : UIPanelData { }

	public partial class UISectionPanel : UIPanel
	{
		protected override void OnInit()
		{
            SettingBtn.onClick.AddListener(() => { UIKit.OpenPanel<UISettingPanel>(UILevel.Popup); });

            BackBtn.onClick.AddListener(() => {
                CloseSelf();
                UIKit.OpenPanel<UIMenuPanel>(UILevel.Common);
            });
		}

		protected override void OnOpen(IData data) { }

		protected override void OnShow() { }

		protected override void OnHide() { }

		protected override void OnClose() { }
	}
}
