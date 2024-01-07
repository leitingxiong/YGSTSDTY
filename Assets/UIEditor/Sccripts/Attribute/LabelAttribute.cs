using UnityEngine;
using System;

/// <summary>
/// 继承PropertyAttribute的类，便能使用一个自定义的PropertyDrawer类
/// 去控制其在Inspector面板上的显示。
/// </summary>
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class LabelAttribute : PropertyAttribute
{
    public string Name { get ; private set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="inputName">Label名称</param>
    public LabelAttribute(string inputName)
    {
        Name = inputName;
    }
}