using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// �̵���ӵ��������̳���SlotContainer
/// </summary>
public class BuySlotContainer : SlotContainer
{
    /// <summary>
    /// �����������Ҫ��Ǯ�ļ���ϵ��(Ĭ��Ϊ150f)
    /// </summary>
    public float toolQuality_coefficient = 150f;

    /// <summary>
    /// ���������ļ���ϵ����Ĭ��Ϊ60f��
    /// </summary>
    public float propQuality_coefficient = 60f;

    /// <summary>
    /// ����ˢ����
    /// </summary>
    public RefreshProduct refreshProduct;

    /// <summary>
    /// �����ܿ�����
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// ��д�����еĳ�ʼ������
    /// </summary>
    public override void InitializedContainer()
    {
        //��ִ�и����е�slot����
        base.InitializedContainer();
        //ˢ����Ʒ��ʼ��
        refreshProduct = SetGameObjectToParent.FindFromFirstLayer("StoreCanvas").GetComponent<RefreshProduct>();
        
    }

    public void Start()
    {
        //ˢ����Ʒ
        RefreshProduct();
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
    /// ���¸����е�ˢ��UI�ķ���
    /// </summary>
    public override void RefreshSlotUI()
    {
        base.RefreshSlotUI();
    }

    /// <summary>
    /// ˢ����Ʒ�ķ���
    /// </summary>
    public void RefreshProduct()
    {
        //��������ֵ��е�һ������
        int randomNumber1 = Random.Range(0, ItemManager.Instance.GetDictionary<ToolType>().Count - 1);
        //ʹ��ElementAt������ʼ�ֵ��
        var element1 = ItemManager.Instance.GetDictionary<ToolType>().ElementAt(randomNumber1);
        //�������һ������Ʒ������
        int randomNumber1_2 = Random.Range(0, 4);
        //����Inventory
        InventoryManager.Instance.buyManager.AddItemInListFromIndex(new ToolItem(element1.Key, (ToolQuality)randomNumber1_2), 0);
        //�ı���Ʒ�۸�UI����
        totalController.storeDataAndUIController.ChangePrice_tool(CalculatItemPrice.CulationTool(new ToolItem(element1.Key, (ToolQuality)randomNumber1_2), toolQuality_coefficient));

        //��������ֵ��е�һ������
        int randomNumber2 = Random.Range(0, ItemManager.Instance.GetDictionary<PropType>().Count - 1);
        //ʹ��ElementAt������ʼ�ֵ��
        var element2 = ItemManager.Instance.GetDictionary<PropType>().ElementAt(randomNumber2);
        //�������һ��������Ʒ������
        int randomNumber2_2 = Random.Range(0, 4);
        //����Inventory
        InventoryManager.Instance.buyManager.AddItemInListFromIndex(new PropItem(element2.Key, (PropQuality)randomNumber2_2), 1);
        //�ı��̵���Ʒ�۸�UI����
        totalController.storeDataAndUIController.ChangePrice_prop1(CalculatItemPrice.CulationProp(new PropItem(element2.Key, (PropQuality)randomNumber2_2), toolQuality_coefficient));

        //��������ֵ��е�һ������
        int randomNumber3 = Random.Range(0, ItemManager.Instance.GetDictionary<PropType>().Count - 1);
        //ʹ��ElementAt������ʼ�ֵ��
        var element3 = ItemManager.Instance.GetDictionary<PropType>().ElementAt(randomNumber3);
        //�������һ������Ʒ������
        int randomNumber3_2 = Random.Range(0, 4);
        //����Inventory
        InventoryManager.Instance.buyManager.AddItemInListFromIndex(new PropItem(element3.Key, (PropQuality)randomNumber3_2), 2);
        //�ı��̵���Ʒ�۸�UI����
        totalController.storeDataAndUIController.ChangePrice_prop2(CalculatItemPrice.CulationProp(new PropItem(element3.Key, (PropQuality)randomNumber3_2), toolQuality_coefficient));

        //ˢ��UI
        RefreshSlotUI();
        //�ٴ�������ʱ
        refreshProduct.timer.StartTimer(refreshProduct.refreshProductTime);
    }
}
