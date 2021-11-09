using System;
using UnityEngine;
using System.Collections;
using IFramework.Core;
using UnityEditor;
using System.Reflection;

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
            DrawBottom();
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
                InspectorFieldColor.Instance.Yellow();
                bool copy = GUILayout.Button("C", GUILayout.Width(20f));
                InspectorFieldColor.Instance.Red();
                bool paste = GUILayout.Button("P", GUILayout.Width(20f));
                InspectorFieldColor.Instance.Default();
                if (reset) {
                    positionProperty.vector3Value = Vector3.zero;
                    GUI.FocusControl(null);
                }
                if (copy) {
                    CopyPosition();
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
                InspectorFieldColor.Instance.Yellow();
                bool copy = GUILayout.Button("C", GUILayout.Width(20f));
                InspectorFieldColor.Instance.Red();
                bool paste = GUILayout.Button("P", GUILayout.Width(20f));
                InspectorFieldColor.Instance.Default();
                if (reset) {
                    scaleProperty.vector3Value = Vector3.one;
                    scale = 1;
                    GUI.FocusControl(null);
                }
                if (copy) {
                    CopyScale();
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
            GUILayout.BeginHorizontal(GUILayout.MinWidth(323f), GUILayout.MaxWidth(float.MaxValue));
            {
                EditorGUILayout.LabelField("Rotation", GUILayout.Width(50f));
                rotationGUI.Draw();
                bool reset = GUILayout.Button("R", GUILayout.Width(20f));
                InspectorFieldColor.Instance.Yellow();
                bool copy = GUILayout.Button("C", GUILayout.Width(20f));
                InspectorFieldColor.Instance.Red();
                bool paste = GUILayout.Button("P", GUILayout.Width(20f));
                InspectorFieldColor.Instance.Default();
                if (reset) {
                    rotationGUI.Reset();
                    GUI.FocusControl(null);
                }
                if (copy) {
                    CopyRotation();
                }
                if (paste) {
                    PasteRotation();
                    GUI.FocusControl(null);
                }
            }
            GUILayout.EndHorizontal();
        }

    void DrawBottom()
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.LabelField("Scale", GUILayout.Width(50f));
            EditorGUIUtility.labelWidth = 10f;
            
            InspectorFieldColor.Instance.Green();
            float newScale = EditorGUILayout.FloatField("A", scale);
            if (!Mathf.Approximately(scale, newScale))
            {
                scale = newScale;
                scaleProperty.vector3Value = Vector3.one * scale;
            }

            if (GUILayout.Button(".", GUILayout.Width(30f)))
            {
                Undo.RecordObjects(targets, "Round");
                for (int i = 0; i < targets.Length; i++)
                {
                    Transform o = targets[i] as Transform;
                    o.localPosition = Round(o.localPosition);
                    o.localScale = Round(o.localScale);
                    scale = o.localScale.x;
                }
            }

            if (GUILayout.Button(".0", GUILayout.Width(30f)))
            {
                Undo.RecordObjects(targets, "Round");
                for (int i = 0; i < targets.Length; i++)
                {
                    Transform o = targets[i] as Transform;
                    o.localPosition = Round(o.localPosition, 1);
                    o.localScale = Round(o.localScale, 1);
                    scale = o.localScale.x;
                }
            }

            if (GUILayout.Button(".00", GUILayout.Width(30f)))
            {
                Undo.RecordObjects(targets, "Round");
                for (int i = 0; i < targets.Length; i++)
                {
                    Transform o = targets[i] as Transform;
                    o.localPosition = Round(o.localPosition, 2);
                    o.localScale = Round(o.localScale, 2);
                    scale = o.localScale.x;
                }
            }
            InspectorFieldColor.Instance.Yellow();
            if (GUILayout.Button("Copy", GUILayout.Width(50f))) {
                CopyPosition();
                CopyRotation();
                CopyScale();
            }
            
            InspectorFieldColor.Instance.Red();
            if (GUILayout.Button("Past", GUILayout.Width(50f))) {
                PastePosition();
                PasteRotation();
                PasteScale();
            }
            
            InspectorFieldColor.Instance.Default();
        }
        EditorGUILayout.EndHorizontal();
    }

        void CopyPosition()
        {
            copyPosition = positionProperty.vector3Value;
        }

        void PastePosition()
        {
            positionProperty.vector3Value = copyPosition;
        }

        void CopyScale()
        {
            copyScale = scaleProperty.vector3Value;
        }

        void PasteScale()
        {
            scaleProperty.vector3Value = copyScale;
            scale = copyScale.x;
        }

        void CopyRotation()
        {
            copyRotation = rotationProperty.quaternionValue;
        }

        void PasteRotation()
        {
            rotationProperty.quaternionValue = copyRotation;
        }

        private static Vector3 Round(Vector3 v3Value, int nDecimalPoint = 0)
        {
            int nScale = 1;
            for (int i = 0; i < nDecimalPoint; i++) {
                nScale *= 10;
            }
            v3Value *= nScale;
            v3Value.x = Mathf.RoundToInt(v3Value.x);
            v3Value.y = Mathf.RoundToInt(v3Value.y);
            v3Value.z = Mathf.RoundToInt(v3Value.z);
            return v3Value / nScale;
        }
    }
}
