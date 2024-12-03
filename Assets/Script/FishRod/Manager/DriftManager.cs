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
    public FishAndCastDataManager fishAndCastDataManager;
    /// <summary>
    /// ��ҵ�ǰװ������������
    /// </summary>
    public DriftData driftData;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        fishAndCastDataManager = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishAndCastDataManager>();
        if (fishAndCastDataManager == null)
        {
            Debug.LogError("fishAndCastDataManager�ǿյ�");
        }
    }

    public void UpdateDriftData(GameObject drift)
    {
        //��ȡ����������
        driftData = drift.GetComponent<DriftData>();
        if(driftData != null)
        {
            //���±���������
            fishAndCastDataManager.breakdownStripSpeed = driftData.wearRate;
            //������������
            fishAndCastDataManager.needleWidth = driftData.toughness;
            //������������
            fishAndCastDataManager.needleThicknessSpeed = driftData.speed;
            fishAndCastDataManager.needleThinkDownSpeed = driftData.speed;
        }
    }

}
