using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Example
{
	public partial class EnemyWeapon
	{
		[SerializeField] public UnityEngine.Transform WeaponName;
		[SerializeField] public UnityEngine.Transform CostMoney;

		public override string ComponentName => "EnemyWeapon";

		public void OnDisable()
		{
			WeaponName = null;
			CostMoney = null;
		}

	}
}
