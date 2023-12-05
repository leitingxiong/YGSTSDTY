using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class SpriteAlphaTest : MonoBehaviour,IPointerClickHandler
{
    public string debugLog = "6666666666";
    
    void Start()
    {
        Image theImage = GetComponent<Image>();
        if (theImage != null && theImage.mainTexture.isReadable)
        /*小于设定的Alpha阈值将导致射线投射事件通过图像，只有大于时才能执行事件。
        需开启纹理的read/write enabled，如果不开启时执行下面的代码就会报错*/
            theImage.alphaHitTestMinimumThreshold = 0.01f;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Debug.LogError(debugLog);
    }
}
