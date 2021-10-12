using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Example.UI
{
	public partial class Enemy	{
		[SerializeField] public UnityEngine.Transform UserName;
		[SerializeField] public UnityEngine.Transform Password;

		public void Clear()
		{
			UserName = null;
			Password = null;
		}

		public override string ComponentName
		{
			get { return "Enemy";}
		}
	}
}
