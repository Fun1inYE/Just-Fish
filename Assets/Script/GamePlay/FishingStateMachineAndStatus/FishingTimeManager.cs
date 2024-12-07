using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 钓鱼状态机中反应时间的管理器
/// </summary>
public class FishingTimeManager : MonoBehaviour
{
    /// <summary>
    /// 鱼上钩所需要最小的时间
    /// </summary>
    public float minbittingTime;
    /// <summary>
    /// 给玩家反应的最大时间
    /// </summary>
    public float maxRectionTime;

    /// <summary>
    /// 鱼上钩所需要最大的时间（默认30f）
    /// </summary>
    public float maxbittingTime = 30f;
    /// <summary>
    /// 给玩家反应的最小时间(默认0.5f)
    /// </summary>
    public float minReactionTime = 0.5f;

    /// <summary>
    /// 给时间做修正
    /// </summary>
    public void FixUpFishingTimeData()
    {
        //如果上钩时间大于最大时间
        minbittingTime = minbittingTime > maxbittingTime ? maxbittingTime - 1 : minbittingTime;

    }

    /// <summary>
    /// 获取随机生成的上钩时间
    /// </summary>
    /// <returns></returns>
    public float GetRangeOfFishingTime()
    {
        Debug.Log(minbittingTime + "  " + maxbittingTime);
        //修正一下时间
        FixUpFishingTimeData();
        //返回对应时间
        return Random.Range(minbittingTime, maxbittingTime);
    }

    /// <summary>
    /// 获取随机生成的玩家反应时间
    /// </summary>
    /// <returns></returns>
    public float GetRangeOfReactionTime()
    {
        //修正一下时间
        FixUpFishingTimeData();
        //返回对应时间
        return Random.Range(minReactionTime, maxRectionTime);
    }

}
