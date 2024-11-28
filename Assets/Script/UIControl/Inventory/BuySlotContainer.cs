using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 商店格子的容器，继承于SlotContainer
/// </summary>
public class BuySlotContainer : SlotContainer
{
    /// <summary>
    /// 购买鱼竿所需要的钱的计算系数(默认为150f)
    /// </summary>
    public float toolQuality_coefficient = 150f;

    /// <summary>
    /// 道具质量的计算系数（默认为60f）
    /// </summary>
    public float propQuality_coefficient = 60f;

    /// <summary>
    /// 引用刷新类
    /// </summary>
    public RefreshProduct refreshProduct;

    /// <summary>
    /// 引用总控制器
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// 重写父类中的初始化方法
    /// </summary>
    public override void InitializedContainer()
    {
        //先执行父类中的slot方法
        base.InitializedContainer();
        //刷新商品初始化
        refreshProduct = SetGameObjectToParent.FindFromFirstLayer("StoreCanvas").GetComponent<RefreshProduct>();
        
    }

    public void Start()
    {
        //刷新商品
        RefreshProduct();
    }

    /// <summary>
    /// 注册总控制器
    /// </summary>
    public void RegisterTotalController()
    {
        //总控制器初始化
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
    }

    /// <summary>
    /// 重新父类中的刷新UI的方法
    /// </summary>
    public override void RefreshSlotUI()
    {
        base.RefreshSlotUI();
    }

    /// <summary>
    /// 刷新商品的方法
    /// </summary>
    public void RefreshProduct()
    {
        //随机生成字典中的一个数字
        int randomNumber1 = Random.Range(0, ItemManager.Instance.GetDictionary<ToolType>().Count - 1);
        //使用ElementAt随机访问键值对
        var element1 = ItemManager.Instance.GetDictionary<ToolType>().ElementAt(randomNumber1);
        //随机生成一个工具品质数字
        int randomNumber1_2 = Random.Range(0, 4);
        //存入Inventory
        InventoryManager.Instance.buyManager.AddItemInListFromIndex(new ToolItem(element1.Key, (ToolQuality)randomNumber1_2), 0);
        //改变商品价格UI文字
        totalController.storeDataAndUIController.ChangePrice_tool(CalculatItemPrice.CulationTool(new ToolItem(element1.Key, (ToolQuality)randomNumber1_2), toolQuality_coefficient));

        //随机生成字典中的一个数字
        int randomNumber2 = Random.Range(0, ItemManager.Instance.GetDictionary<PropType>().Count - 1);
        //使用ElementAt随机访问键值对
        var element2 = ItemManager.Instance.GetDictionary<PropType>().ElementAt(randomNumber2);
        //随机生成一个工道具品质数字
        int randomNumber2_2 = Random.Range(0, 4);
        //存入Inventory
        InventoryManager.Instance.buyManager.AddItemInListFromIndex(new PropItem(element2.Key, (PropQuality)randomNumber2_2), 1);
        //改变商店商品价格UI文字
        totalController.storeDataAndUIController.ChangePrice_prop1(CalculatItemPrice.CulationProp(new PropItem(element2.Key, (PropQuality)randomNumber2_2), toolQuality_coefficient));

        //随机生成字典中的一个数字
        int randomNumber3 = Random.Range(0, ItemManager.Instance.GetDictionary<PropType>().Count - 1);
        //使用ElementAt随机访问键值对
        var element3 = ItemManager.Instance.GetDictionary<PropType>().ElementAt(randomNumber3);
        //随机生成一个道具品质数字
        int randomNumber3_2 = Random.Range(0, 4);
        //存入Inventory
        InventoryManager.Instance.buyManager.AddItemInListFromIndex(new PropItem(element3.Key, (PropQuality)randomNumber3_2), 2);
        //改变商店商品价格UI文字
        totalController.storeDataAndUIController.ChangePrice_prop2(CalculatItemPrice.CulationProp(new PropItem(element3.Key, (PropQuality)randomNumber3_2), toolQuality_coefficient));

        //刷新UI
        RefreshSlotUI();
        //再次启动计时
        refreshProduct.timer.StartTimer(refreshProduct.refreshProductTime);
    }
}
