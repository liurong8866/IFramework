using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;
using UnityEngine.SceneManagement;

// 1.请在菜单 IFramework/UIKit Config 里设置默认命名空间
// 2.用户逻辑代码不会被覆盖，如需重新生成，请手动删除当前代码文件
namespace IFramework.Test.ResourceKit
{
	public class StartPanelData : UIPanelData { }

	public partial class StartPanel : UIPanel
    {
        
        
        private ResourceLoader loader;
		protected override void OnInit()
        {
            
        }

        protected override void OnOpen(IData data)
        {
            loader = ResourceLoader.Allocate();
            ButtonStart.onClick.AddListener(() => {
                loader.LoadAsync("Game",
                                 (result, resource) => {
                                     SceneManager.LoadSceneAsync("Game");
                                 });
            });
        }

		protected override void OnShow() { }

		protected override void OnHide() { }

		protected override void OnClose() { }
	}
}
