using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 商店的相关的控制器
/// </summary>
public class StoreController : MonoBehaviour, IController
{
    //(这里属于背包的一部分)
    /// <summary>
    /// 库存面板的GameObject
    /// </summary>
    public RectTransform inventoryPanelRectTransform;
    /// <summary>
    /// 获取到库存的装备详情页面
    /// </summary>
    public RectTransform detailPanel;
    /// <summary>
    /// 获取到卖出面板
    /// </summary>
    public RectTransform salePanel;

    /// <summary>
    /// 商店的GameObject
    /// </summary>
    public RectTransform storePanel;
    /// <summary>
    /// 判断商店是否正在开启（默认false）
    /// </summary>
    public bool storePanelisOpening = false;
    /// <summary>
    /// 判断卖出面板是否正在开启（默认false）
    /// </summary>
    public bool salePanelisOpening = false;
    /// <summary>
    /// 判断商店选项面板是否开启（默认false）
    /// </summary>
    public bool optionPanelisOpening = false;

    /// <summary>
    /// 商店类型的选择面板
    /// </summary>
    public RectTransform optionPanel;
    /// <summary>
    /// 打开商店按钮
    /// </summary>
    public Button buyButton;
    /// <summary>
    /// 打开卖物品按钮
    /// </summary>
    public Button saleButton;

    /// <summary>
    /// 获取到商店相关脚本
    /// </summary>
    public Store store;

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
    /// 商店命令列表
    /// </summary>
    public List<StoreCommand> commandList;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        inventoryPanelRectTransform = SetGameObjectToParent.FindChildBreadthFirst(SetGameObjectToParent.FindFromFirstLayer("InventoryCanvas").transform, "InventoryPanel").GetComponent<RectTransform>();
        detailPanel = ComponentFinder.GetChildComponent<RectTransform>(inventoryPanelRectTransform.gameObject, "DetailPanel");
        salePanel = ComponentFinder.GetChildComponent<RectTransform>(inventoryPanelRectTransform.gameObject, "SalePanel");

        storePanel = transform.Find("StorePanel").GetComponent<RectTransform>();
        optionPanel = transform.Find("OptionsPanel").GetComponent<RectTransform>();

        buyButton = optionPanel.transform.GetChild(0).GetComponent<Button>();
        saleButton = optionPanel.transform.GetChild(1).GetComponent<Button>();

        store = ComponentFinder.GetChildComponent<Store>(SetGameObjectToParent.FindFromFirstLayer("Prop"), "Store");


        commandList = new List<StoreCommand>();
    }

    /// <summary>
    /// 绑定按钮触发事件
    /// </summary>
    public void Start()
    {
        buyButton.onClick.AddListener(OpenBuyPanel);
        saleButton.onClick.AddListener(OpenInventoryWithSalePanel);
    }

    /// <summary>
    /// 打开商店选项面板的命令
    /// </summary>
    public void OpenOrCloseOptionPanel()
    {
        //如果商店界面和卖出界面都没有开启，就可以开启选项面板
        if (Input.GetKeyDown(KeyCode.E) && salePanelisOpening == false && storePanelisOpening == false && salePanelisOpening == false && store.wasEnterStore)
        {
            //转换optionPanelisOpening的状态
            optionPanelisOpening = !optionPanelisOpening;
            commandList.Add(new OpenOrCloseOptionCommand(optionPanel, optionPanelisOpening));
        }
    }

    /// <summary>
    /// 打开商店界面的命令
    /// </summary>
    public void OpenBuyPanel()
    {
        //判断商店界面是否不是开启的，不是开启的就可以开启
        if(storePanelisOpening == false)
        {
            storePanelisOpening = true;
            //再开器商店界面之前，要先把选项面板关闭
            IndividualTrigger_CloseOptionPanel();
            commandList.Add(new OpenOrCloseStoreCommand(storePanel, true));
        }
    }

    /// <summary>
    /// 关闭商店界面的命令
    /// </summary>
    public void CloseBuyPanel()
    {
        //判断商店界面是否是开启的，如果是开启的就可以关闭 (按esc关闭)
        if (Input.GetKeyDown(KeyCode.Escape) && storePanelisOpening == true)
        {
            storePanelisOpening = false;
            commandList.Add(new OpenOrCloseStoreCommand(storePanel, false));
        }
    }

    /// <summary>
    /// 打开卖出界面的方法
    /// </summary>
    public void OpenInventoryWithSalePanel()
    {
        //当卖出界面没有被打开的时候
        if(salePanelisOpening == false)
        {
            //在开卖出界面之前，要先把选项面板关闭
            IndividualTrigger_CloseOptionPanel();
            detailPanel.gameObject.SetActive(false);
            salePanel.gameObject.SetActive(true);
            salePanelisOpening = true;
            commandList.Add(new OpenOrCloseSaleCommand(inventoryPanelRectTransform.gameObject, true));
        }
    }

    /// <summary>
    /// 关闭卖出页面的方法
    /// </summary>
    public void CloseInventoryWithSalePanel()
    {
        //当按下esc键并且卖出界面打开的时候
        if (Input.GetKeyDown(KeyCode.Escape) && salePanelisOpening == true)
        {
            detailPanel.gameObject.SetActive(true);
            salePanel.gameObject.SetActive(false);
            salePanelisOpening = false;
            commandList.Add(new OpenOrCloseSaleCommand(inventoryPanelRectTransform.gameObject, false));
        }
        
    }

    /// <summary>
    /// 单独触发关闭选项的状态
    /// </summary>
    public void IndividualTrigger_CloseOptionPanel()
    {
        //转换optionPanelisOpening的状态
        optionPanelisOpening = false;
        commandList.Add(new OpenOrCloseOptionCommand(optionPanel, false));
    }

    /// <summary>
    /// 单独触发关闭购买界面的方法
    /// </summary>
    public void IndividualTrigger_CloseBuyPanel()
    {
        storePanelisOpening = false;
        commandList.Add(new OpenOrCloseStoreCommand(storePanel, false));
    }

    /// <summary>
    /// 单独触发关闭卖出界面
    /// </summary>
    public void IndividualTrigger_CloseInventoryWithSalePanel()
    {
        detailPanel.gameObject.SetActive(true);
        salePanel.gameObject.SetActive(false);
        salePanelisOpening = false;
        commandList.Add(new OpenOrCloseSaleCommand(inventoryPanelRectTransform.gameObject, false));
    }

    /// <summary>
    /// 执行命令列表
    /// </summary>
    public void runCommandList()
    {
        //遍历这一帧中所有的命令，并执行命令中的Execute方法
        foreach (StoreCommand command in commandList)
        {
            command.Execute();
        }
        //执行完之后清空列表
        commandList.Clear();
    }
}
