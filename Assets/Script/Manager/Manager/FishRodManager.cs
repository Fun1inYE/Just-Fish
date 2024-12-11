using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 处理装备钓竿上的基础属性的
/// </summary>
public class FishRodManager : MonoBehaviour
{
    /// <summary>
    /// 引用FishAndCastDataManager
    /// </summary>
    public FishingIndicatorDataManager fishAndCastDataManager;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        fishAndCastDataManager = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishingIndicatorDataManager>();
        if(fishAndCastDataManager == null)
        {
            Debug.LogError("fishAndCastDataManager是空的");
        }
    }

    /// <summary>
    /// 更新玩家装备的鱼竿数据
    /// </summary>
    /// <param name="rod">需要更新的鱼竿</param>
    public void UpdateFishRodData(ItemData item)
    {
        //先判断传进来的物品是不是鱼竿
        if (item is ToolItem toolItem)
        {
            //更新钓力数据
            fishAndCastDataManager.processStripSpeed = toolItem.power;
            //更新韧性数据
            fishAndCastDataManager.indicatorWidth = toolItem.toughness;
            //更新灵敏数据
            fishAndCastDataManager.indicatorMoveSpeed = toolItem.speed;
        }
        else
        {
            Debug.LogWarning($"没有获取到名字为{item.type.name}的数据");
        }
    }
}
