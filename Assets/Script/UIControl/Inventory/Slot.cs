using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ���ӵ����
/// </summary>
public enum SlotType { FishItem, ToolItem, PropItem, Equipment, Sale, Buy }

/// <summary>
/// ����ÿһ��slot����
/// </summary>
public class Slot : MonoBehaviour
{
    /// <summary>
    /// ÿ�����ӵ�GameObject
    /// </summary>
    public GameObject slotObj;
    /// <summary>
    /// ÿ��Icon��Image���
    /// </summary>
    public Image icon;
    /// <summary>
    /// ÿ�����Ӵ�ȡ����Ŀ��Text���
    /// </summary>
    public Text itemCountText;
    /// <summary>
    /// ÿһ�����ӵ���ţ����ⲿ��ֵ
    /// </summary>
    public int Index;
    /// <summary>
    /// ���Ӷ�Ӧ�����ݿ⣨ͨ��slotType��Ӧ��
    /// </summary>
    public InventoryData inventory_Database;
    /// <summary>
    /// ÿһ�ָ��ӵ����
    /// </summary>
    [SerializeField]
    public SlotType slotType;
    /// <summary>
    /// ��slot�����
    /// </summary>
    public SlotType interiorSlotType;
    /// <summary>
    /// ��ȡ����ק�ű�
    /// </summary>
    public DragItem dragItemScript;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void InitEverySlot()
    {
        //�ҵ�slot�µ����
        if (slotObj != null)
        {
            icon = SetGameObjectToParent.FindChildBreadthFirst(slotObj.transform, "Icon").GetComponent<Image>();

            if(icon != null)
            {
                //�ű��������Ƚ�gameObject��spriteȫ���ر�
                icon.gameObject.SetActive(false);
                //��ȡ��������ʾText
                itemCountText = SetGameObjectToParent.FindChildBreadthFirst(icon.gameObject.transform, "ItemCount").GetComponent<Text>();
                //�ȹر�������ʾText
                itemCountText.gameObject.SetActive(false);
            }

            //ֻ��buy���Ӳ���Ҫ��ק�ű�
            if(slotType != SlotType.Buy)
            {
                //��ȡ����ק�ű�
                dragItemScript = ComponentFinder.GetOrAddComponent<DragItem>(icon.gameObject);
            }
        }
        else
        {
            Debug.LogError("û���ҵ�iconObj��������룡");
        }
    }

    /// <summary>
    /// ���¸���UI�ķ���
    /// </summary>
    public void UpdateUI()
    {
        //��ͨ��Slot��slotTypeѰ�Ҷ�Ӧƥ��Ŀ������
        switch(slotType)
        {
            case SlotType.FishItem:
                inventory_Database = InventoryManager.Instance.backpackManager;
                break;
            case SlotType.ToolItem:
                inventory_Database = InventoryManager.Instance.toolpackManager;
                break;
            case SlotType.PropItem:
                inventory_Database = InventoryManager.Instance.proppackManager;
                break;
            case SlotType.Equipment:
                inventory_Database = InventoryManager.Instance.equipmentManager;
                break;
            case SlotType.Sale:
                inventory_Database = InventoryManager.Instance.saleManager;
                break;
            case SlotType.Buy:
                inventory_Database = InventoryManager.Instance.buyManager;
                break;
        }
        var item = inventory_Database.list[Index];

        //���и���
        UpdateSlot(item);
    }

    /// <summary>
    /// ͨ����������ItemData�����ж�����������Ʒ���ͣ�Ȼ����и���Icon(��Ҫ����Ϊitem��ItemData���͵ģ��޷�ֱ��ת���ɶ�Ӧ��Ʒ����)
    /// </summary>
    /// <param name="items"></param>
    public void UpdateSlot(ItemData item)
    {
        //���item����FishItem��
        if (item is FishItem fishItem)
        {
            UpdateIconAndAmountUI<FishItem>(fishItem);
        }
        //���item����ToolItem��
        else if (item is ToolItem toolItem)
        {
            UpdateIconAndAmountUI<ToolItem>(toolItem);
        }
        //���item����FishItem��
        else if (item is PropItem propItem)
        {
            UpdateIconAndAmountUI<PropItem>(propItem);
        }
        else if (item is BaitItem baitItem)
        {
            UpdateIconAndAmountUI<BaitItem>(baitItem);
        }
        //������item������Ϊ�յĻ���ֱ�ӽ���������µ�icon�ر�
        else
        {
            icon.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// ���ݴ����������ݸ���Icon������UI
    /// </summary>
    /// <typeparam name="T">�̳���ItemData��Item��</typeparam>
    /// <param name="item">��������ItemData</param>
    public void UpdateIconAndAmountUI<T>(ItemData item) where T : ItemData
    {
        if (item is T typeItem)
        {
            //��ȡͼƬ
            icon.sprite = typeItem.type.GetImage();
            icon.gameObject.SetActive(true);
            //�����������item����Ʒ��������1
            if (item.amount > 1)
            {
                itemCountText.text = item.amount.ToString();
                itemCountText.gameObject.SetActive(true);
            }
            //�����Ʒ��������1�͹ر�������ʾ
            else if (item.amount == 1)
            {
                itemCountText.text = item.amount.ToString();
                itemCountText.gameObject.SetActive(false);
            }
            //�����Ʒ����0�Ļ�
            else
            {
                itemCountText.text = item.amount.ToString();
                //�ر�����UI
                itemCountText.gameObject.SetActive(false);
                //�ر�ͼƬ��ʾ
                icon.gameObject.SetActive(false);
            }
        }
    }
}
