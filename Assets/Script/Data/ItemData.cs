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
    /// 引用物品种类标识符
    /// </summary>
    public ItemIdentifier itemIdentifier;

    /// <summary>
    /// 构造函数
    /// </summary>
    public ItemData()
    {
        itemIdentifier = new ItemIdentifier();
    }
}

/// <summary>
/// 背包中存的数据类，在FishType之上又添加了长度和重量
/// </summary>
public class FishItem : ItemData
{
    /// <summary>
    /// 鱼的信息
    /// </summary>
    public FishType Type;
    /// <summary>
    /// 鱼的长度
    /// </summary>
    public double Length;
    /// <summary>
    /// 鱼的重量
    /// </summary>
    public double Weight;

    /// <summary>
    /// FishItem的构造函数
    /// </summary>
    /// <param name="Type">FishType</param>
    /// <param name="Length">鱼的长度</param>
    /// <param name="Weight">鱼的重量</param>
    public FishItem(FishType Type, double Length, double Weight)
    {
        //初始化标识符的变量
        itemIdentifier = new ItemIdentifier();
        

        this.Type = Type;
        this.Length = Length;
        this.Weight = Weight;

        //给物品标识符赋值
        itemIdentifier.Type = "FishItem";
        itemIdentifier.Name = Type.FishName;

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
    /// 引用ToolType类
    /// </summary>
    public ToolType Type;

    /// <summary>
    /// 工具等级
    /// </summary>
    public ToolQuality toolQuality;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="type"></param>
    public ToolItem(ToolType Type, ToolQuality toolQuality)
    {
        //初始化标识符的变量
        itemIdentifier = new ItemIdentifier();

        this.Type = Type;
        this.toolQuality = toolQuality;

        itemIdentifier.Type = "ToolItem";
        itemIdentifier.Name = Type.ToolName;
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
    /// 引用PropType类
    /// </summary>
    public PropType Type;

    /// <summary>
    /// 道具等级的枚举类
    /// </summary>
    public PropQuality propQuality;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="Type"></param>
    public PropItem(PropType Type, PropQuality propQuality)
    {
        //初始化标识符的变量
        itemIdentifier = new ItemIdentifier();

        this.Type = Type;
        this.propQuality = propQuality;

        itemIdentifier.Type = "PropItem";
        itemIdentifier.Name = Type.PropName;
        itemIdentifier.PropQualityIditenfier = propQuality;
    }
}