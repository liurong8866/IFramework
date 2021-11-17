using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IFramework.Engine
{
    [Serializable]
    public class UIPanelLoadInfo
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

    [AddComponentMenu("IFramework/UIPanelLoader")]
    public class UIPanelLoader : MonoBehaviour
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
        private List<UIPanelLoadInfo> otherPanels =  new List<UIPanelLoadInfo>();

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(0.2f);
            UIKit.ScriptUse.OpenPanel(PanelName, Level);
            otherPanels.ForEach(panelTesterInfo => { UIKit.ScriptUse.OpenPanel(panelTesterInfo.PanelName, panelTesterInfo.Level); });
        }
    }
}
