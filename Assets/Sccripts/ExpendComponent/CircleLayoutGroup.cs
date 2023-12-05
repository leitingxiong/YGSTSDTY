using UnityEngine;
using System.Collections.Generic;
using ExtendsFunction;

/// <summary>
/// 圆形自动布局组件，编辑器模式下可执行且必须包含有RectTransform组件（该组件不适用于3D对象）
/// </summary>
[ExecuteInEditMode,RequireComponent(typeof(RectTransform))]
public class CircleLayoutGroup : MonoBehaviour
{
    [SerializeField]
    private float radius = 100f;
    //角度差
    [SerializeField]
    private float angleDelta = 30f;
    //开始的方向 0-Right 1-Up 2-Left 3-Down
    [SerializeField]
    private int startDirection = 0;
    //是否控制子物体的大小
    [SerializeField]
    private bool controlChildSize = true;
    //子物体大小
    [SerializeField]
    private Vector2 childSize = Vector2.one * 100f;
    

    //缓存子物体数量
    private int cacheChildCount;
    static private List<RectTransform> children = new List<RectTransform>();

    private void Start()
    {
        cacheChildCount = transform.GetChildCountExtension();
        RefreshLayout();
    }

    private void Update()
    {
        //检测到子物体数量变动
        if (cacheChildCount != transform.GetChildCountExtension())
        {
            //刷新布局
            RefreshLayout();
        }
    }

    /// <summary>
    /// 刷新布局
    /// </summary>
    public void RefreshLayout()
    {
        //再次缓存子物体数量
        cacheChildCount = transform.GetChildCountExtension();
        //获取所有非隐藏状态的子物体
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            if (child.gameObject.activeSelf)
            {
                children.Add(child as RectTransform);
            }
        }
        //形成的扇形的角度 = 子物体间隙数量 * 角度差
        float totalAngle = (children.Count - 1) * angleDelta;
        //总角度的一半
        float halfAngle = totalAngle * 0.5f;
        //遍历这些子物体
        for (int i = 0; i < children.Count; i++)
        {
            RectTransform child = children[i];
            /* 以右侧为0度起点 
             * 方向为Up时角度+90 Left+180 Down+270
             * 方向为Right和Up时 倒序计算角度 
             * 确保层级中的子物体按照从左到右、从上到下的顺序自动布局 */
            float angle = angleDelta * (startDirection < 2 ? children.Count - 1 - i : i) - halfAngle + startDirection * 90f;
            //计算x、y坐标
            float x = radius * Mathf.Cos(angle * Mathf.PI / 180f);
            float y = radius * Mathf.Sin(angle * Mathf.PI / 180f);
            //为子物体赋值坐标
            Vector2 anchorPos = child.anchoredPosition;
            anchorPos.x = x;
            anchorPos.y = y;
            child.anchoredPosition = anchorPos;

            //控制子物体大小
            if (controlChildSize)
            {
                child.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, childSize.x);
                child.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, childSize.y);
            }
        }
    }
}