/* 脚本自动生成于：2021-10-16 21:13:09 ，请勿修改！*/

using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

// 请在菜单 IFramework/UIKit Config 里设置默认命名空间
namespace IFramework.Example
{
	public partial class UIHomePanel
	{
		public const string Name = "UIHomePanel";

		private UIHomePanelData uIHomePanelData = null;

		[SerializeField] public UnityEngine.Transform GameStart;

		[SerializeField] public UnityEngine.Transform GameEnd;

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
