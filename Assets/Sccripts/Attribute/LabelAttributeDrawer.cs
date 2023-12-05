using UnityEditor;
using UnityEngine;

/// <summary>
/// 使用CustomPropertyDrawer自定义PropertyAttribute的显示。
/// 只对可序列化的类有效，非可序列化的类没法在Inspector面板中显示。
/// </summary>
[CustomPropertyDrawer(typeof(LabelAttribute))]
public class LabelAttributeDrawer : PropertyDrawer
{
    /// <summary>
    /// OnGUI方法里只能使用GUI相关方法，不能使用Layout相关方法。
    /// </summary>
    /// <param name="position">位置</param>
    /// <param name="property">参数</param>
    /// <param name="label">显示的名称</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        LabelAttribute attr = attribute as LabelAttribute;
        if (attr.Name.Length > 0)
        {
            label.text = attr.Name;
        }
        EditorGUI.PropertyField(position, property, label);
    }
}