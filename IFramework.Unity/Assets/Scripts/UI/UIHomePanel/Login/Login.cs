using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Test
{
	public partial class Login : UIElement
	{
        private void Awake()
        {
            ButtonLogin.onClick.AddListener(() => {
                Log.Info("登录");
                Log.Info(UserName.text);
                Log.Info(Password.text);
            });
        }

		protected override void BeforeDestroy() { }	}
}