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
    /// ÿ�����ӵ�Icon��GameObject
    /// </summary>
    public GameObject iconObj;
    /// <summary>
    /// ÿ��Icon��Image���
    /// </summary>
    public Image icon;
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
    /// �ű��������
    /// </summary>
    private void Awake()
    {
        //�ҵ�slot�µ�Icon�������ҵ���Image���
        iconObj = gameObject.transform.Find("Icon").gameObject;
        if (iconObj != null)
        {
            if (iconObj.GetComponent<Image>())
            {
                //��ȡ��icon��Image���
                icon = iconObj.GetComponent<Image>();
            }
            else
            {
                Debug.LogError("û���ҵ�iconObj�µ�Image�����������룡");
            }
            //�ű��������Ƚ�gameObject��spriteȫ���ر�
            iconObj.SetActive(false);

            //ֻ��buy���Ӳ���Ҫ��ק�ű�
            if(slotType != SlotType.Buy)
            {
                //��ȡ����ק�ű�
                dragItemScript = iconObj.GetComponent<DragItem>();
                if (dragItemScript == null)
                {
                    Debug.LogError("dragItemScript�ǿյģ�����������Hierarchy����");
                }
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
        UpdateIcon(item);
    }

    /// <summary>
    /// ͨ����������ItemData�����ж�����������Ʒ���ͣ�Ȼ����и���Icon(��Ҫ����Ϊitem��ItemData���͵ģ��޷�ֱ��ת���ɶ�Ӧ��Ʒ����)
    /// </summary>
    /// <param name="items"></param>
    public void UpdateIcon(ItemData item)
    {
        //���item����FishItem��
        if (item is FishItem fishItem && item.itemIdentifier.Type == "FishItem")
        {
            //����ͼƬ
            icon.sprite = fishItem.Type.GetFishImage();
            //����Icon��GameObject
            iconObj.gameObject.SetActive(true);
        }
        //���item����ToolItem��
        else if (item is ToolItem toolItem && item.itemIdentifier.Type == "ToolItem")
        {
            //����ͼƬ
            icon.sprite = toolItem.Type.GetToolImage();
            //����Icon��GameObject
            iconObj.gameObject.SetActive(true);
        }
        //���item����FishItem��
        else if(item is PropItem propItem && item.itemIdentifier.Type == "PropItem")
        {
            //����ͼƬ
            icon.sprite = propItem.Type.GetPropImage();
            //����Icon��GameObject
            iconObj.gameObject.SetActive(true);
        }
        //������item������Ϊ�յĻ���ֱ�ӽ���������µ�icon�ر�
        else
        {
            iconObj.gameObject.SetActive(false);
        }
    }
}
