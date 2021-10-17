/* 脚本自动生成于：2021-10-17 23:38:59 ，请勿修改！*/

using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Test
{
	public partial class UISettingPanel
	{
		public const string Name = "UISettingPanel";

		private UISettingPanelData panelData = null;

		[SerializeField] public Button EventBtn;

		[SerializeField] public Button BackBtn;

		protected override void OnInitData(IData data)
		{
			Data = data as UISettingPanelData ?? new UISettingPanelData();
		}

		protected override void ClearUIComponents()
		{
			EventBtn = null;
			BackBtn = null;
			Data = null;
		}

		public UISettingPanelData Data
		{
			get { return panelData ??= new UISettingPanelData(); }
			set { panelData = value; data = value; }
		}
	}
}
