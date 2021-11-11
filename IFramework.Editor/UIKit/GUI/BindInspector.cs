using System;
using System.Linq;
using IFramework.Core;
using UnityEditor;
using UnityEngine;

namespace IFramework.Editor
{
    [CustomEditor(typeof(Bind), true)]
    public class BindInspector : UnityEditor.Editor
    {
        /// <summary>
        /// 序列化的字段
        /// </summary>
        public BindSerialized bindSerialized;
        
        private Bind bind;
        private GameObject bindBelongsTo;
        private int elementTypeIndex;
        public Bindable<BindType> bindTypeMonitor = new Bindable<BindType>();
        private string[] elementTypeOptions = Array.Empty<string>();

        private void OnEnable()
        {
            bind = target as Bind;
            if (bind != null) {
                bind.ComponentName.IfNothing(() => bind.ComponentName = bind.name.FormatName());
                bind.CustomComponentName.IfNothing(() => bind.CustomComponentName = bind.name.FormatName());
                bindSerialized = new BindSerialized(serializedObject);
                bindTypeMonitor.OnChange += GetElementTypeOptions;
                GetElementTypeOptions(0);
                bindBelongsTo = EditorUtils.GetBindBelongsTo(target as Bind);
            }
        }

        private void GetElementTypeOptions(BindType element)
        {
            if (bind != null) {
                if (element == BindType.DefaultElement) {
                    Component[] components = bind.GetComponents<Component>();

                    // 排除 Bind 自身
                    elementTypeOptions = components.Where(c => c != null && !(c is Bind)).Select(c => c.GetType().FullName).ToArray();
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
            }
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            // base.OnInspectorGUI();
            // // 解决混用Layout重叠问题
            EditorGUILayout.GetControlRect();
            GUILayout.BeginVertical();

            // 组件
            GUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("类型");
            bind.bindType = (BindType)EditorGUILayout.EnumPopup(bind.BindType);
            bindTypeMonitor.Value = bind.BindType;
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            // 组件
            GUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("所属");
            GUILayout.Label(bindBelongsTo.name);
            if(GUILayout.Button("选择",GUILayout.Width(60)))
            {
                Selection.objects = new[] { bindBelongsTo };
            }
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            
            // 类型
            if (bind.BindType == BindType.DefaultElement && elementTypeIndex < elementTypeOptions.Length) {
                GUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("类名称");
                elementTypeIndex = EditorGUILayout.Popup(elementTypeIndex, elementTypeOptions);
                bind.ComponentName = elementTypeOptions[elementTypeIndex].FormatName();
                GUILayout.EndHorizontal();
                GUILayout.Space(5);
            }

            // 类型
            if (bind.BindType != BindType.DefaultElement) {
                GUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("类名称");
                bind.CustomComponentName = EditorGUILayout.TextField(bind.CustomComponentName).FormatName();
                bind.ComponentName = bind.CustomComponentName;
                GUILayout.EndHorizontal();
                GUILayout.Space(5);
            }

            // 注释
            EditorGUILayout.PrefixLabel("字段注释");
            GUILayout.BeginHorizontal();
            bind.Comment = EditorGUILayout.TextArea(bind.Comment, GUILayout.Height(40)).Trim();
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            EditorGUILayout.HelpBox("如果作为子类型，需要设置为Element，同时添加ViewController", MessageType.Info);

            // 结束
            GUILayout.EndVertical();
            EditorGUILayout.GetControlRect();

            // 记忆更新
            bindSerialized.BindType.intValue = bind.bindType.ToInt();
            bindSerialized.ComponentName.stringValue = bind.ComponentName;
            bindSerialized.CustomComponentName.stringValue = bind.CustomComponentName;
            bindSerialized.Comment.stringValue = bind.Comment;
            serializedObject.ApplyModifiedProperties();
        }
    }
    
    
    /// <summary>
    /// 用于在Inspector自定义面板中更改时触发修改，保存Prefab
    /// </summary>
    public class BindSerialized
    {
        public SerializedProperty BindType;
        public SerializedProperty CustomComponentName;
        public SerializedProperty Comment;
        public SerializedProperty ComponentName;
    
        public BindSerialized(SerializedObject serializedObject)
        {
            BindType = serializedObject.FindProperty("bindType");
            CustomComponentName = serializedObject.FindProperty("CustomComponentName");
            Comment = serializedObject.FindProperty("comment");
            ComponentName = serializedObject.FindProperty("componentName");
        }
    }
}
