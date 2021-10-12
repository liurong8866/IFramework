/* 脚本自动生成于：2021-10-12 16:28:44 ，请勿修改！*/

using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Example.UI
{
	public partial class Enemy
	{
		public const string Name = "Enemy";

		private EnemyData enemyData = null;

		// 用户名
		[SerializeField] public UnityEngine.Transform UserName;

		// 密码
		[SerializeField] public UnityEngine.Transform Password;

		protected override void ClearUIComponents()
		{
			UserName = null;
			Password = null;
			Data = null;
		}

		public EnemyData Data
		{
			get { return enemyData ??= new EnemyData(); }
			set { enemyData = value; data = value; }
		}
	}
}
