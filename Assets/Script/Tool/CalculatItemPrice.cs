using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Ʒ��ֵ����
/// </summary>
public static class CalculatItemPrice
{
    /// <summary>
    /// ������ļ�ֵ������������
    /// </summary>
    /// <param name="fishItem"></param>
    /// <param name="length_coefficient">���ȼ���ϵ��</param>
    /// <param name="weight_coefficient">��������ϵ��</param>
    /// <returns></returns>
    public static int CulationFish(FishItem fishItem, float length_coefficient, float weight_coefficient)
    {
        //���յļ�����
        int result = 0;
        //��ȡ����ĳ��ȣ�������
        int length = (int)fishItem.length;
        //��ȡ���������������
        int weight = (int)fishItem.weight;
        //��ʱ��Ϊ������㹫ʽ
        result = (int)(length * length_coefficient + weight * weight_coefficient);
        return result;
    }

    /// <summary>
    /// ��������ļ��㷽ʽ
    /// </summary>
    /// <param name="toolItem">�������Ĺ���</param>
    /// <param name="toolQuality_coefficient">���������ļ���ϵ��</param>
    /// <returns></returns>
    public static int CulationTool(ToolItem toolItem, float toolQuality_coefficient)
    {
        //���յļ�����
        int result = 0;
        result = (int)(toolItem.toolQuality + 1) * (int)toolQuality_coefficient;
        return result;
    }

    /// <summary>
    /// ���������ļ��㷽ʽ
    /// </summary>
    /// <param name="propItem">�������ĵ�����</param>
    /// <param name="propQuality_coefficient">���������ļ���ϵ��</param>
    /// <returns></returns>
    public static int CulationProp(PropItem propItem, float propQuality_coefficient)
    {
        //���յļ�����
        int result = 0;
        result = (int)(propItem.propQuality + 1) * (int)propQuality_coefficient;
        return result;
    }
}
