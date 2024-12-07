using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 背包UI的控制器
/// </summary>
public class InventoryController : MonoBehaviour, IController
{
    /// <summary>
    /// 库存面板的GameObject
    /// </summary>
    public GameObject inventoryPanel;
    /// <summary>
    /// 库存面板的RectTransform
    /// </summary>
    public RectTransform inventoryRectTransform;
    /// <summary>
    /// 获取到库存的装备详情页面
    /// </summary>
    public RectTransform detailPanel;
    /// <summary>
    /// 获取到卖出面板
    /// </summary>
    public RectTransform salePanel;
    /// <summary>
    /// 判断库存是否打开(默认为false)
    /// </summary>
    public bool isOpen = false;
    /// <summary>
    /// 获取到背包按钮
    /// </summary>
    public Button backpackBtn;
    /// <summary>
    /// 获取到工具背包按钮
    /// </summary>
    public Button toolpackBtn;
    /// <summary>
    /// 获取到道具背包按钮
    /// </summary>
    public Button proppcakBtn;
    /// <summary>
    /// 背包的Content
    /// </summary>
    public GameObject backpackContent;
    /// <summary>
    /// 工具包的Content
    /// </summary>
    public GameObject toolpackContent;
    /// <summary>
    /// 道具包的Content
    /// </summary>
    public GameObject proppcakContent;

    public List<IUIObserver> oberverlist;

    /// <summary>
    /// 来自于ICommand接口，判断该控制器是否能运行
    /// </summary>
    private bool _canRun = true;
    public bool canRun
    {
        get { return _canRun; }
        set { _canRun = value; }
    }

    /// <summary>
    /// 命令列表
    /// </summary>
    public List<IinventoryCommand> commandList { get; private set; }

    /// <summary>
    /// 脚本初始化
    /// </summary>
    private void Awake()
    {
        inventoryPanel = SetGameObjectToParent.FindChildRecursive(transform, "InventoryPanel").gameObject;
        if(inventoryPanel == null)
        {
            Debug.LogError("inventoryPanel是空的，请检查代码!");
        }

        detailPanel = ComponentFinder.GetChildComponent<RectTransform>(inventoryPanel, "DetailPanel");
        salePanel = ComponentFinder.GetChildComponent<RectTransform>(inventoryPanel, "SalePanel");

        //按钮初始化
        backpackBtn = ComponentFinder.GetChildComponent<Button>(gameObject, "BackPackButton");
        toolpackBtn = ComponentFinder.GetChildComponent<Button>(gameObject, "ToolPackButton");
        proppcakBtn = ComponentFinder.GetChildComponent<Button>(gameObject, "PropPackButton");

        //UI容器初始化
        backpackContent = ComponentFinder.GetChildComponent<SlotContainer>(gameObject, "Content").gameObject;
        toolpackContent = ComponentFinder.GetChildComponent<SlotContainer>(gameObject, "ToolItemListPanel").gameObject;
        proppcakContent = ComponentFinder.GetChildComponent<SlotContainer>(gameObject, "PropItemListPanel").gameObject;

        inventoryRectTransform = inventoryPanel.GetComponent<RectTransform>();
        if(inventoryRectTransform == null)
        {
            Debug.LogError("inventoryRectTransform是空的，请检查代码!");
        }

        oberverlist = new List<IUIObserver>();
        commandList = new List<IinventoryCommand>();
    }

    private void Start()
    {
        //给按钮添加监听事件
        backpackBtn.onClick.AddListener(OpenBackPack);
        toolpackBtn.onClick.AddListener(OpenToolPack);
        proppcakBtn.onClick.AddListener(OpenPropPack);
    }

    /// <summary>
    /// 控制背包开关的方法，右边为装备详情（命令模式）
    /// </summary>
    public void OpenAndCloseInventoryWithDetailPanel()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            isOpen = !isOpen;
            //打开装备面板
            detailPanel.gameObject.SetActive(true);
            //关闭卖出面板
            salePanel.gameObject.SetActive(false);
            commandList.Add(new OpenAndCloseInventory(inventoryPanel, inventoryRectTransform, isOpen));
        }
        //只有关闭的方法
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            isOpen = false;
            //打开装备面板
            detailPanel.gameObject.SetActive(true);
            //关闭卖出面板
            salePanel.gameObject.SetActive(false);
            commandList.Add(new OpenAndCloseInventory(inventoryPanel, inventoryRectTransform, isOpen));
        }
    }

    /// <summary>
    /// 打开背包Content的方法（命令模式）
    /// </summary>
    public void OpenBackPack()
    {
        commandList.Add(new OpenInventoryContent(true, false, false, backpackContent, toolpackContent, proppcakContent));
    }
    /// <summary>
    /// 打开背包Content的方法（命令模式）
    /// </summary>
    public void OpenToolPack()
    {
        commandList.Add(new OpenInventoryContent(false, true, false, backpackContent, toolpackContent, proppcakContent));
    }
    /// <summary>
    /// 打开工具包Content的方法
    /// </summary>
    public void OpenPropPack()
    {
        commandList.Add(new OpenInventoryContent(false, false, true, backpackContent, toolpackContent, proppcakContent));
    }

    /// <summary>
    /// 执行命令列表
    /// </summary>
    public void runCommandList()
    {
        //遍历这一帧中所有的命令，并执行命令中的Execute方法
        foreach (IinventoryCommand command in commandList)
        {
            command.Execute();
        }
        //执行完之后清空列表
        commandList.Clear();
    }
}
