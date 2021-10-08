using System;
using System.Linq;
using IFramework.Core;
using UnityEditor;
using UnityEngine;

namespace IFramework.Editor
{
    [CustomEditor(typeof(AbstractBind), true)]
    public class BindInspector : UnityEditor.Editor
    {
        private int elementTypeIndex = 0;
        public static readonly Bindable<BindType> bindTypeMonitor = new Bindable<BindType>();
        private string[] elementTypeOptions = Array.Empty<string>();
        private AbstractBind bind => target as AbstractBind;

        private void OnEnable()
        {
            bind.ComponentName.IfNullOrEmpty(() => bind.ComponentName = bind.name);
            bindTypeMonitor.OnChange += GetElementTypeOptions;
            GetElementTypeOptions(0);
        }

        private void GetElementTypeOptions(BindType index)
        {
            Component[] components = bind.GetComponents<Component>();

            // 排除 AbstractBind 自身
            elementTypeOptions = components.Where(c => !(c is AbstractBind))
                   .Select(c => c.GetType().FullName)
                   .ToArray();

            elementTypeIndex = elementTypeOptions.ToList()
                   .FindIndex((componentName) => componentName.Contains(bind.ComponentName));

            if (elementTypeIndex == -1 || elementTypeIndex >= elementTypeOptions.Length) { elementTypeIndex = 0; }
        }

        public override void OnInspectorGUI()
        {
            // base.OnInspectorGUI();
            // // 解决混用Layout重叠问题
            EditorGUILayout.GetControlRect();
            GUILayout.BeginVertical();

            // 组件
            GUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("组件");
            bind.BindType = (BindType)EditorGUILayout.EnumPopup(bind.BindType);
            bindTypeMonitor.Value = bind.BindType;
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            // 类型
            if (bind.BindType == BindType.DefaultElement && elementTypeIndex < elementTypeOptions.Length) {
                GUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("类型");
                elementTypeIndex = EditorGUILayout.Popup(elementTypeIndex, elementTypeOptions);
                bind.ComponentName = elementTypeOptions[elementTypeIndex];
                GUILayout.EndHorizontal();
                GUILayout.Space(5);
            }

            // 类型
            if (bind.BindType != BindType.DefaultElement) {
                GUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("类型");
                bind.ComponentName = EditorGUILayout.TextField(bind.ComponentName);
                GUILayout.EndHorizontal();
                GUILayout.Space(5);
            }

            // 注释
            EditorGUILayout.PrefixLabel("注释");
            GUILayout.BeginHorizontal();
            bind.Comment = EditorGUILayout.TextArea(bind.Comment, GUILayout.Height(60));
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            
            // 提示
            EditorGUILayout.HelpBox("代码生成", MessageType.Info);
            GUILayout.Space(10);
            //
            GUILayout.EndVertical();
            EditorGUILayout.GetControlRect();
        }
    }
}
