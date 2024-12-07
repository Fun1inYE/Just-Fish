using System.Linq;
using UnityEngine;

/// <summary>
/// 商店格子的容器，继承于SlotContainer
/// </summary>
public class BuySlotContainer : SlotContainer
{
    /// <summary>
    /// 引用刷新物品类
    /// </summary>
    public RefreshProduct refreshProduct;

    /// <summary>
    /// 引用总控制器
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// 引用经济管理器
    /// </summary>
    public EconomyManager economyManager;

    /// <summary>
    /// 重写父类中的初始化方法
    /// </summary>
    public override void InitializedContainer()
    {
        //先执行父类中的slot方法
        base.InitializedContainer();
        //刷新商品初始化
        refreshProduct = SetGameObjectToParent.FindFromFirstLayer("StoreCanvas").GetComponent<RefreshProduct>();
        //经济管理的初始化
        economyManager = SetGameObjectToParent.FindFromFirstLayer("EconomyManager").GetComponent<EconomyManager>();
    }

    /// <summary>
    /// 注册总控制器
    /// </summary>
    public void RegisterTotalController()
    {
        //总控制器初始化
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();

        //总控制器初始化完比之后才能正常刷新商店
        RefreshProduct();
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
        RefreshStoreSlot_0();
        RefreshStoreSlot_1();
        RefreshStoreSlot_2();

        //刷新UI
        RefreshSlotUI();
        //再次启动计时
        refreshProduct.timer.StartTimer(refreshProduct.refreshProductTime);
    }

    /// <summary>
    /// 刷新商店的一号位
    /// </summary>
    public void RefreshStoreSlot_0()
    {
        ItemData item = GenerateAndTransferStoreSlotFromIndex(0);
        totalController.storeDataAndUIController.ChangePrice_Slot0(economyManager.ReturnBuyWithCoefficient(item));
    }

    /// <summary>
    /// 刷新商店的二号位
    /// </summary>
    /// <param name="type"></param>
    public void RefreshStoreSlot_1()
    {
        ItemData item = GenerateAndTransferStoreSlotFromIndex(1);
        totalController.storeDataAndUIController.ChangePrice_Slot1(economyManager.ReturnBuyWithCoefficient(item));
    }

    /// <summary>
    /// 刷新商店的三号位
    /// </summary>
    /// <param name="type"></param>
    public void RefreshStoreSlot_2()
    {
        ItemData item = GenerateAndTransferStoreSlotFromIndex(2);
        totalController.storeDataAndUIController.ChangePrice_Slot2(economyManager.ReturnBuyWithCoefficient(item));
    }

    /// <summary>
    /// 生成物品并且将其放到对应index序号的商店格子中,并且返回对应的ItemData
    /// </summary>
    /// <param name="index"></param>
    private ItemData GenerateAndTransferStoreSlotFromIndex(int index)
    {
        //先随机获取到一个物品类型，等待被包装成物品
        BaseType baseType = ItemManager.Instance.GetRandomItemFromRandomDictionary();

        //如果baseType类型为FishType
        if (baseType is FishType fishType)
        {
            double weight = Random.Range(1f, 30f);
            double length = Random.Range(1f, 30f);
            //将type包装成item
            FishItem fishItem = new FishItem(fishType, length, weight);
            //生成并且存入商店格子中
            InventoryManager.Instance.buyManager.AddItemInListFromIndex(fishItem, index);

            return fishItem;
        }

        //如果baseType类型为ToolType
        if (baseType is ToolType toolType)
        {
            //随机获取到鱼竿的质量
            int randomNumber = Random.Range(0, System.Enum.GetValues(typeof(ToolQuality)).Length);

            ToolItem toolItem = new ToolItem(toolType, (ToolQuality)randomNumber);

            InventoryManager.Instance.buyManager.AddItemInListFromIndex(toolItem, index);

            return toolItem;
        }

        //如果baseType类型为PropType
        if (baseType is PropType propType)
        {
            //随机获取到鱼鳔的质量
            int randomNumber = Random.Range(0, System.Enum.GetValues(typeof(PropQuality)).Length);

            PropItem propItem = new PropItem(propType, (PropQuality)randomNumber);

            InventoryManager.Instance.buyManager.AddItemInListFromIndex(propItem, index);

            return propItem;
        }

        //如果baseType类型为BaitType
        if (baseType is BaitType baitType)
        {
            //随机生成鱼饵的数量 20 ~ 50个
            int randomNumber = Random.Range(20, 51);

            BaitItem baitItem = new BaitItem(baitType, 99, randomNumber);

            InventoryManager.Instance.buyManager.AddItemInListFromIndex(baitItem, index);

            return baitItem;
        }

        Debug.LogError($"没有检测到对应的{baseType.GetType()}，请检查代码");
        return null;
    }
}
