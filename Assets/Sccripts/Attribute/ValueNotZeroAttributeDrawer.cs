using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

/// <summary>
/// 使用CustomPropertyDrawer自定义PropertyAttribute的显示。
/// 只对可序列化的类有效，非可序列化的类没法在Inspector面板中显示。
/// </summary>
[CustomPropertyDrawer(typeof(ValueNotZeroAttribute))]
public class ValueNotZeroAttributeDrawer : PropertyDrawer
{
    /// <summary>
    /// OnGUI方法里只能使用GUI相关方法，不能使用Layout相关方法。
    /// </summary>
    /// <param name="position">位置</param>
    /// <param name="property">参数</param>
    /// <param name="label">显示的名称</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        ValueNotZeroAttribute targetAttribute = attribute as ValueNotZeroAttribute;
        switch (property.propertyType)
        {
            case SerializedPropertyType.Integer:
                int intValue = property.intValue;
                intValue = EditorGUI.IntField(position, label, intValue);
                if (intValue != 0) 
                    property.intValue = intValue;
                else if(targetAttribute.ShowTips)
                    EditorUtility.DisplayDialog("提示", $"Integer变量：{label.text}\n值不能为0", "确定");
                break;

            case SerializedPropertyType.Float:
                float floatValue = property.floatValue;
                floatValue = EditorGUI.FloatField(position, label, floatValue);
                if (floatValue != 0)
                    property.floatValue = floatValue;
                else if(targetAttribute.ShowTips)
                    EditorUtility.DisplayDialog("提示", $"Float变量：{label.text}\n值不能为0", "确定");
                break;

            case SerializedPropertyType.Vector2:
                Vector2 vec2Value = property.vector2Value;
                vec2Value = EditorGUI.Vector2Field(position, label, vec2Value);

                if(vec2Value.x == 0 && vec2Value.y == 0 && targetAttribute.ShowTips)
                    EditorUtility.DisplayDialog("提示", $"Vector2变量：{label.text}\n值不能全为0", "确定");

                property.vector2Value = new Vector2(
                    (vec2Value.x != 0 || vec2Value.y != 0) ? vec2Value.x : property.vector2Value.x,
                    (vec2Value.y != 0 || vec2Value.x != 0) ? vec2Value.y : property.vector2Value.y);
                break;
            case SerializedPropertyType.Vector3:
                Vector3 vec3Value = property.vector3Value;
                vec3Value = EditorGUI.Vector3Field(position, label, vec3Value);

                if (vec3Value.x == 0 && vec3Value.y == 0 && vec3Value.z == 0 && targetAttribute.ShowTips)
                    EditorUtility.DisplayDialog("提示", $"Vector3变量：{label.text}\n值不能全为0", "确定");

                property.vector3Value = new Vector3(
                    (vec3Value.x != 0 || vec3Value.y != 0 || vec3Value.z != 0) ? vec3Value.x : property.vector3Value.x,
                    (vec3Value.y != 0 || vec3Value.x != 0 || vec3Value.z != 0) ? vec3Value.y : property.vector3Value.y,
                    (vec3Value.z != 0 || vec3Value.x != 0 || vec3Value.y != 0) ? vec3Value.z : property.vector3Value.z);
                break;
            case SerializedPropertyType.Vector4:
                Vector4 vec4Value = property.vector4Value;
                vec4Value = EditorGUI.Vector4Field(position, label, vec4Value);

                if (vec4Value.x == 0 && vec4Value.y == 0 && vec4Value.z == 0 && vec4Value.w == 0 && targetAttribute.ShowTips)
                    EditorUtility.DisplayDialog("提示", $"Vector4变量：{label.text}\n值不能全为0", "确定");

                property.vector4Value = new Vector4(
                    (vec4Value.x != 0 || vec4Value.y != 0 || vec4Value.z != 0 || vec4Value.w != 0) ? vec4Value.x : property.vector4Value.x,
                    (vec4Value.y != 0 || vec4Value.x != 0 || vec4Value.z != 0 || vec4Value.w != 0) ? vec4Value.y : property.vector4Value.y,
                    (vec4Value.z != 0 || vec4Value.y != 0 || vec4Value.x != 0 || vec4Value.w != 0) ? vec4Value.z : property.vector4Value.z,
                    (vec4Value.w != 0 || vec4Value.z != 0 || vec4Value.y != 0 || vec4Value.x != 0) ? vec4Value.w : property.vector4Value.w);
                break;
            default:
                base.OnGUI(position, property, label);
                break;
        }
    }
}