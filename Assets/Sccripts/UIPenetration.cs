using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//引入Unity点击事件的命名空间
using UnityEngine.EventSystems;

public class UIPenetration : MonoBehaviour,IPointerClickHandler
{
    [Label("穿透层数")]
    public int passNum = 1;
    /// <summary>
    /// 为了避免同一点击区域，有多个穿透脚本响应导致的死循环
    /// </summary>
    private bool hasPassedEvent = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.LogError(this.gameObject.name);
        PassEvent(eventData, ExecuteEvents.pointerClickHandler);
    }

    //把点击事件传递下去
    public void PassEvent<T>(PointerEventData eventData, ExecuteEvents.EventFunction<T> function) where T : IEventSystemHandler
    {
        //表示这个事件已经透传过，如果传回到自己，就忽略掉，然后在透传结束，将标志位设置回来
        if (hasPassedEvent)
            return;
        hasPassedEvent = true;
        //用于缓存射线检测到的所有对象
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        GameObject current = this.gameObject;
        Debug.LogError(results.Count + "results");
        for (int i = 0; i < results.Count; i++)
        {
            //因为挂载该脚本的对象已经响应了点击事件，我们就不应该再重复触发了
            if (current != results[i].gameObject)
            {
                //RaycastAll后ugui会自己排序
                if (ExecuteEvents.Execute(results[i].gameObject, eventData, function))
                {
                    //如果你只想响应透下去的最近的一个响应，这里ExecuteEvents.Execute后直接break就行。
                    if (i == passNum)
                        break;
                }
            }
        }
        results.Clear();
        hasPassedEvent = false;
    }
}