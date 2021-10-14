using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Example.UI
{
    public class LoginPanelData : UIPanelData { }

    public partial class LoginPanel : UIPanel
    {
        protected override void OnInit(IData data = null)
        {
            this.data = data as LoginPanelData ?? new LoginPanelData();
        }

        protected override void OnOpen(IData data = null) { }

        protected override void OnShow() { }

        protected override void OnHide() { }

        protected override void OnClose() { }
    }
}
