using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Test
{
    public class GamePadData : UIPanelData
    {
        public int Index;
    }

	public partial class GamePad : UIPanel
	{
		protected override void OnInit()
		{
		}

		protected override void OnOpen(IData data = null) { }

		protected override void OnShow() { }

		protected override void OnHide() { }

		protected override void OnClose() { }
	}
}
