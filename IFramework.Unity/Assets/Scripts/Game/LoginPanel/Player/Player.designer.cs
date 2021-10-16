using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Example
{
	public partial class Player
	{
		[SerializeField] public PlayerWeapon Weapon;

		public override string ComponentName => "Player";

		public void OnDisable()
		{
			Weapon = null;
		}

	}
}
