using JetBrains.Annotations;
using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// slot������������
/// </summary>
public class SlotContainer : MonoBehaviour
{
    /// <summary>
    /// SlotContainer�е�����GameObject
    /// </summary>
    public GameObject[] slotGameObjects;

    /// <summary>
    /// SlotContainer�����е�Slot
    /// </summary>
    public Slot[] slots;

    /// <summary>
    /// �ű���ʼ��(ͨ��InventoryManager����)
    /// </summary>
    public virtual void InitializedContainer()
    {
        //��slotGameObjects�������ʼ��
        slots = new Slot[slotGameObjects.Length];
        //�����ӳ�ʼ��
        for (int i = 0; i < slotGameObjects.Length; i++)
        {
            //��ÿһ��slot��ȡֵ
            slots[i] = slotGameObjects[i].GetComponent<Slot>();
            //��ÿһ�����ӽ��г�ʼ��
            if (slots[i] != null)
            {
                slots[i].InitEverySlot();
                slots[i].Index = i;
            }
            else
            {
                Debug.LogError($"slot��{i}λ��λ���ǿյģ���������Ƿ������⣡");
            }
        }
    }

    /// <summary>
    /// �����е�Slot��ֵ�����ӦInventory�е�fishItemList�е�ֵ��(ˢ��SlotUI)
    /// </summary>
    public virtual void RefreshSlotUI()
    {
        for (int i = 0; i < slotGameObjects.Length; i++)
        {
            slots[i].UpdateUI();
        }
    }
}
