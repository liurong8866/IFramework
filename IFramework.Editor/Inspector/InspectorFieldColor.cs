using IFramework.Core;
using UnityEngine;

namespace IFramework.Editor
{
    
    public class InspectorFieldColor : Singleton<InspectorFieldColor>
    {
        private Color originColor;

        private InspectorFieldColor()
        {
            originColor = GUI.color;
        }

        public void Default()
        {
            GUI.color = originColor;
        }
        
        public void Red()
        {
            GUI.color = new Color(1f, 0.5f, 0.5f, 1f);
        }
        
        public void Yellow()
        {
            GUI.color = new Color(1f, 1f, 0.5f, 1f);
        }
        
        public void Green()
        {
            GUI.color = new Color(0.5f, 1f, 0.5f, 1f);
        }
    }

}
