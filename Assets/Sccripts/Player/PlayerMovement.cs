using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Playercontroller
{
    private float horizontalInput;
    private float verticalInput;
    [SerializeField] private int moveSpeed = 10;

    #region 输入事件处理器
    private void VerticalInputHandler(float obj)
    {
        verticalInput = obj;
    }

    private void HorizontalInputHandler(float obj)
    {
        horizontalInput = obj;
    }
    #endregion

    private void PlayerMoveMent()
    {
        rb2d.velocity = new Vector2(horizontalInput, verticalInput) * moveSpeed;
    }
}