using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ��������࣬����һЩinventory���ݵĲ���
/// </summary>
public class InventoryData
{
    /// <summary>
    /// ��Ʒ�б�
    /// </summary>
    public List<ItemData> list = new List<ItemData>();
    /// <summary>
    /// �жϵ�ǰ�������Ƿ���Խ�����Ʒ
    /// </summary>
    public bool canExchangeItemWithEquipmentSlotContainer = true;

    /// <summary>
    /// ���б��������Ʒ(��Ʒ����Ĭ��Ϊ1)
    /// </summary>
    public void AddItemInList(ItemData item, int amount = 0)
    {
        //itemData.TypedefaultTypeΪ��Slot�Ƿ��ҵ��ı�ʶ��Ĭ��Ϊfalse��
        bool hasFoundDefaultTypeSlot = false;

        //���ж�item�Ƿ���Զѵ�
        if (item.canStack)
        {
            //���������е���Ʒ
            foreach(ItemData data in list)
            {
                //�����list���ҵ����봫������item������һ������Ʒ
                if (item.type.name == data.type.name)
                {
                    //���жϴ���Ʒ�Ķѵ����Ƿ���ڴ���Ʒ�����ѵ���
                    if(data.amount == data.maxAmount)
                    {
                        Debug.Log("�����Ѿ�Ϊ����Ʒ�����ѵ�����");
                        continue;
                    }
                    //������Ʒ�������amount���ظ���Ʒ
                    data.amount += amount;
                    //����Identifier
                    data.itemIdentifier.amountIditenfier = data.amount;
                    hasFoundDefaultTypeSlot = true;
                    break;
                }
            }
        }

        //Ѱ�ҿո���
        for(int i = 0; i < list.Count; i++)
        {
            //�����ǰ����Ϊ�ո���
            if (list[i].itemIdentifier.Type == "defaultType" && !hasFoundDefaultTypeSlot)
            {
                list[i] = item;
                hasFoundDefaultTypeSlot = true;
                break;
            }
        }
        //���û���ҵ�null��slot
        if (hasFoundDefaultTypeSlot == false)
        {
            Debug.Log("***�������ˣ�");
            //��ʾUI
        }
    }

    /// <summary>
    /// ͨ��������б��������Ʒ
    /// </summary>
    /// <param name="item">����������Ʒ</param>
    /// <param name="index">��Ҫ��ӵ����(���㿪ʼ����)</param>
    public void AddItemInListFromIndex(ItemData item, int index)
    {
        if(index < 0 || index + 1 > list.Count)
        {
            Debug.LogError("���������б�����Ǵ���ģ��������! ");
            return;
        }

        list[index] = item;
    }

    /// <summary>
    /// ɾ���б��ж�Ӧ��ŵ���Ʒ��ɾ��һ�������е�ȫ�����ݣ�
    /// </summary>
    /// <param name="index">��Ӧ����Ʒ���</param>
    /// /// <param name="deleteAll">�Ƿ�ɾ��һ�������е�ȫ�����ݣ�Ĭ��Ϊfalse��</param>
    public void DeleteItemInListFromIndex(int index, bool deleteAll = false)
    {
        //��⴫����������Ƿ���ڵ���fishItemList���������
        if (index + 1 >= list.Count && index < 0)
        {
            Debug.LogError("�������Ź��󲻶ԣ�������룡");
        }
        else
        {
            if(deleteAll || list[index].amount == 0)
            {
                list[index] = new ItemData(new BaseType());
            }
            else
            {
                list[index].amount -= 1;
                //����Identifier
                list[index].itemIdentifier.amountIditenfier = list[index].amount;
            }
        }
    }

    /// <summary>
    /// ˳λɾ���б������һλ�����ݣ�ɾ��һ�������е�ȫ�����ݣ�
    /// </summary>
    /// <param name="deleteAll">�Ƿ�ɾ��һ�������е�ȫ�����ݣ�Ĭ��Ϊfalse��</param>
    public void DeleteLastItemInList(bool deleteAll = false)
    {
        //ֱ�Ӵ��б��ĩβ��ʼ����
        for (int i = list.Count - 1; i >= 0; i--)
        {
            //�������Ʒ��ʶ����ΪdefaultType
            if (list[i].itemIdentifier.Type != "defaultType")
            {
                if(deleteAll || list[i].amount == 0)
                {
                    list[i] = new ItemData(new BaseType());
                    break;
                }
                else
                {
                    list[i].amount -= 1;
                    //����Identifier
                    list[i].itemIdentifier.amountIditenfier = list[i].amount;
                    break;
                }
            }
        }
    }

    /// <summary>
    /// ɾ���б�������Ԫ��
    /// </summary>
    public void DeletAllItemInList()
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = new ItemData(new BaseType());
        }
    }

    /// <summary>
    /// ��ѯlist�е�ÿһ��Ԫ��
    /// </summary>
    public void CheckInventoryListData()
    {
        for(int i = 0; i < list.Count; i++)
        {
            Debug.Log(list[i].GetType().Name);
        }
    }

    /// <summary>
    /// �趨�Ƿ������װ�������н�����״̬�ķ���
    /// </summary>
    /// <param name="canExchange"></param>
    public void ReverseCanExchageState(bool canExchange)
    {
        canExchangeItemWithEquipmentSlotContainer = canExchange;
    }

    /// <summary>
    /// ��ȡ�Ƿ������װ�������н����ķ���
    /// </summary>
    /// <returns></returns>
    public bool GetExchangeState()
    {
        return canExchangeItemWithEquipmentSlotContainer;
    }
}
