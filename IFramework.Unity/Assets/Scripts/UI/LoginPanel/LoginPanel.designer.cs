/* 脚本自动生成于：2021-10-14 22:13:13 ，请勿修改！*/

using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Example.UI
{
	public partial class LoginPanel
	{
		public const string Name = "LoginPanel";

		private LoginPanelData loginPanelData = null;

		// 用户
		[SerializeField] public Player Player;

		protected override void ClearUIComponents()
		{
			Player = null;
			Data = null;
		}

		public LoginPanelData Data
		{
			get { return loginPanelData ??= new LoginPanelData(); }
			set { loginPanelData = value; data = value; }
		}
	}
}
