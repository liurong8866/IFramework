/* 脚本自动生成于：2021-10-17 23:38:50 ，请勿修改！*/

using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Test
{
	public partial class UISectionPanel
	{
		public const string Name = "UISectionPanel";

		private UISectionPanelData panelData = null;

		[SerializeField] public Button BackBtn;

		[SerializeField] public Button SettingBtn;

		protected override void OnInitData(IData data)
		{
			Data = data as UISectionPanelData ?? new UISectionPanelData();
		}

		protected override void ClearUIComponents()
		{
			BackBtn = null;
			SettingBtn = null;
			Data = null;
		}

		public UISectionPanelData Data
		{
			get { return panelData ??= new UISectionPanelData(); }
			set { panelData = value; data = value; }
		}
	}
}
