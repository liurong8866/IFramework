using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Test
{
	public class PanelData : UIPanelData { }

	public partial class Panel : UIPanel
	{
		protected override void OnInit()
		{
		}

		protected override void OnOpen(IData data) { }

		protected override void OnShow() { }

		protected override void OnHide() { }

		protected override void OnClose() { }
	}
}
