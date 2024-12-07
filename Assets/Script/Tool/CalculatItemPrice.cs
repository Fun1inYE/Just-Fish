using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 计算物品价值的类
/// </summary>
public static class CalculatItemPrice
{
    /// <summary>
    /// 计算鱼的价值（返回整数）
    /// </summary>
    /// <param name="fishItem"></param>
    /// <param name="length_coefficient">长度计算系数</param>
    /// <param name="weight_coefficient">重量计算系数</param>
    /// <returns></returns>
    public static int CulationFish(FishItem fishItem, float length_coefficient, float weight_coefficient)
    {
        //最终的计算结果
        int result = 0;
        //获取到鱼的长度（整数）
        int length = (int)fishItem.length;
        //获取鱼的重量（整数）
        int weight = (int)fishItem.weight;
        //暂时定为这个计算公式
        result = (int)(length * length_coefficient + weight * weight_coefficient);
        return result;
    }

    /// <summary>
    /// 鱼竿卖出的计算方式
    /// </summary>
    /// <param name="toolItem">传进来的工具</param>
    /// <param name="toolQuality_coefficient">工具质量的计算系数</param>
    /// <returns></returns>
    public static int CulationTool(ToolItem toolItem, float toolQuality_coefficient)
    {
        //最终的计算结果
        int result = 0;
        result = (int)(toolItem.toolQuality + 1) * (int)toolQuality_coefficient;
        return result;
    }

    /// <summary>
    /// 鱼鳔卖出的计算方式
    /// </summary>
    /// <param name="propItem">传进来的道具类</param>
    /// <param name="propQuality_coefficient">道具质量的计算系数</param>
    /// <returns></returns>
    public static int CulationProp(PropItem propItem, float propQuality_coefficient)
    {
        //最终的计算结果
        int result = 0;
        result = (int)(propItem.propQuality + 1) * (int)propQuality_coefficient;
        return result;
    }
}
