using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcessStripSlot : MonoBehaviour
{
    /// <summary>
    /// ����FishIndicatorController
    /// </summary>
    public FishIndicatorController fishIndicatorController;
    /// <summary>
    /// ProcessStripSlot��RectTransform
    /// </summary>
    public RectTransform processStripSlotRectTransform;
    /// <summary>
    /// ProcessStripSlot�Ľ�����
    /// </summary>
    public RectTransform processStrip;
    /// <summary>
    /// ProcessStripSlot�Ľ��������ߵ��ٶ�
    /// </summary>
    public float processStripSpeed = 15f;
    /// <summary>
    /// ProcessStripSlot�Ľ�������̵��ٶ�
    /// </summary>
    public float processStripDownSpeed = 15f;
    /// <summary>
    /// �ж��Ƿ������
    /// </summary>
    public bool isFinish = false;

    /// <summary>
    /// �ű���ʼ��
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
        //Ĭ��û�е�����
        isFinish = false;
        // ������ʱ�����������Ϊ0
        processStrip.sizeDelta = new Vector2(0f, processStrip.sizeDelta.y);
    }

    /// <summary>
    /// �ǽ������ķ���
    /// </summary>
    public void RiseStrip()
    {
        //��ȡ��processStrip���
        float currentWidth = processStrip.sizeDelta.x;

        //��ָʾ���Ŀ�ȵ�������ʱ���������ʼ����
        //if (fishIndicatorController.indicatorScript.indicator.sizeDelta.x == fishIndicatorController.needleScript.needle.sizeDelta.x)
        //{
        //    currentWidth += wireBrokenSilderSpeed * Time.deltaTime;
        //    ���������
        //    currentWidth = Mathf.Min(currentWidth, movingRange.movingRangeCorners[3].x - movingRange.movingRangeCorners[0].x);
        //}

        //ָʾ����Ŀ�����ס֮��
        if (fishIndicatorController.needleScript.GetCorners()[0].x <= fishIndicatorController.targetScript.GetCorners()[0].x &&
            fishIndicatorController.needleScript.GetCorners()[3].x >= fishIndicatorController.targetScript.GetCorners()[3].x)
        {
            //�𽥱��
            currentWidth += processStripSpeed * Time.deltaTime;
            //���������
            currentWidth = Mathf.Min(currentWidth, processStripSlotRectTransform.sizeDelta.x);
        }
        else
        {
            //�ɿ��ո��֮��ָʾ�������ص���ʼ���
            //currentWidth = Mathf.Lerp(currentWidth, 0f, breakdownStripSpeed * Time.deltaTime);

            //�����¿��
            currentWidth -= processStripDownSpeed * Time.deltaTime;
            //���������
            currentWidth = Mathf.Max(currentWidth, 0f);
        }
        //���±������Ŀ��
        processStrip.sizeDelta = new Vector2(currentWidth, processStrip.sizeDelta.y);

        //�жϵ������Ƿ��Ѿ�������
        if (currentWidth == processStripSlotRectTransform.sizeDelta.x)
        {
            isFinish = true;
        }
    }
}
