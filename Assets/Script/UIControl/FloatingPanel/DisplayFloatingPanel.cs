using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.VersionControl;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

/// <summary>
/// �����������ķ���
/// </summary>
public class DisplayFloatingPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    /// <summary>
    /// ����floatingPanel�ű�
    /// </summary>
    public FloatingPanel floatingPanel;
    /// <summary>
    /// ������������RectTransform
    /// </summary>
    public RectTransform floatingPanelRectTransform;
    /// <summary>
    /// ��Ⱦ���������
    /// </summary>
    public Camera cameraCanvas;
    /// <summary>
    /// ���ù��ر�Icon��slot
    /// </summary>
    public Slot slot;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    private void Awake()
    {
        //��ȡfloatingPanel
        floatingPanel = SetGameObjectToParent.FindChildBreadthFirst(SetGameObjectToParent.FindFromFirstLayer("DisplayFloatingPanelCanvas").transform, "FloatingPanel").GetComponent<FloatingPanel>();
        if (floatingPanel == null)
        {
            Debug.LogError("floatingPanel�ǿյģ�������룡");
        }
        //��ȡfloatingPanelRectTransform
        floatingPanelRectTransform = floatingPanel.gameObject.GetComponent<RectTransform>();
        if (floatingPanelRectTransform == null)
        {
            Debug.LogError("floatingPanelRectTransform�ǿյģ�������룡");
        }
        //��ȡslot�ű�
        slot = GetComponentInParent<Slot>();
        if (slot == null)
        {
            Debug.LogError("floatingPanelScript�ǿյģ�������룡");
        }
        //��ȡ����Ⱦ��Camera���
        cameraCanvas = SetGameObjectToParent.FindFromFirstLayer("Main Camera").GetComponent<Camera>();
        if (cameraCanvas == null)
        {
            Debug.LogError("û���ҵ�cameraCanvas��������룡");
        }

        //��ʼ��floatingPanel
        floatingPanel.InitFloatingPanelObj();
    }

    private void Update()
    {
        //����������ķ���
        MouseFollowWithOffSetOnUpdate();
    }

    /// <summary>
    /// �������������ƶ��ķ�������ƫ������
    /// </summary>
    public void MouseFollowWithOffSetOnUpdate()
    {
        //�������ȵ�ƫ����
        float widthOffset = floatingPanelRectTransform.rect.width / 2 + 10f;
        //������߶ȵ�ƫ����
        float heightOffset = floatingPanelRectTransform.rect.height / 2 + 10f;
        //��ȡ�����λ��
        Vector2 screenPoint = Input.mousePosition;

        //����һ���������꣬�����������ת���������
        Vector2 localPoint;
        //����Ļ����ת��Ϊ��������
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            floatingPanelRectTransform.parent as RectTransform,
            screenPoint,
            cameraCanvas,
            out localPoint
        );

        //������ı����������ƫ����
        floatingPanelRectTransform.localPosition = new Vector2(localPoint.x + widthOffset, localPoint.y + heightOffset);
    }

    /// <summary>
    /// ����ƶ���UI��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        //��ʶ�����ָ��ʲô��Ʒ����
        var item = slot.inventory_Database.list[slot.Index];
        //��������
        floatingPanel.gameObject.SetActive(true);
        //�����������ڵ���Ϣ
        UpdatePanelData(item);
        
    }

    /// <summary>
    /// ����ƶ���UI��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //�ر�������
        floatingPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// ��갴�µķ���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //�ر�������
        floatingPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// ����ɿ��ķ���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        //�ر�������
        floatingPanel.gameObject.SetActive(false);
    }

    /// <summary>
    /// ͨ��slot��������������������
    /// </summary>
    /// <param name="slotType">slot������</param>
    public void UpdatePanelData(ItemData item)
    {
        switch(item)
        {
            case FishItem fishItem:
                floatingPanel.GetNameUI().text = fishItem.type.name;
                floatingPanel.GetDeputyUI().text = fishItem.fishedTime.ToString();
                floatingPanel.GetDescriptionUI().text = fishItem.type.description;
                break;
            case ToolItem toolItem:
                floatingPanel.GetNameUI().text = toolItem.type.name;
                floatingPanel.GetDeputyUI().text = toolItem.toolQuality.ToString();
                floatingPanel.GetDescriptionUI().text = toolItem.type.description;
                break;
            case PropItem propItem:
                floatingPanel.GetNameUI().text = propItem.type.name;
                floatingPanel.GetDeputyUI().text = propItem.propQuality.ToString();
                floatingPanel.GetDescriptionUI().text = propItem.type.description;
                break;
            case BaitItem baitItem:
                floatingPanel.GetNameUI().text = baitItem.type.name;
                floatingPanel.GetDeputyUI().text = "������" + baitItem.amount.ToString();
                floatingPanel.GetDescriptionUI().text = baitItem.type.description;
                break;
            //һ�㲻��ִ�е�����
            default:
                //�ر�����floating
                floatingPanel.gameObject.SetActive(false);
                break;
        }
    }

    /// <summary>
    /// ��ȡ�����ߵ�Ʒ��
    /// </summary>
    public string GetToolQualityInfo(ToolItem toolItem)
    {
        //�Ȼ�ȡ������Ʒ��
        ToolQuality toolQuality = toolItem.toolQuality;
        //ͨ������Ʒ�ʷ��ض�Ӧ����
        switch (toolQuality)
        {
            case ToolQuality.Normal:
                return "��ͨ";
            case ToolQuality.Advanced:
                return "�߼�";
            case ToolQuality.Epic:
                return "ʷʫ";
            case ToolQuality.Legendary:
                return "����";
            default:
                return "����Ʒ�ʴ���";
        }
    }

    public string GetPropQualityInfo(PropItem propItem)
    {
        //�Ȼ�ȡ������Ʒ��
        PropQuality propQuality = propItem.propQuality;
        //ͨ������Ʒ�ʷ��ض�Ӧ����
        switch (propQuality)
        {
            case PropQuality.Normal:
                return "��ͨ";
            case PropQuality.Advanced:
                return "�߼�";
            case PropQuality.Epic:
                return "ʷʫ";
            case PropQuality.Legendary:
                return "����";
            default:
                return "����Ʒ�ʴ���";
        }
    }
}
