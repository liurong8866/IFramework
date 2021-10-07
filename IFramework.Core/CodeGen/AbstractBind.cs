using UnityEngine;
using UnityEngine.UI;

namespace IFramework.Core
{
    public abstract class AbstractBind : MonoBehaviour, IBind
    {
        public Transform Transform => transform;

        public string Comment { get; protected set; }

        public BindType BindType { get; set; } = BindType.DefaultElement;

        [HideInInspector] public string CustomComponentName;

        [HideInInspector] public string GeneratePath;

        [HideInInspector] [SerializeField] private string componentName;

        public virtual string ComponentName {
            get {
                if (BindType == BindType.DefaultElement) {
                    if (componentName.IsNullOrEmpty()) { componentName = GetDefaultComponentName(); }
                    return componentName;
                }
                return CustomComponentName;
            }
            set => componentName = value;
        }

        private string GetDefaultComponentName()
        {
            if (GetComponent<ViewController>()) return GetComponent<ViewController>().GetType().FullName;
            
            if (gameObject.GetComponent<ScrollRect>()) return "UnityEngine.UI.ScrollRect";
            if (GetComponent<InputField>()) return "UnityEngine.UI.InputField";
            
            // ugui bind
            if (gameObject.GetComponent<Dropdown>()) return "UnityEngine.UI.Dropdown";
            if (GetComponent<Button>()) return "UnityEngine.UI.Button";
            if (GetComponent<Text>()) return "UnityEngine.UI.Text";
            if (GetComponent<RawImage>()) return "UnityEngine.UI.RawImage";
            if (GetComponent<Toggle>()) return "UnityEngine.UI.Toggle";
            if (GetComponent<Slider>()) return "UnityEngine.UI.Slider";
            if (GetComponent<Scrollbar>()) return "UnityEngine.UI.Scrollbar";
            if (GetComponent<Image>()) return "UnityEngine.UI.Image";
            if (GetComponent<ToggleGroup>()) return "UnityEngine.UI.ToggleGroup";
            
            // other
            if (GetComponent<Rigidbody>()) return "Rigidbody";
            if (GetComponent<Rigidbody2D>()) return "Rigidbody2D";
            
            if (GetComponent<BoxCollider2D>()) return "BoxCollider2D";
            if (GetComponent<BoxCollider>()) return "BoxCollider";
            if (GetComponent<CircleCollider2D>()) return "CircleCollider2D";
            if (GetComponent<SphereCollider>()) return "SphereCollider";
            if (GetComponent<MeshCollider>()) return "MeshCollider";
            if (GetComponent<Collider>()) return "Collider";
            if (GetComponent<Collider2D>()) return "Collider2D";
            
            if (GetComponent<Animator>()) return "Animator";
            if (GetComponent<Canvas>()) return "Canvas";
            if (GetComponent<Camera>()) return "Camera";
            if (GetComponent<RectTransform>()) return "RectTransform";
            if (GetComponent<MeshRenderer>()) return "MeshRenderer";
            if (GetComponent<SpriteRenderer>()) return "SpriteRenderer";
            
            // text mesh pro supported
            if (GetComponent("TMP.TextMeshProUGUI")) return "TMP.TextMeshProUGUI";
            if (GetComponent("TMPro.TextMeshProUGUI")) return "TMPro.TextMeshProUGUI";
            if (GetComponent("TMPro.TextMeshPro")) return "TMPro.TextMeshPro";
            if (GetComponent("TMPro.TMP_InputField")) return "TMPro.TMP_InputField";
            
            return "Transform";
        }
    }
}
