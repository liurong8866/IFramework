using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IFramework.Engine
{
    [Serializable]
    public class UIPanelTesterInfo
    {
        /// <summary>
        /// 页面的名字
        /// </summary>
        public string PanelName;

        /// <summary>
        /// 层级名字
        /// </summary>
        public UILevel Level;
    }

    public class UIPanelTester : MonoBehaviour
    {
        /// <summary>
        /// 页面的名字
        /// </summary>
        public string PanelName;

        /// <summary>
        /// 层级名字
        /// </summary>
        public UILevel Level;

        [SerializeField]
        private List<UIPanelTesterInfo> otherPanels =  new List<UIPanelTesterInfo>();

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.2f);
            UIKit.ScriptUse.OpenPanel(PanelName, Level);
            otherPanels.ForEach(panelTesterInfo => { UIKit.ScriptUse.OpenPanel(panelTesterInfo.PanelName, panelTesterInfo.Level); });
        }
    }
}
