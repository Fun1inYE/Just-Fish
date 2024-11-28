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
    /// ������Ʒ�����ʶ��
    /// </summary>
    public ItemIdentifier itemIdentifier;

    /// <summary>
    /// ���캯��
    /// </summary>
    public ItemData()
    {
        itemIdentifier = new ItemIdentifier();
    }
}

/// <summary>
/// �����д�������࣬��FishType֮��������˳��Ⱥ�����
/// </summary>
public class FishItem : ItemData
{
    /// <summary>
    /// �����Ϣ
    /// </summary>
    public FishType Type;
    /// <summary>
    /// ��ĳ���
    /// </summary>
    public double Length;
    /// <summary>
    /// �������
    /// </summary>
    public double Weight;

    /// <summary>
    /// FishItem�Ĺ��캯��
    /// </summary>
    /// <param name="Type">FishType</param>
    /// <param name="Length">��ĳ���</param>
    /// <param name="Weight">�������</param>
    public FishItem(FishType Type, double Length, double Weight)
    {
        //��ʼ����ʶ���ı���
        itemIdentifier = new ItemIdentifier();
        

        this.Type = Type;
        this.Length = Length;
        this.Weight = Weight;

        //����Ʒ��ʶ����ֵ
        itemIdentifier.Type = "FishItem";
        itemIdentifier.Name = Type.FishName;

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
    /// ����ToolType��
    /// </summary>
    public ToolType Type;

    /// <summary>
    /// ���ߵȼ�
    /// </summary>
    public ToolQuality toolQuality;

    /// <summary>
    /// ���캯��
    /// </summary>
    /// <param name="type"></param>
    public ToolItem(ToolType Type, ToolQuality toolQuality)
    {
        //��ʼ����ʶ���ı���
        itemIdentifier = new ItemIdentifier();

        this.Type = Type;
        this.toolQuality = toolQuality;

        itemIdentifier.Type = "ToolItem";
        itemIdentifier.Name = Type.ToolName;
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
    /// ����PropType��
    /// </summary>
    public PropType Type;

    /// <summary>
    /// ���ߵȼ���ö����
    /// </summary>
    public PropQuality propQuality;

    /// <summary>
    /// ���캯��
    /// </summary>
    /// <param name="Type"></param>
    public PropItem(PropType Type, PropQuality propQuality)
    {
        //��ʼ����ʶ���ı���
        itemIdentifier = new ItemIdentifier();

        this.Type = Type;
        this.propQuality = propQuality;

        itemIdentifier.Type = "PropItem";
        itemIdentifier.Name = Type.PropName;
        itemIdentifier.PropQualityIditenfier = propQuality;
    }
}