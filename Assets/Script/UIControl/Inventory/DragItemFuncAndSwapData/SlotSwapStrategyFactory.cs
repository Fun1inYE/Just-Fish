using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���𴴽��������ԵĹ�����
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
