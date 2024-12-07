using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �����ն����ݵĹ�����
/// </summary>
public class BaitManager : MonoBehaviour
{
    /// <summary>
    /// ���õ���ʱ�������
    /// </summary>
    public FishingTimeManager fishingTimeManager;
    /// <summary>
    /// �������Ϣ
    /// </summary>
    public BaitData baitData;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        fishingTimeManager = SetGameObjectToParent.FindFromFirstLayer("StateMachine").GetComponent<FishingTimeManager>();
        if(fishingTimeManager == null)
        {
            Debug.LogError("fishingTimeManager�ǿյ�");
        }
    }

    public void UpdateDriftData(ItemData item)
    {
        if (item is BaitItem baitItem)
        {
            //������С�Ϲ�ʱ��
            fishingTimeManager.minbittingTime = baitItem.minBittingTime;
            //�������Ӧʱ��
            fishingTimeManager.maxRectionTime = baitItem.maxReactionTime;
        }
    }
}
