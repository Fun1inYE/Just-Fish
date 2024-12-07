using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingIndicatorDataManager : MonoBehaviour
{
    //�����ǵ���ָʾ���ĸ��ֲ���
    /// <summary>
    /// Indicator�Ŀ��
    /// </summary>
    public float indicatorWidth;
    /// <summary>
    /// Indicator���ٶ�
    /// </summary>
    public float indicatorMoveSpeed;
    /// <summary>
    /// ������ȵ������ٶ�
    /// </summary>
    public float processStripSpeed;
    /// <summary>
    /// ����Ľ��ȵļ����ٶ�
    /// </summary>
    public float processStripDownSpeed;
    /// <summary>
    /// ָ��ĳ�ʼ���
    /// </summary>
    public float needleWidth;
    /// <summary>
    /// ָ��ı���ٶ�
    /// </summary>
    public float needleThicknessSpeed;
    /// <summary>
    /// ָ���խ���ٶ�
    /// </summary>
    public float needleThinkDownSpeed;
    /// <summary>
    /// ĥ����������ٶ�
    /// </summary>
    public float breakdownStripSpeed;
    /// <summary>
    /// ĥ������խ���ٶ�
    /// </summary>
    public float breakDownStripDownSpeed;

    /// <summary>
    /// ���µ���ָʾ��������
    /// </summary>
    public void FixUpFishIndicatorData()
    {
        //ָ��Ŀ�Ȳ��ܴ���ָʾ���Ŀ��
        needleWidth = needleWidth > indicatorWidth ? indicatorWidth - 0.01f : needleWidth;
        //ָ����Сֵ
        needleWidth = needleWidth < 1f ? 1f : needleWidth;
        //ָʾ����Сֵ
        indicatorWidth = indicatorWidth < 25f ? 25f : indicatorWidth;
        //ָʾ���ƶ���Сֵ
        indicatorMoveSpeed = indicatorMoveSpeed < 15f ? 15f : indicatorMoveSpeed;
        //��������������Сֵ
        processStripSpeed = processStripSpeed < 1f ? 1f : processStripSpeed;
        //�����������խ��Сֵ
        processStripDownSpeed = processStripDownSpeed < 1f ? 1f : processStripDownSpeed;
        //ָ���������ֵ
        needleThicknessSpeed = needleThicknessSpeed < 5f ? 5f : needleThicknessSpeed;
        //ָ���խ������ֵ
        needleThinkDownSpeed = needleThinkDownSpeed < 5f ? 5f : needleThinkDownSpeed;
        //��������������ֵ
        breakdownStripSpeed = breakdownStripSpeed < 1f ? 1f : breakdownStripSpeed;
        //��������խ������ֵ
        breakDownStripDownSpeed = breakDownStripDownSpeed < 1f ? 1f : breakDownStripDownSpeed;

        //TODO: ָʾ����Ȳ��ܴ��ڿ��ƶ���Χ
    }
}
