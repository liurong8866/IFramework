using System;
using IFramework.Core;
using UnityEngine;
using UnityEngine.UI;

namespace IFramework.Engine
{
    [MonoSingleton("UIRoot")]
    public class UIRoot : MonoSingleton<UIRoot>
    {
        public Camera UICamera;
        public Canvas Canvas;
        public UnityEngine.UI.CanvasScaler CanvasScaler;
        public GraphicRaycaster GraphicRaycaster;

        // 面板对象
        public RectTransform Background;
        public RectTransform Common;
        public RectTransform Popup;
        public RectTransform CanvasPanel;

        private new static UIRoot instance;

        /// <summary>
        /// 获取单例
        /// </summary>
        public new static UIRoot Instance {
            get {
                // 如果没有则查找
                if (!instance) {
                    instance = FindObjectOfType<UIRoot>();
                }

                // 如果没找到则
                if (!instance) {
                    Instantiate(Resources.Load<GameObject>("UIRoot"));
                    instance = MonoSingletonProperty<UIRoot>.Instance;
                    instance.name = "UIRoot";
                    DontDestroyOnLoad(instance);
                }
                return instance;
            }
        }

        /// <summary>
        /// 摄像机
        /// </summary>
        public Camera Camera => UICamera;

        public void SetResolution(int width, int height, float matchOnWidthOrHeight)
        {
            CanvasScaler.referenceResolution = new Vector2(width, height);
            CanvasScaler.matchWidthOrHeight = matchOnWidthOrHeight;
        }

        public Vector2 GetResolution()
        {
            return CanvasScaler.referenceResolution;
        }

        public float GetMatchOrWidthOrHeight()
        {
            return CanvasScaler.matchWidthOrHeight;
        }

        /// <summary>
        /// 设置面板层级
        /// </summary>
        public void SetPanelLevel(IPanel panel, UILevel level)
        {
            Canvas canvas = panel.Transform.GetComponent<Canvas>();
            if (canvas) {
                panel.Transform.SetParent(CanvasPanel);
            }
            else {
                switch (level) {
                    case UILevel.Background:
                        panel.Transform.SetParent(Background);
                        break;
                    case UILevel.Common:
                        panel.Transform.SetParent(Common);
                        break;
                    case UILevel.Popup:
                        panel.Transform.SetParent(Popup);
                        break;
                    case UILevel.Canvas:
                    default: throw new ArgumentOutOfRangeException(nameof(level), level, null);
                }
            }
        }
    }
}
