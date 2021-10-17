/* 脚本自动生成于：2021-10-17 13:07:20 ，请勿修改！*/

using System;
using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Test
{
	public partial class GamePad
	{
		public const string Name = "GamePad";

		private GamePadData panelData = null;

		[SerializeField] public Text GamePadText;

		[SerializeField] public Button ButtonClose;

		protected override void ClearUIComponents()
		{
			GamePadText = null;
			ButtonClose = null;
			Data = null;
		}

		public GamePadData Data
		{
			get { return panelData ??= new GamePadData(); }
			set { panelData = value; data = value; }
		}
	}
}
