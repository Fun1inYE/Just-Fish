using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���ӽ����Ĳ��Է����ӿ�
/// </summary>
public abstract class ISlotSwapStrategy
{
    /// <summary>
    /// ��ʶ�Ƿ��пɶѵ���Ʒ�ڶѵ���ͬ��Ʒ(Ĭ��false)
    /// </summary>
    public bool canStackSwap = false;
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
    public virtual void Swap(Slot originalSlot, Slot targetSlot)
    {
        //���ж���֮ǰ�жϵ��϶�����Ʒ�Ƿ���Զѵ�����Ŀ��Ͳ�������Ʒ����һ��
        if (canStackSwap && originalSlot.GetSlotItem().type.name == targetSlot.GetSlotItem().type.name)
        {
            //����ƶ�����ӵ���Ʒ���޵Ļ�
            if (originalSlot.GetSlotItem().amount + targetSlot.GetSlotItem().amount > targetSlot.GetSlotItem().maxAmount)
            {
                //�������
                originalSlot.GetSlotItem().amount = originalSlot.GetSlotItem().amount - targetSlot.GetSlotItem().maxAmount + targetSlot.GetSlotItem().amount;
                targetSlot.GetSlotItem().amount = targetSlot.GetSlotItem().maxAmount;
            }
            //����ƶ������û�е����������޵Ļ�
            else if (originalSlot.GetSlotItem().amount + targetSlot.GetSlotItem().amount <= targetSlot.GetSlotItem().maxAmount)
            {
                //�������
                targetSlot.GetSlotItem().amount = originalSlot.GetSlotItem().amount + targetSlot.GetSlotItem().amount;
                originalSlot.GetSlotItem().amount = 0;
            }
        }
        else
        {
            // ִ����ͨ���ӽ������߼�
            ItemData tempData = originalSlot.inventory_Database.list[originalSlot.Index];
            originalSlot.inventory_Database.list[originalSlot.Index] = targetSlot.inventory_Database.list[targetSlot.Index];
            targetSlot.inventory_Database.list[targetSlot.Index] = tempData;
        }
    }
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

        //���ж��϶��ĸ����е���Ʒ�Ƿ���Զѵ�
        if (originalSlot.GetSlotItem().canStack)
        {
            //�ƶ�����Ʒ�ǿ��Զѵ���
            canStackSwap = true;
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
        base.Swap(originalSlot, targetSlot);
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

        //���ж��϶��ĸ����е���Ʒ�Ƿ���Զѵ�
        if (originalSlot.GetSlotItem().canStack)
        {
            //�ƶ�����Ʒ�ǿ��Զѵ���
            canStackSwap = true;
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
        base.Swap(originalSlot, targetSlot);
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

        //���ж��϶��ĸ����е���Ʒ�Ƿ���Զѵ�
        if (originalSlot.GetSlotItem().canStack)
        {
            Debug.Log("originalSlot.GetSlotItem().canStack");
            //�ƶ�����Ʒ�ǿ��Զѵ���
            canStackSwap = true;
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
        base.Swap(originalSlot, targetSlot);
    }
}