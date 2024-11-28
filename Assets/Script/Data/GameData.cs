using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 存储的游戏数据
/// </summary>
[Serializable]
public class GameData
{
    /// <summary>
    /// 存档名字(默认名字为defaultArchiveName)
    /// </summary>
    public string archiveSaveName = "defaultArchiveName";
    /// <summary>
    /// 玩家自己起的名字(默认名字为defaultName)
    /// </summary>
    public string playerName = "defaultPlayerName";
    /// <summary>
    /// 创建存档时间(默认为"")
    /// </summary>
    public string createArchiveTime = "";
    /// <summary>
    /// 修改存档时间(默认为"")
    /// </summary>
    public string reviseArchiveTime = "";

    /// <summary>
    /// 构造函数
    /// </summary>
    public GameData()
    {
        archiveSaveName = "defaultArchiveName";
        playerName = "defaultName";
    }
}

/// <summary>
/// 玩家相关数据
/// </summary>
public class PlayerGameData
{
    /// <summary>
    /// 玩家数据标识符
    /// </summary>
    public PlayerDataIditentifier playerDataIditentifier;

    /// <summary>
    /// 构造函数
    /// </summary>
    public PlayerGameData()
    {
        playerDataIditentifier = new PlayerDataIditentifier();
    }
}


/// <summary>
/// 库存相关数据
/// </summary>
[System.Serializable]
public class InventoryItemData
{
    /// <summary>
    /// 背包栏数据
    /// </summary>
    public List<ItemIdentifier> backpackItemIdentifier { get; set; }
    /// <summary>
    /// 工具栏数据
    /// </summary>
    public List<ItemIdentifier> toolpackItemIdentifier { get; set; }
    /// <summary>
    /// 道具栏数据
    /// </summary>
    public List<ItemIdentifier> proppackItemIdentifier { get; set; }
    /// <summary>
    /// 装备栏数据
    /// </summary>
    public List<ItemIdentifier> equipmentItemIdentifier { get; set; }
    /// <summary>
    /// 商店栏数据
    /// </summary>
    public List<ItemIdentifier> storepackItemIdentifier { get; set; }
    /// <summary>
    /// 卖出栏数据
    /// </summary>
    public List<ItemIdentifier> salepackItemIdentifier { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    public InventoryItemData()
    {
        backpackItemIdentifier = new List<ItemIdentifier>();
        toolpackItemIdentifier = new List<ItemIdentifier>();
        proppackItemIdentifier = new List<ItemIdentifier>();
        equipmentItemIdentifier = new List<ItemIdentifier>();
        storepackItemIdentifier = new List<ItemIdentifier>();
        salepackItemIdentifier = new List<ItemIdentifier>();
    }
       
}