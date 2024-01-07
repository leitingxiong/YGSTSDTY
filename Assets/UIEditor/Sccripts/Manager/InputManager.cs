using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UniversalPool;

public class InputManager : SingleComponent<InputManager>
{
    #region 水平输入事件完整声明格式
    /*
    * 事件的拥有者(Event Source) -----> InputManager类
    * 事件(Event) -----> OnInputHorizontal
    * 事件的响应者(Event Subscriber) -----> PlayerController等
    * 事件处理器(Event Handler) -----> HorizontalInputHandler
    * 事件的订阅关系(+= Operator) -----> +=
    */
    public delegate void InputHorizontalEventHandler(float _horizontalValue);//为了事件而声明的委托类型最好加上EventHandler后缀,这个委托可以指向一个无返回值的方法，
    private InputHorizontalEventHandler inputHorizontalEventHandler;//完整声明格式中最重要的就是这个委托类型的字段，用来存储事件处理器
    public event InputHorizontalEventHandler OnInputHorizontal
    {
        add
        {
            inputHorizontalEventHandler += value;//我们虽然不知道未来传进来的事件处理器是什么，我们可以用上下文关键字Value来表示
        }
        remove
        {
            inputHorizontalEventHandler -= value;//移除事件处理器
        }
    }
    #endregion
    //action委托必定没有返回值,func委托必定具有一个返回值
    public event Action<float> OnInputVertical;//垂直输入事件的简略声明

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        #region 水平方向输入
        if (inputHorizontalEventHandler != null)
        {
            inputHorizontalEventHandler(Input.GetAxisRaw("Horizontal"));//只能由事件拥有者在其内部调用内部逻辑调用
        }
        #endregion
        #region 垂直方向输入
        if (OnInputVertical != null)
        {
            OnInputVertical(Input.GetAxisRaw("Vertical"));//GetAxisRaw只有-1、0、1三个值
        }
        #endregion
    }
}