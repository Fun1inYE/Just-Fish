using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 钓竿的数据
/// </summary>
public class FishRodData : MonoBehaviour
{
    /// <summary>
    /// 鱼竿的钓力（影响钓鱼进度的速度）
    /// </summary>
    [Header("钓力")]
    public float power;
    /// <summary>
    /// 鱼竿的韧性(影响Indicator的宽度)
    /// </summary>
    [Header("韧性")]
    public float toughness;
    /// <summary>
    /// 鱼竿的速度（影响Indicator移动的速度）
    /// </summary>
    [Header("灵敏度")]
    public float speed;
}
