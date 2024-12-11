using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ����װ�������ϵĻ������Ե�
/// </summary>
public class FishRodManager : MonoBehaviour
{
    /// <summary>
    /// ����FishAndCastDataManager
    /// </summary>
    public FishingIndicatorDataManager fishAndCastDataManager;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        fishAndCastDataManager = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishingIndicatorDataManager>();
        if(fishAndCastDataManager == null)
        {
            Debug.LogError("fishAndCastDataManager�ǿյ�");
        }
    }

    /// <summary>
    /// �������װ�����������
    /// </summary>
    /// <param name="rod">��Ҫ���µ����</param>
    public void UpdateFishRodData(ItemData item)
    {
        //���жϴ���������Ʒ�ǲ������
        if (item is ToolItem toolItem)
        {
            //���µ�������
            fishAndCastDataManager.processStripSpeed = toolItem.power;
            //������������
            fishAndCastDataManager.indicatorWidth = toolItem.toughness;
            //������������
            fishAndCastDataManager.indicatorMoveSpeed = toolItem.speed;
        }
        else
        {
            Debug.LogWarning($"û�л�ȡ������Ϊ{item.type.name}������");
        }
    }
}
