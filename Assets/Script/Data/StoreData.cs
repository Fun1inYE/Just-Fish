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
    /// �̵�һ��λ�����µļ۸���ֵ
    /// </summary>
    public int price_slot0;
    /// <summary>
    /// �̵����λ�����µļ۸���ֵ
    /// </summary>
    public int price_slot1;
    /// <summary>
    /// �̵�����λ�����µļ۸���ֵ
    /// </summary>
    public int price_slot2;

    /// <summary>
    /// ί���¼������ѹ۲��߸ø��¼۸���
    /// </summary>
    public event Action OnPriceChange;

    //����UI�۲��߸ĸ���UI��
    public void NodifyPrice_Slot0(int price)
    {
        price_slot0 = price;
        OnPriceChange?.Invoke();
    }

    //����UI�۲��߸ĸ���UI��
    public void NodifyPrice_Slot1(int price)
    {
        price_slot1 = price;
        OnPriceChange?.Invoke();
    }

    //����UI�۲��߸ĸ���UI��
    public void NodifyPrice_Slot2(int price)
    {
        price_slot2 = price;
        OnPriceChange?.Invoke();
    }
}
