using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 管理诱饵数据的管理器
/// </summary>
public class BaitManager : MonoBehaviour
{
    /// <summary>
    /// 引用钓鱼时间的理器
    /// </summary>
    public FishingTimeManager fishingTimeManager;
    /// <summary>
    /// 鱼饵的信息
    /// </summary>
    public BaitData baitData;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        fishingTimeManager = SetGameObjectToParent.FindFromFirstLayer("StateMachine").GetComponent<FishingTimeManager>();
        if(fishingTimeManager == null)
        {
            Debug.LogError("fishingTimeManager是空的");
        }
    }

    public void UpdateDriftData(ItemData item)
    {
        if (item is BaitItem baitItem)
        {
            //更新最小上钩时间
            fishingTimeManager.minbittingTime = baitItem.minBittingTime;
            //更新最大反应时间
            fishingTimeManager.maxRectionTime = baitItem.maxReactionTime;
        }
    }
}
