using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �����������
/// </summary>
public class InventoryManager : MonoBehaviour
{
    /// <summary>
    /// ����������
    /// </summary>
    public InventoryData backpackManager;
    /// <summary>
    /// ���߹�����
    /// </summary>
    public InventoryData toolpackManager;
    /// <summary>
    /// ���߹�����
    /// </summary>
    public InventoryData proppackManager;
    /// <summary>
    /// װ��������
    /// </summary>
    public InventoryData equipmentManager;
    /// <summary>
    /// ����������
    /// </summary>
    public InventoryData saleManager;
    /// <summary>
    /// ���������
    /// </summary>
    public InventoryData buyManager;
    /// <summary>
    /// ������UI����
    /// </summary>
    public SlotContainer backpackSlotContainer;
    /// <summary>
    /// ���ߵ�UI����
    /// </summary>
    public SlotContainer toolpackSlotContainer;
    /// <summary>
    /// ���ߵ�UI����
    /// </summary>
    public SlotContainer proppackSlotContainer;
    /// <summary>
    /// װ����UI����
    /// </summary>
    public EquipmentSlotContainer equipmentSlotContainer;
    /// <summary>
    /// ����UI����
    /// </summary>
    public SaleSlotContainer saleSlotContainer;
    /// <summary>
    /// ����UI����
    /// </summary>
    public BuySlotContainer buySlotContainer;

    /// <summary>
    /// �Ƿ���Ը�װ����������Ʒ����(Ĭ��true)
    /// </summary>
    public bool canExchangeItemWithEquipmentSlotContainer = true;

    /// <summary>
    /// ���ñ����洢����
    /// </summary>
    public InventoryItemData inventoryItemData;

    /// <summary>
    /// ���������ĵ���
    /// </summary>
    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        //������ʼ��
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        //���ֿ������ʼ��,��ʼ������
        backpackManager = new InventoryData();
        InitializedContent(backpackManager, 20);
        toolpackManager = new InventoryData();
        InitializedContent(toolpackManager, 20);
        proppackManager = new InventoryData();
        InitializedContent(proppackManager, 20);
        equipmentManager = new InventoryData();
        InitializedContent(equipmentManager, 3);
        saleManager = new InventoryData();
        InitializedContent(saleManager, 3);
        buyManager = new InventoryData();
        InitializedContent(buyManager, 3);


        //��ȡ����ͬ�ı�����UI,ͬʱ��ʼ������
        backpackSlotContainer = ComponentFinder.GetChildComponent<SlotContainer>(gameObject, "Content");
        backpackSlotContainer.InitializedContainer();
        toolpackSlotContainer = ComponentFinder.GetChildComponent<SlotContainer>(gameObject, "ToolItemListPanel");
        toolpackSlotContainer.InitializedContainer();
        proppackSlotContainer = ComponentFinder.GetChildComponent<SlotContainer>(gameObject, "PropItemListPanel");
        proppackSlotContainer.InitializedContainer();
        equipmentSlotContainer = ComponentFinder.GetChildComponent<EquipmentSlotContainer>(gameObject, "EquipmentContent");
        equipmentSlotContainer.InitializedContainer();
        saleSlotContainer = ComponentFinder.GetChildComponent<SaleSlotContainer>(gameObject, "SaleContent");
        saleSlotContainer.InitializedContainer();
        buySlotContainer = ComponentFinder.GetChildComponent<BuySlotContainer>(SetGameObjectToParent.FindFromFirstLayer("StoreCanvas"), "StoreContent");
        buySlotContainer.InitializedContainer();

