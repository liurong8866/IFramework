/* 脚本自动生成于：2021-10-17 23:38:23 ，请勿修改！*/

using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Test
{
	public partial class UIABCPanel
	{
		public const string Name = "UIABCPanel";

		private UIABCPanelData panelData = null;

		[SerializeField] public Button Button;

		protected override void OnInitData(IData data)
		{
			Data = data as UIABCPanelData ?? new UIABCPanelData();
		}

		protected override void ClearUIComponents()
		{
			Button = null;
			Data = null;
		}

		public UIABCPanelData Data
		{
			get { return panelData ??= new UIABCPanelData(); }
			set { panelData = value; data = value; }
		}
	}
}
