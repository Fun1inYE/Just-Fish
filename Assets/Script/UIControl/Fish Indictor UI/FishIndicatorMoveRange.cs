using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 指示器可移动范围
/// </summary>
public class FishIndicatorMoveRange : MonoBehaviour
{
    /// <summary>
    /// 引用FishIndicatorController
    /// </summary>
    public FishIndicatorController fishIndicatorController;
    /// <summary>
    /// 获取到FishIndicatorMoveRange的RectTransform
    /// </summary>
    public RectTransform fishIndicatorMoveRange;

    /// <summary>
    /// 获取到FishIndicatorMoveRange的四个角
    /// </summary>
    public Vector3[] fishIndicatorMoveRangeFourCorners;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        fishIndicatorMoveRange = GetComponent<RectTransform>();
        if(fishIndicatorMoveRange == null)
        {
            Debug.LogError("fishIndicatorMoveRange是空的，请检查代码！");
        }
        fishIndicatorMoveRangeFourCorners = new Vector3[4];

        fishIndicatorController = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishIndicatorController>();

    }

    /// <summary>
    /// 初始化四个角
    /// </summary>
    public void Start()
    {
        //获取到四个角的坐标
        fishIndicatorMoveRangeFourCorners = GetFourCorners.GetFourCornersCoordinate(fishIndicatorMoveRange);
    }

    /// <summary>
    /// 返回四个角的坐标
    /// </summary>
    /// <returns></returns>
    public Vector3[] GetCorners()
    {
        return GetFourCorners.GetFourCornersCoordinate(fishIndicatorMoveRange);
    }
}
