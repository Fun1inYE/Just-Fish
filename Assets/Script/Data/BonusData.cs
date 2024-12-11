using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备类别，用来区分数据加成给哪种装备的
/// </summary>
public enum EquipmentType { Rod, Drift, Bait}

/// <summary>
/// 数值加成数据类
/// </summary>
public class BonusData
{
    /// <summary>
    /// 武器类别
    /// </summary>
    public EquipmentType equipmentType;

    /// <summary>
    /// 加成属性名字
    /// </summary>
    public string name;

    /// <summary>
    /// 对应属性的等级
    /// </summary>
    public int level;

    /// <summary>
    /// 加成百分比，限制在0到1(默认0.1)
    /// </summary>
    [Range(0, 1)]
    public readonly double tempPercent = 0.1;

    /// <summary>
    /// 最终计算的加成百分比
    /// </summary>
    public double bonusData;

    /// <summary>
    /// 构造函数
    /// </summary>
    public BonusData(EquipmentType equipmentType, string name, int level, double tempPercent = 0.1f)
    {
        this.equipmentType = equipmentType;
        this.name = name;
        this.level = level;
        bonusData = tempPercent * level;
    }
}
