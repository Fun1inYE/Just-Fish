using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessStripSlot : MonoBehaviour
{
    /// <summary>
    /// 引用FishIndicatorController
    /// </summary>
    public FishIndicatorController fishIndicatorController;
    /// <summary>
    /// ProcessStripSlot的RectTransform
    /// </summary>
    public RectTransform processStripSlotRectTransform;
    /// <summary>
    /// ProcessStripSlot的进度条
    /// </summary>
    public RectTransform processStrip;
    /// <summary>
    /// ProcessStripSlot的进度条长高的速度
    /// </summary>
    public float processStripSpeed = 15f;
    /// <summary>
    /// ProcessStripSlot的进度条变短的速度
    /// </summary>
    public float processStripDownSpeed = 15f;
    /// <summary>
    /// 判断是否钓完鱼
    /// </summary>
    public bool isFinish = false;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        fishIndicatorController = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishIndicatorController>();
        processStripSlotRectTransform = GetComponent<RectTransform>();
        processStrip = ComponentFinder.GetChildComponent<RectTransform>(gameObject, "ProcessStrip");
    }

    /// <summary>
    /// 
    /// </summary>
    public void OnEnable()
    {
        //默认没有钓到鱼
        isFinish = false;
        // 启动的时候进度条进度为0
        processStrip.sizeDelta = new Vector2(0f, processStrip.sizeDelta.y);
    }

    /// <summary>
    /// 涨进度条的方法
    /// </summary>
    public void RiseStrip()
    {
        //获取到processStrip宽度
        float currentWidth = processStrip.sizeDelta.x;

        //当指示器的宽度到达最大的时候崩线条开始增加
        //if (fishIndicatorController.indicatorScript.indicator.sizeDelta.x == fishIndicatorController.needleScript.needle.sizeDelta.x)
        //{
        //    currentWidth += wireBrokenSilderSpeed * Time.deltaTime;
        //    限制最大宽度
        //    currentWidth = Mathf.Min(currentWidth, movingRange.movingRangeCorners[3].x - movingRange.movingRangeCorners[0].x);
        //}

        //指示器把目标包裹住之后
        if (fishIndicatorController.needleScript.GetCorners()[0].x <= fishIndicatorController.targetScript.GetCorners()[0].x &&
            fishIndicatorController.needleScript.GetCorners()[3].x >= fishIndicatorController.targetScript.GetCorners()[3].x)
        {
            //逐渐变宽
            currentWidth += processStripSpeed * Time.deltaTime;
            //限制最大宽度
            currentWidth = Mathf.Min(currentWidth, processStripSlotRectTransform.sizeDelta.x);
        }
        else
        {
            //松开空格键之后，指示条逐渐缩回到初始宽度
            //currentWidth = Mathf.Lerp(currentWidth, 0f, breakdownStripSpeed * Time.deltaTime);

            //计算新宽度
            currentWidth -= processStripDownSpeed * Time.deltaTime;
            //限制最大宽度
            currentWidth = Mathf.Max(currentWidth, 0f);
        }
        //更新崩线条的宽度
        processStrip.sizeDelta = new Vector2(currentWidth, processStrip.sizeDelta.y);

        //判断钓鱼条是否已经蓄满了
        if (currentWidth == processStripSlotRectTransform.sizeDelta.x)
        {
            isFinish = true;
        }
    }
}
