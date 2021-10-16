using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

// 1.请在菜单 IFramework/UIKit Config 里设置默认命名空间
// 2.用户逻辑代码不会被覆盖，如需重新生成，请手动删除当前代码文件
namespace IFramework.Example
{
	public class UIHomePanelData : UIPanelData { }

	public partial class UIHomePanel : UIPanel
	{
		protected override void OnInit(IData data = null)
		{
		this.data = data as UIHomePanelData ?? new UIHomePanelData();
		}

		protected override void OnOpen(IData data = null) { }

		protected override void OnShow() { }

		protected override void OnHide() { }

		protected override void OnClose() { }
	}
}
