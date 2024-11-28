using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �洢����Ϸ����
/// </summary>
[Serializable]
public class GameData
{
    /// <summary>
    /// �浵����(Ĭ������ΪdefaultArchiveName)
    /// </summary>
    public string archiveSaveName = "defaultArchiveName";
    /// <summary>
    /// ����Լ��������(Ĭ������ΪdefaultName)
    /// </summary>
    public string playerName = "defaultPlayerName";
    /// <summary>
    /// �����浵ʱ��(Ĭ��Ϊ"")
    /// </summary>
    public string createArchiveTime = "";
    /// <summary>
    /// �޸Ĵ浵ʱ��(Ĭ��Ϊ"")
    /// </summary>
    public string reviseArchiveTime = "";

    /// <summary>
    /// ���캯��
    /// </summary>
    public GameData()
    {
        archiveSaveName = "defaultArchiveName";
        playerName = "defaultName";
    }
}

/// <summary>
/// ����������
/// </summary>
public class PlayerGameData
{
    /// <summary>
    /// ������ݱ�ʶ��
    /// </summary>
    public PlayerDataIditentifier playerDataIditentifier;

    /// <summary>
    /// ���캯��
    /// </summary>
    public PlayerGameData()
    {
        playerDataIditentifier = new PlayerDataIditentifier();
    }
}


/// <summary>
/// ����������
/// </summary>
[System.Serializable]
public class InventoryItemData
{
    /// <summary>
    /// ����������
    /// </summary>
    public List<ItemIdentifier> backpackItemIdentifier { get; set; }
    /// <summary>
    /// ����������
    /// </summary>
    public List<ItemIdentifier> toolpackItemIdentifier { get; set; }
    /// <summary>
    /// ����������
    /// </summary>
    public List<ItemIdentifier> proppackItemIdentifier { get; set; }
    /// <summary>
    /// װ��������
    /// </summary>
    public List<ItemIdentifier> equipmentItemIdentifier { get; set; }
    /// <summary>
    /// �̵�������
    /// </summary>
    public List<ItemIdentifier> storepackItemIdentifier { get; set; }
    /// <summary>
    /// ����������
    /// </summary>
    public List<ItemIdentifier> salepackItemIdentifier { get; set; }

    /// <summary>
    /// ���캯��
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