        //��ʼ�������洢����
        inventoryItemData = new InventoryItemData();
    }

    /// <summary>
    /// ��ʼ������Manager�е�����
    /// </summary>
    /// <param name="manager">ָ��������</param>
    /// <param name="content">Ҫ����������</param>
    public void InitializedContent(InventoryData manager, int content)
    {
        //�Ƚ������е�list����������ܽ��в���
        for(int i = 0; i < content; i++)
        {
            manager.list.Add(new ItemData(new BaseType()));
            //�����еı�ʶ����Ҳ����ʼ��
            manager.list[i].itemIdentifier = new ItemIdentifier();
        }
    }

    /// <summary>
    /// ��Ϊ��һ���ֵ�����������TotalController�����ˣ��������ǳ�ʼ�������Ĵ���
    /// </summary>
    public void RegisterContainersTotalController()
    {
        equipmentSlotContainer.RegisterTotalController();
        saleSlotContainer.RegisterTotalController();
        buySlotContainer.RegisterTotalController();
    }

    private void Update()
    {
        //TODO: ���԰���
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    InventoryLevelControl.LevelUp();  
        //    //���ø���
        //    slotContainer.UpdateSlot();
        //}
        //if(Input.GetKeyDown(KeyCode.O))
        //{
        //    InventoryLevelControl.LevelDown();
        //    slotContainer.UpdateSlot();
        //}
        if (Input.GetKeyDown(KeyCode.O))
        {
            toolpackManager.CheckInventoryListData();
        }
    }

    /// <summary>
    /// ˢ������container�ķ���
    /// </summary>
    public void RefreshAllContainer()
    {
        backpackSlotContainer.RefreshSlotUI();
        toolpackSlotContainer.RefreshSlotUI();
        proppackSlotContainer.RefreshSlotUI();
        equipmentSlotContainer.RefreshSlotUI();
        saleSlotContainer.RefreshSlotUI();
        buySlotContainer.RefreshSlotUI();
    }

    /// <summary>
    /// �洢����
    /// </summary>
    public void SaveData()
    {
        if(inventoryItemData != null)
        {
            //��ձ�ʶ������
            inventoryItemData.backpackItemIdentifier.Clear();
            inventoryItemData.toolpackItemIdentifier.Clear();
            inventoryItemData.proppackItemIdentifier.Clear();
            inventoryItemData.equipmentItemIdentifier.Clear();
            inventoryItemData.storepackItemIdentifier.Clear();
            inventoryItemData.salepackItemIdentifier.Clear();

            //�洢������ʶ������
            for (int i = 0; i < backpackManager.list.Count; i++)
                inventoryItemData.backpackItemIdentifier.Add(backpackManager.list[i].itemIdentifier);
            for (int i = 0; i < toolpackManager.list.Count; i++)
                inventoryItemData.toolpackItemIdentifier.Add(toolpackManager.list[i].itemIdentifier);
            for (int i = 0; i < proppackManager.list.Count; i++)
                inventoryItemData.proppackItemIdentifier.Add(proppackManager.list[i].itemIdentifier);
            for (int i = 0; i < equipmentManager.list.Count; i++)
                inventoryItemData.equipmentItemIdentifier.Add(equipmentManager.list[i].itemIdentifier);
            for (int i = 0; i < buyManager.list.Count; i++)
                inventoryItemData.storepackItemIdentifier.Add(buyManager.list[i].itemIdentifier);
            for (int i = 0; i < saleManager.list.Count; i++)
                inventoryItemData.salepackItemIdentifier.Add(saleManager.list[i].itemIdentifier);

            //�洢�����ر�ʶ������
            SaveManager.Instance.Save(GameManager.Instance.gameData.archiveSaveName, "backpackItemIdentifier.sav", inventoryItemData.backpackItemIdentifier);
            SaveManager.Instance.Save(GameManager.Instance.gameData.archiveSaveName, "toolpackItemIdentifier.sav", inventoryItemData.toolpackItemIdentifier);
            SaveManager.Instance.Save(GameManager.Instance.gameData.archiveSaveName, "proppackItemIdentifier.sav", inventoryItemData.proppackItemIdentifier);
            SaveManager.Instance.Save(GameManager.Instance.gameData.archiveSaveName, "equipmentItemIdentifier.sav", inventoryItemData.equipmentItemIdentifier);
            SaveManager.Instance.Save(GameManager.Instance.gameData.archiveSaveName, "storepackItemIdentifier.sav", inventoryItemData.storepackItemIdentifier);
            SaveManager.Instance.Save(GameManager.Instance.gameData.archiveSaveName, "salepackItemIdentifier.sav", inventoryItemData.salepackItemIdentifier);

        }
    }

    /// <summary>
    /// ��ȡ����
    /// </summary>
    public void LoadData()
    {
        if(inventoryItemData != null)
        {
            //��ȡ����
            inventoryItemData.backpackItemIdentifier = SaveManager.Instance.Load<List<ItemIdentifier>>(GameManager.Instance.gameData.archiveSaveName, "backpackItemIdentifier.sav");
            inventoryItemData.toolpackItemIdentifier = SaveManager.Instance.Load<List<ItemIdentifier>>(GameManager.Instance.gameData.archiveSaveName, "toolpackItemIdentifier.sav");
            inventoryItemData.proppackItemIdentifier = SaveManager.Instance.Load<List<ItemIdentifier>>(GameManager.Instance.gameData.archiveSaveName, "proppackItemIdentifier.sav");
            inventoryItemData.equipmentItemIdentifier = SaveManager.Instance.Load<List<ItemIdentifier>>(GameManager.Instance.gameData.archiveSaveName, "equipmentItemIdentifier.sav");
            inventoryItemData.storepackItemIdentifier = SaveManager.Instance.Load<List<ItemIdentifier>>(GameManager.Instance.gameData.archiveSaveName, "storepackItemIdentifier.sav");
            inventoryItemData.salepackItemIdentifier = SaveManager.Instance.Load<List<ItemIdentifier>>(GameManager.Instance.gameData.archiveSaveName, "salepackItemIdentifier.sav");


            //ÿ��������д���
            for (int i = 0; i < inventoryItemData.backpackItemIdentifier.Count; i++)
                backpackManager.AddItemInList(IdentifierHelper(inventoryItemData.backpackItemIdentifier[i]));
            for (int i = 0; i < inventoryItemData.toolpackItemIdentifier.Count; i++)
                toolpackManager.AddItemInList(IdentifierHelper(inventoryItemData.toolpackItemIdentifier[i]));
            for (int i = 0; i < inventoryItemData.proppackItemIdentifier.Count; i++)
                proppackManager.AddItemInList(IdentifierHelper(inventoryItemData.proppackItemIdentifier[i]));
            for (int i = 0; i < inventoryItemData.equipmentItemIdentifier.Count; i++)
                equipmentManager.AddItemInList(IdentifierHelper(inventoryItemData.equipmentItemIdentifier[i]));
            for (int i = 0; i < inventoryItemData.storepackItemIdentifier.Count; i++)
                buyManager.AddItemInList(IdentifierHelper(inventoryItemData.storepackItemIdentifier[i]));
            for (int i = 0; i < inventoryItemData.salepackItemIdentifier.Count; i++)
                saleManager.AddItemInList(IdentifierHelper(inventoryItemData.salepackItemIdentifier[i]));
        }
    }

    //TODO:Ҫ�����ƶ�����Ӧλ��
    /// <summary>
    /// ��ʶ��������
    /// </summary>
    /// <param name="identifier"></param>
    public ItemData IdentifierHelper(ItemIdentifier identifier)
    {
        //ͨ����ʶ���е�Typeʶ������ʲô��Ʒ
        switch (identifier.Type)
        {
            case "FishItem":
                return new FishItem(new FishType(ItemManager.Instance.GetGameObjectFromName(identifier.Name)), identifier.FishLength, identifier.FishWeight);
            case "ToolItem":
                return new ToolItem(new ToolType(ItemManager.Instance.GetGameObjectFromName(identifier.Name)), identifier.ToolQualityIditenfier);
            case "PropItem":
                return new PropItem(new PropType(ItemManager.Instance.GetGameObjectFromName(identifier.Name)), identifier.PropQualityIditenfier);
            case "BaitItem":
                return new BaitItem(new BaitType(ItemManager.Instance.GetGameObjectFromName(identifier.Name)), identifier.maxAmountIditenfier, identifier.amountIditenfier);
            default:
                //Ĭ�Ϸ���һ��ItemData
                return new ItemData(new BaseType());
        }

    }

    
}