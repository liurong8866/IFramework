/* 脚本自动生成于：2021-10-16 21:01:14 ，请勿修改！*/

using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

// 请在菜单 IFramework/UIKit Config 里设置默认命名空间
namespace IFramework.Example
{
	public partial class LoginPanel1
	{
		public const string Name = "LoginPanel1";

		private LoginPanel1Data loginPanel1Data = null;

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

		public LoginPanel1Data Data
		{
			get { return loginPanel1Data ??= new LoginPanel1Data(); }
			set { loginPanel1Data = value; data = value; }
		}
	}
}
