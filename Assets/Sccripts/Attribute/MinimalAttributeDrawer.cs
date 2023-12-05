using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using ExtendsFunction;

/// <summary>
/// 使用CustomPropertyDrawer自定义PropertyAttribute的显示。
/// 只对可序列化的类有效，非可序列化的类没法在Inspector面板中显示。
/// </summary>
[CustomPropertyDrawer(typeof(MinimalAttribute))]
public class MinimalAttributeDrawer : PropertyDrawer
{
    /// <summary>
    /// OnGUI方法里只能使用GUI相关方法，不能使用Layout相关方法。
    /// </summary>
    /// <param name="position">位置</param>
    /// <param name="property">参数</param>
    /// <param name="label">显示的名称</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        MinimalAttribute targetAttribute = attribute as MinimalAttribute;
        if (!string.IsNullOrEmpty(targetAttribute.NickName))
            label.text = targetAttribute.NickName;

        switch (property.propertyType)
        {
            case SerializedPropertyType.Integer:
                int intValue = property.intValue;
                intValue = targetAttribute.ShowTips ? EditorGUI.DelayedIntField(position, label, intValue) : EditorGUI.IntField(position, label, intValue);
                if (intValue >= targetAttribute.MinValue)
                    property.intValue = intValue;
                else if (targetAttribute.ShowTips)
                    EditorUtility.DisplayDialog("提示", $"Integer变量：{label.text}\n值小于设定的最小值{targetAttribute.MinValue}", "确定");
                break;
            case SerializedPropertyType.Float:
                float floatValue = property.floatValue;
                floatValue = targetAttribute.ShowTips ? EditorGUI.DelayedFloatField(position, label, floatValue) : EditorGUI.FloatField(position, label, floatValue);
                if (floatValue >= targetAttribute.MinValue)
                    property.floatValue = floatValue;
                else if (targetAttribute.ShowTips)
                    EditorUtility.DisplayDialog("提示", $"Float变量：{label.text}\n值小于设定的最小值{targetAttribute.MinValue}", "确定");
                break;
            case SerializedPropertyType.Vector2:
                Vector2 vec2Value = property.vector2Value;
                vec2Value = EditorGUI.Vector2Field(position, label, vec2Value);
                if (vec2Value.Vector2ALessThanB(targetAttribute.VectorMinValue, targetAttribute.UseAndOperate))
                {
                    if (targetAttribute.ShowTips)
                        EditorUtility.DisplayDialog("提示", $"Vector2变量：{label.text}\n值小于设定的最小值{targetAttribute.VectorMinValue}", "确定");
                    break;
                }
                property.vector2Value = vec2Value;
                break;
            case SerializedPropertyType.Vector3:
                Vector3 vec3Value = property.vector3Value;
                vec3Value = EditorGUI.Vector3Field(position, label, vec3Value);
                if (vec3Value.Vector3ALessThanB((Vector3)targetAttribute.VectorMinValue, targetAttribute.UseAndOperate))
                {

                    if (targetAttribute.ShowTips)
                        EditorUtility.DisplayDialog("提示", $"Vector3变量：{label.text}\n值小于设定的最小值{targetAttribute.VectorMinValue}", "确定");
                    break;
                }
                property.vector3Value = vec3Value;
                break;
            case SerializedPropertyType.Vector4:
                Vector4 vec4Value = property.vector4Value;
                vec4Value = EditorGUI.Vector4Field(position, label, vec4Value);

                if (vec4Value.Vector4ALessThanB(targetAttribute.VectorMinValue, targetAttribute.UseAndOperate)) 
                {
                    if(targetAttribute.ShowTips)
                        EditorUtility.DisplayDialog("提示", $"Vector4变量：{label.text}\n值小于设定的最小值{targetAttribute.VectorMinValue}", "确定");
                    break;
                }
                property.vector4Value = vec4Value;
                break;
            default:
                base.OnGUI(position, property, label);
                break;
        }
    }
}