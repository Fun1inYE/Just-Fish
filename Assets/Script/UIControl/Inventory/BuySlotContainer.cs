using System.Linq;
using UnityEngine;

/// <summary>
/// �̵���ӵ��������̳���SlotContainer
/// </summary>
public class BuySlotContainer : SlotContainer
{
    /// <summary>
    /// ����ˢ����Ʒ��
    /// </summary>
    public RefreshProduct refreshProduct;

    /// <summary>
    /// �����ܿ�����
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// ���þ��ù�����
    /// </summary>
    public EconomyManager economyManager;

    /// <summary>
    /// ��д�����еĳ�ʼ������
    /// </summary>
    public override void InitializedContainer()
    {
        //��ִ�и����е�slot����
        base.InitializedContainer();
        //ˢ����Ʒ��ʼ��
        refreshProduct = SetGameObjectToParent.FindFromFirstLayer("StoreCanvas").GetComponent<RefreshProduct>();
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

        //�ܿ�������ʼ�����֮���������ˢ���̵�
        RefreshProduct();
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
        RefreshStoreSlot_0();
        RefreshStoreSlot_1();
        RefreshStoreSlot_2();

        //ˢ��UI
        RefreshSlotUI();
        //�ٴ�������ʱ
        refreshProduct.timer.StartTimer(refreshProduct.refreshProductTime);
    }

    /// <summary>
    /// ˢ���̵��һ��λ
    /// </summary>
    public void RefreshStoreSlot_0()
    {
        ItemData item = GenerateAndTransferStoreSlotFromIndex(0);
        totalController.storeDataAndUIController.ChangePrice_Slot0(economyManager.ReturnBuyWithCoefficient(item));
    }

    /// <summary>
    /// ˢ���̵�Ķ���λ
    /// </summary>
    /// <param name="type"></param>
    public void RefreshStoreSlot_1()
    {
        ItemData item = GenerateAndTransferStoreSlotFromIndex(1);
        totalController.storeDataAndUIController.ChangePrice_Slot1(economyManager.ReturnBuyWithCoefficient(item));
    }

    /// <summary>
    /// ˢ���̵������λ
    /// </summary>
    /// <param name="type"></param>
    public void RefreshStoreSlot_2()
    {
        ItemData item = GenerateAndTransferStoreSlotFromIndex(2);
        totalController.storeDataAndUIController.ChangePrice_Slot2(economyManager.ReturnBuyWithCoefficient(item));
    }

    /// <summary>
    /// ������Ʒ���ҽ���ŵ���Ӧindex��ŵ��̵������,���ҷ��ض�Ӧ��ItemData
    /// </summary>
    /// <param name="index"></param>
    private ItemData GenerateAndTransferStoreSlotFromIndex(int index)
    {
        //�������ȡ��һ����Ʒ���ͣ��ȴ�����װ����Ʒ
        BaseType baseType = ItemManager.Instance.GetRandomItemFromRandomDictionary();

        //���baseType����ΪFishType
        if (baseType is FishType fishType)
        {
            double weight = Random.Range(1f, 30f);
            double length = Random.Range(1f, 30f);
            //��type��װ��item
            FishItem fishItem = new FishItem(fishType, length, weight);
            //���ɲ��Ҵ����̵������
            InventoryManager.Instance.buyManager.AddItemInListFromIndex(fishItem, index);

            return fishItem;
        }

        //���baseType����ΪToolType
        if (baseType is ToolType toolType)
        {
            //�����ȡ����͵�����
            int randomNumber = Random.Range(0, System.Enum.GetValues(typeof(ToolQuality)).Length);

            ToolItem toolItem = new ToolItem(toolType, (ToolQuality)randomNumber);

            InventoryManager.Instance.buyManager.AddItemInListFromIndex(toolItem, index);

            return toolItem;
        }

        //���baseType����ΪPropType
        if (baseType is PropType propType)
        {
            //�����ȡ������������
            int randomNumber = Random.Range(0, System.Enum.GetValues(typeof(PropQuality)).Length);

            PropItem propItem = new PropItem(propType, (PropQuality)randomNumber);

            InventoryManager.Instance.buyManager.AddItemInListFromIndex(propItem, index);

            return propItem;
        }

        //���baseType����ΪBaitType
        if (baseType is BaitType baitType)
        {
            //���������������� 20 ~ 50��
            int randomNumber = Random.Range(20, 51);

            BaitItem baitItem = new BaitItem(baitType, 99, randomNumber);

            InventoryManager.Instance.buyManager.AddItemInListFromIndex(baitItem, index);

            return baitItem;
        }

        Debug.LogError($"û�м�⵽��Ӧ��{baseType.GetType()}���������");
        return null;
    }
}
