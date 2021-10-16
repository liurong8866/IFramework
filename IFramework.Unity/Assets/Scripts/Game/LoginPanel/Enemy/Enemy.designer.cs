using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Example
{
	public partial class Enemy
	{
		[SerializeField] public EnemyWeapon Weapon;

		public override string ComponentName => "Enemy";

		public void OnDisable()
		{
			Weapon = null;
		}

	}
}
