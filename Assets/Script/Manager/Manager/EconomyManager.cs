using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ù�������ÿ����Ʒ�������㣩
/// </summary>
public class EconomyManager : MonoBehaviour
{
    /// <summary>
    /// ����ϵ����Ĭ��1.2��
    /// </summary>
    public readonly float buyEco_Coefficient = 1.2f;
    /// <summary>
    /// ����ϵ����Ĭ��0.8��
    /// </summary>
    public readonly float saleEco_Coefficient = 0.8f;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        
    }

    /// <summary>
    /// ����ͨ��������ֵ���������۸�
    /// </summary>
    /// <returns>������ļ�ֵ</returns>
    public int ReturnBuyWithCoefficient(ItemData item)
    {
        return CalculatPriceFormItemType(item, buyEco_Coefficient);
    }

    /// <summary>
    /// ����ͨ��������ֵ�����������
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int ReturnSaleWithCoefficient(ItemData item)
    {
        return CalculatPriceFormItemType(item, saleEco_Coefficient);
    }

    /// <summary>
    /// ͨ���������Ʒ���ͽ��ж�Ӧ������Ʒ�ļ���
    /// </summary>
    /// <param name="item"></param>
    /// <param name="coefficient"></param>
    /// <returns></returns>
    private int CalculatPriceFormItemType(ItemData item, float coefficient)
    {
        //���item����ΪfishItem
        if(item is FishItem fishItem)
        {
            //�������Ĳ���
            float weight_Coefficient = 5f;
            //�㳤�ȵĲ���
            float length_Coefficient = 2f;
            //��Ļ�����ֵ x ����/����ϵ�� + ������� x (�������� + ����/����ϵ��) + ��ĳ��� x (���Ȳ��� + ����/����ϵ��)
            float result = (float)(fishItem.baseEco * coefficient + fishItem.weight * (weight_Coefficient + coefficient) + fishItem.length * (length_Coefficient + coefficient));

            //�жϽ����0�Ļ�����Ҫ��1
            if(result == 0)
            {
                result += 1;
            }

            return (int)result;
        }

        //�����Ʒ�����
        if (item is ToolItem toolItem)
        {
            //TODO: ��Ʒ����Ч���ļ���

            //��͵Ļ�����ֵ x  ����/����ϵ��
            float result = toolItem.baseEco * coefficient;

            //�жϽ����0�Ļ�����Ҫ��1
            if (result == 0)
            {
                result += 1;
            }

            return (int)result;
        }

        //�����Ʒ������
        if (item is PropItem propItem)
        {
            //TODO: ��Ʒ����Ч���ļ���

            //�����Ļ�����ֵ x  ����/����ϵ��
            float result = propItem.baseEco * coefficient;

            //�жϽ����0�Ļ�����Ҫ��1
            if (result == 0)
            {
                result += 1;
            }

            return (int)result;
        }

        //�����Ʒ�����
        if (item is BaitItem baitItem)
        {
            //TODO: ��Ʒ����Ч���ļ���

            //����Ļ�����ֵ x  ����/����ϵ�� * ����ĵ�������
            float result = baitItem.baseEco * coefficient * baitItem.amount;

            //�жϽ����0�Ļ�����Ҫ��1
            if (result == 0)
            {
                result += 1;
            }

            return (int)result;
        }

        Debug.LogError("û�м�⵽��Ӧ��item���ͣ�������룡");
        return 0;
    }
}
