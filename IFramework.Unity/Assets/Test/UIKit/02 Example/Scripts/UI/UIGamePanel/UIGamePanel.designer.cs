/* 脚本自动生成于：2021-10-17 23:38:33 ，请勿修改！*/

using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Test
{
	public partial class UIGamePanel
	{
		public const string Name = "UIGamePanel";

		private UIGamePanelData panelData = null;

		[SerializeField] public Text gameText;

		[SerializeField] public Button backBtn;

		protected override void OnInitData(IData data)
		{
			Data = data as UIGamePanelData ?? new UIGamePanelData();
		}

		protected override void ClearUIComponents()
		{
			gameText = null;
			backBtn = null;
			Data = null;
		}

		public UIGamePanelData Data
		{
			get { return panelData ??= new UIGamePanelData(); }
			set { panelData = value; data = value; }
		}
	}
}
