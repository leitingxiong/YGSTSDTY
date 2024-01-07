using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//双击按钮
[AddComponentMenu("UI/ExpendButton/DubleClickButton")]
public class DoubleClickButton : Button
{
    [Serializable]
    public class DoubleClickedEvent : UnityEvent { }

    [SerializeField]
    private DoubleClickedEvent onDoubleClickEvent = new DoubleClickedEvent();

    //这个是双击成功后激活的事件
    public DoubleClickedEvent OnDoubleClick
    {
        get { return onDoubleClickEvent; }
        set { onDoubleClickEvent = value; }
    }

    private DateTime firstClickTime;
    private DateTime secondClickTime;

    /// <summary>
    /// 执行DoubleClick
    /// </summary>
    private void DoDoubleClick()
    {
        if (OnDoubleClick != null)
            OnDoubleClick.Invoke();
        resetTime();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        // 按下按钮时对两次的时间进行记录
        if (firstClickTime.Equals(default(DateTime)))
            firstClickTime = DateTime.Now;
        else
            secondClickTime = DateTime.Now;
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);

        // 在第二次鼠标抬起的时候进行时间的触发,时差小于400ms触发
        if (!firstClickTime.Equals(default(DateTime)) && !secondClickTime.Equals(default(DateTime)))
        {
            TimeSpan intervalTime = secondClickTime - firstClickTime;
            //float milliSeconds = intervalTime.Seconds * 1000 + intervalTime.Milliseconds; 1s=1000ms

            //总毫秒数是否小于400毫秒
            if (intervalTime.TotalMilliseconds < 400)
                DoDoubleClick();
            else
                resetTime();
        }
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
        resetTime();
    }

    /// <summary>
    /// 重置计时
    /// </summary>
    private void resetTime()
    {
        firstClickTime = default(DateTime);
        secondClickTime = default(DateTime);
    }
}