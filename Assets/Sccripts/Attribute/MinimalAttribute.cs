using UnityEngine;
using System;

/// <summary>
/// 设置对应类型的最小值
/// </summary>
//继承PropertyAttribute的类，便能使用一个自定义的PropertyDrawer类去控制其在Inspector面板上的显示。
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class MinimalAttribute : PropertyAttribute
{
    /// <summary>
    /// 最小值
    /// </summary>
    public float MinValue { get; private set; }
    /// <summary>
    /// 向量最小值
    /// </summary>
    public Vector4 VectorMinValue { get; private set; }
    /// <summary>
    /// 显示提示弹窗
    /// </summary>
    public bool ShowTips { get; private set; }
    /// <summary>
    /// 向量的数值是否都不能小于设定值，默认是单个分量能小于设定值，不能所有分量都小于设定值
    /// </summary>
    public bool UseAndOperate { get; private set; }
    /// <summary>
    /// 变量昵称
    /// </summary>
    public string NickName { get; private set; }

    /// <summary>
    /// 无参构造函数
    /// </summary>
    public MinimalAttribute()
    {
        this.MinValue = 0.1f;
        this.VectorMinValue = new Vector4(0.1f, 0.1f, 0.1f, 0.1f);
        this.ShowTips = true;
        this.UseAndOperate = true;
        this.NickName = "";
    }

    /// <summary>
    /// 单参构造函数，同时作用于数值和向量
    /// </summary>
    /// <param name="minValue">最小值</param>
    public MinimalAttribute(float minValue)
    {
        this.MinValue = minValue;
        this.VectorMinValue = new Vector4(minValue, minValue, minValue, minValue);
        this.ShowTips = true;
        this.UseAndOperate = true;
        this.NickName = "";
    }

    /// <summary>
    /// 同时作用于数值和向量，同时设置别名
    /// </summary>
    /// <param name="nickName">设置别名</param>
    /// <param name="minValue">最小值</param>
    public MinimalAttribute(string nickName, float minValue)
    {
        this.MinValue = minValue;
        this.VectorMinValue = new Vector4(minValue, minValue, minValue, minValue);
        this.ShowTips = true;
        this.UseAndOperate = true;
        this.NickName = nickName;
    }

    /// <summary>
    /// 两参构造函数
    /// </summary>
    /// <param name="minValue">浮点数和整型的最小值（向量的所有分量都将使用这一个值）</param>
    /// <param name="useAndOperate">向量的数值是否都不能小于设定值，默认是单个分量能小于设定值，不能所有分量都小于设定值</param>
    public MinimalAttribute(float minValue,bool useAndOperate)
    {
        this.MinValue = minValue;
        this.VectorMinValue = new Vector4(minValue, minValue, minValue, minValue);
        this.ShowTips = true;
        this.UseAndOperate = useAndOperate;
        this.NickName = "";
    }

    /// <summary>
    /// 针对任意的数值和向量，同时设置别名
    /// </summary>
    /// <param name="nickName">设置别名</param>
    /// <param name="minValue">浮点数和整型的最小值（向量的所有分量都将使用这一个值）</param>
    /// <param name="useAndOperate">向量的数值是否都不能小于设定值，默认是单个分量能小于设定值，不能所有分量都小于设定值</param>
    public MinimalAttribute(string nickName, float minValue, bool useAndOperate)
    {
        this.MinValue = minValue;
        this.VectorMinValue = new Vector4(minValue, minValue, minValue, minValue);
        this.ShowTips = true;
        this.UseAndOperate = useAndOperate;
        this.NickName = nickName;
    }

    /// <summary>
    /// 三参构造函数，针对任意的数值和向量
    /// </summary>
    /// <param name="minValue">浮点数和整型的最小值</param>
    /// <param name="showTips">输入值小于设定的最小值时，是否给出提示弹窗</param>
    /// <param name="useAndOperate">向量的数值是否都不能小于设定值，默认是单个分量能小于设定值，不能所有分量都小于设定值</param>
    public MinimalAttribute(float minValue, bool showTips, bool useAndOperate)
    {
        this.MinValue = minValue;
        this.VectorMinValue = new Vector4(minValue, minValue, minValue, minValue);
        this.ShowTips = showTips;
        this.UseAndOperate = useAndOperate;
        this.NickName = "";
    }

    /// <summary>
    /// 针对任意的数值和向量，同时设置别名
    /// </summary>
    /// <param name="nickName">设置别名</param>
    /// <param name="minValue">浮点数和整型的最小值</param>
    /// <param name="showTips">输入值小于设定的最小值时，是否给出提示弹窗</param>
    /// <param name="useAndOperate">向量的数值是否都不能小于设定值，默认是单个分量能小于设定值，不能所有分量都小于设定值</param>
    public MinimalAttribute(string nickName, float minValue, bool showTips, bool useAndOperate)
    {
        this.MinValue = minValue;
        this.VectorMinValue = new Vector4(minValue, minValue, minValue, minValue);
        this.ShowTips = showTips;
        this.UseAndOperate = useAndOperate;
        this.NickName = nickName;
    }

    /// <summary>
    /// 主要作用于二维向量
    /// </summary>
    /// <param name="xMinValue">分量x的最小值同时也可作为数值的最小值</param>
    /// <param name="yMinValue">分量y的最小值</param>
    public MinimalAttribute(float xMinValue, float yMinValue) 
    {
        this.MinValue = xMinValue;
        this.VectorMinValue = new Vector4(xMinValue, yMinValue, xMinValue, xMinValue);
        this.ShowTips = true;
        this.UseAndOperate = true;
        this.NickName = "";
    }

    /// <summary>
    /// 主要作用于二维向量，同时设置别名
    /// </summary>
    /// <param name="nickName">设置别名</param>
    /// <param name="xMinValue">分量x的最小值同时也可作为数值的最小值</param>
    /// <param name="yMinValue">分量y的最小值</param>
    public MinimalAttribute(string nickName, float xMinValue, float yMinValue)
    {
        this.MinValue = xMinValue;
        this.VectorMinValue = new Vector4(xMinValue, yMinValue, xMinValue, xMinValue);
        this.ShowTips = true;
        this.UseAndOperate = true;
        this.NickName = nickName;
    }

    /// <summary>
    /// 主要作用于三维向量，同时设置别名
    /// </summary>
    /// <param name="nickName">设置别名</param>
    /// <param name="xMinValue">分量x的最小值同时也可作为数值的最小值</param>
    /// <param name="yMinValue">分量y的最小值</param>
    /// <param name="showTips">输入值小于设定的最小值时，是否给出提示弹窗</param>
    /// <param name="useAndOperate">向量的数值是否都不能小于设定值，默认是单个分量能小于设定值，不能所有分量都小于设定值</param>
    public MinimalAttribute(string nickName, float xMinValue, float yMinValue, bool showTips, bool useAndOperate)
    {
        this.MinValue = xMinValue;
        this.VectorMinValue = new Vector4(xMinValue, yMinValue, xMinValue, xMinValue);
        this.ShowTips = showTips;
        this.UseAndOperate = useAndOperate;
        this.NickName = nickName;
    }

    /// <summary>
    /// 主要作用于三维向量
    /// </summary>
    /// <param name="xMinValue">分量x的最小值同时也可作为数值的最小值</param>
    /// <param name="yMinValue">分量y的最小值</param>
    /// <param name="zMinValue">分量z的最小值</param>
    public MinimalAttribute(float xMinValue, float yMinValue, float zMinValue)
    {
        this.MinValue = xMinValue;
        this.VectorMinValue = new Vector4(xMinValue, yMinValue, zMinValue, xMinValue);
        this.ShowTips = true;
        this.UseAndOperate = true;
        this.NickName = "";
    }

    /// <summary>
    /// 主要作用于三维向量
    /// </summary>
    /// <param name="xMinValue">分量x的最小值同时也可作为数值的最小值</param>
    /// <param name="yMinValue">分量y的最小值</param>
    /// <param name="zMinValue">分量z的最小值</param>
    /// <param name="showTips">向量的数值是否都不能小于设定值，默认是单个分量能小于设定值，不能所有分量都小于设定值</param>

    public MinimalAttribute(float xMinValue, float yMinValue, float zMinValue, bool showTips)
    {
        this.MinValue = xMinValue;
        this.VectorMinValue = new Vector4(xMinValue, yMinValue, zMinValue, xMinValue);
        this.ShowTips = showTips;
        this.UseAndOperate = true;
        this.NickName = "";
    }

    /// <summary>
    /// 主要作用于三维向量，同时设置别名
    /// </summary>
    /// <param name="nickName">设置别名</param>
    /// <param name="xMinValue">分量x的最小值同时也可作为数值的最小值</param>
    /// <param name="yMinValue">分量y的最小值</param>
    /// <param name="zMinValue">分量z的最小值</param>
    /// <param name="showTips">向量的数值是否都不能小于设定值，默认是单个分量能小于设定值，不能所有分量都小于设定值</param>
    public MinimalAttribute(string nickName, float xMinValue, float yMinValue, float zMinValue, bool showTips)
    {
        this.MinValue = xMinValue;
        this.VectorMinValue = new Vector4(xMinValue, yMinValue, zMinValue, xMinValue);
        this.ShowTips = showTips;
        this.UseAndOperate = true;
        this.NickName = nickName;
    }

    /// <summary>
    /// 主要作用于三维向量，同时设置别名
    /// </summary>
    /// <param name="nickName">设置别名</param>
    /// <param name="xMinValue">分量x的最小值同时也可作为数值的最小值</param>
    /// <param name="yMinValue">分量y的最小值</param>
    /// <param name="zMinValue">分量z的最小值</param>
    /// <param name="showTips">输入值小于设定的最小值时，是否给出提示弹窗</param>
    /// <param name="useAndOperate">向量的数值是否都不能小于设定值，默认是单个分量能小于设定值，不能所有分量都小于设定值</param>
    public MinimalAttribute(string nickName, float xMinValue, float yMinValue, float zMinValue, bool showTips, bool useAndOperate)
    {
        this.MinValue = xMinValue;
        this.VectorMinValue = new Vector4(xMinValue, yMinValue, zMinValue, xMinValue);
        this.ShowTips = showTips;
        this.UseAndOperate = useAndOperate;
        this.NickName = nickName;
    }

    /// <summary>
    /// 四参构造函数，主要作用于四维向量
    /// </summary>
    /// <param name="xMinValue">分量x的最小值同时也可作为数值的最小值</param>
    /// <param name="yMinValue">分量y的最小值</param>
    /// <param name="zMinValue">分量z的最小值</param>
    /// <param name="wMinValue">分量w的最小值</param>
    public MinimalAttribute(float xMinValue, float yMinValue, float zMinValue, float wMinValue)
    {
        this.MinValue = xMinValue;
        this.VectorMinValue = new Vector4(xMinValue, yMinValue, zMinValue, wMinValue);
        this.ShowTips = true;
        this.UseAndOperate = true;
        this.NickName = "";
    }

    /// <summary>
    /// 主要作用于四维向量，同时设置别名
    /// </summary>
    /// <param name="nickName">设置别名</param>
    /// <param name="xMinValue">分量x的最小值同时也可作为数值的最小值</param>
    /// <param name="yMinValue">分量y的最小值</param>
    /// <param name="zMinValue">分量z的最小值</param>
    /// <param name="wMinValue">分量w的最小值</param>
    public MinimalAttribute(string nickName, float xMinValue, float yMinValue, float zMinValue, float wMinValue)
    {
        this.MinValue = xMinValue;
        this.VectorMinValue = new Vector4(xMinValue, yMinValue, zMinValue, wMinValue);
        this.ShowTips = true;
        this.UseAndOperate = true;
        this.NickName = nickName;
    }
}