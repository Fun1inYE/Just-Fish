using System.Collections;
using System.Collections.Generic;
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
    /// ���б��������Ʒ
    /// </summary>
    public void AddItemInList(ItemData item)
    {
        //itemData.TypedefaultTypeΪ��Slot�Ƿ��ҵ��ı�ʶ��Ĭ��Ϊfalse��
        bool hasFoundDefaultTypeSlot = false;

        for(int i = 0; i < list.Count; i++)
        {
            if (list[i].itemIdentifier.Type == "defaultType")
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
    /// ɾ���б��ж�Ӧ��ŵ���Ʒ
    /// </summary>
    /// <param name="index">��Ӧ����Ʒ���</param>
    public void DeleteItemInListFromIndex(int index)
    {
        //��⴫����������Ƿ���ڵ���fishItemList���������
        if (index + 1 >= list.Count && index < 0)
        {
            Debug.LogError("�������Ź��󲻶ԣ�������룡");
        }
        else
        {
            //����fishItemList��Ӧ��ŵ�λ���ÿ�
            list[index] = new ItemData();
        }
    }

    /// <summary>
    /// ˳λɾ���б������һλ������
    /// </summary>
    public void DeleteLastItemInList()
    {
        //ֱ�Ӵ��б��ĩβ��ʼ����
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].itemIdentifier.Type != "defaultType")
            {
                list[i] = new ItemData();
                break;
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
            list[i] = new ItemData();
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
