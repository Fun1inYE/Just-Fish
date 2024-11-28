using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ӽ����Ĳ��Է����ӿ�
/// </summary>
public interface ISlotSwapStrategy
{
    /// <summary>
    /// �ж��Ƿ���Խ���
    /// </summary>
    /// <param name="originalSlot"></param>
    /// <param name="targetSlot"></param>
    /// <returns></returns>
    public bool CanSwap(Slot originalSlot, Slot targetSlot);
    /// <summary>
    /// �������ݵķ���
    /// </summary>
    /// <param name="oriinalSlot"></param>
    /// <param name="targetSlot"></param>
    public void Swap(Slot orignalSlot, Slot targetSlot);
}

/// <summary>
/// ��ͨ���ӽ����ķ���
/// </summary>
public class NormalSlotSwapStrategy : ISlotSwapStrategy
{
    public bool CanSwap(Slot originalSlot, Slot targetSlot)
    {
        //�ȼ��ԴͷoriginalSlot��targetSlot��Ӧ�����ݿ��Ƿ���Խ�����Ʒ
        if (!originalSlot.inventory_Database.GetExchangeState() || !targetSlot.inventory_Database.GetExchangeState())
        {
            return false;
        }

        //��Ŀ�������װ�����Ӷ��������Ӳ�����ͨ����
        if (targetSlot.slotType == SlotType.Equipment && originalSlot.slotType != SlotType.Equipment)
        {
            //���װ�����ӵ�equipmentSlotType���ڲ������ӵ�����
            return targetSlot.interiorSlotType == originalSlot.slotType;
        }
        //��Ŀ��������������Ӷ��������Ӳ�����ͨ����
        else if (targetSlot.slotType == SlotType.Sale && originalSlot.slotType != SlotType.Sale)
        {
            if(targetSlot.interiorSlotType == SlotType.Sale)
            {
                //��Ϊ��������ʲô��Ʒ���ܷ�
                return true;
            }
            else
            {
                //�жϵ�������������Ʒ��Ҫֱ�Ӵ���ͨ�����н��������
                return originalSlot.slotType == targetSlot.interiorSlotType;
            }
        }
        else
        {
            //��ͨ�������ӵķ���
            return originalSlot.slotType == targetSlot.slotType;
        }
    }

    public void Swap(Slot originalSlot, Slot targetSlot)
    {
        // ִ����ͨ���ӽ������߼�
        ItemData tempData = originalSlot.inventory_Database.list[originalSlot.Index];
        originalSlot.inventory_Database.list[originalSlot.Index] = targetSlot.inventory_Database.list[targetSlot.Index];
        targetSlot.inventory_Database.list[targetSlot.Index] = tempData;
    }
}

/// <summary>
/// װ�����ӽ����ķ���
/// </summary>
public class EquipmentSlotSwapStrategy : ISlotSwapStrategy
{
    public bool CanSwap(Slot originalSlot, Slot targetSlot)
    {
        //�ȼ��ԴͷoriginalSlot��targetSlot��Ӧ�����ݿ��Ƿ���Խ�����Ʒ
        if (!originalSlot.inventory_Database.GetExchangeState() || !targetSlot.inventory_Database.GetExchangeState())
        {
            return false;
        }

        //��Ŀ��Slot����װ�����͵ģ���������Slot��װ������
        if (targetSlot.slotType != SlotType.Equipment && originalSlot.slotType == SlotType.Equipment)
        {
            return targetSlot.slotType == originalSlot.interiorSlotType;
        }
        else
        {
            //װ������֮��Ľ�������
            return targetSlot.interiorSlotType == originalSlot.interiorSlotType;
        }
    }

    public void Swap(Slot originalSlot, Slot targetSlot)
    {
        // ִ��װ�����ӵĽ����߼�
        ItemData tempData = originalSlot.inventory_Database.list[originalSlot.Index];
        originalSlot.inventory_Database.list[originalSlot.Index] = targetSlot.inventory_Database.list[targetSlot.Index];
        targetSlot.inventory_Database.list[targetSlot.Index] = tempData;
    }
}

/// <summary>
/// �������ӵĽ�������
/// </summary>
public class SaleSlotSwapStrategy : ISlotSwapStrategy
{
    public bool CanSwap(Slot originalSlot, Slot targetSlot)
    {
        //�ȼ��ԴͷoriginalSlot��targetSlot��Ӧ�����ݿ��Ƿ���Խ�����Ʒ
        if (!originalSlot.inventory_Database.GetExchangeState() || !targetSlot.inventory_Database.GetExchangeState())
        {
            return false;
        }

        //��������slot���������ӣ���Ŀ�겻����������
        if (targetSlot.slotType != SlotType.Sale && originalSlot.slotType == SlotType.Sale)
        {
            //��Ϊ��������ʲô��Ʒ���ܷţ��������������ӱ��������һ�̣��������ӵ����ͻ��Զ����������Ʒ������
            return targetSlot.slotType == originalSlot.interiorSlotType;
        }
        else
        {
            //�������Ӻ����������Լ�����
            return true;
        }
    }

    public void Swap(Slot originalSlot, Slot targetSlot)
    {
        // ִ���������ӵĽ����߼�
        ItemData tempData = originalSlot.inventory_Database.list[originalSlot.Index];
        originalSlot.inventory_Database.list[originalSlot.Index] = targetSlot.inventory_Database.list[targetSlot.Index];
        targetSlot.inventory_Database.list[targetSlot.Index] = tempData;
    }
}