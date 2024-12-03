using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Needle : MonoBehaviour
{
    /// <summary>
    /// 引用FishIndicatorController
    /// </summary>
    public FishIndicatorController fishIndicatorController;
    /// <summary>
    /// 指示器
    /// </summary>
    public RectTransform needle;
    /// <summary>
    /// 指示器的初始宽度(默认1f)
    /// </summary>
    public float needleWidth = 1f;
    /// <summary>
    /// 指示器变宽的速度(默认30f)
    /// </summary>
    public float needleThicknessSpeed = 15f;
    /// <summary>
    /// 指示器变窄的速度(默认15f)
    /// </summary>
    public float needleThinkDownSpeed = 15f;
    
    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        needle = GetComponent<RectTransform>();
        fishIndicatorController = SetGameObjectToParent.FindFromFirstLayer("FishingIndicatorCanvas").GetComponent<FishIndicatorController>();
    }

    /// <summary>
    /// 启动脚本执行
    /// </summary>
    public void OnEnable()
    {
        if (needle != null)
        {
            //给指示器初始化
            needle.sizeDelta = new Vector2(needleWidth, needle.sizeDelta.y);
        }
    }

    /// <summary>
    /// Indicator在IndicatorRange中变宽的方法
    /// </summary>
    public void CtrlThickness()
    {
        float currentWidth = needle.sizeDelta.x;

        if (Input.GetKey(KeyCode.Space))
        {
            //计算新宽度
            currentWidth += needleThicknessSpeed * Time.deltaTime;

            //限制最大宽度
            currentWidth = Mathf.Min(currentWidth, fishIndicatorController.indicatorScript.indicator.sizeDelta.x);
        }
        else
        {
            //松开空格键之后，指示条逐渐缩回到初始宽度

            //计算新宽度
            currentWidth -= needleThinkDownSpeed * Time.deltaTime;
            //限制最大宽度
            currentWidth = Mathf.Max(currentWidth, needleWidth);

        }
        //更新指示器的宽度
        needle.sizeDelta = new Vector2(currentWidth, needle.sizeDelta.y);


    }

    /// <summary>
    /// 返回四个角的坐标
    /// </summary>
    /// <returns></returns>
    public Vector3[] GetCorners()
    {
        return GetFourCorners.GetFourCornersCoordinate(needle);
    }
}
