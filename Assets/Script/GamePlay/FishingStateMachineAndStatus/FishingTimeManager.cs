using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����״̬���з�Ӧʱ��Ĺ�����
/// </summary>
public class FishingTimeManager : MonoBehaviour
{
    /// <summary>
    /// ���Ϲ�����Ҫ��С��ʱ��
    /// </summary>
    public float minbittingTime;
    /// <summary>
    /// ����ҷ�Ӧ�����ʱ��
    /// </summary>
    public float maxRectionTime;

    /// <summary>
    /// ���Ϲ�����Ҫ����ʱ�䣨Ĭ��30f��
    /// </summary>
    public float maxbittingTime = 30f;
    /// <summary>
    /// ����ҷ�Ӧ����Сʱ��(Ĭ��0.5f)
    /// </summary>
    public float minReactionTime = 0.5f;

    /// <summary>
    /// ��ʱ��������
    /// </summary>
    public void FixUpFishingTimeData()
    {
        //����Ϲ�ʱ��������ʱ��
        minbittingTime = minbittingTime > maxbittingTime ? maxbittingTime - 1 : minbittingTime;

    }

    /// <summary>
    /// ��ȡ������ɵ��Ϲ�ʱ��
    /// </summary>
    /// <returns></returns>
    public float GetRangeOfFishingTime()
    {
        Debug.Log(minbittingTime + "  " + maxbittingTime);
        //����һ��ʱ��
        FixUpFishingTimeData();
        //���ض�Ӧʱ��
        return Random.Range(minbittingTime, maxbittingTime);
    }

    /// <summary>
    /// ��ȡ������ɵ���ҷ�Ӧʱ��
    /// </summary>
    /// <returns></returns>
    public float GetRangeOfReactionTime()
    {
        //����һ��ʱ��
        FixUpFishingTimeData();
        //���ض�Ӧʱ��
        return Random.Range(minReactionTime, maxRectionTime);
    }

}
