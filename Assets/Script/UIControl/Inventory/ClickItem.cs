using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 通过双击进行快捷交换物品
/// </summary>
public class ClickItem : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// EventSystem中的方法
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        //判断是否快速点击了两次鼠标
        if(eventData.clickCount % 2 == 0)
        {

        }
    }
}
