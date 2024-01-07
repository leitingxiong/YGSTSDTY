using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StringValidEnum 
{
    /// <summary>
    /// 什么都不检测
    /// </summary>
    None,
    /// <summary>
    /// 是否是16进制颜色格式
    /// </summary>
    HexadecimalColorFormat,
}

/// <summary>
/// 检查字符串是否符合标准
/// </summary>
//继承PropertyAttribute的类，便能使用一个自定义的PropertyDrawer类去控制其在Inspector面板上的显示。
public class StringValidAttribute : PropertyAttribute
{
    /// <summary>
    /// 检测类型
    /// </summary>
    public StringValidEnum ValidEnum { get; private set; }
    /// <summary>
    /// 显示提示弹窗
    /// </summary>
    public bool ShowTips { get; private set; }

    /// <summary>
    /// 无参构造函数
    /// </summary>
    public StringValidAttribute() 
    {
        this.ValidEnum = StringValidEnum.None;
        this.ShowTips = true;
    }

    /// <summary>
    /// 单参构造函数
    /// </summary>
    /// <param name="validEnum">字符串检测类型</param>
    public StringValidAttribute(StringValidEnum validEnum)
    {
        this.ValidEnum = validEnum;
        this.ShowTips = true;
    }

    /// <summary>
    /// 两参构造函数
    /// </summary>
    /// <param name="validEnum">字符串检测类型</param>
    /// <param name="showTips">是否显示提示弹窗</param>
    public StringValidAttribute(StringValidEnum validEnum, bool showTips)
    {
        this.ValidEnum = validEnum;
        this.ShowTips = showTips;
    }
}