using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 库存管理的总类
/// </summary>
public class InventoryManager : MonoBehaviour
{
    /// <summary>
    /// 背包管理器
    /// </summary>
    public InventoryData backpackManager;
    /// <summary>
    /// 工具管理器
    /// </summary>
    public InventoryData toolpackManager;
    /// <summary>
    /// 道具管理器
    /// </summary>
    public InventoryData proppackManager;
    /// <summary>
    /// 装备管理器
    /// </summary>
    public InventoryData equipmentManager;
    /// <summary>
    /// 卖出管理器
    /// </summary>
    public InventoryData saleManager;
    /// <summary>
    /// 买入管理器
    /// </summary>
    public InventoryData buyManager;
    /// <summary>
    /// 背包的UI控制
    /// </summary>
    public SlotContainer backpackSlotContainer;
    /// <summary>
    /// 工具的UI控制
    /// </summary>
    public SlotContainer toolpackSlotContainer;
    /// <summary>
    /// 道具的UI控制
    /// </summary>
    public SlotContainer proppackSlotContainer;
    /// <summary>
    /// 装备的UI控制
    /// </summary>
    public EquipmentSlotContainer equipmentSlotContainer;
    /// <summary>
    /// 卖出UI控制
    /// </summary>
    public SaleSlotContainer saleSlotContainer;
    /// <summary>
    /// 买入UI控制
    /// </summary>
    public BuySlotContainer buySlotContainer;

    /// <summary>
    /// 是否可以跟装备栏交换物品数据(默认true)
    /// </summary>
    public bool canExchangeItemWithEquipmentSlotContainer = true;

    /// <summary>
    /// 引用背包存储数据
    /// </summary>
    public InventoryItemData inventoryItemData;

    /// <summary>
    /// 库存管理器的单例
    /// </summary>
    public static InventoryManager Instance { get; private set; }

    private void Awake()
    {
        //单例初始化
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;

        //各种库存管理初始化,初始化容量
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


        //获取到不同的背包栏UI,同时初始化背包
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

        //初始化背包存储数据
        inventoryItemData = new InventoryItemData();
    }

    /// <summary>
    /// 初始化各种Manager中的容量
    /// </summary>
    /// <param name="manager">指定管理器</param>
    /// <param name="content">要扩多大的容量</param>
    public void InitializedContent(InventoryData manager, int content)
    {
        //先将背包中的list填充起来才能进行操作
        for(int i = 0; i < content; i++)
        {
            manager.list.Add(new ItemData(new BaseType()));
            //给其中的标识符号也做初始化
            manager.list[i].itemIdentifier = new ItemIdentifier();
        }
    }

    /// <summary>
    /// 因为有一部分的容器格子与TotalController关联了，所以这是初始化容器的代码
    /// </summary>
    public void RegisterContainersTotalController()
    {
        equipmentSlotContainer.RegisterTotalController();
        saleSlotContainer.RegisterTotalController();
        buySlotContainer.RegisterTotalController();
    }

    private void Update()
    {
        //TODO: 测试按键
        //if (Input.GetKeyDown(KeyCode.I))
        //{
        //    InventoryLevelControl.LevelUp();  
        //    //调用更新
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
    /// 刷新所有container的方法
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
    /// 存储数据
    /// </summary>
    public void SaveData()
    {
        if(inventoryItemData != null)
        {
            //清空标识符数据
            inventoryItemData.backpackItemIdentifier.Clear();
            inventoryItemData.toolpackItemIdentifier.Clear();
            inventoryItemData.proppackItemIdentifier.Clear();
            inventoryItemData.equipmentItemIdentifier.Clear();
            inventoryItemData.storepackItemIdentifier.Clear();
            inventoryItemData.salepackItemIdentifier.Clear();

            //存储背包标识符数据
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

            //存储库存相关标识符数据
            SaveManager.Instance.Save(GameManager.Instance.gameData.archiveSaveName, "backpackItemIdentifier.sav", inventoryItemData.backpackItemIdentifier);
            SaveManager.Instance.Save(GameManager.Instance.gameData.archiveSaveName, "toolpackItemIdentifier.sav", inventoryItemData.toolpackItemIdentifier);
            SaveManager.Instance.Save(GameManager.Instance.gameData.archiveSaveName, "proppackItemIdentifier.sav", inventoryItemData.proppackItemIdentifier);
            SaveManager.Instance.Save(GameManager.Instance.gameData.archiveSaveName, "equipmentItemIdentifier.sav", inventoryItemData.equipmentItemIdentifier);
            SaveManager.Instance.Save(GameManager.Instance.gameData.archiveSaveName, "storepackItemIdentifier.sav", inventoryItemData.storepackItemIdentifier);
            SaveManager.Instance.Save(GameManager.Instance.gameData.archiveSaveName, "salepackItemIdentifier.sav", inventoryItemData.salepackItemIdentifier);

        }
    }

    /// <summary>
    /// 读取数据
    /// </summary>
    public void LoadData()
    {
        if(inventoryItemData != null)
        {
            //读取数据
            inventoryItemData.backpackItemIdentifier = SaveManager.Instance.Load<List<ItemIdentifier>>(GameManager.Instance.gameData.archiveSaveName, "backpackItemIdentifier.sav");
            inventoryItemData.toolpackItemIdentifier = SaveManager.Instance.Load<List<ItemIdentifier>>(GameManager.Instance.gameData.archiveSaveName, "toolpackItemIdentifier.sav");
            inventoryItemData.proppackItemIdentifier = SaveManager.Instance.Load<List<ItemIdentifier>>(GameManager.Instance.gameData.archiveSaveName, "proppackItemIdentifier.sav");
            inventoryItemData.equipmentItemIdentifier = SaveManager.Instance.Load<List<ItemIdentifier>>(GameManager.Instance.gameData.archiveSaveName, "equipmentItemIdentifier.sav");
            inventoryItemData.storepackItemIdentifier = SaveManager.Instance.Load<List<ItemIdentifier>>(GameManager.Instance.gameData.archiveSaveName, "storepackItemIdentifier.sav");
            inventoryItemData.salepackItemIdentifier = SaveManager.Instance.Load<List<ItemIdentifier>>(GameManager.Instance.gameData.archiveSaveName, "salepackItemIdentifier.sav");


            //每个库存表进行储存
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

    //TODO:要给他移动到对应位置
    /// <summary>
    /// 标识符翻译器
    /// </summary>
    /// <param name="identifier"></param>
    public ItemData IdentifierHelper(ItemIdentifier identifier)
    {
        //通过标识符中的Type识别这是什么物品
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
                //默认返回一个ItemData
                return new ItemData(new BaseType());
        }

    }

    
}