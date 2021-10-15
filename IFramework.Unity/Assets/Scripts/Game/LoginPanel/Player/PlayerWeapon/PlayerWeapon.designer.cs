using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Example.UI
{
	public partial class PlayerWeapon
	{
		[SerializeField] public UnityEngine.Transform WeaponName;
		[SerializeField] public UnityEngine.Transform CostMoney;

		public override string ComponentName => "PlayerWeapon";

		public void OnDisable()
		{
			WeaponName = null;
			CostMoney = null;
		}

	}
}
