using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace IFramework.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Transform), true)]
    public class TransformInspector : UnityEditor.Editor
    {
        private float scale = 1;

        private static Vector3 copyPosition;
        private static Vector3 copyScale;
        private static Quaternion copyRotation;

        private TransformRotationGUI rotationGUI;

        SerializedProperty positionProperty;
        SerializedProperty scaleProperty;
        SerializedProperty rotationProperty;

        private void OnEnable()
        {
            positionProperty = serializedObject.FindProperty("m_LocalPosition");
            rotationProperty = serializedObject.FindProperty("m_LocalRotation");
            scaleProperty = serializedObject.FindProperty("m_LocalScale");
            if (rotationGUI == null) rotationGUI = new TransformRotationGUI();
            rotationGUI.Initialize(rotationProperty, new GUIContent());
            
        }

        public override void OnInspectorGUI()
        {
            // 解决rotation多出空白行问题
            EditorGUIUtility.wideMode = true;
            EditorGUIUtility.labelWidth = 10f;
            DrawPosition();
            DrawRotation();
            DrawScale();
            DrawTools();
            serializedObject.ApplyModifiedProperties();
        }

        void DrawPosition()
        {
            GUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Position", GUILayout.Width(50f));
                EditorGUILayout.PropertyField(positionProperty.FindPropertyRelative("x"));
                EditorGUILayout.PropertyField(positionProperty.FindPropertyRelative("y"));
                EditorGUILayout.PropertyField(positionProperty.FindPropertyRelative("z"));
                bool reset = GUILayout.Button("R", GUILayout.Width(20f));
                InspectorFieldColor.Instance.Red();
                bool paste = GUILayout.Button("P", GUILayout.Width(20f));
                InspectorFieldColor.Instance.Default();
                if (reset) {
                    positionProperty.vector3Value = Vector3.zero;
                    GUI.FocusControl(null);
                }
                if (paste) {
                    PastePosition();
                    GUI.FocusControl(null);
                }
            }
            GUILayout.EndHorizontal();
        }

        void DrawScale()
        {
            GUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Scale", GUILayout.Width(50f));
                EditorGUILayout.PropertyField(scaleProperty.FindPropertyRelative("x"));
                EditorGUILayout.PropertyField(scaleProperty.FindPropertyRelative("y"));
                EditorGUILayout.PropertyField(scaleProperty.FindPropertyRelative("z"));
                bool reset = GUILayout.Button("R", GUILayout.Width(20f));
                InspectorFieldColor.Instance.Red();
                bool paste = GUILayout.Button("P", GUILayout.Width(20f));
                InspectorFieldColor.Instance.Default();
                if (reset) {
                    scaleProperty.vector3Value = Vector3.one;
                    scale = 1;
                    GUI.FocusControl(null);
                }
                if (paste) {
                    PasteScale();
                    GUI.FocusControl(null);
                }
            }
            GUILayout.EndHorizontal();
        }

        void DrawRotation()
        {
            // 需要设置最小宽度，与其Position Scale保持一致
            GUILayout.BeginHorizontal(GUILayout.MinWidth(302f), GUILayout.MaxWidth(float.MaxValue));
            {
                EditorGUILayout.LabelField("Rotation", GUILayout.Width(50f));
                rotationGUI.Draw();
                bool reset = GUILayout.Button("R", GUILayout.Width(20f));
                InspectorFieldColor.Instance.Red();
                bool paste = GUILayout.Button("P", GUILayout.Width(20f));
                InspectorFieldColor.Instance.Default();
                if (reset) {
                    rotationProperty.quaternionValue = Quaternion.identity;
                    GUI.FocusControl(null);
                }
                if (paste) {
                    PasteRotation();
                    GUI.FocusControl(null);
                }
            }
            GUILayout.EndHorizontal();
        }

        void DrawTools()
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.LabelField("Scale", GUILayout.Width(50f));
                EditorGUIUtility.labelWidth = 10f;
                InspectorFieldColor.Instance.Green();
                float newScale = EditorGUILayout.FloatField("A", scale);
                
                if (!Mathf.Approximately(scale, newScale)) {
                    scale = newScale;
                    scaleProperty.vector3Value = Vector3.one * scale;
                }
                
                if (GUILayout.Button(".", GUILayout.Width(20f))) {
                    Undo.RecordObjects(targets, "Round");
                    for (int i = 0; i < targets.Length; i++) {
                        Transform o = targets[i] as Transform;
                        o.localPosition = EditorUtils.Round(o.localPosition);
                        o.localScale = EditorUtils.Round(o.localScale);
                        scale = o.localScale.x;
                    }
                    GUI.FocusControl(null);
                }
                
                if (GUILayout.Button(".0", GUILayout.Width(22f))) {
                    Undo.RecordObjects(targets, "Round");
                    for (int i = 0; i < targets.Length; i++) {
                        Transform o = targets[i] as Transform;
                        o.localPosition = EditorUtils.Round(o.localPosition, 1);
                        o.localScale = EditorUtils.Round(o.localScale, 1);
                        scale = o.localScale.x;
                    }
                    GUI.FocusControl(null);
                }
                
                if (GUILayout.Button(".00", GUILayout.Width(30f))) {
                    Undo.RecordObjects(targets, "Round");
                    for (int i = 0; i < targets.Length; i++) {
                        Transform o = targets[i] as Transform;
                        o.localPosition = EditorUtils.Round(o.localPosition, 2);
                        o.localScale = EditorUtils.Round(o.localScale, 2);
                        scale = o.localScale.x;
                    }
                    GUI.FocusControl(null);
                }
                
                InspectorFieldColor.Instance.Default();
                if (GUILayout.Button("Reset", GUILayout.Width(42f))) {
                    positionProperty.vector3Value = Vector3.zero;
                    rotationProperty.quaternionValue = Quaternion.identity;
                    scaleProperty.vector3Value = Vector3.one;
                    GUI.FocusControl(null);
                }
                
                InspectorFieldColor.Instance.Yellow();
                if (GUILayout.Button("Copy", GUILayout.Width(42f))) {
                    copyPosition = positionProperty.vector3Value;
                    copyScale = scaleProperty.vector3Value;
                    copyRotation = rotationProperty.quaternionValue;
                    GUI.FocusControl(null);
                }
                
                InspectorFieldColor.Instance.Red();
                if (GUILayout.Button("Past", GUILayout.Width(42f))) {
                    PastePosition();
                    PasteRotation();
                    PasteScale();
                    GUI.FocusControl(null);
                }
                
                InspectorFieldColor.Instance.Default();
            }
            EditorGUILayout.EndHorizontal();
        }

        void PastePosition()
        {
            positionProperty.vector3Value = copyPosition;
        }

        void PasteScale()
        {
            scaleProperty.vector3Value = copyScale;
            scale = copyScale.x;
        }

        void PasteRotation()
        {
            rotationProperty.quaternionValue = copyRotation;
        }

    }
}
