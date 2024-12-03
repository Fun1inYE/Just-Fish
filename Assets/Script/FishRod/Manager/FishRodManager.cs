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
    public FishAndCastDataManager fishAndCastDataManager;

    /// <summary>
    /// 玩家当前装备的钓竿数据
    /// </summary>
    public FishRodData fishRodData;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        fishAndCastDataManager = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishAndCastDataManager>();
        if(fishAndCastDataManager == null)
        {
            Debug.LogError("fishAndCastDataManager是空的");
        }
    }

    /// <summary>
    /// 更新玩家装备的鱼竿数据
    /// </summary>
    /// <param name="rod">需要更新的鱼竿</param>
    public void UpdateFishRodData(GameObject rod)
    {
        //获取到钓竿数据
        fishRodData = rod.GetComponent<FishRodData>();
        if (fishRodData != null)
        {
            //更新钓力数据
            fishAndCastDataManager.processStripSpeed = fishRodData.power;
            //更新韧性数据
            fishAndCastDataManager.indicatorWidth = fishRodData.toughness;
            //更新灵敏数据
            fishAndCastDataManager.indicatorMoveSpeed = fishRodData.speed;
        }
        else
        {
            Debug.LogWarning($"没有获取到名字为{rod.name}的数据");
        }
    }
}
