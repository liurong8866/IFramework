using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// UIPanel 基础类
    /// </summary>
    public abstract class UIPanel : MonoBehaviour, IPanel
    {
        public Transform Transform => transform;

        public IPanelLoader Loader { get; set; }

        public PanelInfo Info { get; set; }
        
        public PanelState State { get; set; }

        protected IData data;
        
        public void Init(IData data = null)
        {
            this.data = data;
            OnInit(data);
        }

        public void Open(IData data = null)
        {
            State = PanelState.Opening;
            OnOpen(data);
        }

        public void Show()
        {
            throw new System.NotImplementedException();
        }

        public void Hide()
        {
            State = PanelState.Hide;
            // TODO
        }

        public void Close(bool destroy = true)
        {
            throw new System.NotImplementedException();
        }
        
        protected virtual void OnInit(IData data = null) { }

        protected virtual void OnOpen(IData data = null) { }
    }
}
