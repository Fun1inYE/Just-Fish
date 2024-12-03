using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
    /// 向列表中添加物品(物品数量默认为1)
    /// </summary>
    public void AddItemInList(ItemData item, int amount = 0)
    {
        //itemData.TypedefaultType为的Slot是否被找到的标识（默认为false）
        bool hasFoundDefaultTypeSlot = false;

        //先判断item是否可以堆叠
        if (item.canStack)
        {
            //遍历背包中的物品
            foreach(ItemData data in list)
            {
                //如果在list中找到了与传进来的item的名字一样的物品
                if (item.type.name == data.type.name)
                {
                    //在判断此物品的堆叠数是否等于此物品的最大堆叠数
                    if(data.amount == data.maxAmount)
                    {
                        Debug.Log("格子已经为此物品的最大堆叠数了");
                        continue;
                    }
                    //给此物品格子添加amount个重复物品
                    data.amount += amount;
                    //更新Identifier
                    data.itemIdentifier.amountIditenfier = data.amount;
                    hasFoundDefaultTypeSlot = true;
                    break;
                }
            }
        }

        //寻找空格子
        for(int i = 0; i < list.Count; i++)
        {
            //如果当前格子为空格子
            if (list[i].itemIdentifier.Type == "defaultType" && !hasFoundDefaultTypeSlot)
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
            //显示UI
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
    /// 删除列表中对应序号的物品（删除一个格子中的全部数据）
    /// </summary>
    /// <param name="index">对应的物品序号</param>
    /// /// <param name="deleteAll">是否删除一个格子中的全部数据（默认为false）</param>
    public void DeleteItemInListFromIndex(int index, bool deleteAll = false)
    {
        //检测传进来的序号是否大于等于fishItemList的最大数量
        if (index + 1 >= list.Count && index < 0)
        {
            Debug.LogError("传入的序号过大不对，请检查代码！");
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
                //更新Identifier
                list[index].itemIdentifier.amountIditenfier = list[index].amount;
            }
        }
    }

    /// <summary>
    /// 顺位删除列表中最后一位的数据（删除一个格子中的全部数据）
    /// </summary>
    /// <param name="deleteAll">是否删除一个格子中的全部数据（默认为false）</param>
    public void DeleteLastItemInList(bool deleteAll = false)
    {
        //直接从列表的末尾开始遍历
        for (int i = list.Count - 1; i >= 0; i--)
        {
            //如果该物品标识符不为defaultType
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
                    //更新Identifier
                    list[i].itemIdentifier.amountIditenfier = list[i].amount;
                    break;
                }
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
            list[i] = new ItemData(new BaseType());
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
