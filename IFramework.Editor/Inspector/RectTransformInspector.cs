using System.Reflection;
using IFramework.Core;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

// using UnityEngine.UI;

namespace IFramework.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(RectTransform))]
    public class RectTransformInspector : AbstractCustomInspector
    {
        private RectTransform rectTransform;
        private static GUIStyle styleMove;
        private static GUIStyle stylePivotSetup;
        private bool autoSetNativeSize;

        private float buttonWidth = 56f;
        private float scaleAll = 1;
        private SerializedProperty spAnchoredPosition;
        private SerializedProperty spLocalRotation;
        private SerializedProperty spLocalScale;
        private SerializedProperty spPivot;
        private SerializedProperty spSizeDelta;

        public RectTransformInspector() : base("RectTransformEditor") { }

        private void OnEnable()
        {
            rectTransform = target as RectTransform;
            spAnchoredPosition = serializedObject.FindProperty("m_AnchoredPosition");
            spSizeDelta = serializedObject.FindProperty("m_SizeDelta");
            spLocalRotation = serializedObject.FindProperty("m_LocalRotation");
            spLocalScale = serializedObject.FindProperty("m_LocalScale");
            spPivot = serializedObject.FindProperty("m_Pivot");
            scaleAll = spLocalScale.FindPropertyRelative("x").floatValue;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();
            if (stylePivotSetup == null) {
                stylePivotSetup = new GUIStyle("PreButton") {
                    // normal = new GUIStyle("CN Box").normal,
                    // active = new GUIStyle("AppToolbar").normal,
                    overflow = new RectOffset(),
                    padding = new RectOffset(0, 0, 0, 0),
                    fixedWidth = 21,
                    fixedHeight = 21
                };
                styleMove = new GUIStyle(stylePivotSetup) {
                    padding = new RectOffset(0, 0, -2, 0)
                };
            }
            GUILayout.BeginHorizontal();
            {
                GUILayout.BeginVertical();
                {
                    // EditorGUILayout.LabelField("Pivot", GUILayout.Width(50f));
                    GUILayout.BeginHorizontal();
                    {
                        ActivePivotColor(new Vector2(0, 1));
                        if (GUILayout.Button("", stylePivotSetup)) {
                            spPivot.vector2Value = new Vector2(0, 1);
                        }
                        ActivePivotColor(new Vector2(0.5f, 1));
                        if (GUILayout.Button("", stylePivotSetup)) {
                            spPivot.vector2Value = new Vector2(0.5f, 1);
                        }
                        ActivePivotColor(new Vector2(1, 1));
                        if (GUILayout.Button("", stylePivotSetup)) {
                            spPivot.vector2Value = new Vector2(1, 1);
                        }
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    {
                        ActivePivotColor(new Vector2(0, 0.5f));
                        if (GUILayout.Button("", stylePivotSetup)) {
                            spPivot.vector2Value = new Vector2(0, 0.5f);
                        }
                        ActivePivotColor(new Vector2(0.5f, 0.5f));
                        if (GUILayout.Button("+", styleMove)) {
                            spPivot.vector2Value = new Vector2(0.5f, 0.5f);
                        }
                        ActivePivotColor(new Vector2(1, 0.5f));
                        if (GUILayout.Button("", stylePivotSetup)) {
                            spPivot.vector2Value = new Vector2(1, 0.5f);
                        }
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    {
                        ActivePivotColor(new Vector2(0, 0));
                        if (GUILayout.Button("", stylePivotSetup)) {
                            spPivot.vector2Value = new Vector2(0, 0);
                        }
                        ActivePivotColor(new Vector2(0.5f, 0));
                        if (GUILayout.Button("", stylePivotSetup)) {
                            spPivot.vector2Value = new Vector2(0.5f, 0);
                        }
                        ActivePivotColor(new Vector2(1, 0));
                        if (GUILayout.Button("", stylePivotSetup)) {
                            spPivot.vector2Value = new Vector2(1, 0);
                        }
                        ActivePivotColor(new Vector2(2, 2));
                    }
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();
                GUILayout.BeginVertical();
                {
                    GUILayout.BeginHorizontal();
                    {
                        InspectorFieldColor.Instance.Green();
                        EditorGUIUtility.labelWidth = 20;
                        float newScale = EditorGUILayout.FloatField("▲", scaleAll);
                        if (!Mathf.Approximately(scaleAll, newScale)) {
                            scaleAll = newScale;
                            spLocalScale.vector3Value = Vector3.one * scaleAll;
                        }
                        if (GUILayout.Button(".", GUILayout.Width(20f))) {
                            Undo.RecordObjects(targets, "Round");
                            spAnchoredPosition.vector2Value = EditorUtils.Round(spAnchoredPosition.vector2Value, 0);
                            spSizeDelta.vector2Value = EditorUtils.Round(spSizeDelta.vector2Value, 0);
                            spLocalScale.vector3Value = EditorUtils.Round(spLocalScale.vector3Value, 0);
                            GUI.FocusControl(null);
                        }
                        if (GUILayout.Button(".0", GUILayout.Width(22f))) {
                            Undo.RecordObjects(targets, "Round");
                            spAnchoredPosition.vector2Value = EditorUtils.Round(spAnchoredPosition.vector2Value, 1);
                            spSizeDelta.vector2Value = EditorUtils.Round(spSizeDelta.vector2Value, 1);
                            spLocalScale.vector3Value = EditorUtils.Round(spLocalScale.vector3Value, 1);
                            GUI.FocusControl(null);
                        }
                        if (GUILayout.Button(".00", GUILayout.Width(30f))) {
                            Undo.RecordObjects(targets, "Round");
                            spAnchoredPosition.vector2Value = EditorUtils.Round(spAnchoredPosition.vector2Value, 2);
                            spSizeDelta.vector2Value = EditorUtils.Round(spSizeDelta.vector2Value, 2);
                            spLocalScale.vector3Value = EditorUtils.Round(spLocalScale.vector3Value, 2);
                            GUI.FocusControl(null);
                        }
                        InspectorFieldColor.Instance.Default();
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    {
                        if (GUILayout.Button("Reset")) {
                            rectTransform.LocalIdentity();
                            scaleAll = spLocalScale.vector3Value.x;
                            GUI.FocusControl(null);
                        }
                        if (GUILayout.Button("Position")) {
                            rectTransform.LocalPositionIdentity();
                            GUI.FocusControl(null);
                        }
                        if (GUILayout.Button("Size")) {
                            rectTransform.SizeDeltaIdentity();
                            GUI.FocusControl(null);
                        }
                        if (GUILayout.Button("Rotation")) {
                            rectTransform.LocalRotationIdentity();
                            GUI.FocusControl(null);
                        }
                        if (GUILayout.Button("Scale")) {
                            rectTransform.LocalScaleIdentity();
                            scaleAll = rectTransform.localScale.x;
                            GUI.FocusControl(null);
                        }
                    }
                    GUILayout.EndHorizontal();
                    GUILayout.BeginHorizontal();
                    if (GUILayout.Button("Fill",GUILayout.MinWidth(buttonWidth))) {
                        Undo.RecordObjects(targets, "Fill");
                        foreach (Object item in targets) {
                            RectTransform rtf = item as RectTransform;
                            rtf.anchorMax = Vector2.one;
                            rtf.anchorMin = Vector2.zero;
                            rtf.offsetMax = Vector2.zero;
                            rtf.offsetMin = Vector2.zero;
                        }
                        GUI.FocusControl(null);
                    }
                    if (GUILayout.Button("Normal",GUILayout.MinWidth(buttonWidth))) {
                        Undo.RecordObjects(targets, "Normal");
                        foreach (Object item in targets) {
                            RectTransform rtf = item as RectTransform;
                            Rect rect = rtf.rect;
                            rtf.anchorMax = new Vector2(0.5f, 0.5f);
                            rtf.anchorMin = new Vector2(0.5f, 0.5f);
                            rtf.sizeDelta = rect.size;
                        }
                        GUI.FocusControl(null);
                    }
                    InspectorFieldColor.Instance.Yellow();
                    if (GUILayout.Button("Copy",GUILayout.MinWidth(buttonWidth))) {
                        ComponentUtility.CopyComponent(target as RectTransform);
                        GUI.FocusControl(null);
                    }
                    InspectorFieldColor.Instance.Red();
                    if (GUILayout.Button("Paste",GUILayout.MinWidth(buttonWidth))) {
                        foreach (Object item in targets) {
                            ComponentUtility.PasteComponentValues(item as RectTransform);
                        }
                        GUI.FocusControl(null);
                    }
                    InspectorFieldColor.Instance.Default();
                    GUILayout.EndHorizontal();
                }
                GUILayout.EndVertical();
            }
            GUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// 激活Pivot的颜色
        /// </summary>
        /// <param name="vector2"></param>
        private void ActivePivotColor(Vector2 vector2)
        {
            if (spPivot.vector2Value == vector2) {
                InspectorFieldColor.Instance.Green();
            }
            else {
                InspectorFieldColor.Instance.Default();
            }
        }
    }
}
