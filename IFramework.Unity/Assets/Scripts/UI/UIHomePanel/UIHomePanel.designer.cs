/* 脚本自动生成于：2021-10-15 19:05:27 ，请勿修改！*/

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

		[SerializeField] public UnityEngine.UI.Button GameStart;

		[SerializeField] public UnityEngine.UI.Button GameEnd;

		protected override void ClearUIComponents()
		{
			GameStart = null;
			GameEnd = null;
			Data = null;
		}

		public UIHomePanelData Data
		{
			get { return uIHomePanelData ??= new UIHomePanelData(); }
			set { uIHomePanelData = value; data = value; }
		}
	}
}
