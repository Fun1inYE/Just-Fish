using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    /// <summary>
    /// 购买按钮
    /// </summary>
    public Button buyButton;

    /// <summary>
    /// 引用总控制器
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        buyButton = GetComponent<Button>();
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
    }

    /// <summary>
    /// 按钮绑定监听事件
    /// </summary>
    public void Start()
    {
        buyButton.onClick.AddListener(BuyItem);
    }

    /// <summary>
    /// 购买物品的方法
    /// </summary>
    public void BuyItem()
    {
        //将数字字符串转换成int型
        int productPrice = int.Parse(transform.parent.GetComponent<Text>().text);

        //判断玩家的钱是否大于当前要购买的商品
        if (totalController.playerDataAndUIController.playerData.coin >= productPrice)
        {
            //获取到itemData
            ItemData itemData = transform.parent.parent.GetComponent<Slot>().inventory_Database.list[transform.parent.parent.GetComponent<Slot>().Index];
            //假如物品数据不为空的话可以进行购买
            if (itemData.itemIdentifier.Type != "defaultType")
            {
                //播放购买声音
                AudioManager.Instance.PlayAudio("ShortCoinDrop");
                //给玩家金币数据进行对应的减少
                totalController.playerDataAndUIController.ChangeCoin(-productPrice);
                //识别购买的物品并且放入对应背包
                RecognizeItemAndPutInInventory(itemData);
            }
            else
            {
                //TODO：物品已卖光的提示
                Debug.Log("***物品卖光了");
            }
            //传进去按钮对应的slot序号
            InventoryManager.Instance.buyManager.DeleteItemInListFromIndex(transform.parent.parent.GetComponent<Slot>().Index);
            //刷新buySlotUI
            InventoryManager.Instance.buySlotContainer.RefreshSlotUI();
        }
        else
        {
            //TODO:提醒钱不够的UI
            Debug.Log("玩家所持金币不够！");
        }
        
    }

    /// <summary>
    /// 识别Item并且自动填入对应背包
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    public void RecognizeItemAndPutInInventory(ItemData itemData)
    {
        if(itemData is ToolItem toolItem)
        {
            InventoryManager.Instance.toolpackManager.AddItemInList(toolItem);
            InventoryManager.Instance.toolpackSlotContainer.RefreshSlotUI();
        }
        if (itemData is PropItem propItem)
        {
            InventoryManager.Instance.proppackManager.AddItemInList(propItem);
        }

    }
}
