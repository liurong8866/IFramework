using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Example.UI
{
	public partial class Player	{
		[SerializeField] public UnityEngine.Transform UserName;
		[SerializeField] public UnityEngine.Transform Password;

		public override string ComponentName
		{
			get { return "Player";}
		}

		public void OnDisable()
		{
			UserName = null;
			Password = null;
		}

	}
}
