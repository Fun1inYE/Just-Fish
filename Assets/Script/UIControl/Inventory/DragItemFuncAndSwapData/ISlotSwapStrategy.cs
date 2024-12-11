using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 格子交换的策略方法接口
/// </summary>
public abstract class ISlotSwapStrategy
{
    /// <summary>
    /// 标识是否有可堆叠物品在堆叠相同物品(默认false)
    /// </summary>
    public bool canStackSwap = false;
    /// <summary>
    /// 判断是否可以交换
    /// </summary>
    /// <param name="originalSlot"></param>
    /// <param name="targetSlot"></param>
    /// <returns></returns>
    public abstract bool CanSwap(Slot originalSlot, Slot targetSlot);
    /// <summary>
    /// 交换数据的方法
    /// </summary>
    /// <param name="oriinalSlot"></param>
    /// <param name="targetSlot"></param>
    public virtual void Swap(Slot originalSlot, Slot targetSlot)
    {
        //先判断在之前判断的拖动的物品是否可以堆叠并且目标和操作的物品名字一样
        if (canStackSwap && originalSlot.GetSlotItem().type.name == targetSlot.GetSlotItem().type.name)
        {
            //如果移动完叠加到物品上限的话
            if (originalSlot.GetSlotItem().amount + targetSlot.GetSlotItem().amount > targetSlot.GetSlotItem().maxAmount)
            {
                //计算过程
                originalSlot.GetSlotItem().amount = originalSlot.GetSlotItem().amount - targetSlot.GetSlotItem().maxAmount + targetSlot.GetSlotItem().amount;
                targetSlot.GetSlotItem().amount = targetSlot.GetSlotItem().maxAmount;
            }
            //如果移动完叠加没有到达物体上限的话
            else if (originalSlot.GetSlotItem().amount + targetSlot.GetSlotItem().amount <= targetSlot.GetSlotItem().maxAmount)
            {
                //计算过程
                targetSlot.GetSlotItem().amount = originalSlot.GetSlotItem().amount + targetSlot.GetSlotItem().amount;
                originalSlot.GetSlotItem().amount = 0;
            }
        }
        else
        {
            // 执行普通格子交换的逻辑
            ItemData tempData = originalSlot.inventory_Database.list[originalSlot.Index];
            originalSlot.inventory_Database.list[originalSlot.Index] = targetSlot.inventory_Database.list[targetSlot.Index];
            targetSlot.inventory_Database.list[targetSlot.Index] = tempData;
        }
    }
}

/// <summary>
/// 普通格子交换的方法
/// </summary>
public class NormalSlotSwapStrategy : ISlotSwapStrategy
{
    public override bool CanSwap(Slot originalSlot, Slot targetSlot)
    {
        //先检查源头originalSlot和targetSlot对应的数据库是否可以交换物品
        if (!originalSlot.inventory_Database.GetExchangeState() || !targetSlot.inventory_Database.GetExchangeState())
        {
            return false;
        }

        //再判断拖动的格子中的物品是否可以堆叠
        if (originalSlot.GetSlotItem().canStack)
        {
            //移动的物品是可以堆叠的
            canStackSwap = true;
        }

        //从普通格子移动到装备格子
        if (targetSlot.slotType == SlotType.Equipment && originalSlot.slotType != SlotType.Equipment)
        {
            //检查装备的鱼竿位置
            if(targetSlot.Index == 0)
            {
                return originalSlot.inventory_Database.list[originalSlot.Index] is ToolItem;
            }
            //检查装备的鱼鳔位置
            else if (targetSlot.Index == 1)
            {
                return originalSlot.inventory_Database.list[originalSlot.Index] is PropItem;
            }
            //检查装备的鱼饵位置
            else if (targetSlot.Index == 2)
            {
                return originalSlot.inventory_Database.list[originalSlot.Index] is BaitItem;
            }
            else
            {
                Debug.LogWarning("移动了一个未知物品！");
                return false;
            }
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

    public override void Swap(Slot originalSlot, Slot targetSlot)
    {
        base.Swap(originalSlot, targetSlot);
    }
}

/// <summary>
/// 装备格子交换的方法(因为三个格子分别放三种不同的东西，所以装备格子之间不能互换)
/// </summary>
public class EquipmentSlotSwapStrategy : ISlotSwapStrategy
{
    public override bool CanSwap(Slot originalSlot, Slot targetSlot)
    {
        //先检查源头originalSlot和targetSlot对应的数据库是否可以交换物品
        if (!originalSlot.inventory_Database.GetExchangeState() || !targetSlot.inventory_Database.GetExchangeState())
        {
            return false;
        }

        //再判断拖动的格子中的物品是否可以堆叠
        if (originalSlot.GetSlotItem().canStack)
        {
            //移动的物品是可以堆叠的
            canStackSwap = true;
        }

        //当目标Slot不是装备类型的，而操作的Slot是装备格子
        if (targetSlot.slotType != SlotType.Equipment && originalSlot.slotType == SlotType.Equipment)
        {
            //首先判断两个格子基本类行是否可以交换
            if (targetSlot.slotType == originalSlot.interiorSlotType)
            {
                if (targetSlot.GetSlotItem().GetType() == originalSlot.GetSlotItem().GetType())
                {
                    return true;
                }
                else if(targetSlot.GetSlotItem().itemIdentifier.Type == "defaultType")
                {
                    return true;
                }
            }
        }

        return false;
    }

    public override void Swap(Slot originalSlot, Slot targetSlot)
    {
        base.Swap(originalSlot, targetSlot);
    }
}

/// <summary>
/// 卖出格子的交换方法
/// </summary>
public class SaleSlotSwapStrategy : ISlotSwapStrategy
{
    public override bool CanSwap(Slot originalSlot, Slot targetSlot)
    {
        //先检查源头originalSlot和targetSlot对应的数据库是否可以交换物品
        if (!originalSlot.inventory_Database.GetExchangeState() || !targetSlot.inventory_Database.GetExchangeState())
        {
            return false;
        }

        //再判断拖动的格子中的物品是否可以堆叠
        if (originalSlot.GetSlotItem().canStack)
        {
            Debug.Log("originalSlot.GetSlotItem().canStack");
            //移动的物品是可以堆叠的
            canStackSwap = true;
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

    public override void Swap(Slot originalSlot, Slot targetSlot)
    {
        base.Swap(originalSlot, targetSlot);
    }
}