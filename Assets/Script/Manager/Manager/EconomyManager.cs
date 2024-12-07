using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 经济管理器（每个物品单独计算）
/// </summary>
public class EconomyManager : MonoBehaviour
{
    /// <summary>
    /// 买入系数（默认1.2）
    /// </summary>
    public readonly float buyEco_Coefficient = 1.2f;
    /// <summary>
    /// 卖出系数（默认0.8）
    /// </summary>
    public readonly float saleEco_Coefficient = 0.8f;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        
    }

    /// <summary>
    /// 返回通过基础价值计算的买入价格
    /// </summary>
    /// <returns>计算完的价值</returns>
    public int ReturnBuyWithCoefficient(ItemData item)
    {
        return CalculatPriceFormItemType(item, buyEco_Coefficient);
    }

    /// <summary>
    /// 返回通过基础价值计算出的卖出
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public int ReturnSaleWithCoefficient(ItemData item)
    {
        return CalculatPriceFormItemType(item, saleEco_Coefficient);
    }

    /// <summary>
    /// 通过传入的物品类型进行对应类型物品的计算
    /// </summary>
    /// <param name="item"></param>
    /// <param name="coefficient"></param>
    /// <returns></returns>
    private int CalculatPriceFormItemType(ItemData item, float coefficient)
    {
        //如果item类型为fishItem
        if(item is FishItem fishItem)
        {
            //鱼重量的参数
            float weight_Coefficient = 5f;
            //鱼长度的参数
            float length_Coefficient = 2f;
            //鱼的基础价值 x 买入/卖出系数 + 鱼的重量 x (重量参数 + 买入/卖出系数) + 鱼的长度 x (长度参数 + 买入/卖出系数)
            float result = (float)(fishItem.baseEco * coefficient + fishItem.weight * (weight_Coefficient + coefficient) + fishItem.length * (length_Coefficient + coefficient));

            //判断结果是0的话，需要加1
            if(result == 0)
            {
                result += 1;
            }

            return (int)result;
        }

        //如果物品是鱼竿
        if (item is ToolItem toolItem)
        {
            //TODO: 物品特殊效果的计算

            //鱼竿的基础价值 x  买入/卖出系数
            float result = toolItem.baseEco * coefficient;

            //判断结果是0的话，需要加1
            if (result == 0)
            {
                result += 1;
            }

            return (int)result;
        }

        //如果物品是鱼鳔
        if (item is PropItem propItem)
        {
            //TODO: 物品特殊效果的计算

            //鱼鳔的基础价值 x  买入/卖出系数
            float result = propItem.baseEco * coefficient;

            //判断结果是0的话，需要加1
            if (result == 0)
            {
                result += 1;
            }

            return (int)result;
        }

        //如果物品是鱼饵
        if (item is BaitItem baitItem)
        {
            //TODO: 物品特殊效果的计算

            //鱼饵的基础价值 x  买入/卖出系数 * 鱼饵的叠加数量
            float result = baitItem.baseEco * coefficient * baitItem.amount;

            //判断结果是0的话，需要加1
            if (result == 0)
            {
                result += 1;
            }

            return (int)result;
        }

        Debug.LogError("没有检测到对应的item类型，请检查代码！");
        return 0;
    }
}
