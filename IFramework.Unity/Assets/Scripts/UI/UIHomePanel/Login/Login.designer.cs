using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Test
{
	public partial class Login
	{
		[SerializeField] public Text UserName;
		[SerializeField] public Text Password;
		[SerializeField] public Button ButtonLogin;

		public override string ComponentName => "Login";

		public void OnDisable()
		{
			UserName = null;
			Password = null;
			ButtonLogin = null;
		}
	}
}
