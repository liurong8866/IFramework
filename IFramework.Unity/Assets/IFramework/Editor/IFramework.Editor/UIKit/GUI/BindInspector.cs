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
        private BindType bindTypeValue;
        private int elementTypeIndex;
        private AbstractBind bind => target as AbstractBind;
        private string[] elementTypeOptions = Array.Empty<string>();
        public Bindable<BindType> bindTypeMonitor = new Bindable<BindType>();

        private void OnEnable()
        {
            if (bind != null) {
                bind.ComponentName.IfNothing(() => bind.ComponentName = bind.name.FormatName());
                bind.CustomComponentName.IfNothing(() => bind.CustomComponentName = bind.name.FormatName());
                bindTypeMonitor.OnChange = null;
                bindTypeMonitor.OnChange += GetElementTypeOptions;
                bind.SerializedFiled = new AbstractBind.Serialized(serializedObject);
            }
        }

        private void OnDisable()
        {
            bindTypeMonitor.OnChange -= GetElementTypeOptions;
        }

        private void GetElementTypeOptions(BindType element)
        {
            if (bind == null) return;

            if (element == BindType.DefaultElement) {
                Component[] components = bind.GetComponents<Component>();

                // 排除 AbstractBind 自身
                elementTypeOptions = components.Where(c => c != null && !(c is AbstractBind)).Select(c => c.GetType().FullName).ToArray();
                elementTypeIndex = elementTypeOptions.ToList().FindIndex(componentName => componentName.Contains(bind.ComponentName));

                if (elementTypeIndex == -1 || elementTypeIndex >= elementTypeOptions.Length) {
                    elementTypeIndex = 0;
                }
                // 更新组件名称
                bind.ComponentName = elementTypeOptions[elementTypeIndex].FormatName();
                
            }
            else {
                // 更新组件名称 如果是其他，则显示用户自定义组件名，默认为GameObject名称
                bind.ComponentName = bind.CustomComponentName;
            }
            bind.bindType = bindTypeValue;
            bind.SerializedFiled.ComponentName.stringValue = bind.ComponentName;
            serializedObject.ApplyModifiedProperties();
        }

        public override void OnInspectorGUI()
        {
            // base.OnInspectorGUI();
            // 解决混用Layout重叠问题
            EditorGUILayout.GetControlRect();
            GUILayout.BeginVertical();

            // 组件
            GUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("绑定类型");
            bindTypeValue = (BindType)EditorGUILayout.EnumPopup(bindTypeValue);
            bindTypeMonitor.Value = bindTypeValue;
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            
            // 类型
            if (bindTypeValue == BindType.DefaultElement && elementTypeIndex < elementTypeOptions.Length) {
                GUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("类名称");
                elementTypeIndex = EditorGUILayout.Popup(elementTypeIndex, elementTypeOptions);
                bind.ComponentName = elementTypeOptions[elementTypeIndex].FormatName();
                bind.SerializedFiled.ComponentName.stringValue = bind.ComponentName;
                GUILayout.EndHorizontal();
                GUILayout.Space(5);
            }
            
            // 类型
            if (bindTypeValue != BindType.DefaultElement) {
                GUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("类名称");
                bind.CustomComponentName = EditorGUILayout.TextField(bind.CustomComponentName).FormatName();
                bind.ComponentName = bind.CustomComponentName;
                bind.SerializedFiled.ComponentName.stringValue = bind.ComponentName;
                GUILayout.EndHorizontal();
                GUILayout.Space(5);
            }
            
            // 注释
            EditorGUILayout.PrefixLabel("字段注释");
            GUILayout.BeginHorizontal();
            bind.Comment = EditorGUILayout.TextArea(bind.Comment, GUILayout.Height(40)).Trim();
            bind.SerializedFiled.Comment.stringValue = bind.Comment;
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            EditorGUILayout.HelpBox("如果作为子类型，需要设置为Element，同时添加ViewController", MessageType.Info);
            serializedObject.ApplyModifiedProperties();
            
            // 结束
            GUILayout.EndVertical();
            EditorGUILayout.GetControlRect();
        }
    }
}
