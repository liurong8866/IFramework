using IFramework.Core;
using UnityEngine;

namespace IFramework.Engine
{
    /// <summary>
    /// UIPanel 基础类
    /// </summary>
    public abstract class UIPanel : IocMonoBehaviour, IPanel
    {
        public Transform Transform => transform;

        public IPanelLoader Loader { get; set; }

        public PanelInfo Info { get; set; }

        public PanelState State { get; set; }

        protected IData data;

        public IManager Manager => UIManager.Instance;

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
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            State = PanelState.Hide;
        }

        protected virtual void OnInit(IData data = null) { }

        protected virtual void OnOpen(IData data = null) { }

        public void Close(bool destroy = true)
        {
            Info.Data = data;
        }
    }
}
