using JetBrains.Annotations;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// slot的容器设置类
/// </summary>
public class SlotContainer : MonoBehaviour
{
    /// <summary>
    /// SlotContainer中的所有GameObject
    /// </summary>
    public GameObject[] slotGameObjects;

    /// <summary>
    /// SlotContainer中所有的Slot
    /// </summary>
    public Slot[] slots;

    /// <summary>
    /// 脚本初始化(通过InventoryManager驱动)
    /// </summary>
    public virtual void InitializedContainer()
    {
        //将slotGameObjects的数组初始化
        slots = new Slot[slotGameObjects.Length];
        //给格子初始化
        for (int i = 0; i < slotGameObjects.Length; i++)
        {
            //给每一个slot获取值
            slots[i] = slotGameObjects[i].GetComponent<Slot>();
            //给每一个格子进行初始化
            if (slots[i] != null)
            {
                slots[i].InitEverySlot();
                slots[i].Index = i;
            }
            else
            {
                Debug.LogError($"slot第{i}位的位置是空的，请检查代码是否有问题！");
            }
        }
    }

    /// <summary>
    /// 将所有的Slot的值赋予对应Inventory中的fishItemList中的值。(刷新SlotUI)
    /// </summary>
    public virtual void RefreshSlotUI()
    {
        for (int i = 0; i < slotGameObjects.Length; i++)
        {
            slots[i].UpdateUI();
        }
    }
}
