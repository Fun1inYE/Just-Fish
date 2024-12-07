using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 诱饵的数据
/// </summary>
public class BaitData : MonoBehaviour
{
    /// <summary>
    /// 诱饵最小上钩时间
    /// </summary>
    [Header("最小上钩时间")]
    public float minBittingTime;

    /// <summary>
    /// 诱饵的最大反应时间
    /// </summary>
    [Header("最大反应时间")]
    public float maxReactionTime;

    /// <summary>
    /// 鱼竿的基础价值
    /// </summary>
    [Header("基础价值")]
    public float baseEco;

}
