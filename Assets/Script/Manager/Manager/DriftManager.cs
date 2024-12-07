using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ư�ϵĻ������Ե�
/// </summary>
public class DriftManager : MonoBehaviour
{
    /// <summary>
    /// ����FishAndCastDataManager
    /// </summary>
    public FishingIndicatorDataManager fishingIndicatorDataManager;
    /// <summary>
    /// ��ҵ�ǰװ������������
    /// </summary>
    public DriftData driftData;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        fishingIndicatorDataManager = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishingIndicatorDataManager>();
        if (fishingIndicatorDataManager == null)
        {
            Debug.LogError("fishingIndicatorDataManager�ǿյ�");
        }
    }

    public void UpdateDriftData(ItemData item)
    {
        //��ȡ����������
        
        if(item is PropItem propItem)
        {
            //���±���������
            fishingIndicatorDataManager.breakdownStripSpeed = propItem.wearRate;
            //������������
            fishingIndicatorDataManager.needleWidth = propItem.toughness;
            //������������
            fishingIndicatorDataManager.needleThicknessSpeed = propItem.speed;
            fishingIndicatorDataManager.needleThinkDownSpeed = propItem.speed;
        }
    }

}
