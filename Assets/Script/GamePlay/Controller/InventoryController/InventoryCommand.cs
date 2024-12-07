using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 关于背包UI的控制命令接口
/// </summary>
public interface IinventoryCommand
{
    /// <summary>
    /// 执行命令的方法
    /// </summary>
    public void Execute();
}

/// <summary>
/// 开关背包的操作
/// </summary>
public class OpenAndCloseInventory : IinventoryCommand
{
    /// <summary>
    /// 背包面板
    /// </summary>
    public GameObject inventoryPanel;
    /// <summary>
    /// 背包的RectTransform
    /// </summary>
    public RectTransform inventoryRectTransform;
    /// <summary>
    /// 判断背包是否被打开, 通过外界传值（默认是false）
    /// </summary>
    public bool isOpen;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="inventoryPanel">背包面板</param>
    public OpenAndCloseInventory(GameObject inventoryPanel, RectTransform inventoryRectTransform, bool isOpen)
    {
        this.inventoryPanel = inventoryPanel;
        this.inventoryRectTransform = inventoryRectTransform;
        this.isOpen = isOpen;
    }
    //执行命令
    public void Execute()
    {
        //打开背包panel的GameObject
        inventoryPanel.SetActive(isOpen);

        //如果isOpen为true的话，就更新一次backpackContainer
        if (isOpen == true)
        {
            InventoryManager.Instance.RefreshAllContainer();
        }

        //告诉观察背包，商店的脚本：背包开启或者关闭了
        TotalController.Instance.UIChangedNotify();
    }
}

/// <summary>
/// 切换库存内容物的类
/// </summary>
public class OpenInventoryContent : IinventoryCommand
{
    /// <summary>
    /// 背包的UI容器
    /// </summary>
    public GameObject backpackContent;
    /// <summary>
    /// 工具包的UI容器
    /// </summary>
    public GameObject toolpackContent;
    /// <summary>
    /// 道具包的UI容器
    /// </summary>
    public GameObject proppackContent;
    /// <summary>
    /// 背包界面是否开启(默认为false)
    /// </summary>
    bool isBackPackOpen = false;
    /// <summary>
    /// 工具包界面是否启动(默认为false)
    /// </summary>
    bool isToolPackOpen = false;
    /// <summary>
    /// 道具包是否启动(默认为false)
    /// </summary>
    bool isPropPackOpen = false;
    /// <summary>
    /// 构造函数
    /// </summary>
    public OpenInventoryContent(bool isBackPackOpen, bool isToolPackOpen, bool isPropPackOpen, GameObject backpackContent, GameObject toolpackContent, GameObject proppackContent)
    {
        this.isBackPackOpen = isBackPackOpen;
        this.isToolPackOpen = isToolPackOpen;
        this.isPropPackOpen = isPropPackOpen;
        this.backpackContent = backpackContent;
        this.toolpackContent = toolpackContent;
        this.proppackContent = proppackContent;
    }
    /// <summary>
    /// 执行命令
    /// </summary>
    public void Execute()
    {
        //打开或者关闭窗口
        backpackContent.SetActive(isBackPackOpen);
        toolpackContent.SetActive(isToolPackOpen);
        proppackContent.SetActive(isPropPackOpen);
        //转完窗口刷新一下UI
        InventoryManager.Instance.RefreshAllContainer();
    }
}
