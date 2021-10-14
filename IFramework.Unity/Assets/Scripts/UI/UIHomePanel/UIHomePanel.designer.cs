/* 脚本自动生成于：2021-10-15 00:01:24 ，请勿修改！*/

using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Example.UI
{
	public partial class UIHomePanel
	{
		public const string Name = "UIHomePanel";

		private UIHomePanelData uIHomePanelData = null;

		[SerializeField] public UnityEngine.UI.Button ButtonStart;

		[SerializeField] public UnityEngine.UI.Button ButtonEnd;

		[SerializeField] public UnityEngine.UI.Button ButtonAbout;

		protected override void ClearUIComponents()
		{
			ButtonStart = null;
			ButtonEnd = null;
			ButtonAbout = null;
			Data = null;
		}

		public UIHomePanelData Data
		{
			get { return uIHomePanelData ??= new UIHomePanelData(); }
			set { uIHomePanelData = value; data = value; }
		}
	}
}
