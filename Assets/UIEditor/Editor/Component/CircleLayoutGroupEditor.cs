using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(CircleLayoutGroup))]
public class CircleLayoutGroupEditor : Editor
{
    private enum Direction
    {
        Right = 0,
        Up = 1,
        Left = 2,
        Down = 3
    }

    private CircleLayoutGroup circleLayoutGroup;
    private SerializedProperty radius;
    private SerializedProperty angleDelta;
    private SerializedProperty startDirection;
    private SerializedProperty autoRefresh;
    private SerializedProperty controlChildSize;
    private SerializedProperty childSize;
    private Direction direction;

    private void OnEnable()
    {
        circleLayoutGroup = target as CircleLayoutGroup;
        radius = serializedObject.FindProperty("radius");
        angleDelta = serializedObject.FindProperty("angleDelta");
        startDirection = serializedObject.FindProperty("startDirection");
        controlChildSize = serializedObject.FindProperty("controlChildSize");
        childSize = serializedObject.FindProperty("childSize");

        direction = (Direction)startDirection.intValue;
        circleLayoutGroup.RefreshLayout();
    }

    public override void OnInspectorGUI()
    {
        float newRadius = EditorGUILayout.FloatField("Radius", radius.floatValue);
        if (newRadius != radius.floatValue)
        {
            Undo.RecordObject(target, "Radius");
            radius.floatValue = newRadius;
            IsChanged();
        }

        float newAngleDelta = EditorGUILayout.FloatField("Angle Delta", angleDelta.floatValue);
        if (newAngleDelta != angleDelta.floatValue)
        {
            Undo.RecordObject(target, "Angle Delta");
            angleDelta.floatValue = newAngleDelta;
            IsChanged();
        }

        Direction newDirection = (Direction)EditorGUILayout.EnumPopup("Start Direction", direction);
        if (newDirection != direction)
        {
            Undo.RecordObject(target, "Start Direction");
            direction = newDirection;
            startDirection.intValue = (int)direction;
            IsChanged();
        }

        bool newControlChildSize = EditorGUILayout.Toggle("Control Child Size", controlChildSize.boolValue);
        if (newControlChildSize != controlChildSize.boolValue)
        {
            Undo.RecordObject(target, "Control Child Size");
            controlChildSize.boolValue = newControlChildSize;
            IsChanged();
        }

        if (controlChildSize.boolValue)
        {
            Vector2 newChildSize = EditorGUILayout.Vector2Field("Child Size", childSize.vector2Value);
            if (newChildSize != childSize.vector2Value)
            {
                Undo.RecordObject(target, "Child Size");
                childSize.vector2Value = newChildSize;
                IsChanged();
            }
        }
    }

    /// <summary>
    /// 数据发生了变动
    /// </summary>
    private void IsChanged()
    {
        if (GUI.changed)
        {
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(target);
            circleLayoutGroup.RefreshLayout();
        }
    }
}
#endif