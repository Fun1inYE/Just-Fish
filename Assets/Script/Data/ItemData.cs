using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// ������Ʒ�Ļ���
/// </summary>
public class ItemData
{
    /// <summary>
    /// (Ĭ�ϵ�typeΪnull)
    /// </summary>
    public BaseType type;

    /// <summary>
    /// ��Ʒ������
    /// </summary>
    public int amount;

    /// <summary>
    /// ��Ʒ���ѵ���
    /// </summary>
    public int maxAmount;

    /// <summary>
    /// ������Ʒ�����ʶ��
    /// </summary>
    public ItemIdentifier itemIdentifier;

    /// <summary>
    /// �ж������Ƿ���Զѵ���Ĭ��false��
    /// </summary>
    public bool canStack;

    /// <summary>
    /// ���캯��
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
/// �����д�������࣬��FishType֮��������˳��Ⱥ�����
/// </summary>
public class FishItem : ItemData
{
    /// <summary>
    /// ��ĳ���
    /// </summary>
    public double Length;
    /// <summary>
    /// �������
    /// </summary>
    public double Weight;

    /// <summary>
    /// ����������ʱ��
    /// </summary>
    public string fishedTime;

    /// <summary>
    /// FishItem�Ĺ��캯��
    /// </summary>
    /// <param name="Type">FishType</param>
    /// <param name="Length">��ĳ���</param>
    /// <param name="Weight">�������</param>
    public FishItem(FishType type, double Length, double Weight) : base(type, false)
    {
        this.Length = Length;
        this.Weight = Weight;
        // ��ȡ��ǰϵͳʱ��
        DateTime currentTime = DateTime.Now;
        // ��ϵͳʱ���ʽ��Ϊ�ַ���
        fishedTime = currentTime.ToString("yyyy-MM-dd HH:mm:ss");

        //����Ʒ��ʶ����ֵ
        itemIdentifier.Type = "FishItem";
        itemIdentifier.Name = type.name;

        //��ĳ��Ⱥ�������ʶ����ֵ
        itemIdentifier.FishLength = Length;
        itemIdentifier.FishWeight = Weight;
    }
}

/// <summary>
/// ���ߵȼ�
/// </summary>
public enum ToolQuality { Normal, Advanced, Epic, Legendary, Default }

/// <summary>
/// �����д�Ĺ�����
/// </summary>
public class ToolItem : ItemData
{
    /// <summary>
    /// ���ߵȼ�
    /// </summary>
    public ToolQuality toolQuality;

    /// <summary>
    /// ���캯��
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
/// ���ߵȼ�
/// </summary>
[System.Serializable]
public enum PropQuality { Normal, Advanced, Epic, Legendary, Default}

/// <summary>
/// �����д�ĵ�����
/// </summary>
[System.Serializable]
public class PropItem : ItemData
{
    /// <summary>
    /// ���ߵȼ���ö����
    /// </summary>
    public PropQuality propQuality;

    /// <summary>
    /// ���캯��
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
/// �����д���ն�
/// </summary>
public class BaitItem : ItemData
{
    /// <summary>
    /// ���캯��(�ն����Զѵ�,���Ϊ99)
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
