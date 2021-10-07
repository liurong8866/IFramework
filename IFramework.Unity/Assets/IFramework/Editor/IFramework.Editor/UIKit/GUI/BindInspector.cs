using System;
using IFramework.Core;
using UnityEditor;
using UnityEngine;

namespace IFramework.Editor
{
    [CustomEditor(typeof(AbstractBind), true)]
    public class BindInspector : UnityEditor.Editor
    {
        private AbstractBind bind => target as AbstractBind;

        private void OnEnable()
        {
            bind.ComponentName.IfNullOrEmpty(() => bind.ComponentName = bind.name);
        }

        public override void OnInspectorGUI()
        {
            // base.OnInspectorGUI();
            // // 解决混用Layout重叠问题
            // EditorGUILayout.GetControlRect();
            
            GUILayout.BeginVertical();
            
            // 类型
            GUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel("类型");
            bind.BindType = (BindType)EditorGUILayout.EnumPopup(bind.BindType);
            // bind.BindType = (BindType)EditorGUILayout.EnumFlagsField(bind.BindType);
            GUILayout.EndHorizontal();
            GUILayout.Space(5);

            // 类名
            // if (bind.BindType != BindType.DefaultElement) {
                GUILayout.BeginHorizontal();
                EditorGUILayout.PrefixLabel("类名");
                bind.ComponentName = EditorGUILayout.TextField(bind.ComponentName);
                GUILayout.EndHorizontal();
                GUILayout.Space(5);
            // }
            
            //
            GUILayout.BeginHorizontal();
            bind.CustomComment = EditorGUILayout.TextField(bind.CustomComment);
            GUILayout.EndHorizontal();
            GUILayout.Space(5);
            //
            EditorGUILayout.HelpBox("代码生成", MessageType.Info);
            GUILayout.Space(10);
            //
            GUILayout.EndVertical();
        }
    }
}
