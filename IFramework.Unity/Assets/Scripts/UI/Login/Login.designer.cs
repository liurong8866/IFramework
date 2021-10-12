/* 自动生成于：2021-10-12 14:17:01 ，请勿修改！*/

using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Example.UI
{
	public partial class Login
	{
		public const string Name = "Login";

		private LoginData loginData = null;

		protected override void ClearUIComponents()
		{
			Data = null;
		}

		public LoginData Data
		{
			get => loginData ??= new LoginData();
			set { loginData = value; data = value; }
		}
	}
}
