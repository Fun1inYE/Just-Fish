using System;
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
    /// 引用钓鱼数据管理器
    /// </summary>
    public FishAndCastDataManager fishAndCastDataManager;

    /// <summary>
    /// 创建一个更新钓鱼数据的事件
    /// </summary>
    public Action OnUpdateFishingData;

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

        fishAndCastDataManager = GetComponent<FishAndCastDataManager>();

        //初始化默认enable为false
        enabled = false;
    }

    /// <summary>
    /// 注册钓鱼指示器脚本
    /// </summary>
    public void RegisterFishIndicatorController()
    {
        enabled = true;

        //将更新数据的方法注册到事件当中
        OnUpdateFishingData += SetIndicatorWidth;
        OnUpdateFishingData += SetIndicatorSpeed;
        OnUpdateFishingData += SetProcessStripUpSpeed;
        OnUpdateFishingData += SetProcessStripDown;
        OnUpdateFishingData += SetNeedleInitWidth;
        OnUpdateFishingData += SetNeedleThicknessSpeed;
        OnUpdateFishingData += SetNeedleDownSpeed;
        OnUpdateFishingData += SetBreakStripUpSpeed;
        OnUpdateFishingData += SetBreakStripDownSpeed;
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

    private void OnDestroy()
    {
        //取消注册
        OnUpdateFishingData -= SetIndicatorWidth;
        OnUpdateFishingData -= SetIndicatorSpeed;
        OnUpdateFishingData -= SetProcessStripUpSpeed;
        OnUpdateFishingData -= SetProcessStripDown;
        OnUpdateFishingData -= SetNeedleInitWidth;
        OnUpdateFishingData -= SetNeedleThicknessSpeed;
        OnUpdateFishingData -= SetNeedleDownSpeed;
        OnUpdateFishingData -= SetBreakStripUpSpeed;
        OnUpdateFishingData -= SetBreakStripDownSpeed;
    }

    /// <summary>
    /// 设定钓鱼指示器能否打开
    /// </summary>
    /// <param name="active"></param>
    public void FishingIndicatorSetActive(bool active)
    {
        if(active)
        {
            //融合数据
            fishAndCastDataManager.FixUpFishIndicatorData();
            //更新数据
            OnUpdateFishingData.Invoke();
            //启动钓鱼指示器
            fishIndicatorRectTransform.gameObject.SetActive(active);
        }
        else
        {
            //关闭钓鱼指示器
            fishIndicatorRectTransform.gameObject.SetActive(active);
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

    /// <summary>
    /// 设置Indicator的宽度
    /// </summary>
    /// <param name="value"></param>
    public void SetIndicatorWidth()
    {
        indicatorScript.indicatorWidth = fishAndCastDataManager.indicatorWidth;
    }

    /// <summary>
    /// 设置Indicator的速度
    /// </summary>
    /// <param name="value"></param>
    public void SetIndicatorSpeed()
    {
        indicatorScript.indicatorMoveSpeed = fishAndCastDataManager.indicatorMoveSpeed;
    }

    /// <summary>
    /// 设置钓鱼进度的增长速度
    /// </summary>
    /// <param name="value"></param>
    public void SetProcessStripUpSpeed()
    {
        processStripSlotScript.processStripSpeed = fishAndCastDataManager.processStripSpeed;
    }

    /// <summary>
    /// 设置钓鱼的进度的减少速度
    /// </summary>
    /// <param name="value"></param>
    public void SetProcessStripDown()
    {
        processStripSlotScript.processStripDownSpeed = fishAndCastDataManager.processStripDownSpeed;
    }

    /// <summary>
    /// 设置指针的初始宽度
    /// </summary>
    /// <param name="value"></param>
    public void SetNeedleInitWidth()
    {
        needleScript.needleWidth = fishAndCastDataManager.needleWidth;
    }

    /// <summary>
    /// 设置指针的变宽速度
    /// </summary>
    /// <param name="value"></param>
    public void SetNeedleThicknessSpeed()
    {
        needleScript.needleThicknessSpeed = fishAndCastDataManager.needleThicknessSpeed;
    }

    /// <summary>
    /// 设置指针变窄的速度
    /// </summary>
    /// <param name="value"></param>
    public void SetNeedleDownSpeed()
    {
        needleScript.needleThinkDownSpeed = fishAndCastDataManager.needleThinkDownSpeed;
    }

    /// <summary>
    /// 设置磨损条增大的速度
    /// </summary>
    /// <param name="value"></param>
    public void SetBreakStripUpSpeed()
    {
        breakStripSlotScript.breakdownStripSpeed = fishAndCastDataManager.breakdownStripSpeed;
    }

    /// <summary>
    /// 设置磨损条变窄的速度
    /// </summary>
    /// <param name="value"></param>
    public void SetBreakStripDownSpeed()
    {
        breakStripSlotScript.breakDownStripDownSpeed = fishAndCastDataManager.breakDownStripDownSpeed;
    }
}
