using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ͨ��˫�����п�ݽ�����Ʒ
/// </summary>
public class ClickItem : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// EventSystem�еķ���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        //�ж��Ƿ���ٵ�����������
        if(eventData.clickCount % 2 == 0)
        {

        }
    }
}
