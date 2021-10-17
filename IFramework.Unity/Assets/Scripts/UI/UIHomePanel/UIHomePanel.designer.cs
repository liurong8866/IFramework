/* 脚本自动生成于：2021-10-16 23:57:35 ，请勿修改！*/

using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Test
{
	public partial class UIHomePanel
	{
		public const string Name = "UIHomePanel";

		private UIHomePanelData panelData = null;

		[SerializeField] public Button ButtonStart;

		[SerializeField] public Button ButtonEnd;

		[SerializeField] public Login Login;

        protected override void OnInitData(IData data)
        {
            Data = data as UIHomePanelData ?? new UIHomePanelData();
        }

        protected override void ClearUIComponents()
		{
			ButtonStart = null;
			ButtonEnd = null;
			Login = null;
			Data = null;
		}

		public UIHomePanelData Data
		{
			get { return panelData ??= new UIHomePanelData(); }
			set { panelData = value; data = value; }
		}
	}
}
