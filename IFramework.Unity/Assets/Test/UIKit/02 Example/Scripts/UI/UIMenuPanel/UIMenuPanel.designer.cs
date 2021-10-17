/* 脚本自动生成于：2021-10-17 23:56:54 ，请勿修改！*/

using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Test
{
	public partial class UIMenuPanel
	{
		public const string Name = "UIMenuPanel";

		private UIMenuPanelData panelData = null;

		[SerializeField] public Image ImageBg;
		[SerializeField] public Button BtnPlay;
		[SerializeField] public Button BtnSetting;
		protected override void OnInitData(IData data)
		{
			Data = data as UIMenuPanelData ?? new UIMenuPanelData();
		}

		protected override void ClearUIComponents()
		{
			ImageBg = null;
			BtnPlay = null;
			BtnSetting = null;
			Data = null;
		}

		public UIMenuPanelData Data
		{
			get { return panelData ??= new UIMenuPanelData(); }
			set { panelData = value; data = value; }
		}
	}
}
