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

        public virtual void Show()
        {
            gameObject.SetActive(true);
            OnShow();
        }

        public virtual void Hide()
        {
            OnHide();
            gameObject.SetActive(false);
            State = PanelState.Hide;
        }
        
        public virtual void Close(bool destroy = true)
        {
            Info.Data = data;
            Hide();
            State = PanelState.Closed;
            OnClose();
            // TODO
            if(destroy) Destroy(gameObject);
            this.As<IPanel>().Loader.Unload();
            this.As<IPanel>().Loader = null;
            // Data = null;

        }
        
        /// <summary>
        /// 面板初始化事件，通常用于赋值、注册事件等
        /// </summary>
        protected virtual void OnInit(IData data = null) { }

        /// <summary>
        /// 面板打开时事件
        /// </summary>
        protected virtual void OnOpen(IData data = null) { }
        
        /// <summary>
        /// 面板显示时事件
        /// </summary>
        protected virtual void OnShow() { }

        /// <summary>
        /// 面板隐藏时事件
        /// </summary>
        protected virtual void OnHide() { }

        /// <summary>
        /// 面板关闭时事件
        /// </summary>
        protected abstract void OnClose();

        /// <summary>
        /// 清理UI组件
        /// </summary>
        protected abstract void ClearUIComponents();

    }
}
