using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 库存数据类，还有一些inventory数据的操作
/// </summary>
public class InventoryData
{
    /// <summary>
    /// 物品列表
    /// </summary>
    public List<ItemData> list = new List<ItemData>();
    /// <summary>
    /// 判断当前背包栏是否可以交换物品
    /// </summary>
    public bool canExchangeItemWithEquipmentSlotContainer = true;

    /// <summary>
    /// 向列表中添加物品
    /// </summary>
    public void AddItemInList(ItemData item)
    {
        //itemData.TypedefaultType为的Slot是否被找到的标识（默认为false）
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
        //如果没有找到null的slot
        if (hasFoundDefaultTypeSlot == false)
        {
            Debug.Log("***背包满了！");
        }
    }

    /// <summary>
    /// 通过序号向列表中添加物品
    /// </summary>
    /// <param name="item">传进来的物品</param>
    /// <param name="index">需要添加的序号(从零开始计算)</param>
    public void AddItemInListFromIndex(ItemData item, int index)
    {
        if(index < 0 || index + 1 > list.Count)
        {
            Debug.LogError("传进来的列表序号是错误的，请检查代码! ");
            return;
        }

        list[index] = item;
    }

    /// <summary>
    /// 删除列表中对应序号的物品
    /// </summary>
    /// <param name="index">对应的物品序号</param>
    public void DeleteItemInListFromIndex(int index)
    {
        //检测传进来的序号是否大于等于fishItemList的最大数量
        if (index + 1 >= list.Count && index < 0)
        {
            Debug.LogError("传入的序号过大不对，请检查代码！");
        }
        else
        {
            //将从fishItemList对应序号的位置置空
            list[index] = new ItemData();
        }
    }

    /// <summary>
    /// 顺位删除列表中最后一位的数据
    /// </summary>
    public void DeleteLastItemInList()
    {
        //直接从列表的末尾开始遍历
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
    /// 删除列表中所有元素
    /// </summary>
    public void DeletAllItemInList()
    {
        for (int i = 0; i < list.Count; i++)
        {
            list[i] = new ItemData();
        }
    }

    /// <summary>
    /// 查询list中的每一个元素
    /// </summary>
    public void CheckInventoryListData()
    {
        for(int i = 0; i < list.Count; i++)
        {
            Debug.Log(list[i].GetType().Name);
        }
    }

    /// <summary>
    /// 设定是否可以与装备栏进行交换的状态的方法
    /// </summary>
    /// <param name="canExchange"></param>
    public void ReverseCanExchageState(bool canExchange)
    {
        canExchangeItemWithEquipmentSlotContainer = canExchange;
    }

    /// <summary>
    /// 获取是否可以与装备栏进行交换的方法
    /// </summary>
    /// <returns></returns>
    public bool GetExchangeState()
    {
        return canExchangeItemWithEquipmentSlotContainer;
    }
}
