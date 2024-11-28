using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// ��ק�ķ���
/// </summary>
public class DragItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    /// <summary>
    /// ����Ҫ��ק��UI��rectTransform
    /// </summary>
    private RectTransform rectTransform;
    /// <summary>
    /// UI��ק֮ǰ��λ��
    /// </summary>
    private Vector2 originalPosition;
    /// <summary>
    /// UI��ק֮ǰ�ĸ�GameObject
    /// </summary>
    public GameObject originalParentGameObject;
    /// <summary>
    /// ��ȡ��Slot�ű�������ק��Icon�ϵ�SlotGameObject�µ�Slot�ű���
    /// </summary>
    private Slot originalSlot;
    /// <summary>
    /// Ŀ��Slot
    /// </summary>
    private Slot targetSlot;
    /// <summary>
    /// ��ȡ��DragCanvas
    /// </summary>
    public Canvas dragCanvas;
    /// <summary>
    /// ������ק������ֵ��Ĭ��Ϊ5��
    /// </summary>
    public int dragThreshold = 5;

    /// <summary>
    /// �ű���ʼ��
    /// </summary>
    private void Awake()
    {
        //����Ƿ��ҵ���UI��rectTransform
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            originalPosition = rectTransform.anchoredPosition;
        }
        else
        {
            Debug.LogError($"û�л�ȡ��{gameObject.name}��rectTransform���");
        }

        //���dragCanvas
        if (dragCanvas == null)
        {
            Debug.LogError("dragCanvas�ǿյģ�����Hierarchy�����Ƿ������ȷ");
        }

        //���SystemEvent�Ƿ����
        EventSystem eventSystem = EventSystem.current;
        if (eventSystem != null)
        {
            eventSystem.pixelDragThreshold = dragThreshold;
        }
        else
        {
            Debug.LogError("û��SystemEvent������Hierarchy����");
        }
    }

    /// <summary>
    /// ��ʼ��ק�ķ���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //��¼ԭ���ڵ�
            originalParentGameObject = transform.parent.gameObject;
            //��¼ԭλ��
            originalPosition = rectTransform.anchoredPosition;
            //��¼�����Slot��GameObject�µ�Slot�ű�
            originalSlot = GetSlotScript(gameObject);
            //����ǰgameObject�µ�icon��GameObjectŲ����DragCanvas������Ⱦ
            SetGameObjectToParent.SetParentFromFirstLayerParent("InventoryCanvas", "DragCanvas", gameObject);
        }
    }

    /// <summary>
    /// ��ק�еķ���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //���ǵ�dragCanvas�ı�������
            rectTransform.anchoredPosition += eventData.delta / dragCanvas.scaleFactor;
        }
    }

    /// <summary>
    /// ��ק�����ķ���
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //���ŷŶ���������
            AudioManager.Instance.PlayAudio("LayDownObject", true);

            //��UI�Ż�ԭλ�ý�����Ⱦ
            transform.SetParent(originalParentGameObject.transform, true);
            
            //ʹ�����߼���ж�UI
            List<RaycastResult> raycastReslut = new List<RaycastResult>();
            //����������������������UI���м�¼
            EventSystem.current.RaycastAll(eventData, raycastReslut);

            //�Խ�����б���
            foreach (RaycastResult result in raycastReslut)
            {
                GameObject hoveredObject = result.gameObject;

                //�ӽ���л�ȡ��������ͣ��Slot�Ľű�
                if (hoveredObject.GetComponent<Slot>())
                {
                    //��ȡ����������ͣ��slot��gameObject�µ�Slot�ű�
                    targetSlot = hoveredObject.GetComponent<Slot>();
                    //������Ͼͽ�UI�Ż�ԭλ��
                    rectTransform.anchoredPosition = originalPosition;
                    //�������ݽ���
                    SwapData();
                }
            }
            //������Ͼͽ�UI�Ż�ԭλ��
            rectTransform.anchoredPosition = originalPosition;
        }
    }

    /// <summary>
    /// ��ȡ��SlotGameObject�µĽű�
    /// </summary>
    /// <param name="obj">�����Slot��GameObject</param>
    /// <returns>Slot�ű�</returns>
    private Slot GetSlotScript(GameObject obj)
    {
        Slot slotScript = obj.transform.parent.GetComponent<Slot>();
        if (slotScript != null)
        {
            return slotScript;
        }
        else
        {
            Debug.LogError("slotScript�ǿյģ�����������Hierarchy���ڣ�");
            return null;
        }
    }

    /// <summary>
    /// ���ڽ�������
    /// </summary>
    private void SwapData()
    {
        //��ͨ�����Թ�����ȡ��ԴSlot��������
        ISlotSwapStrategy originalSlotStrategy = SlotSwapStrategyFactory.GetSwapStrategy(originalSlot);

        //ͨ�������ж��Ƿ��ܹ���������
        if(originalSlotStrategy.CanSwap(originalSlot, targetSlot))
        {
            originalSlotStrategy.Swap(originalSlot, targetSlot);
        }

        //������֮�������������Container
        InventoryManager.Instance.RefreshAllContainer();
    }
}
