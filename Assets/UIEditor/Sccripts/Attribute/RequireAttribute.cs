using UnityEngine;
using GameUtil;
using System;

/// <summary>
/// 表明该变量必须具有引用
/// </summary>
//继承PropertyAttribute的类，便能使用一个自定义的PropertyDrawer类去控制其在Inspector面板上的显示。
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false,Inherited = false),]
public class RequireAttribute : PropertyAttribute
{
    /// <summary>
    /// 有具体引用时的颜色
    /// </summary>
    public Color HaveReferenceColor { get; private set; }
    /// <summary>
    /// 无具体引用时的颜色
    /// </summary>
    public Color WithoutReferenceColor { get; private set; }

    /// <summary>
    /// 无参构造函数（表明无引用的时候颜色是红色）
    /// </summary>
    public RequireAttribute()
    {
        HaveReferenceColor = Color.green;
        WithoutReferenceColor = Color.red;
    }

    /// <summary>
    /// 设置有引用时的颜色 
    /// </summary>
    /// <param name="hexadecimalFormatHave">16进制颜色格式</param>
    public RequireAttribute(string hexadecimalFormatHave) 
    {
        Color temporary = Color.white;
        if (!OtherUtility.IsBas16ColorFormat(hexadecimalFormatHave))
            hexadecimalFormatHave = "#FFFFFF";
        ColorUtility.TryParseHtmlString(hexadecimalFormatHave, out temporary);
        HaveReferenceColor = temporary;
        WithoutReferenceColor = Color.red;
    }

    /// <summary>
    /// 两参构造函数，设置有引用和无引用时的颜色 
    /// </summary>
    /// <param name="hexadecimalFormatHave">具有引用时的颜色（十六进制格式）</param>
    /// <param name="hexadecimalFormatWithout">无具体引用时的颜色（十六进制格式）</param>
    public RequireAttribute(string hexadecimalFormatHave, string hexadecimalFormatWithout)
    {
        Color temporary = Color.white;
        Color temporary_1 = Color.white;
        if (!OtherUtility.IsBas16ColorFormat(hexadecimalFormatHave)) 
            hexadecimalFormatHave = "#FFFFFF";
        if (!OtherUtility.IsBas16ColorFormat(hexadecimalFormatWithout))
            hexadecimalFormatWithout = "#FFFFFF";
        ColorUtility.TryParseHtmlString(hexadecimalFormatHave, out temporary);
        ColorUtility.TryParseHtmlString(hexadecimalFormatWithout, out temporary);
        HaveReferenceColor = temporary;
        WithoutReferenceColor = temporary_1;
    }
}