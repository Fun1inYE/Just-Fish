using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 装备栏的管理器（继承于SlotContainer）
/// </summary>
public class EquipmentSlotContainer : SlotContainer
{
    /// <summary>
    /// 引用总控制器
    /// </summary>
    public TotalController totalController;
    /// <summary>
    /// 装备格子中的鱼竿(默认为null)
    /// </summary>
    public GameObject fishRod;
    /// <summary>
    /// 装备格子中的鱼鳔(默认为null)
    /// </summary>
    public GameObject drift;
    /// <summary>
    /// 最后一次操作的ToolItem
    /// </summary>
    public ItemData lastToolItem;
    /// <summary>
    /// 最后一次操作的PropItem
    /// </summary>
    public ItemData lastPropItem;

    /// <summary>
    /// 注册总控制器
    /// </summary>
    public void RegisterTotalController()
    {
        //总控制器初始化
        totalController = SetGameObjectToParent.FindFromFirstLayer("GameRoot").GetComponent<TotalController>();
        //同时个两个装备初始化
        fishRod = null;
        drift = null;
    }

    /// <summary>
    /// 在这里重写刷新装备格子的方法                                               
    /// </summary>
    public override void RefreshSlotUI()
    {
        for (int i = 0; i < slotGameObjects.Length; i++)
        {
            slots[i].UpdateUI();
        }

        //检查玩家对装备格子的操作
        CheckEquipmentSlotAndEquip();
    }

    #region TestCode
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Debug.Log(fishRod + "   " + drift);
        }
    }
    #endregion 

    /// <summary>
    /// 检查三个装备栏的方法，如果有装备被拖入装备格子中的话，就更新对应位置的GameObject
    /// </summary>
    public void CheckEquipmentSlotAndEquip()
    {
        //鱼竿装备栏如果不是空的话
        if (slots[0].inventory_Database.list[slots[0].Index].itemIdentifier.Type == "ToolItem")
        {
            if (fishRod == null && slots[0].inventory_Database.list[slots[0].Index] is ToolItem toolItem)
            {
                //更新最后一次操作的ToolItem
                lastToolItem = toolItem;
                //获取到鱼竿名字
                string name = toolItem.Type.ToolName;
                //从对象池中拿出对应的GameObject
                fishRod = PoolManager.Instance.GetGameObjectFromPool(name);
                //调整鱼竿的Scale
                AdjustScaleFromPlayer(fishRod);
                //将鱼竿拿到玩家obj的对应点位上
                SetGameObjectToParent.SetParentFromFirstLayerParent("Player", "FishRodPoint", fishRod);
                //调用钓鱼控制器初始化鱼竿
                totalController.fishandCastController.EquipmentFishRod();
            }
            //直接给道具格子交换物品的情况
            if (fishRod != null && slots[0].inventory_Database.list[slots[0].Index] is ToolItem toolItem1)
            {
                //如果这次的toolItem跟以前的不一样的话，就进行交换
                if(lastToolItem != toolItem1)
                {
                    //更新最后一次操作的ToolItem
                    lastToolItem = toolItem1;

                    //先将原鱼竿放回对象池中
                    PoolManager.Instance.ReturnGameObjectToPool(fishRod);
                    SetGameObjectToParent.SetParent("Pool", fishRod);
                    //卸载一次鱼竿
                    totalController.fishandCastController.UnEquipmentFishRod();                

                    //获取到新鱼竿名字
                    string name = toolItem1.Type.ToolName;
                    //从对象池中拿出对应的GameObject
                    fishRod = PoolManager.Instance.GetGameObjectFromPool(name);
                    //调整鱼竿的Scale
                    AdjustScaleFromPlayer(fishRod);
                    //将鱼竿拿到玩家obj的对应点位上
                    SetGameObjectToParent.SetParentFromFirstLayerParent("Player", "FishRodPoint", fishRod);
                    //调用钓鱼控制器初始化鱼竿
                    totalController.fishandCastController.EquipmentFishRod();
                }
            }
        }
        //直接将鱼竿从装备栏拿下来情况
        else if (slots[0].inventory_Database.list[slots[0].Index].itemIdentifier.Type == "defaultType" && fishRod != null)
        {
            PoolManager.Instance.ReturnGameObjectToPool(fishRod);
            SetGameObjectToParent.SetParent("Pool", fishRod);
            //将鱼竿置空
            fishRod = null;
            //卸载鱼竿
            totalController.fishandCastController.UnEquipmentFishRod();
        }

        //-----------------------------------------------------------------------------------------------------------------------//

        //鱼漂装备栏如果是PropItem类型
        if (slots[1].inventory_Database.list[slots[1].Index].itemIdentifier.Type == "PropItem" && slots[1].inventory_Database.list[slots[1].Index] is PropItem propItem)
        {
            //如果propItem不为空且装备格子没有鱼鳔
            if (drift == null)
            {
                //更新最后一次操作的ToolItem
                lastPropItem = propItem;

                //获取到鱼鳔的名字
                string name = propItem.Type.PropName;
                //从对象池中拿出对应的GameObject
                drift = PoolManager.Instance.GetGameObjectFromPool(name);
                //将鱼鳔拿到玩家obj的对应点位上
                SetGameObjectToParent.SetParentFromFirstLayerParent("Player", "DriftPoint", drift);
                //调用钓鱼控制器初始化鱼漂
                totalController.fishandCastController.EquipmentDrift();
            }
            //直接给道具格子交换物品的情况
            if (drift != null)
            {
                //如果propItem和lastpropItem是不一样的话
                if (lastPropItem != propItem)
                {
                    //更新最后一次操作的ToolItem
                    lastPropItem = propItem;

                    //先将原鱼鳔放回对象池中
                    PoolManager.Instance.ReturnGameObjectToPool(drift);
                    SetGameObjectToParent.SetParent("Pool", drift);
                    //卸载一次鱼鳔
                    totalController.fishandCastController.UnEquipmentDrift();

                    //获取到鱼鳔的名字
                    string name = propItem.Type.PropName;
                    //从对象池中拿出对应的GameObject
                    drift = PoolManager.Instance.GetGameObjectFromPool(name);
                    //将鱼鳔拿到玩家obj的对应点位上
                    SetGameObjectToParent.SetParentFromFirstLayerParent("Player", "DriftPoint", drift);
                    //调用钓鱼控制器初始化鱼漂
                    totalController.fishandCastController.EquipmentDrift();
                }
            }

        }
        //直接将鱼鳔从装备栏拿下来情况
        else if (slots[1].inventory_Database.list[slots[1].Index].itemIdentifier.Type == "defaultType" && drift != null)
        {
            //直接将drift送回对象池
            PoolManager.Instance.ReturnGameObjectToPool(drift);
            SetGameObjectToParent.SetParent("Pool", drift);
            //将鱼漂置空
            drift = null;
            //卸载鱼漂
            totalController.fishandCastController.UnEquipmentDrift();
        }

        //TODO:还有三号位

    }

    /// <summary>
    /// 获取玩家的Scale，防止在装备完装备的时候发现方向不对
    /// </summary>
    /// <param name="obj">要调整的gameObject</param>
    public void AdjustScaleFromPlayer(GameObject obj)
    {
        //获取到玩家
        GameObject player = SetGameObjectToParent.FindFromFirstLayer("Player").gameObject;
        //获取到玩家的本地Scale
        Vector3 playerScale = player.transform.localScale;
        //让需要调整的gameObject进行调整
        obj.transform.localScale = playerScale;
    }
}
