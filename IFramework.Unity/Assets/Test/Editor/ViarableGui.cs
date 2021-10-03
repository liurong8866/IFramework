using IFramework.Core;
using UnityEditor;
using UnityEngine;

namespace IFramework.Test.Viarable
{
    public class ViarableGui : EditorWindow
    {
        private ConfigInt platformIndex;
        private ConfigBool autoGenerateName;
        private ConfigBool isSimulation;

        private void Awake()
        {
            platformIndex = new ConfigInt("platformIndex");
            autoGenerateName = new ConfigBool("autoGenerateName", true);
            isSimulation = new ConfigBool("isSimulation", true);
            bool unused = autoGenerateName.Value;
        }

        //        [MenuItem("IFramework/Test/Window")]
        public static void Open()
        {
            ViarableGui window = GetWindow<ViarableGui>();
            window.Show();
        }

        private void OnGUI()
        {
            // 选择平台
            platformIndex.Value = GUILayout.Toolbar(platformIndex.Value, new[] { "Window", "MacOS", "iOS", "Android", "WebGL", "PS4", "PS5", "XboxOne" });
            GUILayout.Space(10);

            // 是否自动生成常量
            autoGenerateName.Value = GUILayout.Toggle(autoGenerateName.Value, "打 AB 包时，自动生成资源名常量代码");
            GUILayout.Space(10);

            // 模拟模式
            isSimulation.Value = GUILayout.Toggle(isSimulation.Value, "模拟模式（勾选后每当资源修改时无需再打 AB 包，开发阶段建议勾选，打真机包时取消勾选并打一次 AB 包）");
            GUILayout.Space(10);
        }
    }
}
