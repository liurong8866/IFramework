/* 脚本自动生成于：2021-10-16 12:22:35 ，请勿修改！*/

using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

// 请在菜单 IFramework/UIKit Config 里设置默认命名空间
namespace IFramework.Example
{
	public partial class LoginPanel
	{
		public const string Name = "LoginPanel";

		private LoginPanelData loginPanelData = null;

		// 用户
		[SerializeField] public Player Player;

		// 敌人
		[SerializeField] public Enemy Enemy;

		protected override void ClearUIComponents()
		{
			Player = null;
			Enemy = null;
			Data = null;
		}

		public LoginPanelData Data
		{
			get { return loginPanelData ??= new LoginPanelData(); }
			set { loginPanelData = value; data = value; }
		}
	}
}
