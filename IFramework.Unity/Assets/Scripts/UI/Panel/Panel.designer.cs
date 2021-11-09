/* 脚本自动生成于：2021-11-08 20:17:18 ，请勿修改！*/

using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Test
{
	public partial class Panel
	{
		public const string Name = "Panel";

		private PanelData panelData = null;

		protected override void OnInitData(IData data)
		{
			Data = data as PanelData ?? new PanelData();
		}

		protected override void ClearUIComponents()
		{
			Data = null;
		}

		public PanelData Data
		{
			get { return panelData ??= new PanelData(); }
			set { panelData = value; data = value; }
		}
	}
}
