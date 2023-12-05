using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Playercontroller : SingleComponent<Playercontroller>
{
    private Rigidbody2D rb2d;

    protected override void Awake()
    {
        base.Awake();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        InputManager.Instance.OnInputHorizontal += HorizontalInputHandler;
        InputManager.Instance.OnInputVertical += VerticalInputHandler;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        PlayerMoveMent();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        InputManager.Instance.OnInputHorizontal -= HorizontalInputHandler;
        InputManager.Instance.OnInputVertical -= VerticalInputHandler;
    }
}