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
    public FishAndCastDataManager fishAndCastDataManager;

    /// <summary>
    /// ��ҵ�ǰװ���ĵ�������
    /// </summary>
    public FishRodData fishRodData;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        fishAndCastDataManager = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishAndCastDataManager>();
        if(fishAndCastDataManager == null)
        {
            Debug.LogError("fishAndCastDataManager�ǿյ�");
        }
    }

    /// <summary>
    /// �������װ�����������
    /// </summary>
    /// <param name="rod">��Ҫ���µ����</param>
    public void UpdateFishRodData(GameObject rod)
    {
        //��ȡ����������
        fishRodData = rod.GetComponent<FishRodData>();
        if (fishRodData != null)
        {
            //���µ�������
            fishAndCastDataManager.processStripSpeed = fishRodData.power;
            //������������
            fishAndCastDataManager.indicatorWidth = fishRodData.toughness;
            //������������
            fishAndCastDataManager.indicatorMoveSpeed = fishRodData.speed;
        }
        else
        {
            Debug.LogWarning($"û�л�ȡ������Ϊ{rod.name}������");
        }
    }
}
