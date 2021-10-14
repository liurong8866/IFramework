using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Example.UI
{
	public partial class Enemy
	{
		[SerializeField] public UnityEngine.Transform UserName;
		[SerializeField] public UnityEngine.Transform Password;

		public override string ComponentName => "Enemy";

		public void OnDisable()
		{
			UserName = null;
			Password = null;
		}

	}
}
