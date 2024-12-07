using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

/// <summary>
/// 商店相关的操作命令
/// </summary>
public interface StoreCommand
{
    /// <summary>
    /// 执行命令的方法
    /// </summary>
    public void Execute();
}

/// <summary>
/// 执行开关商店选项面板的方法
/// </summary>
public class OpenOrCloseOptionCommand : StoreCommand
{
    /// <summary>
    /// 商店类型的选择面板
    /// </summary>
    public RectTransform optionPanel;
    /// <summary>
    /// 判断选项面板是否打开
    /// </summary>
    public bool isOpen;

    public OpenOrCloseOptionCommand(RectTransform optionPanel, bool isOpen)
    {
        this.optionPanel = optionPanel;
        this.isOpen = isOpen;
    }

    public void Execute()
    {
        optionPanel.gameObject.SetActive(isOpen);
    }
}


/// <summary>
/// 开关商店的命令
/// </summary>
public class OpenOrCloseStoreCommand : StoreCommand
{
    /// <summary>
    /// 商店的GameObject
    /// </summary>
    public RectTransform storePanel;
    /// <summary>
    /// 商店是否打开（默认false）
    /// </summary>
    public bool isOpen = false;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="storePanel"></param>
    /// <param name="isOpen"></param>
    public OpenOrCloseStoreCommand(RectTransform storePanel, bool isOpen)
    {
        this.storePanel = storePanel;
        this.isOpen = isOpen;
    }

    /// <summary>
    /// 执行命令
    /// </summary>
    public void Execute()
    {
        storePanel.gameObject.SetActive(isOpen);

        //告诉观察背包，商店的脚本：背包开启或者关闭了
        TotalController.Instance.UIChangedNotify();
    }
}

/// <summary>
/// 开关卖出界面的命令
/// </summary>
public class OpenOrCloseSaleCommand : StoreCommand
{
    /// <summary>
    /// 库存面板
    /// </summary>
    public GameObject inventoryPanel;
    /// <summary>
    /// 判断卖出界面是否打开
    /// </summary>
    public bool isOpen;

    /// <summary>
    /// 构造函数
    /// </summary>
    public OpenOrCloseSaleCommand(GameObject inventoryPanel, bool isOpen)
    {
        this.inventoryPanel = inventoryPanel;
        this.isOpen = isOpen;
    }

    /// <summary>
    /// 执行命令
    /// </summary>
    public void Execute()
    {
        inventoryPanel.SetActive(isOpen);
        //刷新卖出界面UI
        InventoryManager.Instance.saleSlotContainer.RefreshSlotUI();

        //告诉观察背包，商店的脚本：背包开启或者关闭了
        TotalController.Instance.UIChangedNotify();
    }
}