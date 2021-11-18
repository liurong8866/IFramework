/* 脚本自动生成于：2021-11-18 11:25:56 ，请勿修改！*/

using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

// 请在菜单 IFramework/UIKit Config 里设置默认命名空间
namespace IFramework.Test.ResourceKit
{
	public partial class StartPanel
	{
		public const string Name = "StartPanel";

		private StartPanelData panelData = null;

		[SerializeField] public Button ButtonStart;
		protected override void OnInitData(IData data)
		{
			Data = data as StartPanelData ?? new StartPanelData();
		}

		protected override void ClearUIComponents()
		{
            ButtonStart = null;
			Data = null;
		}

		public StartPanelData Data
		{
			get { return panelData ??= new StartPanelData(); }
			set { panelData = value; data = value; }
		}
	}
}
