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
            // UIKit.OpenPanel<UIHomePanel>(UILevel.Popup);
            
            // 传递初始数据给 UIHomePanel
            UIKit.OpenPanel<UIHomePanel>(UILevel.Popup,  new UIHomePanelData()
            {
                Coin = 10
            });
            
            
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
