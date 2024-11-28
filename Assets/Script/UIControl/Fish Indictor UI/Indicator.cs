using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 指示器脚本
/// </summary>
public class Indicator : MonoBehaviour
{
    /// <summary>
    /// 引用FishIndicatorController
    /// </summary>
    public FishIndicatorController fishIndicatorController;
    /// <summary>
    /// 获取到Indicator的RectTransform
    /// </summary>
    public RectTransform indicator;

    /// <summary>
    /// 获取到Indicator的四个角
    /// </summary>
    public Vector3[] indicatorFourCorners;

    /// <summary>
    /// Indicator的移动速度
    /// </summary>
    public float indicatorMoveSpeed = 50f;

    /// <summary>
    /// 判断Indicator可以向那个方向走
    /// </summary>
    public bool canMoveToLeft = true;
    public bool canMoveToRight = true;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        fishIndicatorController = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishIndicatorController>();
        indicator = GetComponent<RectTransform>();
        if (indicator == null)
        {
            Debug.LogError("fishIndicatorMoveRange是空的，请检查代码！");
        }
        indicatorFourCorners = new Vector3[4];
    }

    /// <summary>
    /// 初始化四个角
    /// </summary>
    public void Start()
    {
        //获取到四个角的坐标
        indicatorFourCorners = GetFourCorners.GetFourCornersCoordinate(indicator);
    }
    
    /// <summary>
    /// Indicator移动的方法
    /// </summary>
    public void Move()
    {
        float horizontal = 0f;
        if (Input.GetKey(KeyCode.A) && canMoveToLeft)
        {
            horizontal = -1f;
        }
        if (Input.GetKey(KeyCode.D) && canMoveToRight)
        {
            horizontal = 1f;
        }
        // 获取 indicatorRange 的轴心点的坐标
        Vector2 newPosition = indicator.anchoredPosition;

        // 判断 indicatorScript 是否在 fishIndicatorMoveRangeScript 内移动
        bool isWithinLeftBound = fishIndicatorController.fishIndicatorMoveRangeScript.GetCorners()[0].x <= GetCorners()[0].x;
        bool isWithinRightBound = fishIndicatorController.fishIndicatorMoveRangeScript.GetCorners()[3].x >= GetCorners()[3].x;

        // 超出左边界，禁用向左移动的键盘响应
        if (!isWithinLeftBound)
        {
            canMoveToLeft = false;
        }
        else
        {
            canMoveToLeft = true;
        }

        // 超出右边界，禁用向右移动的键盘响应
        if (!isWithinRightBound)
        {
            canMoveToRight = false;
        }
        else
        {
            canMoveToRight = true;
        }

        //设置Indicator的新的位置
        newPosition.x += horizontal * indicatorMoveSpeed * Time.deltaTime;

        // 设置 indicatorScript 的 anchoredPosition
        indicator.anchoredPosition = newPosition;
    }

    /// <summary>
    /// 返回四个角的坐标
    /// </summary>
    /// <returns></returns>
    public Vector3[] GetCorners()
    {
        return GetFourCorners.GetFourCornersCoordinate(indicator);
    }
}
