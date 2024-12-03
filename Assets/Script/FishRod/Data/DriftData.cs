using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 鱼鳔的数据
/// </summary>
public class DriftData : MonoBehaviour
{
    /// <summary>
    /// 鱼鳔的磨损速度
    /// </summary>
    [Header("磨损速度")]
    public float wearRate;
    /// <summary>
    /// 鱼鳔的韧性
    /// </summary>
    [Header("韧性")]
    public float toughness;
    /// <summary>
    /// 鱼鳔的灵敏度
    /// </summary>
    [Header("灵敏度")]
    public float speed;
}
