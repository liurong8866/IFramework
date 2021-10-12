/* 脚本自动生成于：2021-10-12 17:46:46 ，请勿修改！*/

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

		[SerializeField] public Player Player;

		// 用户名
		[SerializeField] public UnityEngine.Transform UserName;

		// 密码
		[SerializeField] public UnityEngine.Transform Password;

		[SerializeField] public Enemy Enemy;

		protected override void ClearUIComponents()
		{
			Player = null;
			UserName = null;
			Password = null;
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
