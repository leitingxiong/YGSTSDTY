using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GameUtil;

/// <summary>
/// 使用CustomPropertyDrawer自定义PropertyAttribute的显示。
/// 只对可序列化的类有效，非可序列化的类没法在Inspector面板中显示。
/// </summary>
[CustomPropertyDrawer(typeof(StringValidAttribute))]
public class StringValidAttributeDrawer : PropertyDrawer
{
    /// <summary>
    /// OnGUI方法里只能使用GUI相关方法，不能使用Layout相关方法。
    /// </summary>
    /// <param name="position">位置</param>
    /// <param name="property">参数</param>
    /// <param name="label">显示的名称</param>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        StringValidAttribute targetAttribute = attribute as StringValidAttribute;
        switch (targetAttribute.ValidEnum)
        {
            case StringValidEnum.HexadecimalColorFormat:
                string stringValue = property.stringValue;
                stringValue = EditorGUI.DelayedTextField(position, label, stringValue);
                if (OtherUtility.IsBas16ColorFormat(stringValue))
                    property.stringValue = stringValue;
                else if (targetAttribute.ShowTips)
                {
                    EditorUtility.DisplayDialog("提示", $"String变量：{label.text}\n值不是十六进制的颜色格式", "确定");
                    stringValue = property.stringValue;
                    property.stringValue = OtherUtility.IsBas16ColorFormat(stringValue) ? stringValue : "#FFFFFF";
                }
                break;
            default:
                base.OnGUI(position, property, label);
                break;
        }
    }
}