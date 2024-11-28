using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaleButton : MonoBehaviour
{
    /// <summary>
    /// 卖出按钮
    /// </summary>
    public Button saleButton;

    /// <summary>
    /// 引用总控制器
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        saleButton = GetComponent<Button>();
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
    }

    /// <summary>
    /// 绑定按钮监听事件
    /// </summary>
    public void OnEnable()
    {
        saleButton.onClick.AddListener(SaleItem);
    }

    public void OnDisable()
    {
        //移除button的监听事件
        saleButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// 卖出物品的方法
    /// </summary>
    public void SaleItem()
    {
        //如果当前卖出钱币大于0的话
        if(totalController.playerDataAndUIController.playerData.sallingCoin > 0)
        {
            //播放卖出音效
            AudioManager.Instance.PlayAudio("SaleItem");
            //将钱币假如玩家数据中
            totalController.playerDataAndUIController.ChangeCoin(totalController.playerDataAndUIController.playerData.sallingCoin);
        }
        
        //先将sallingCoin置零
        totalController.playerDataAndUIController.ChangeSallingCoinToZero();

        //清除saleManager中的物品数据
        InventoryManager.Instance.saleManager.DeletAllItemInList();
        //刷新saleSlotUI
        InventoryManager.Instance.saleSlotContainer.RefreshSlotUI();
    }

}
