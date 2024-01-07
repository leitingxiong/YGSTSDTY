using UnityEngine;
using System;

/// <summary>
/// 如果是向量则值不能全为0，整型和浮点型则不能为0
/// </summary>
//继承PropertyAttribute的类，便能使用一个自定义的PropertyDrawer类去控制其在Inspector面板上的显示。
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class ValueNotZeroAttribute : PropertyAttribute
{
    public bool ShowTips { get; private set; }

    /// <summary>
    /// 无参构造函数，显示提示信息
    /// </summary>
    public ValueNotZeroAttribute() 
    {
        this.ShowTips = true;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="showTips">是否显示提示信息</param>
    public ValueNotZeroAttribute(bool showTips)
    {
        this.ShowTips = showTips;
    }
}