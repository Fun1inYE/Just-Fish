using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// װ����������������ݼӳɸ�����װ����
/// </summary>
public enum EquipmentType { Rod, Drift, Bait}

/// <summary>
/// ��ֵ�ӳ�������
/// </summary>
public class BonusData
{
    /// <summary>
    /// �������
    /// </summary>
    public EquipmentType equipmentType;

    /// <summary>
    /// �ӳ���������
    /// </summary>
    public string name;

    /// <summary>
    /// ��Ӧ���Եĵȼ�
    /// </summary>
    public int level;

    /// <summary>
    /// �ӳɰٷֱȣ�������0��1(Ĭ��0.1)
    /// </summary>
    [Range(0, 1)]
    public readonly double tempPercent = 0.1;

    /// <summary>
    /// ���ռ���ļӳɰٷֱ�
    /// </summary>
    public double bonusData;

    /// <summary>
    /// ���캯��
    /// </summary>
    public BonusData(EquipmentType equipmentType, string name, int level, double tempPercent = 0.1f)
    {
        this.equipmentType = equipmentType;
        this.name = name;
        this.level = level;
        bonusData = tempPercent * level;
    }
}
