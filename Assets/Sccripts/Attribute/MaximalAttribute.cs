using UnityEngine;
using System;

/// <summary>
/// 设置对应类型的最大值
/// </summary>
//继承PropertyAttribute的类，便能使用一个自定义的PropertyDrawer类去控制其在Inspector面板上的显示。
[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
public class MaximalAttribute : PropertyAttribute
{
    /// <summary>
    /// 最大值
    /// </summary>
    public float MaxValue { get; private set; }
    /// <summary>
    /// 向量最大值
    /// </summary>
    public Vector4 VectorMaxValue { get; private set; }
    /// <summary>
    /// 显示提示弹窗
    /// </summary>
    public bool ShowTips { get; private set; }
    /// <summary>
    /// 向量的数值是否都不能大于设定值，默认是单个分量能大于设定值，不能所有分量都大于设定值
    /// </summary>
    public bool UseAndOperate { get; private set; }
    /// <summary>
    /// 变量昵称
    /// </summary>
    public string NickName { get; private set; }

    /// <summary>
    /// 无参构造函数
    /// </summary>
    public MaximalAttribute()
    {
        this.MaxValue = 0.1f;
        this.VectorMaxValue = new Vector4(0.1f, 0.1f, 0.1f, 0.1f);
        this.ShowTips = true;
        this.UseAndOperate = true;
        this.NickName = "";
    }

    /// <summary>
    /// 单参构造函数，同时作用于数值和向量
    /// </summary>
    /// <param name="maxValue">最大值</param>
    public MaximalAttribute(float maxValue)
    {
        this.MaxValue = maxValue;
        this.VectorMaxValue = new Vector4(maxValue, maxValue, maxValue, maxValue);
        this.ShowTips = true;
        this.UseAndOperate = true;
        this.NickName = "";
    }

    /// <summary>
    /// 同时作用于数值和向量，同时设置别名
    /// </summary>
    /// <param name="nickName">设置别名</param>
    /// <param name="maxValue">最大值</param>
    public MaximalAttribute(string nickName, float maxValue)
    {
        this.MaxValue = maxValue;
        this.VectorMaxValue = new Vector4(maxValue, maxValue, maxValue, maxValue);
        this.ShowTips = true;
        this.UseAndOperate = true;
        this.NickName = nickName;
    }

    /// <summary>
    /// 两参构造函数
    /// </summary>
    /// <param name="maxValue">浮点数和整型的最大值（向量的所有分量都将使用这一个值）</param>
    /// <param name="useAndOperate">向量的数值是否都不能大于设定值，默认是单个分量能大于设定值，不能所有分量都大于设定值</param>
    public MaximalAttribute(float maxValue, bool useAndOperate)
    {
        this.MaxValue = maxValue;
        this.VectorMaxValue = new Vector4(maxValue, maxValue, maxValue, maxValue);
        this.ShowTips = true;
        this.UseAndOperate = useAndOperate;
        this.NickName = "";
    }

    /// <summary>
    /// 针对任意的数值和向量，同时设置别名
    /// </summary>
    /// <param name="nickName">设置别名</param>
    /// <param name="maxValue">浮点数和整型的最大值（向量的所有分量都将使用这一个值）</param>
    /// <param name="useAndOperate">向量的数值是否都不能大于设定值，默认是单个分量能大于设定值，不能所有分量都大于设定值</param>
    public MaximalAttribute(string nickName, float maxValue, bool useAndOperate)
    {
        this.MaxValue = maxValue;
        this.VectorMaxValue = new Vector4(maxValue, maxValue, maxValue, maxValue);
        this.ShowTips = true;
        this.UseAndOperate = useAndOperate;
        this.NickName = nickName;
    }

    /// <summary>
    /// 三参构造函数，针对任意的数值和向量
    /// </summary>
    /// <param name="maxValue">浮点数和整型的最大值</param>
    /// <param name="showTips">输入值大于设定的最大值时，是否给出提示弹窗</param>
    /// <param name="useAndOperate">向量的数值是否都不能大于设定值，默认是单个分量能大于设定值，不能所有分量都大于设定值</param>
    public MaximalAttribute(float maxValue, bool showTips, bool useAndOperate)
    {
        this.MaxValue = maxValue;
        this.VectorMaxValue = new Vector4(maxValue, maxValue, maxValue, maxValue);
        this.ShowTips = showTips;
        this.UseAndOperate = useAndOperate;
        this.NickName = "";
    }

    /// <summary>
    /// 针对任意的数值和向量，同时设置别名
    /// </summary>
    /// <param name="nickName">设置别名</param>
    /// <param name="maxValue">浮点数和整型的最大值</param>
    /// <param name="showTips">输入值大于设定的最大值时，是否给出提示弹窗</param>
    /// <param name="useAndOperate">向量的数值是否都不能大于设定值，默认是单个分量能大于设定值，不能所有分量都大于设定值</param>
    public MaximalAttribute(string nickName, float maxValue, bool showTips, bool useAndOperate)
    {
        this.MaxValue = maxValue;
        this.VectorMaxValue = new Vector4(maxValue, maxValue, maxValue, maxValue);
        this.ShowTips = showTips;
        this.UseAndOperate = useAndOperate;
        this.NickName = nickName;
    }

    /// <summary>
    /// 主要作用于二维向量
    /// </summary>
    /// <param name="xMaxValue">分量x的最大值同时也可作为数值的最大值</param>
    /// <param name="yMaxValue">分量y的最大值</param>
    public MaximalAttribute(float xMaxValue, float yMaxValue)
    {
        this.MaxValue = xMaxValue;
        this.VectorMaxValue = new Vector4(xMaxValue, yMaxValue, xMaxValue, xMaxValue);
        this.ShowTips = true;
        this.UseAndOperate = true;
        this.NickName = "";
    }

    /// <summary>
    /// 主要作用于二维向量，同时设置别名
    /// </summary>
    /// <param name="nickName">设置别名</param>
    /// <param name="xMaxValue">分量x的最大值同时也可作为数值的最大值</param>
    /// <param name="yMaxValue">分量y的最大值</param>
    public MaximalAttribute(string nickName, float xMaxValue, float yMaxValue)
    {
        this.MaxValue = xMaxValue;
        this.VectorMaxValue = new Vector4(xMaxValue, yMaxValue, xMaxValue, xMaxValue);
        this.ShowTips = true;
        this.UseAndOperate = true;
        this.NickName = nickName;
    }

    /// <summary>
    /// 主要作用于三维向量，同时设置别名
    /// </summary>
    /// <param name="nickName">设置别名</param>
    /// <param name="xMaxValue">分量x的最大值同时也可作为数值的最大值</param>
    /// <param name="yMaxValue">分量y的最大值</param>
    /// <param name="showTips">输入值大于设定的最大值时，是否给出提示弹窗</param>
    /// <param name="useAndOperate">向量的数值是否都不能大于设定值，默认是单个分量能大于设定值，不能所有分量都大于设定值</param>
    public MaximalAttribute(string nickName, float xMaxValue, float yMaxValue, bool showTips, bool useAndOperate)
    {
        this.MaxValue = xMaxValue;
        this.VectorMaxValue = new Vector4(xMaxValue, yMaxValue, xMaxValue, xMaxValue);
        this.ShowTips = showTips;
        this.UseAndOperate = useAndOperate;
        this.NickName = nickName;
    }

    /// <summary>
    /// 主要作用于三维向量
    /// </summary>
    /// <param name="xMaxValue">分量x的最大值同时也可作为数值的最大值</param>
    /// <param name="yMaxValue">分量y的最大值</param>
    /// <param name="zMaxValue">分量z的最大值</param>
    public MaximalAttribute(float xMaxValue, float yMaxValue, float zMaxValue)
    {
        this.MaxValue = xMaxValue;
        this.VectorMaxValue = new Vector4(xMaxValue, yMaxValue, zMaxValue, xMaxValue);
        this.ShowTips = true;
        this.UseAndOperate = true;
        this.NickName = "";
    }

    /// <summary>
    /// 主要作用于三维向量
    /// </summary>
    /// <param name="xMaxValue">分量x的最大值同时也可作为数值的最大值</param>
    /// <param name="yMaxValue">分量y的最大值</param>
    /// <param name="zMaxValue">分量z的最大值</param>
    /// <param name="showTips">向量的数值是否都不能大于设定值，默认是单个分量能大于设定值，不能所有分量都大于设定值</param>

    public MaximalAttribute(float xMaxValue, float yMaxValue, float zMaxValue, bool showTips)
    {
        this.MaxValue = xMaxValue;
        this.VectorMaxValue = new Vector4(xMaxValue, yMaxValue, zMaxValue, xMaxValue);
        this.ShowTips = showTips;
        this.UseAndOperate = true;
        this.NickName = "";
    }

    /// <summary>
    /// 主要作用于三维向量，同时设置别名
    /// </summary>
    /// <param name="nickName">设置别名</param>
    /// <param name="xMaxValue">分量x的最大值同时也可作为数值的最大值</param>
    /// <param name="yMaxValue">分量y的最大值</param>
    /// <param name="zMaxValue">分量z的最大值</param>
    /// <param name="showTips">向量的数值是否都不能大于设定值，默认是单个分量能大于设定值，不能所有分量都大于设定值</param>
    public MaximalAttribute(string nickName, float xMaxValue, float yMaxValue, float zMaxValue, bool showTips)
    {
        this.MaxValue = xMaxValue;
        this.VectorMaxValue = new Vector4(xMaxValue, yMaxValue, zMaxValue, xMaxValue);
        this.ShowTips = showTips;
        this.UseAndOperate = true;
        this.NickName = nickName;
    }

    /// <summary>
    /// 主要作用于三维向量，同时设置别名
    /// </summary>
    /// <param name="nickName">设置别名</param>
    /// <param name="xMaxValue">分量x的最大值同时也可作为数值的最大值</param>
    /// <param name="yMaxValue">分量y的最大值</param>
    /// <param name="zMaxValue">分量z的最大值</param>
    /// <param name="showTips">输入值大于设定的最大值时，是否给出提示弹窗</param>
    /// <param name="useAndOperate">向量的数值是否都不能大于设定值，默认是单个分量能大于设定值，不能所有分量都大于设定值</param>
    public MaximalAttribute(string nickName, float xMaxValue, float yMaxValue, float zMaxValue, bool showTips, bool useAndOperate)
    {
        this.MaxValue = xMaxValue;
        this.VectorMaxValue = new Vector4(xMaxValue, yMaxValue, zMaxValue, xMaxValue);
        this.ShowTips = showTips;
        this.UseAndOperate = useAndOperate;
        this.NickName = nickName;
    }

    /// <summary>
    /// 四参构造函数，主要作用于四维向量
    /// </summary>
    /// <param name="xMaxValue">分量x的最大值同时也可作为数值的最大值</param>
    /// <param name="yMaxValue">分量y的最大值</param>
    /// <param name="zMaxValue">分量z的最大值</param>
    /// <param name="wMaxValue">分量w的最大值</param>
    public MaximalAttribute(float xMaxValue, float yMaxValue, float zMaxValue, float wMaxValue)
    {
        this.MaxValue = xMaxValue;
        this.VectorMaxValue = new Vector4(xMaxValue, yMaxValue, zMaxValue, wMaxValue);
        this.ShowTips = true;
        this.UseAndOperate = true;
        this.NickName = "";
    }

    /// <summary>
    /// 主要作用于四维向量，同时设置别名
    /// </summary>
    /// <param name="nickName">设置别名</param>
    /// <param name="xMaxValue">分量x的最大值同时也可作为数值的最大值</param>
    /// <param name="yMaxValue">分量y的最大值</param>
    /// <param name="zMaxValue">分量z的最大值</param>
    /// <param name="wMaxValue">分量w的最大值</param>
    public MaximalAttribute(string nickName, float xMaxValue, float yMaxValue, float zMaxValue, float wMaxValue)
    {
        this.MaxValue = xMaxValue;
        this.VectorMaxValue = new Vector4(xMaxValue, yMaxValue, zMaxValue, wMaxValue);
        this.ShowTips = true;
        this.UseAndOperate = true;
        this.NickName = nickName;
    }
}