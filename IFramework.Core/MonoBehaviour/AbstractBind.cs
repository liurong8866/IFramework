using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace IFramework.Core
{
    public abstract class AbstractBind : MonoBehaviour, IBind
    {
        public Transform Transform => transform;

        /// <summary>
        /// 绑定类型
        /// </summary>
        public BindType BindType { get; set; } = BindType.DefaultElement;

        /// <summary>
        /// 组件名称
        /// </summary>
        public virtual string ComponentName {
            get {
                if (BindType == BindType.DefaultElement && componentName.Nothing()) {
                    // 如果没有设置，则取当前物体上绑定的组件
                    return componentName = GetDefaultComponentName();
                }
                return componentName;
            }
            set => componentName = value;
        }

        /// <summary>
        /// 自定义名称
        /// </summary>
        public string CustomComponentName { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public string Comment { get => comment; set => comment = value; }

        [HideInInspector] [SerializeField] private string comment = "";

        [HideInInspector] [SerializeField] private string componentName = "";

        /// <summary>
        /// 取当前物体上绑定的组件
        /// </summary>
        /// <returns></returns>
        [SuppressMessage("ReSharper", "Unity.PreferGenericMethodOverload")]
        private string GetDefaultComponentName()
        {
            if (GetComponent("ViewController")) return GetComponent<ViewController>().GetType().FullName;

            // UGUI
            if (GetComponent("UnityEngine.UI.Button")) return "UnityEngine.UI.Button";
            if (GetComponent("UnityEngine.UI.Text")) return "UnityEngine.UI.Text";
            if (GetComponent("UnityEngine.UI.InputField") != null) return "UnityEngine.UI.InputField";
            if (GetComponent("UnityEngine.UI.Image")) return "UnityEngine.UI.Image";
            if (GetComponent("UnityEngine.UI.RawImage")) return "UnityEngine.UI.RawImage";
            if (GetComponent("UnityEngine.UI.Dropdown")) return "UnityEngine.UI.Dropdown";
            if (GetComponent("UnityEngine.UI.Toggle")) return "UnityEngine.UI.Toggle";
            if (GetComponent("UnityEngine.UI.Slider")) return "UnityEngine.UI.Slider";
            if (GetComponent("UnityEngine.UI.Scrollbar")) return "UnityEngine.UI.Scrollbar";
            if (GetComponent("UnityEngine.UI.ToggleGroup")) return "UnityEngine.UI.ToggleGroup";
            if (GetComponent("UnityEngine.UI.ScrollRect")) return "UnityEngine.UI.ScrollRect";

            // 富文本
            if (GetComponent("TMP.TextMeshProUGUI")) return "TMP.TextMeshProUGUI";
            if (GetComponent("TMPro.TextMeshProUGUI")) return "TMPro.TextMeshProUGUI";
            if (GetComponent("TMPro.TextMeshPro")) return "TMPro.TextMeshPro";
            if (GetComponent("TMPro.TMP_InputField")) return "TMPro.TMP_InputField";

            // Unity 组件
            if (GetComponent("Rigidbody")) return "Rigidbody";
            if (GetComponent("Rigidbody2D")) return "Rigidbody2D";
            if (GetComponent("BoxCollider2D")) return "BoxCollider2D";
            if (GetComponent("BoxCollider")) return "BoxCollider";
            if (GetComponent("CircleCollider2D")) return "CircleCollider2D";
            if (GetComponent("SphereCollider")) return "SphereCollider";
            if (GetComponent("MeshCollider")) return "MeshCollider";
            if (GetComponent("Collider")) return "Collider";
            if (GetComponent("Collider2D")) return "Collider2D";
            if (GetComponent("Animator")) return "Animator";
            if (GetComponent("Canvas")) return "Canvas";
            if (GetComponent("Camera")) return "Camera";
            if (GetComponent("RectTransform")) return "RectTransform";
            if (GetComponent("MeshRenderer")) return "MeshRenderer";
            if (GetComponent("SpriteRenderer")) return "SpriteRenderer";

            return "Transform";
        }
    }
}
