using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �̵������
/// </summary>
public class StoreData
{
    /// <summary>
    /// ������Ʒ�۸�
    /// </summary>
    public int price_tool;
    /// <summary>
    /// ��һ�����߼۸�
    /// </summary>
    public int price_prop1;
    /// <summary>
    /// �ڶ������߼۸�
    /// </summary>
    public int price_prop2;

    /// <summary>
    /// ί���¼������ѹ۲��߸ø��¼۸���
    /// </summary>
    public event Action OnPriceChange;

    //����UI�۲��߸ĸ���UI��
    public void NodifyPrice_Tool(int price)
    {
        price_tool = price;
        OnPriceChange?.Invoke();
    }

    //����UI�۲��߸ĸ���UI��
    public void NodifyPrice_Prop1(int price)
    {
        price_prop1 = price;
        OnPriceChange?.Invoke();
    }

    //����UI�۲��߸ĸ���UI��
    public void NodifyPrice_Prop2(int price)
    {
        price_prop2 = price;
        OnPriceChange?.Invoke();
    }
}
