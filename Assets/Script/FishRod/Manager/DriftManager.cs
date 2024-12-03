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
    public FishAndCastDataManager fishAndCastDataManager;
    /// <summary>
    /// 玩家当前装备的鱼鳔数据
    /// </summary>
    public DriftData driftData;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        fishAndCastDataManager = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishAndCastDataManager>();
        if (fishAndCastDataManager == null)
        {
            Debug.LogError("fishAndCastDataManager是空的");
        }
    }

    public void UpdateDriftData(GameObject drift)
    {
        //获取到鳔的数据
        driftData = drift.GetComponent<DriftData>();
        if(driftData != null)
        {
            //更新崩线条数据
            fishAndCastDataManager.breakdownStripSpeed = driftData.wearRate;
            //更新韧性数据
            fishAndCastDataManager.needleWidth = driftData.toughness;
            //更新灵敏数据
            fishAndCastDataManager.needleThicknessSpeed = driftData.speed;
            fishAndCastDataManager.needleThinkDownSpeed = driftData.speed;
        }
    }

}
