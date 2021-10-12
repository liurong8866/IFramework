using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Example.UI
{
	public class PlayerData : UIPanelData { }

	public partial class Player : UIPanel
	{
		protected override void OnInit(IData data = null)
		{
		this.data = data as PlayerData ?? new PlayerData();
		}

		protected override void OnOpen(IData data = null) { }

		protected override void OnShow() { }

		protected override void OnHide() { }

		protected override void OnClose() { }
	}
}
