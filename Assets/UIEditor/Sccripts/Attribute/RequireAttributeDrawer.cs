using UnityEditor;
using UnityEngine;

/// <summary>
/// 使用CustomPropertyDrawer自定义PropertyAttribute的显示。
/// 只对可序列化的类有效，非可序列化的类没法在Inspector面板中显示。
/// </summary>
[CustomPropertyDrawer(typeof(RequireAttribute))]
public class RequireAttributeDrawer : PropertyDrawer
{
    /// <summary>
    /// OnGUI方法里只能使用GUI相关方法，不能使用Layout相关方法。
    /// </summary>
    /// <param name="position">位置</param>
    /// <param name="property">参数</param>
    /// <param name="label">显示的名称</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        RequireAttribute targetAttribute = attribute as RequireAttribute;
        Color originalColor = GUI.backgroundColor;
        //判断参数类型是否是引用类型
        if ((property.propertyType == SerializedPropertyType.ObjectReference || property.propertyType == SerializedPropertyType.ExposedReference))
        {
            if (property.objectReferenceValue == null)
                GUI.backgroundColor = targetAttribute.WithoutReferenceColor;
            else
                GUI.backgroundColor = targetAttribute.HaveReferenceColor;
        }

        EditorGUI.PropertyField(position, property);
        GUI.backgroundColor = originalColor;
    }
}
