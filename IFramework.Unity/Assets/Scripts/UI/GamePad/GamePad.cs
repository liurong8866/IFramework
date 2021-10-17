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
		protected override void OnInit(IData data = null)
		{
			Data = data as GamePadData ?? new GamePadData();
            GamePadText.text = Data.Index.ToString();
            ButtonClose.onClick.AddListener(() => {
                UIKit.ClosePanel(this);
            });
        }

		protected override void OnOpen(IData data = null) { }

		protected override void OnShow() { }

		protected override void OnHide() { }

		protected override void OnClose() { }
	}
}
