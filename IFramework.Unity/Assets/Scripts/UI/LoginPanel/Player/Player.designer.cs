using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Example.UI
{
	public partial class Player
	{
		[SerializeField] public Weapon Weapon;

		public override string ComponentName => "Player";

		public void OnDisable()
		{
			Weapon = null;
		}

	}
}
