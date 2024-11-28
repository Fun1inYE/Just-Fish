using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    /// <summary>
    /// ����ť
    /// </summary>
    public Button buyButton;

    /// <summary>
    /// �����ܿ�����
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    public void Awake()
    {
        buyButton = GetComponent<Button>();
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
    }

    /// <summary>
    /// ��ť�󶨼����¼�
    /// </summary>
    public void Start()
    {
        buyButton.onClick.AddListener(BuyItem);
    }

    /// <summary>
    /// ������Ʒ�ķ���
    /// </summary>
    public void BuyItem()
    {
        //�������ַ���ת����int��
        int productPrice = int.Parse(transform.parent.GetComponent<Text>().text);

        //�ж���ҵ�Ǯ�Ƿ���ڵ�ǰҪ�������Ʒ
        if (totalController.playerDataAndUIController.playerData.coin >= productPrice)
        {
            //��ȡ��itemData
            ItemData itemData = transform.parent.parent.GetComponent<Slot>().inventory_Database.list[transform.parent.parent.GetComponent<Slot>().Index];
            //������Ʒ���ݲ�Ϊ�յĻ����Խ��й���
            if (itemData.itemIdentifier.Type != "defaultType")
            {
                //���Ź�������
                AudioManager.Instance.PlayAudio("ShortCoinDrop");
                //����ҽ�����ݽ��ж�Ӧ�ļ���
                totalController.playerDataAndUIController.ChangeCoin(-productPrice);
                //ʶ�������Ʒ���ҷ����Ӧ����
                RecognizeItemAndPutInInventory(itemData);
            }
            else
            {
                //TODO����Ʒ���������ʾ
                Debug.Log("***��Ʒ������");
            }
            //����ȥ��ť��Ӧ��slot���
            InventoryManager.Instance.buyManager.DeleteItemInListFromIndex(transform.parent.parent.GetComponent<Slot>().Index);
            //ˢ��buySlotUI
            InventoryManager.Instance.buySlotContainer.RefreshSlotUI();
        }
        else
        {
            //TODO:����Ǯ������UI
            Debug.Log("������ֽ�Ҳ�����");
        }
        
    }

    /// <summary>
    /// ʶ��Item�����Զ������Ӧ����
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public void RecognizeItemAndPutInInventory(ItemData itemData)
    {
        if(itemData is ToolItem toolItem)
        {
            InventoryManager.Instance.toolpackManager.AddItemInList(toolItem);
            InventoryManager.Instance.toolpackSlotContainer.RefreshSlotUI();
        }
        if (itemData is PropItem propItem)
        {
            InventoryManager.Instance.proppackManager.AddItemInList(propItem);
        }

    }
}
