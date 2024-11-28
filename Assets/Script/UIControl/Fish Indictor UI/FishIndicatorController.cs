using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 整个钓鱼指示器的控制器
/// </summary>
public class FishIndicatorController : MonoBehaviour, IController
{
    /// <summary>
    /// 整个钓鱼指示器的RectTransform
    /// </summary>
    public RectTransform fishIndicatorRectTransform;
    /// <summary>
    /// 获取到FishIndicatorMoveRange脚本
    /// </summary>
    public FishIndicatorMoveRange fishIndicatorMoveRangeScript;
    /// <summary>
    /// 获取到Indicator脚本
    /// </summary>
    public Indicator indicatorScript;
    /// <summary>
    /// 获取到Needle脚本
    /// </summary>
    public Needle needleScript;
    /// <summary>
    /// 获取到Target脚本
    /// </summary>
    public Target targetScript;
    /// <summary>
    /// 引用ProcessStripSlot脚本
    /// </summary>
    public ProcessStripSlot processStripSlotScript;
    /// <summary>
    /// 引用BreakStripSlot脚本
    /// </summary>
    public BreakStripSlot breakStripSlotScript;

    /// <summary>
    /// 来自于ICommand接口，判断该控制器是否能运行
    /// </summary>
    private bool _canRun = true;
    public bool canRun
    {
        get { return _canRun; }
        set { _canRun = value; }
    }

    /// <summary>
    /// 脚本初始化
    /// </summary>
    public void Awake()
    {
        fishIndicatorRectTransform = ComponentFinder.GetChildComponent<RectTransform>(gameObject, "FishIndicator");
        fishIndicatorMoveRangeScript = ComponentFinder.GetChildComponent<FishIndicatorMoveRange>(gameObject, "FishIndicatorMoveRange");
        indicatorScript = ComponentFinder.GetChildComponent<Indicator>(gameObject, "Indicator");
        needleScript = ComponentFinder.GetChildComponent<Needle>(gameObject, "Needle");
        targetScript = ComponentFinder.GetChildComponent<Target>(gameObject, "Target");
        processStripSlotScript = ComponentFinder.GetChildComponent<ProcessStripSlot>(gameObject, "ProcessStripSlot");
        breakStripSlotScript = ComponentFinder.GetChildComponent<BreakStripSlot>(gameObject, "BreakStripSlot");

        //初始化默认enable为false
        enabled = false;
    }

    /// <summary>
    /// 注册钓鱼指示器脚本
    /// </summary>
    public void RegisterFishIndicatorController()
    {
        enabled = true;
    }

    public void Update()
    {
        //只有钓鱼指示器显示的时候才能进行操作
        if (fishIndicatorRectTransform.gameObject.activeInHierarchy == true)
        {
            ControlIndicator();
            ControlNeedle();
            ControlTarget();
            ControlProcessStripSlot();
            ControlBreakStripSlot();
        }
        
    }

    /// <summary>
    /// 控制Indicator的方法
    /// </summary>
    public void ControlIndicator()
    {
        indicatorScript.Move();
    }

    /// <summary>
    /// 控制Needle的方法
    /// </summary>
    public void ControlNeedle()
    {
        needleScript.CtrlThickness();
    }

    /// <summary>
    /// 控制Target的方法
    /// </summary>
    public void ControlTarget()
    {
        targetScript.Move();
        targetScript.CheckToNeedChangeDirction();
    }

    /// <summary>
    /// 控制钓鱼进度的方法
    /// </summary>
    public void ControlProcessStripSlot()
    {
        processStripSlotScript.RiseStrip();
    }

    /// <summary>
    /// 控制崩线条进度的方法
    /// </summary>
    public void ControlBreakStripSlot()
    {
        breakStripSlotScript.RiseStrip();
    }

}
