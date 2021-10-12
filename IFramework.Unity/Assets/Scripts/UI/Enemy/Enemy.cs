using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Example.UI
{
	public class EnemyData : UIPanelData { }

	public partial class Enemy : UIPanel
	{
		protected override void OnInit(IData data = null)
		{
		this.data = data as EnemyData ?? new EnemyData();
		}

		protected override void OnOpen(IData data = null) { }

		protected override void OnShow() { }

		protected override void OnHide() { }

		protected override void OnClose() { }
	}
}
