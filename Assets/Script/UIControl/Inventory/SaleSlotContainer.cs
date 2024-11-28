using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// 卖出界面的Container，继承于SlotContainer
/// </summary>
public class SaleSlotContainer : SlotContainer
{
    /// <summary>
    /// 长度计算系数（默认为1f）
    /// </summary>
    public float length_coefficient = 1f;
    /// <summary>
    /// 重量计算系数（默认为3f）
    /// </summary>
    public float weight_coefficient = 3f;

    /// <summary>
    /// 工具质量的计算系数（默认为100f）
    /// </summary>
    public float toolQuality_coefficient = 100f;
    /// <summary>
    /// 道具质量的计算系数（默认为100f）
    /// </summary>
    public float propQuality_coefficient = 50f;
    /// <summary>
    /// 鱼竿计算系数
    /// </summary>
    public float tool_coefficient = 10f;
    /// <summary>
    /// 鱼鳔的计算系数
    /// </summary>
    public float prop_coefficient = 5f;

    /// <summary>
    /// 引用总控制器
    /// </summary>
    public TotalController totalController;

    /// <summary>
    /// 重写SlotContainer中的InitializedContainer()方法，为当前类多初始化总控制器
    /// </summary>
    public override void InitializedContainer()
    {
        base.InitializedContainer();
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
    /// 在这里重写刷新装备格子的方法                                               
    /// </summary>
    public override void RefreshSlotUI()
    {
        //先给PlayerUI的sallingCoin置零
        totalController.playerDataAndUIController.ChangeSallingCoinToZero();

        //重新计算saleSlot中的物品价值
        for (int i = 0; i < slotGameObjects.Length; i++)
        {
            //更新UI
            slots[i].UpdateUI();
            
            //计算价值
            CheckSaleSlotAndCalCulation(slots[i], i);
        }
    }

    /// <summary>
    /// 检查卖出格子并且计算价钱的方法
    /// </summary>
    /// <param name="slot">传进来要检查的格子</param>
    /// <param name="index">传进来要检查的格子的序号</param>
    public void CheckSaleSlotAndCalCulation(Slot slot, int index)
    {
        if(slot.inventory_Database.list[index].itemIdentifier.Type != "defaultType")
        {
            //如果传进来的slot中的数据是FishItem,就将该格子的类型转换成对应类型，以下同理
            if (slot.inventory_Database.list[index] is FishItem fishItem)
            {
                //转换格子类型
                slot.interiorSlotType = SlotType.FishItem;
                //计算价钱
                int result = CalculatItemPrice.CulationFish(fishItem, length_coefficient, weight_coefficient);
                //调用总控制器中的更新PlayerUI的方法,更改预显示的金币数量
                totalController.playerDataAndUIController.ChangeSallingCoin(result);
            }
            //计算鱼竿的价钱
            else if (slot.inventory_Database.list[index] is ToolItem toolItem)
            {
                //转换格子类型
                slot.interiorSlotType = SlotType.ToolItem;
                //计算价钱
                int result = CalculatItemPrice.CulationTool(toolItem, toolQuality_coefficient);
                //调用总控制器中的更新PlayerUI的方法,更改预显示的金币数量
                totalController.playerDataAndUIController.ChangeSallingCoin(result);
            }
            //计算鱼鳔的价钱
            else if (slot.inventory_Database.list[index] is PropItem propItem)
            {
                //转换格子类型
                slot.interiorSlotType = SlotType.PropItem;
                //计算价钱
                int result = CalculatItemPrice.CulationProp(propItem, propQuality_coefficient);
                //调用总控制器中的更新PlayerUI的方法,更改预显示的金币数量
                totalController.playerDataAndUIController.ChangeSallingCoin(result);
            }
            //如果什么都不是的话
            else
            {
                Debug.Log("执行到这句话了");
            }
        }
        
        //当物品标识符为默认物品的时候，要把格子转换回来
        else
        {
            //转换格子类型,使这个格子能够放任何物品
            slot.interiorSlotType = SlotType.Sale;
        }
    }
}
