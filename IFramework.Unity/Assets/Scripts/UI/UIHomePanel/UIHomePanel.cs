using UnityEngine;
using UnityEngine.UI;
using IFramework.Core;
using IFramework.Engine;

namespace IFramework.Test
{
    public class UIHomePanelData : UIPanelData
    {
        public int Coin;
    }

    public partial class UIHomePanel : UIPanel
    {
        private int count = 0;

        protected override void OnInit(IData data = null)
        {
            Data = data as UIHomePanelData ?? new UIHomePanelData();
            ButtonStart.onClick.AddListener(() => {
                Log.Info("开始游戏" + Data.Coin);
                UIKit.OpenPanel<GamePad>(UILevel.Common,
                                         PanelOpenType.Multiple,
                                         new GamePadData() {
                                             Index = ++count
                                         });
            });
            ButtonEnd.onClick.AddListener(() => {
                Log.Info("结束游戏"); 
                
                UIKit.ClosePanel<GamePad>();

                // if (mUIMultiPanel && GUILayout.Button("关闭当前")) {
                //     UIKit.ClosePanel(mUIMultiPanel);
                //     mUIMultiPanel = null;
                // }
                //
            });
        }

        protected override void OnOpen(IData data = null)
        {
            // 每次 OpenPanel 的时候使用
            Debug.Log((data as UIHomePanelData)?.Coin);
        }

        protected override void OnShow() { }

        protected override void OnHide() { }

        protected override void OnClose() { }
    }
}
