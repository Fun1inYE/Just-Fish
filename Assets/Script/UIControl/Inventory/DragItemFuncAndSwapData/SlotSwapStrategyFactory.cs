using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 负责创建交换策略的工厂类
/// </summary>
public static class SlotSwapStrategyFactory
{
    public static ISlotSwapStrategy GetSwapStrategy(Slot slot)
    {
        switch (slot.slotType)
        {
            case SlotType.Equipment:
                return new EquipmentSlotSwapStrategy();
            case SlotType.Sale:
                return new SaleSlotSwapStrategy();
            default:
                return new NormalSlotSwapStrategy();
        }
    }
}
