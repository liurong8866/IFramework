using System.Collections;
using System.Collections.Generic;
using IFramework.Engine;
using UnityEngine;

namespace IFramework.Test
{
    public class ViewTest : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            // 在 Forward 层级打开
            // UIKit.OpenPanel<UIHomePanel>(UILevel.Background);
            
            // 传递初始数据给 UIHomePanel
            UIKit.OpenPanel<UIHomePanel>(new UIHomePanelData()
            {
                Coin = 10
            });
            
            // // 从 UIHomePanelTest.prefab 加载界面 
            // UIKit.OpenPanel<UIHomePanel>(prefabName: "UIHomePanelTest");
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
