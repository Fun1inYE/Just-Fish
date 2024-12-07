using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 商店的数据
/// </summary>
public class StoreData
{
    /// <summary>
    /// 商店一号位格子下的价格数值
    /// </summary>
    public int price_slot0;
    /// <summary>
    /// 商店二号位格子下的价格数值
    /// </summary>
    public int price_slot1;
    /// <summary>
    /// 商店三号位格子下的价格数值
    /// </summary>
    public int price_slot2;

    /// <summary>
    /// 委托事件，提醒观察者该更新价格了
    /// </summary>
    public event Action OnPriceChange;

    //提醒UI观察者改更新UI了
    public void NodifyPrice_Slot0(int price)
    {
        price_slot0 = price;
        OnPriceChange?.Invoke();
    }

    //提醒UI观察者改更新UI了
    public void NodifyPrice_Slot1(int price)
    {
        price_slot1 = price;
        OnPriceChange?.Invoke();
    }

    //提醒UI观察者改更新UI了
    public void NodifyPrice_Slot2(int price)
    {
        price_slot2 = price;
        OnPriceChange?.Invoke();
    }
}
