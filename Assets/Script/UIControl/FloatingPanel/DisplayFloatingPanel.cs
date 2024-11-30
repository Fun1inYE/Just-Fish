using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

/// <summary>
/// �����������ķ���
/// </summary>
public class DisplayFloatingPanel : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler
{
    /// <summary>
    /// ���õ���������GameObject
    /// </summary>
    public GameObject floatingPanel;
    /// <summary>
    /// ����������������
    /// </summary>
    public FloatingController floatingController;
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
        floatingPanel = SetGameObjectToParent.FindChildBreadthFirst(SetGameObjectToParent.FindFromFirstLayer("DisplayFloatingPanelCanvas").transform, "FloatingPanel").gameObject;
        if (floatingPanel == null)
        {
            Debug.LogError("floatingPanel�ǿյģ�������룡");
        }
        //��ȡfloatingPanelRectTransform
        floatingPanelRectTransform = floatingPanel.GetComponent<RectTransform>();
        if(floatingPanelRectTransform == null)
        {
            Debug.LogError("floatingPanelRectTransform�ǿյģ�������룡");
        }
        //��ȡfloatingController
        floatingController = floatingPanel.GetComponent<FloatingController>();
        if (floatingController == null)
        {
            Debug.LogError("floatingController�ǿյģ�������룡");
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
        //��ʶ�����ָ��ʲôSlot����
        SlotType slotType = slot.interiorSlotType;
        //��������
        floatingPanel.SetActive(true);
        //�����������ڵ���Ϣ
        UpdatePanelData(slotType);
    }

    /// <summary>
    /// ����ƶ���UI��
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        //�ر�����������
        floatingController.CloseAllFloating();
        //�ر�������
        floatingPanel.SetActive(false);
    }

    /// <summary>
    /// ��갴�µķ���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown(PointerEventData eventData)
    {
        //�ر�����������
        floatingController.CloseAllFloating();
        //�ر�������
        floatingPanel.SetActive(false);
    }

    /// <summary>
    /// ����ɿ��ķ���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp(PointerEventData eventData)
    {
        //�ر�����������
        floatingController.CloseAllFloating();
        //�ر�������
        floatingPanel.SetActive(false);
    }

    /// <summary>
    /// ͨ��slot��������������������
    /// </summary>
    /// <param name="slotType">slot������</param>
    public void UpdatePanelData(SlotType slotType)
    {
        switch(slotType)
        {
            case SlotType.FishItem:
                if (slot.inventory_Database.list[slot.Index] is FishItem fishItem)
                {
                    floatingController.OpenFloating(true, false, false);
                    //����fishItemFloatingScript����
                    floatingController.fishItemFloatingScript.Name.text = fishItem.type.name;
                    floatingController.fishItemFloatingScript.FishedTime.text = fishItem.fishedTime;
                    floatingController.fishItemFloatingScript.LengthValue.text = fishItem.Length.ToString();
                    floatingController.fishItemFloatingScript.WeightValue.text = fishItem.Weight.ToString();
                }
                break;
            case SlotType.ToolItem:
                if (slot.inventory_Database.list[slot.Index] is ToolItem toolItem)
                {
                    floatingController.OpenFloating(false, true, false);
                    //����toolItemFloatingScript������
                    floatingController.toolItemFloatingScript.Name.text = toolItem.type.name;
                    floatingController.toolItemFloatingScript.ToolQuality.text = GetToolQualityInfo(toolItem);
                    floatingController.toolItemFloatingScript.Description.text = toolItem.type.description;
                }
                break;
            case SlotType.PropItem:
                if (slot.inventory_Database.list[slot.Index] is PropItem propItem)
                {
                    floatingController.OpenFloating(false, false, true);
                    //����propItemFloatingScript������
                    floatingController.propItemFloatingScript.Name.text = propItem.type.name;
                    floatingController.propItemFloatingScript.PropQuality.text = GetPropQualityInfo(propItem);
                    floatingController.propItemFloatingScript.Description.text = propItem.type.description;
                }
                break;
            //һ�㲻��ִ�е�����
            default:
                //�ر�����floating
                floatingController.CloseAllFloating();
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
