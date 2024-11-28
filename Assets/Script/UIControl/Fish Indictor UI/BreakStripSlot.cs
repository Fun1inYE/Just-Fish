using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakStripSlot : MonoBehaviour
{
    /// <summary>
    /// ����FishIndicatorController
    /// </summary>
    public FishIndicatorController fishIndicatorController;
    /// <summary>
    /// BreakStripSlotSlot��RectTransform
    /// </summary>
    public RectTransform breakStripSlotRectTransform;
    /// <summary>
    /// BreakStripSlotSlot�Ľ�����
    /// </summary>
    public RectTransform breakdownStrip;
    /// <summary>
    /// BreakStripSlotSlot�Ľ��������ߵ��ٶ�(Ĭ��Ϊ15f)
    /// </summary>
    public float breakdownStripSpeed = 15f;
    /// <summary>
    /// BreakStripSlotSlot�Ľ�������̵��ٶ�(Ĭ��Ϊ0f)
    /// </summary>
    public float breakDownStripDownSpeed = 0f;
    /// <summary>
    /// �ж����Ƿ����
    /// </summary>
    public bool isBreakdown = false;

    /// <summary>
    /// �ű���ʼ��
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
        //Ĭ��û�е�����
        isBreakdown = false;
        // ������ʱ�����������Ϊ0
        breakdownStrip.sizeDelta = new Vector2(0f, breakdownStrip.sizeDelta.y);
    }

    /// <summary>
    /// �ǽ������ķ���
    /// </summary>
    public void RiseStrip()
    {
        //��ȡ��processStrip���
        float currentWidth = breakdownStrip.sizeDelta.x;

        //ָʾ��ָ�����ָʾ����
        if (fishIndicatorController.indicatorScript.indicator.sizeDelta.x == fishIndicatorController.needleScript.needle.sizeDelta.x)
        {
            //�𽥱��
            currentWidth += breakdownStripSpeed * Time.deltaTime;
            //���������
            currentWidth = Mathf.Min(currentWidth, breakStripSlotRectTransform.sizeDelta.x);
        }
        else
        {
            //�����¿��
            currentWidth -= breakDownStripDownSpeed * Time.deltaTime;
            //���������
            currentWidth = Mathf.Max(currentWidth, 0f);
        }

        //���±������Ŀ��
        breakdownStrip.sizeDelta = new Vector2(currentWidth, breakdownStrip.sizeDelta.y);

        //�жϱ������Ƿ��Ѿ�������
        if (currentWidth == breakStripSlotRectTransform.sizeDelta.x)
        {
            isBreakdown = true;
        }
    }
}
