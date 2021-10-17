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

        protected IData data {
            get => Info?.Data;
            set {
                Info ??= new PanelInfo();
                Info.Data = value;
            }
        }

        public IManager Manager => UIManager.Instance;

        public void Init(IData data)
        {
            OnInitData(data);
            OnInit();
        }

        public void Open(IData data)
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

        void IPanel.Close(bool destroy)
        {
            Hide();
            State = PanelState.Closed;
            OnClose();
            if (destroy) Destroy(gameObject);
            this.As<IPanel>().Loader.Unload();
        }

        /// <summary>
        /// 面板初始化事件，通常用于赋值、注册事件等
        /// </summary>
        protected virtual void OnInit() { }

        /// <summary>
        /// 面板初始化事件，通常用于赋值、注册事件等
        /// </summary>
        protected abstract void OnInitData(IData data);
        
        /// <summary>
        /// 面板打开时事件
        /// </summary>
        protected virtual void OnOpen(IData data) { }

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
