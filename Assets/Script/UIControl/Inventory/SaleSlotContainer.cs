using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// ���������Container���̳���SlotContainer
/// </summary>
public class SaleSlotContainer : SlotContainer
{
    /// <summary>
    /// �����ܿ�����
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// ���þ��ù�����
    /// </summary>
    public EconomyManager economyManager;

    /// <summary>
    /// ��дSlotContainer�е�InitializedContainer()������Ϊ��ǰ����ʼ���ܿ�����
    /// </summary>
    public override void InitializedContainer()
    {
        base.InitializedContainer();

        //���ù���ĳ�ʼ��
        economyManager = SetGameObjectToParent.FindFromFirstLayer("EconomyManager").GetComponent<EconomyManager>();
    }

    /// <summary>
    /// ע���ܿ�����
    /// </summary>
    public void RegisterTotalController()
    {
        //�ܿ�������ʼ��
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
    }

    /// <summary>
    /// ��������дˢ��װ�����ӵķ���                                               
    /// </summary>
    public override void RefreshSlotUI()
    {
        //�ȸ�PlayerUI��sallingCoin����
        totalController.playerDataAndUIController.ChangeSallingCoinToZero();

        //���¼���saleSlot�е���Ʒ��ֵ
        for (int i = 0; i < slotGameObjects.Length; i++)
        {
            //����UI
            slots[i].UpdateUI();
            
            //�����ֵ
            CheckSaleSlotAndCalCulation(slots[i], i);
        }
    }

    /// <summary>
    /// ����������Ӳ��Ҽ����Ǯ�ķ���
    /// </summary>
    /// <param name="slot">������Ҫ���ĸ���</param>
    /// <param name="index">������Ҫ���ĸ��ӵ����</param>
    public void CheckSaleSlotAndCalCulation(Slot slot, int index)
    {
        if(slot.inventory_Database.list[index].itemIdentifier.Type != "defaultType")
        {
            //�����������slot�е�������FishItem,�ͽ��ø��ӵ�����ת���ɶ�Ӧ���ͣ�����ͬ��
            if (slot.inventory_Database.list[index] is FishItem fishItem)
            {
                //ת����������
                slot.interiorSlotType = SlotType.FishItem;
                //����������Ǯ�������ܿ������еĸ���PlayerUI�ķ���,����Ԥ��ʾ�Ľ������
                totalController.playerDataAndUIController.ChangeSallingCoin(economyManager.ReturnSaleWithCoefficient(fishItem));
            }
            //������͵ļ�Ǯ
            else if (slot.inventory_Database.list[index] is ToolItem toolItem)
            {
                //ת����������
                slot.interiorSlotType = SlotType.ToolItem;

                totalController.playerDataAndUIController.ChangeSallingCoin(economyManager.ReturnSaleWithCoefficient(toolItem));
            }
            //���������ļ�Ǯ
            else if (slot.inventory_Database.list[index] is PropItem propItem)
            {
                //ת����������
                slot.interiorSlotType = SlotType.PropItem;
                //�����ܿ������еĸ���PlayerUI�ķ���,����Ԥ��ʾ�Ľ������
                totalController.playerDataAndUIController.ChangeSallingCoin(economyManager.ReturnSaleWithCoefficient(propItem));
            }
            //��������ļ�Ǯ
            else if (slot.inventory_Database.list[index] is BaitItem baitItem)
            {
                //ת����������
                slot.interiorSlotType = SlotType.PropItem;
                //�����ܿ������еĸ���PlayerUI�ķ���,����Ԥ��ʾ�Ľ������
                totalController.playerDataAndUIController.ChangeSallingCoin(economyManager.ReturnSaleWithCoefficient(baitItem));
            }
            //���ʲô�����ǵĻ�
            else
            {
                Debug.LogError("���������������һ��δ֪��Ʒ����");
            }
        }
        
        //����Ʒ��ʶ��ΪĬ����Ʒ��ʱ��Ҫ�Ѹ���ת������
        else
        {
            //ת����������,ʹ��������ܹ����κ���Ʒ
            slot.interiorSlotType = SlotType.Sale;
        }
    }
}
