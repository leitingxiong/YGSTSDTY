using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

//长按按钮
[AddComponentMenu("UI/ExpendButton/LongClickButton")]
public class LongClickButton : Button
{
    /// <summary>
    /// 长按时间（ms）
    /// </summary>
    public int longPressTime = 400;

    [Serializable]
    public class LongClickEvent : UnityEvent {}

    private LongClickEvent onLongClickUpEvent = new LongClickEvent();
    private LongClickEvent onLongPressEvent = new LongClickEvent();

    /// <summary>
    /// 倒计时协程
    /// </summary>
    private Coroutine countDownCoroutine = null;

    /// <summary>
    /// 长按后抬起时响应。（使用属性封装一次，控制外部对内部私有字段的访问）
    /// </summary>
    public LongClickEvent OnLongClickUp
    {
        get { return onLongClickUpEvent; }
        set { onLongClickUpEvent = value; }
    }

    /// <summary>
    /// 长按事件
    /// </summary>
    public LongClickEvent OnLongPressEvent
    {
        get { return onLongPressEvent; }
        set { onLongPressEvent = value; }
    }

    private DateTime firstClickTime = default(DateTime);
    private DateTime firstClickTime_Up = default(DateTime);

    /// <summary>
    /// 长按抬起的事件处理器
    /// </summary>
    private void LongClickUpHandler() 
    {
        if (OnLongClickUp != null)
            OnLongClickUp.Invoke();
        resetTime();
    }

    /// <summary>
    /// 长按事件处理器
    /// </summary>
    private void LongPressHandler()
    {
        if (OnLongPressEvent != null)
            OnLongPressEvent.Invoke();
        if (countDownCoroutine != null)
            StopCoroutine(countDownCoroutine);
    }

    /// <summary>
    /// 鼠标按下时执行
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (firstClickTime.Equals(default(DateTime)))
        {
            firstClickTime = DateTime.Now;
            countDownCoroutine = StartCoroutine(CountDown());
        }
    }

    /// <summary>
    /// 鼠标抬起时执行
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        // 在鼠标抬起的时候进行事件触发，时差大于600ms触发
        if (!firstClickTime.Equals(default(DateTime)))
            firstClickTime_Up = DateTime.Now;
        if (!firstClickTime.Equals(default(DateTime)) && !firstClickTime_Up.Equals(default(DateTime)))
        {
            //计算两次时间戳的间隔时间（毫秒）
            TimeSpan intervalTime = firstClickTime_Up - firstClickTime;
            if (intervalTime.TotalMilliseconds > longPressTime)
                LongClickUpHandler();
            else 
                resetTime();
        }
    }

    /// <summary>
    /// 鼠标移出
    /// </summary>
    /// <param name="eventData"></param>
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
        firstClickTime_Up = default(DateTime);
    }

    /// <summary>
    /// 协程倒计时
    /// </summary>
    /// <returns></returns>
    private IEnumerator CountDown() 
    {
        TimeSpan temporary = firstClickTime - DateTime.Now;
        while (temporary.TotalMilliseconds < longPressTime)
        {
            temporary = DateTime.Now - firstClickTime;
            //yield return 后面的返回值永远只有一帧，不论是yield return 1还是yield return 100
            yield return null;
        }
        LongPressHandler();
    }
}