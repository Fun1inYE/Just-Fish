using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 处理鱼漂上的基础属性的
/// </summary>
public class DriftManager : MonoBehaviour
{
    /// <summary>
    /// 引用FishAndCastDataManager
    /// </summary>
    public FishingIndicatorDataManager fishingIndicatorDataManager;
    /// <summary>
    /// 玩家当前装备的鱼鳔数据
    /// </summary>
    public DriftData driftData;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        fishingIndicatorDataManager = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishingIndicatorDataManager>();
        if (fishingIndicatorDataManager == null)
        {
            Debug.LogError("fishingIndicatorDataManager是空的");
        }
    }

    public void UpdateDriftData(ItemData item)
    {
        //获取到鳔的数据
        
        if(item is PropItem propItem)
        {
            //更新崩线条数据
            fishingIndicatorDataManager.breakdownStripSpeed = propItem.wearRate;
            //更新韧性数据
            fishingIndicatorDataManager.needleWidth = propItem.toughness;
            //更新灵敏数据
            fishingIndicatorDataManager.needleThicknessSpeed = propItem.speed;
            fishingIndicatorDataManager.needleThinkDownSpeed = propItem.speed;
        }
    }

}
