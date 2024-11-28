using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 格子交换的策略方法接口
/// </summary>
public interface ISlotSwapStrategy
{
    /// <summary>
    /// 判断是否可以交换
    /// </summary>
    /// <param name="originalSlot"></param>
    /// <param name="targetSlot"></param>
    /// <returns></returns>
    public bool CanSwap(Slot originalSlot, Slot targetSlot);
    /// <summary>
    /// 交换数据的方法
    /// </summary>
    /// <param name="oriinalSlot"></param>
    /// <param name="targetSlot"></param>
    public void Swap(Slot orignalSlot, Slot targetSlot);
}

/// <summary>
/// 普通格子交换的方法
/// </summary>
public class NormalSlotSwapStrategy : ISlotSwapStrategy
{
    public bool CanSwap(Slot originalSlot, Slot targetSlot)
    {
        //先检查源头originalSlot和targetSlot对应的数据库是否可以交换物品
        if (!originalSlot.inventory_Database.GetExchangeState() || !targetSlot.inventory_Database.GetExchangeState())
        {
            return false;
        }

        //当目标格子是装备格子而操作格子不是普通格子
        if (targetSlot.slotType == SlotType.Equipment && originalSlot.slotType != SlotType.Equipment)
        {
            //如果装备格子的equipmentSlotType等于操作格子的类型
            return targetSlot.interiorSlotType == originalSlot.slotType;
        }
        //当目标格子是卖出格子而操作格子不是普通格子
        else if (targetSlot.slotType == SlotType.Sale && originalSlot.slotType != SlotType.Sale)
        {
            if(targetSlot.interiorSlotType == SlotType.Sale)
            {
                //因为卖出格子什么物品都能放
                return true;
            }
            else
            {
                //判断当卖出栏中有物品，要直接从普通格子中交换的情况
                return originalSlot.slotType == targetSlot.interiorSlotType;
            }
        }
        else
        {
            //普通交换格子的方法
            return originalSlot.slotType == targetSlot.slotType;
        }
    }

    public void Swap(Slot originalSlot, Slot targetSlot)
    {
        // 执行普通格子交换的逻辑
        ItemData tempData = originalSlot.inventory_Database.list[originalSlot.Index];
        originalSlot.inventory_Database.list[originalSlot.Index] = targetSlot.inventory_Database.list[targetSlot.Index];
        targetSlot.inventory_Database.list[targetSlot.Index] = tempData;
    }
}

/// <summary>
/// 装备格子交换的方法
/// </summary>
public class EquipmentSlotSwapStrategy : ISlotSwapStrategy
{
    public bool CanSwap(Slot originalSlot, Slot targetSlot)
    {
        //先检查源头originalSlot和targetSlot对应的数据库是否可以交换物品
        if (!originalSlot.inventory_Database.GetExchangeState() || !targetSlot.inventory_Database.GetExchangeState())
        {
            return false;
        }

        //当目标Slot不是装备类型的，而操作的Slot是装备格子
        if (targetSlot.slotType != SlotType.Equipment && originalSlot.slotType == SlotType.Equipment)
        {
            return targetSlot.slotType == originalSlot.interiorSlotType;
        }
        else
        {
            //装备格子之间的交换方法
            return targetSlot.interiorSlotType == originalSlot.interiorSlotType;
        }
    }

    public void Swap(Slot originalSlot, Slot targetSlot)
    {
        // 执行装备格子的交换逻辑
        ItemData tempData = originalSlot.inventory_Database.list[originalSlot.Index];
        originalSlot.inventory_Database.list[originalSlot.Index] = targetSlot.inventory_Database.list[targetSlot.Index];
        targetSlot.inventory_Database.list[targetSlot.Index] = tempData;
    }
}

/// <summary>
/// 卖出格子的交换方法
/// </summary>
public class SaleSlotSwapStrategy : ISlotSwapStrategy
{
    public bool CanSwap(Slot originalSlot, Slot targetSlot)
    {
        //先检查源头originalSlot和targetSlot对应的数据库是否可以交换物品
        if (!originalSlot.inventory_Database.GetExchangeState() || !targetSlot.inventory_Database.GetExchangeState())
        {
            return false;
        }

        //当操作的slot是卖出格子，而目标不是卖出格子
        if (targetSlot.slotType != SlotType.Sale && originalSlot.slotType == SlotType.Sale)
        {
            //因为卖出格子什么物品都能放，并且在卖出格子被放入的那一刻，卖出格子的类型会自动变成那种物品的类型
            return targetSlot.slotType == originalSlot.interiorSlotType;
        }
        else
        {
            //卖出格子和卖出格子自己交换
            return true;
        }
    }

    public void Swap(Slot originalSlot, Slot targetSlot)
    {
        // 执行卖出格子的交换逻辑
        ItemData tempData = originalSlot.inventory_Database.list[originalSlot.Index];
        originalSlot.inventory_Database.list[originalSlot.Index] = targetSlot.inventory_Database.list[targetSlot.Index];
        targetSlot.inventory_Database.list[targetSlot.Index] = tempData;
    }
}