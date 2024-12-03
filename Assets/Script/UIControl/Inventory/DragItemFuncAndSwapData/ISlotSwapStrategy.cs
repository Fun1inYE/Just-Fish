using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ӽ����Ĳ��Է����ӿ�
/// </summary>
public abstract class ISlotSwapStrategy
{
    /// <summary>
    /// �ж��Ƿ���Խ���
    /// </summary>
    /// <param name="originalSlot"></param>
    /// <param name="targetSlot"></param>
    /// <returns></returns>
    public abstract bool CanSwap(Slot originalSlot, Slot targetSlot);
    /// <summary>
    /// �������ݵķ���
    /// </summary>
    /// <param name="oriinalSlot"></param>
    /// <param name="targetSlot"></param>
    public abstract void Swap(Slot orignalSlot, Slot targetSlot);
}

/// <summary>
/// ��ͨ���ӽ����ķ���
/// </summary>
public class NormalSlotSwapStrategy : ISlotSwapStrategy
{
    public override bool CanSwap(Slot originalSlot, Slot targetSlot)
    {
        //�ȼ��ԴͷoriginalSlot��targetSlot��Ӧ�����ݿ��Ƿ���Խ�����Ʒ
        if (!originalSlot.inventory_Database.GetExchangeState() || !targetSlot.inventory_Database.GetExchangeState())
        {
            return false;
        }

        //����ͨ�����ƶ���װ������
        if (targetSlot.slotType == SlotType.Equipment && originalSlot.slotType != SlotType.Equipment)
        {
            //���װ�������λ��
            if(targetSlot.Index == 0)
            {
                return originalSlot.inventory_Database.list[originalSlot.Index] is ToolItem;
            }
            //���װ��������λ��
            else if (targetSlot.Index == 1)
            {
                return originalSlot.inventory_Database.list[originalSlot.Index] is PropItem;
            }
            //���װ�������λ��
            else if (targetSlot.Index == 2)
            {
                return originalSlot.inventory_Database.list[originalSlot.Index] is BaitItem;
            }
            else
            {
                Debug.LogWarning("�ƶ���һ��δ֪��Ʒ��");
                return false;
            }
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

    public override void Swap(Slot originalSlot, Slot targetSlot)
    {
        // ִ����ͨ���ӽ������߼�
        ItemData tempData = originalSlot.inventory_Database.list[originalSlot.Index];
        originalSlot.inventory_Database.list[originalSlot.Index] = targetSlot.inventory_Database.list[targetSlot.Index];
        targetSlot.inventory_Database.list[targetSlot.Index] = tempData;
    }
}

/// <summary>
/// װ�����ӽ����ķ���(��Ϊ�������ӷֱ�����ֲ�ͬ�Ķ���������װ������֮�䲻�ܻ���)
/// </summary>
public class EquipmentSlotSwapStrategy : ISlotSwapStrategy
{
    public override bool CanSwap(Slot originalSlot, Slot targetSlot)
    {
        //�ȼ��ԴͷoriginalSlot��targetSlot��Ӧ�����ݿ��Ƿ���Խ�����Ʒ
        if (!originalSlot.inventory_Database.GetExchangeState() || !targetSlot.inventory_Database.GetExchangeState())
        {
            return false;
        }

        //��Ŀ��Slot����װ�����͵ģ���������Slot��װ������
        if (targetSlot.slotType != SlotType.Equipment && originalSlot.slotType == SlotType.Equipment)
        {
            //�����ж��������ӻ��������Ƿ���Խ���
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
    public override bool CanSwap(Slot originalSlot, Slot targetSlot)
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

    public override void Swap(Slot originalSlot, Slot targetSlot)
    {
        // ִ���������ӵĽ����߼�
        ItemData tempData = originalSlot.inventory_Database.list[originalSlot.Index];
        originalSlot.inventory_Database.list[originalSlot.Index] = targetSlot.inventory_Database.list[targetSlot.Index];
        targetSlot.inventory_Database.list[targetSlot.Index] = tempData;
    }
}