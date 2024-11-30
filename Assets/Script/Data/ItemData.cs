using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// 所有物品的基类
/// </summary>
public class ItemData
{
    /// <summary>
    /// (默认的type为null)
    /// </summary>
    public BaseType type;

    /// <summary>
    /// 物品的数量
    /// </summary>
    public int amount;

    /// <summary>
    /// 物品最大堆叠数
    /// </summary>
    public int maxAmount;

    /// <summary>
    /// 引用物品种类标识符
    /// </summary>
    public ItemIdentifier itemIdentifier;

    /// <summary>
    /// 判断物体是否可以堆叠（默认false）
    /// </summary>
    public bool canStack;

    /// <summary>
    /// 构造函数
    /// </summary>
    public ItemData(BaseType type, bool canStack = false, int maxAmount = 1, int amount = 1)
    {
        this.type = type;
        this.canStack = canStack;
        this.amount = amount;
        this.maxAmount = maxAmount;
        itemIdentifier = new ItemIdentifier();
    }
}

/// <summary>
/// 背包中存的数据类，在FishType之上又添加了长度和重量
/// </summary>
public class FishItem : ItemData
{
    /// <summary>
    /// 鱼的长度
    /// </summary>
    public double Length;
    /// <summary>
    /// 鱼的重量
    /// </summary>
    public double Weight;

    /// <summary>
    /// 被钓起来的时间
    /// </summary>
    public string fishedTime;

    /// <summary>
    /// FishItem的构造函数
    /// </summary>
    /// <param name="Type">FishType</param>
    /// <param name="Length">鱼的长度</param>
    /// <param name="Weight">鱼的重量</param>
    public FishItem(FishType type, double Length, double Weight) : base(type, false)
    {
        this.Length = Length;
        this.Weight = Weight;
        // 获取当前系统时间
        DateTime currentTime = DateTime.Now;
        // 将系统时间格式化为字符串
        fishedTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss");

        //给物品标识符赋值
        itemIdentifier.Type = "FishItem";
        itemIdentifier.Name = type.name;

        //鱼的长度和重量标识符赋值
        itemIdentifier.FishLength = Length;
        itemIdentifier.FishWeight = Weight;
    }
}

/// <summary>
/// 工具等级
/// </summary>
public enum ToolQuality { Normal, Advanced, Epic, Legendary, Default }

/// <summary>
/// 背包中存的工具类
/// </summary>
public class ToolItem : ItemData
{
    /// <summary>
    /// 工具等级
    /// </summary>
    public ToolQuality toolQuality;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="type"></param>
    public ToolItem(ToolType type, ToolQuality toolQuality) : base(type, false)
    {
        this.toolQuality = toolQuality;

        itemIdentifier.Type = "ToolItem";
        itemIdentifier.Name = type.name;
        itemIdentifier.ToolQualityIditenfier = toolQuality;
    }
}

/// <summary>
/// 工具等级
/// </summary>
[System.Serializable]
public enum PropQuality { Normal, Advanced, Epic, Legendary, Default}

/// <summary>
/// 背包中存的道具类
/// </summary>
[System.Serializable]
public class PropItem : ItemData
{
    /// <summary>
    /// 道具等级的枚举类
    /// </summary>
    public PropQuality propQuality;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="Type"></param>
    public PropItem(PropType type, PropQuality propQuality) : base(type, false)
    {
        this.propQuality = propQuality;

        itemIdentifier.Type = "PropItem";
        itemIdentifier.Name = type.name;
        itemIdentifier.PropQualityIditenfier = propQuality;
    }
}

/// <summary>
/// 背包中存的诱饵
/// </summary>
public class BaitItem : ItemData
{
    /// <summary>
    /// 构造函数(诱饵可以堆叠,最大为99)
    /// </summary>
    /// <param name="canStack"></param>
    public BaitItem(BaitType type, int maxAmount, int amount) : base(type, true, maxAmount, amount)
    {
        itemIdentifier.Type = "BaitItem";
        itemIdentifier.Name = type.name;
        itemIdentifier.amountIditenfier = amount;
        itemIdentifier.maxAmountIditenfier = maxAmount;
    }
}
