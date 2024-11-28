using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 游戏测试类（信息初始化）
/// </summary>
public class GameRoot : MonoBehaviour
{
    //初始化鱼类信息
    InitType<FishType> initFishType;
    //初始化钓竿信息
    InitType<ToolType> initToolType;
    //初始化鱼鳔信息
    InitType<PropType> initProptype;
    //初始化UI信息
    InitDisplayUI initDisplayUI;

    //初始化Panel
    InitPanel initPanel;

    //引用主控制器
    TotalController totalController;
    

    private void Awake()
    {
        //初始化鱼类
        initFishType = new InitType<FishType>("Prefab/Fish", new FishTypeFactory());
        //初始化鱼类的信息
        initFishType.InitTypes();

        initToolType = new InitType<ToolType>("Prefab/FishRod", new ToolTypeFactory());
        initToolType.InitTypes();
        initProptype = new InitType<PropType>("Prefab/Drift", new PropTypeFactory());
        initProptype.InitTypes();
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
        //查询鱼类字典中的元素
        //ItemManager.Instance.CheckDicElement();
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
    }
}
