using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingIndicatorDataManager : MonoBehaviour
{
    //以下是钓鱼指示器的各种参数
    /// <summary>
    /// Indicator的宽度
    /// </summary>
    public float indicatorWidth;
    /// <summary>
    /// Indicator的速度
    /// </summary>
    public float indicatorMoveSpeed;
    /// <summary>
    /// 钓鱼进度的增长速度
    /// </summary>
    public float processStripSpeed;
    /// <summary>
    /// 钓鱼的进度的减少速度
    /// </summary>
    public float processStripDownSpeed;
    /// <summary>
    /// 指针的初始宽度
    /// </summary>
    public float needleWidth;
    /// <summary>
    /// 指针的变宽速度
    /// </summary>
    public float needleThicknessSpeed;
    /// <summary>
    /// 指针变窄的速度
    /// </summary>
    public float needleThinkDownSpeed;
    /// <summary>
    /// 磨损条增大的速度
    /// </summary>
    public float breakdownStripSpeed;
    /// <summary>
    /// 磨损条变窄的速度
    /// </summary>
    public float breakDownStripDownSpeed;

    /// <summary>
    /// 更新钓鱼指示器的数据
    /// </summary>
    public void FixUpFishIndicatorData()
    {
        //指针的宽度不能大于指示器的宽度
        needleWidth = needleWidth > indicatorWidth ? indicatorWidth - 0.01f : needleWidth;
        //指针最小值
        needleWidth = needleWidth < 1f ? 1f : needleWidth;
        //指示器最小值
        indicatorWidth = indicatorWidth < 25f ? 25f : indicatorWidth;
        //指示器移动最小值
        indicatorMoveSpeed = indicatorMoveSpeed < 15f ? 15f : indicatorMoveSpeed;
        //钓鱼进度条变宽最小值
        processStripSpeed = processStripSpeed < 1f ? 1f : processStripSpeed;
        //钓鱼进度条变窄最小值
        processStripDownSpeed = processStripDownSpeed < 1f ? 1f : processStripDownSpeed;
        //指针变宽的最慢值
        needleThicknessSpeed = needleThicknessSpeed < 5f ? 5f : needleThicknessSpeed;
        //指针变窄的最慢值
        needleThinkDownSpeed = needleThinkDownSpeed < 5f ? 5f : needleThinkDownSpeed;
        //崩线条变宽的最慢值
        breakdownStripSpeed = breakdownStripSpeed < 1f ? 1f : breakdownStripSpeed;
        //崩线条变窄的最慢值
        breakDownStripDownSpeed = breakDownStripDownSpeed < 1f ? 1f : breakDownStripDownSpeed;

        //TODO: 指示器宽度不能大于可移动范围
    }
}
