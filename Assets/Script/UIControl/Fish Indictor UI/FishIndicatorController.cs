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

        //��ʼ��Ĭ��enableΪfalse
        enabled = false;
    }

    /// <summary>
    /// ע�����ָʾ���ű�
    /// </summary>
    public void RegisterFishIndicatorController()
    {
        enabled = true;
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

}
