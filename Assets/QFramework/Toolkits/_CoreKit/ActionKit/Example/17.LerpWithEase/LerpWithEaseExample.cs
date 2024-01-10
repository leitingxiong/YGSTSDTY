using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QFramework.Example
{
    public class LerpWithEaseExample : MonoBehaviour
    {
        void Start()
        {
            Application.targetFrameRate = 60;
            var spriteTransform = transform.Find("Sprite");

            ActionKit.Sequence()
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.Linear(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.OutBack(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InBack(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InOutBack(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InOutBounce(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.OutBounce(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InBounce(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.OutCircle(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InCircle(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InOutCircle(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InCubic(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.OutCubic(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InOutCubic(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.OutElastic(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InElastic(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InOutElastic(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InOutExpo(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InExpo(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.OutExpo(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InOutQuad(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InQuad(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.OutQuad(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InOutQuart(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InQuart(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.OutQuart(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InOutQuint(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InQuint(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.OutQuint(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InOutSine(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.InSine(0, 5, f)); })
                .Lerp01(3.0f, f => { spriteTransform.LocalPositionX(EaseUtility.OutSine(0, 5, f)); })
                .Start(this);
        }
    }
}