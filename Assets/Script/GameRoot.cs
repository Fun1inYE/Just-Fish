using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 游戏测试类（信息初始化）
/// </summary>
public class GameRoot : MonoBehaviour
{
    //要初始化FishType的列表
    public List<FishType> fishTypeList;
    //要初始化ToolType的列表
    public List<ToolType> toolTypeList;
    //要初始化PropType的列表
    public List<PropType> propTypeList;
    //要初始化BaseType的列表
    public List<BaitType> baitTypeList;

    //初始化UI信息
    InitDisplayUI initDisplayUI;

    //初始化Panel
    InitPanel initPanel;

    //引用主控制器
    TotalController totalController;
    

    private void Awake()
    {
        //初始化物品列表
        InitList(fishTypeList, new FishTypeFactory());
        InitList(toolTypeList, new ToolTypeFactory());
        InitList(propTypeList, new PropTypeFactory());
        InitList(baitTypeList, new BaitTypeFactory());

        initDisplayUI = new InitDisplayUI("Prefab/UI/DisplayUI", "Prefab/UI/DisplayText");
        initDisplayUI.InitTypes();

        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();

        initPanel = new InitPanel("Prefab/UI/MainUI");
        initPanel.InitTypes(1);

        //创建存档格子
        initPanel = new InitPanel("Prefab/UI/ArchiveInfoPanel");
        initPanel.InitTypes(20);
    }
    private void Start()
    {
        //将帧数限制到165帧
        Application.targetFrameRate = 165;
    }

    /// <summary>
    /// 对传进来的初始化列表及逆行初始化
    /// </summary>
    /// <typeparam name="T">继承BaseType的类</typeparam>
    /// <param name="list">要初始化列表</param>
    /// <param name="factory">对应的实例化工厂类</param>
    public void InitList<T>(List<T> list, IItemFactory<T> factory) where T : BaseType
    {
        //如果传进来的list不为空的话
        if (list.Count == 0)
        {
            Debug.LogWarning($"{list.GetType().Name}中是空的，请检查该列表是不是有问题");
            return;
        }
        foreach (T type in list)
        {
            Debug.Log(type.obj);
            //从抽象工厂类获取对应类的实例
            T buildType = factory.CreateItem(type.obj);
            //将实例传入ItemManager中的字典
            ItemManager.Instance.AddOrGetType(buildType, buildType.obj);
        }
    }

    private void Update()
    {
        //------------------------------------------TestCode--------------------------------------------------//

        if (Input.GetKeyDown(KeyCode.V))
        {
            //随机生成字典中的一个数字
            int randomNumber = Random.Range(0, ItemManager.Instance.GetDictionary<FishType>().Count - 1);
            //使用ElementAt随机访问键值对
            var element = ItemManager.Instance.GetDictionary<FishType>().ElementAt(randomNumber);

            //随机生成鱼的长度
            double fishLength = System.Math.Round(Random.Range(1f, 5f), 2);
            //随机生成鱼的重量
            double fishWeight = System.Math.Round(Random.Range(1f, 10f), 2);

            //存入Inventory
            InventoryManager.Instance.backpackManager.AddItemInList(new FishItem(element.Key, fishLength, fishWeight));
            //然后更新UI
            InventoryManager.Instance.backpackSlotContainer.RefreshSlotUI();
        }
        //TODO: (测试)删除背包中的鱼        
        if (Input.GetKeyDown(KeyCode.N))
        {
            InventoryManager.Instance.backpackManager.DeleteLastItemInList();
            //然后更新UI
            InventoryManager.Instance.backpackSlotContainer.RefreshSlotUI();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            //随机生成字典中的一个数字
            int randomNumber = Random.Range(0, ItemManager.Instance.GetDictionary<ToolType>().Count - 1);
            //使用ElementAt随机访问键值对
            var element = ItemManager.Instance.GetDictionary<ToolType>().ElementAt(randomNumber);
            //随机生成一个工具品质数字
            int randomNumber2 = Random.Range(0, 4);

            //存入Inventory
            InventoryManager.Instance.toolpackManager.AddItemInList(new ToolItem(element.Key, (ToolQuality)randomNumber2));
            //然后更新UI
            InventoryManager.Instance.toolpackSlotContainer.RefreshSlotUI();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            //随机生成字典中的一个数字
            int randomNumber = Random.Range(0, ItemManager.Instance.GetDictionary<PropType>().Count - 1);
            //使用ElementAt随机访问键值对
            var element = ItemManager.Instance.GetDictionary<PropType>().ElementAt(randomNumber);
            //随机生成一个工具品质数字
            int randomNumber2 = Random.Range(0, 4);

            //存入Inventory
            InventoryManager.Instance.proppackManager.AddItemInList(new PropItem(element.Key, (PropQuality)randomNumber2));
            //然后更新UI
            InventoryManager.Instance.proppackSlotContainer.RefreshSlotUI();
        }

        if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            //随机生成字典中的一个数字
            int randomNumber = Random.Range(0, ItemManager.Instance.GetDictionary<BaitType>().Count - 1);
            //使用ElementAt随机访问键值对
            var element = ItemManager.Instance.GetDictionary<BaitType>().ElementAt(randomNumber);

            //存入Inventory
            InventoryManager.Instance.proppackManager.AddItemInList(new BaitItem(element.Key, 99, 1), 1);
            //然后更新UI
            InventoryManager.Instance.proppackSlotContainer.RefreshSlotUI();
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            InventoryManager.Instance.proppackManager.DeleteLastItemInList();
            //然后更新UI
            InventoryManager.Instance.proppackSlotContainer.RefreshSlotUI();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ItemManager.Instance.CheckDictionary();
        }
    }
}
