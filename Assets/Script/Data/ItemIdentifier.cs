using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��Ϸ�ı�ʶ��(ֻ�н���ʱ����޸�ʱ��)
/// </summary>
public class GameDataIdentifier
{
    /// <summary>
    /// �����浵ʱ��(Ĭ��Ϊ"")
    /// </summary>
    public string createArchiveTimeIdentifier = "";
    /// <summary>
    /// �޸Ĵ浵ʱ��(Ĭ��Ϊ"")
    /// </summary>
    public string reviseArchiveTimeIdentifier = "";

    public GameDataIdentifier()
    {
        createArchiveTimeIdentifier = "";
        reviseArchiveTimeIdentifier = "";
    }
}


/// <summary>
/// ��Ʒ�ı�ʶ��
/// </summary>
public class ItemIdentifier
{
    /// <summary>
    /// ��Ʒ�����ࣨĬ��defaultType��
    /// </summary>
    public string Type = "defaultType";
    /// <summary>
    /// ��Ʒ�����֣�Ĭ��defaultName��
    /// </summary>
    public string Name = "defaultName";

    /// <summary>
    /// ��ĳ���(Ĭ��Ϊ0)
    /// </summary>
    public double FishLength = 0f;
    /// <summary>
    /// �������(Ĭ��Ϊ0)
    /// </summary>
    public double FishWeight = 0f;

    /// <summary>
    /// ����Ʒ�ʱ�ʶ��(Ĭ��Ϊ0)
    /// </summary>
    public ToolQuality ToolQualityIditenfier = 0;

    /// <summary>
    /// ����Ʒ�ʱ�ʶ��(Ĭ��Ϊ0)
    /// </summary>
    public PropQuality PropQualityIditenfier = 0;

    /// <summary>
    /// ���캯��
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
/// ��ҵ�������ݱ�ʶ��
/// </summary>
public class PlayerDataIditentifier
{
    /// <summary>
    /// ��ҵ����ֵı�ʶ��(Ĭ��ΪdefaultName)
    /// </summary>
    public string playerNameIdetentifier { get; set; } = "defaultName";
    /// <summary>
    /// �������λ�õ�X�����ʶ����Ĭ��Ϊ0��
    /// </summary>
    public double coordinate_XIdetentifier = 0f;
    /// <summary>
    /// �������λ�õ�Y�����ʶ����Ĭ��Ϊ0��
    /// </summary>
    public double coordinate_YIdetentifier = 0f;
    /// <summary>
    /// ��ҵ������ı�ʶ����Ĭ��Ϊ0��
    /// </summary>
    public int coinIdetentifier = 0;

    /// <summary>
    /// ���캯��
    /// </summary>
    public PlayerDataIditentifier()
    {
        playerNameIdetentifier = "defaultName";
        coordinate_XIdetentifier = 0f;
        coordinate_YIdetentifier = 0f;
        coinIdetentifier = 0;
    }
}