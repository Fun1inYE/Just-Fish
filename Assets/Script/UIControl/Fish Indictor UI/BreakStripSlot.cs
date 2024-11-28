using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakStripSlot : MonoBehaviour
{
    /// <summary>
    /// 引用FishIndicatorController
    /// </summary>
    public FishIndicatorController fishIndicatorController;
    /// <summary>
    /// BreakStripSlotSlot的RectTransform
    /// </summary>
    public RectTransform breakStripSlotRectTransform;
    /// <summary>
    /// BreakStripSlotSlot的进度条
    /// </summary>
    public RectTransform breakdownStrip;
    /// <summary>
    /// BreakStripSlotSlot的进度条长高的速度(默认为15f)
    /// </summary>
    public float breakdownStripSpeed = 15f;
    /// <summary>
    /// BreakStripSlotSlot的进度条变短的速度(默认为0f)
    /// </summary>
    public float breakDownStripDownSpeed = 0f;
    /// <summary>
    /// 判断线是否断了
    /// </summary>
    public bool isBreakdown = false;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        fishIndicatorController = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishIndicatorController>();
        breakStripSlotRectTransform = GetComponent<RectTransform>();
        breakdownStrip = ComponentFinder.GetChildComponent<RectTransform>(gameObject, "BreakStrip");
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnEnable()
    {
        //默认没有钓到鱼
        isBreakdown = false;
        // 启动的时候进度条进度为0
        breakdownStrip.sizeDelta = new Vector2(0f, breakdownStrip.sizeDelta.y);
    }

    /// <summary>
    /// 涨进度条的方法
    /// </summary>
    public void RiseStrip()
    {
        //获取到processStrip宽度
        float currentWidth = breakdownStrip.sizeDelta.x;

        //指示器指针充满指示器后
        if (fishIndicatorController.indicatorScript.indicator.sizeDelta.x == fishIndicatorController.needleScript.needle.sizeDelta.x)
        {
            //逐渐变宽
            currentWidth += breakdownStripSpeed * Time.deltaTime;
            //限制最大宽度
            currentWidth = Mathf.Min(currentWidth, breakStripSlotRectTransform.sizeDelta.x);
        }
        else
        {
            //计算新宽度
            currentWidth -= breakDownStripDownSpeed * Time.deltaTime;
            //限制最大宽度
            currentWidth = Mathf.Max(currentWidth, 0f);
        }

        //更新崩线条的宽度
        breakdownStrip.sizeDelta = new Vector2(currentWidth, breakdownStrip.sizeDelta.y);

        //判断崩线条是否已经蓄满了
        if (currentWidth == breakStripSlotRectTransform.sizeDelta.x)
        {
            isBreakdown = true;
        }
    }
}
