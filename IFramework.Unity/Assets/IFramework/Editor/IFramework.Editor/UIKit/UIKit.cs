using IFramework.Core;
using UnityEditor;

namespace IFramework.Editor
{
    public class UiKit
    {
        public static void BindScript()
        {
            Selection.activeGameObject.AddComponent<Bind>();
        }
    }
}
