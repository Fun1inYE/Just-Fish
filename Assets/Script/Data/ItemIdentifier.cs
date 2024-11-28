using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 游戏的标识符(只有建档时间和修改时间)
/// </summary>
public class GameDataIdentifier
{
    /// <summary>
    /// 创建存档时间(默认为"")
    /// </summary>
    public string createArchiveTimeIdentifier = "";
    /// <summary>
    /// 修改存档时间(默认为"")
    /// </summary>
    public string reviseArchiveTimeIdentifier = "";

    public GameDataIdentifier()
    {
        createArchiveTimeIdentifier = "";
        reviseArchiveTimeIdentifier = "";
    }
}


/// <summary>
/// 物品的标识符
/// </summary>
public class ItemIdentifier
{
    /// <summary>
    /// 物品的种类（默认defaultType）
    /// </summary>
    public string Type = "defaultType";
    /// <summary>
    /// 物品的名字（默认defaultName）
    /// </summary>
    public string Name = "defaultName";

    /// <summary>
    /// 鱼的长度(默认为0)
    /// </summary>
    public double FishLength = 0f;
    /// <summary>
    /// 鱼的重量(默认为0)
    /// </summary>
    public double FishWeight = 0f;

    /// <summary>
    /// 工具品质标识符(默认为0)
    /// </summary>
    public ToolQuality ToolQualityIditenfier = 0;

    /// <summary>
    /// 道具品质标识符(默认为0)
    /// </summary>
    public PropQuality PropQualityIditenfier = 0;

    /// <summary>
    /// 构造函数
    /// </summary>
    public ItemIdentifier()
    {
        Type = "defaultType";
        Name = "defaultName";
        FishLength = 0f;
        FishWeight = 0f;
        ToolQualityIditenfier = 0;
        PropQualityIditenfier = 0;
    }
}

/// <summary>
/// 玩家的相关数据标识符
/// </summary>
public class PlayerDataIditentifier
{
    /// <summary>
    /// 玩家的名字的标识符(默认为defaultName)
    /// </summary>
    public string playerNameIdetentifier { get; set; } = "defaultName";
    /// <summary>
    /// 玩家所处位置的X坐标标识符（默认为0）
    /// </summary>
    public double coordinate_XIdetentifier = 0f;
    /// <summary>
    /// 玩家所处位置的Y坐标标识符（默认为0）
    /// </summary>
    public double coordinate_YIdetentifier = 0f;
    /// <summary>
    /// 金币的数量的标识符（默认为0）
    /// </summary>
    public int coinIdetentifier = 0;

    /// <summary>
    /// 构造函数
    /// </summary>
    public PlayerDataIditentifier()
    {
        playerNameIdetentifier = "defaultName";
        coordinate_XIdetentifier = 0f;
        coordinate_YIdetentifier = 0f;
        coinIdetentifier = 0;
    }
}