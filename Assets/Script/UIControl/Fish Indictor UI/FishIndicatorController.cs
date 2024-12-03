using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������ָʾ���Ŀ�����
/// </summary>
public class FishIndicatorController : MonoBehaviour, IController
{
    /// <summary>
    /// ��������ָʾ����RectTransform
    /// </summary>
    public RectTransform fishIndicatorRectTransform;
    /// <summary>
    /// ��ȡ��FishIndicatorMoveRange�ű�
    /// </summary>
    public FishIndicatorMoveRange fishIndicatorMoveRangeScript;
    /// <summary>
    /// ��ȡ��Indicator�ű�
    /// </summary>
    public Indicator indicatorScript;
    /// <summary>
    /// ��ȡ��Needle�ű�
    /// </summary>
    public Needle needleScript;
    /// <summary>
    /// ��ȡ��Target�ű�
    /// </summary>
    public Target targetScript;
    /// <summary>
    /// ����ProcessStripSlot�ű�
    /// </summary>
    public ProcessStripSlot processStripSlotScript;
    /// <summary>
    /// ����BreakStripSlot�ű�
    /// </summary>
    public BreakStripSlot breakStripSlotScript;

    /// <summary>
    /// ���õ������ݹ�����
    /// </summary>
    public FishAndCastDataManager fishAndCastDataManager;

    /// <summary>
    /// ����һ�����µ������ݵ��¼�
    /// </summary>
    public Action OnUpdateFishingData;

    /// <summary>
    /// ������ICommand�ӿڣ��жϸÿ������Ƿ�������
    /// </summary>
    private bool _canRun = true;
    public bool canRun
    {
        get { return _canRun; }
        set { _canRun = value; }
    }

    /// <summary>
    /// �ű���ʼ��
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

        //��ʼ��Ĭ��enableΪfalse
        enabled = false;
    }

    /// <summary>
    /// ע�����ָʾ���ű�
    /// </summary>
    public void RegisterFishIndicatorController()
    {
        enabled = true;

        //���������ݵķ���ע�ᵽ�¼�����
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
        //ֻ�е���ָʾ����ʾ��ʱ����ܽ��в���
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
        //ȡ��ע��
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
    /// �趨����ָʾ���ܷ��
    /// </summary>
    /// <param name="active"></param>
    public void FishingIndicatorSetActive(bool active)
    {
        if(active)
        {
            //�ں�����
            fishAndCastDataManager.FixUpFishIndicatorData();
            //��������
            OnUpdateFishingData.Invoke();
            //��������ָʾ��
            fishIndicatorRectTransform.gameObject.SetActive(active);
        }
        else
        {
            //�رյ���ָʾ��
            fishIndicatorRectTransform.gameObject.SetActive(active);
        }
    }

    /// <summary>
    /// ����Indicator�ķ���
    /// </summary>
    public void ControlIndicator()
    {
        indicatorScript.Move();
    }

    /// <summary>
    /// ����Needle�ķ���
    /// </summary>
    public void ControlNeedle()
    {
        needleScript.CtrlThickness();
    }

    /// <summary>
    /// ����Target�ķ���
    /// </summary>
    public void ControlTarget()
    {
        targetScript.Move();
        targetScript.CheckToNeedChangeDirction();
    }

    /// <summary>
    /// ���Ƶ�����ȵķ���
    /// </summary>
    public void ControlProcessStripSlot()
    {
        processStripSlotScript.RiseStrip();
    }

    /// <summary>
    /// ���Ʊ��������ȵķ���
    /// </summary>
    public void ControlBreakStripSlot()
    {
        breakStripSlotScript.RiseStrip();
    }

    /// <summary>
    /// ����Indicator�Ŀ��
    /// </summary>
    /// <param name="value"></param>
    public void SetIndicatorWidth()
    {
        indicatorScript.indicatorWidth = fishAndCastDataManager.indicatorWidth;
    }

    /// <summary>
    /// ����Indicator���ٶ�
    /// </summary>
    /// <param name="value"></param>
    public void SetIndicatorSpeed()
    {
        indicatorScript.indicatorMoveSpeed = fishAndCastDataManager.indicatorMoveSpeed;
    }

    /// <summary>
    /// ���õ�����ȵ������ٶ�
    /// </summary>
    /// <param name="value"></param>
    public void SetProcessStripUpSpeed()
    {
        processStripSlotScript.processStripSpeed = fishAndCastDataManager.processStripSpeed;
    }

    /// <summary>
    /// ���õ���Ľ��ȵļ����ٶ�
    /// </summary>
    /// <param name="value"></param>
    public void SetProcessStripDown()
    {
        processStripSlotScript.processStripDownSpeed = fishAndCastDataManager.processStripDownSpeed;
    }

    /// <summary>
    /// ����ָ��ĳ�ʼ���
    /// </summary>
    /// <param name="value"></param>
    public void SetNeedleInitWidth()
    {
        needleScript.needleWidth = fishAndCastDataManager.needleWidth;
    }

    /// <summary>
    /// ����ָ��ı���ٶ�
    /// </summary>
    /// <param name="value"></param>
    public void SetNeedleThicknessSpeed()
    {
        needleScript.needleThicknessSpeed = fishAndCastDataManager.needleThicknessSpeed;
    }

    /// <summary>
    /// ����ָ���խ���ٶ�
    /// </summary>
    /// <param name="value"></param>
    public void SetNeedleDownSpeed()
    {
        needleScript.needleThinkDownSpeed = fishAndCastDataManager.needleThinkDownSpeed;
    }

    /// <summary>
    /// ����ĥ����������ٶ�
    /// </summary>
    /// <param name="value"></param>
    public void SetBreakStripUpSpeed()
    {
        breakStripSlotScript.breakdownStripSpeed = fishAndCastDataManager.breakdownStripSpeed;
    }

    /// <summary>
    /// ����ĥ������խ���ٶ�
    /// </summary>
    /// <param name="value"></param>
    public void SetBreakStripDownSpeed()
    {
        breakStripSlotScript.breakDownStripDownSpeed = fishAndCastDataManager.breakDownStripDownSpeed;
    }
}
