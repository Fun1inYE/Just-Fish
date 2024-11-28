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
    /// ���ȼ���ϵ����Ĭ��Ϊ1f��
    /// </summary>
    public float length_coefficient = 1f;
    /// <summary>
    /// ��������ϵ����Ĭ��Ϊ3f��
    /// </summary>
    public float weight_coefficient = 3f;

    /// <summary>
    /// ���������ļ���ϵ����Ĭ��Ϊ100f��
    /// </summary>
    public float toolQuality_coefficient = 100f;
    /// <summary>
    /// ���������ļ���ϵ����Ĭ��Ϊ100f��
    /// </summary>
    public float propQuality_coefficient = 50f;
    /// <summary>
    /// ��ͼ���ϵ��
    /// </summary>
    public float tool_coefficient = 10f;
    /// <summary>
    /// �����ļ���ϵ��
    /// </summary>
    public float prop_coefficient = 5f;

    /// <summary>
    /// �����ܿ�����
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// ��дSlotContainer�е�InitializedContainer()������Ϊ��ǰ����ʼ���ܿ�����
    /// </summary>
    public override void InitializedContainer()
    {
        base.InitializedContainer();
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
                //�����Ǯ
                int result = CalculatItemPrice.CulationFish(fishItem, length_coefficient, weight_coefficient);
                //�����ܿ������еĸ���PlayerUI�ķ���,����Ԥ��ʾ�Ľ������
                totalController.playerDataAndUIController.ChangeSallingCoin(result);
            }
            //������͵ļ�Ǯ
            else if (slot.inventory_Database.list[index] is ToolItem toolItem)
            {
                //ת����������
                slot.interiorSlotType = SlotType.ToolItem;
                //�����Ǯ
                int result = CalculatItemPrice.CulationTool(toolItem, toolQuality_coefficient);
                //�����ܿ������еĸ���PlayerUI�ķ���,����Ԥ��ʾ�Ľ������
                totalController.playerDataAndUIController.ChangeSallingCoin(result);
            }
            //���������ļ�Ǯ
            else if (slot.inventory_Database.list[index] is PropItem propItem)
            {
                //ת����������
                slot.interiorSlotType = SlotType.PropItem;
                //�����Ǯ
                int result = CalculatItemPrice.CulationProp(propItem, propQuality_coefficient);
                //�����ܿ������еĸ���PlayerUI�ķ���,����Ԥ��ʾ�Ľ������
                totalController.playerDataAndUIController.ChangeSallingCoin(result);
            }
            //���ʲô�����ǵĻ�
            else
            {
                Debug.Log("ִ�е���仰��");
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
