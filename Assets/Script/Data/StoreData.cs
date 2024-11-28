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
    /// 工具商品价格
    /// </summary>
    public int price_tool;
    /// <summary>
    /// 第一个道具价格
    /// </summary>
    public int price_prop1;
    /// <summary>
    /// 第二个道具价格
    /// </summary>
    public int price_prop2;

    /// <summary>
    /// 委托事件，提醒观察者该更新价格了
    /// </summary>
    public event Action OnPriceChange;

    //提醒UI观察者改更新UI了
    public void NodifyPrice_Tool(int price)
    {
        price_tool = price;
        OnPriceChange?.Invoke();
    }

    //提醒UI观察者改更新UI了
    public void NodifyPrice_Prop1(int price)
    {
        price_prop1 = price;
        OnPriceChange?.Invoke();
    }

    //提醒UI观察者改更新UI了
    public void NodifyPrice_Prop2(int price)
    {
        price_prop2 = price;
        OnPriceChange?.Invoke();
    }
}
