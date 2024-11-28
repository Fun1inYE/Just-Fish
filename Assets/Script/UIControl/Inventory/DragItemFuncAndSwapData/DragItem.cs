using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// 拖拽的方法
/// </summary>
public class DragItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    /// <summary>
    /// 这是要拖拽的UI的rectTransform
    /// </summary>
    private RectTransform rectTransform;
    /// <summary>
    /// UI拖拽之前的位置
    /// </summary>
    private Vector2 originalPosition;
    /// <summary>
    /// UI拖拽之前的父GameObject
    /// </summary>
    public GameObject originalParentGameObject;
    /// <summary>
    /// 获取到Slot脚本（被拖拽的Icon上的SlotGameObject下的Slot脚本）
    /// </summary>
    private Slot originalSlot;
    /// <summary>
    /// 目标Slot
    /// </summary>
    private Slot targetSlot;
    /// <summary>
    /// 获取到DragCanvas
    /// </summary>
    public Canvas dragCanvas;
    /// <summary>
    /// 调整拖拽触发阈值（默认为5）
    /// </summary>
    public int dragThreshold = 5;

    /// <summary>
    /// 脚本初始化
    /// </summary>
    private void Awake()
    {
        //检测是否找到了UI的rectTransform
        rectTransform = GetComponent<RectTransform>();
        if (rectTransform != null)
        {
            originalPosition = rectTransform.anchoredPosition;
        }
        else
        {
            Debug.LogError($"没有获取到{gameObject.name}的rectTransform组件");
        }

        //检查dragCanvas
        if (dragCanvas == null)
        {
            Debug.LogError("dragCanvas是空的，请检查Hierarchy窗口是否挂载正确");
        }

        //检测SystemEvent是否存在
        EventSystem eventSystem = EventSystem.current;
        if (eventSystem != null)
        {
            eventSystem.pixelDragThreshold = dragThreshold;
        }
        else
        {
            Debug.LogError("没有SystemEvent，请检查Hierarchy窗口");
        }
    }

    /// <summary>
    /// 开始拖拽的方法
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //记录原父节点
            originalParentGameObject = transform.parent.gameObject;
            //记录原位置
            originalPosition = rectTransform.anchoredPosition;
            //记录点击的Slot的GameObject下的Slot脚本
            originalSlot = GetSlotScript(gameObject);
            //将当前gameObject下的icon的GameObject挪动到DragCanvas单独渲染
            SetGameObjectToParent.SetParentFromFirstLayerParent("InventoryCanvas", "DragCanvas", gameObject);
        }
    }

    /// <summary>
    /// 拖拽中的方法
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //考虑到dragCanvas的比例缩放
            rectTransform.anchoredPosition += eventData.delta / dragCanvas.scaleFactor;
        }
    }

    /// <summary>
    /// 拖拽结束的方法
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //播放放东西的声音
            AudioManager.Instance.PlayAudio("LayDownObject", true);

            //将UI放回原位置进行渲染
            transform.SetParent(originalParentGameObject.transform, true);
            
            //使用射线检测判断UI
            List<RaycastResult> raycastReslut = new List<RaycastResult>();
            //将鼠标射出的射线所穿过的UI进行记录
            EventSystem.current.RaycastAll(eventData, raycastReslut);

            //对结果进行遍历
            foreach (RaycastResult result in raycastReslut)
            {
                GameObject hoveredObject = result.gameObject;

                //从结果中获取鼠标最后悬停的Slot的脚本
                if (hoveredObject.GetComponent<Slot>())
                {
                    //获取到最后鼠标悬停的slot的gameObject下的Slot脚本
                    targetSlot = hoveredObject.GetComponent<Slot>();
                    //交换完毕就将UI放回原位置
                    rectTransform.anchoredPosition = originalPosition;
                    //进行数据交换
                    SwapData();
                }
            }
            //遍历完毕就将UI放回原位置
            rectTransform.anchoredPosition = originalPosition;
        }
    }

    /// <summary>
    /// 获取到SlotGameObject下的脚本
    /// </summary>
    /// <param name="obj">传入的Slot的GameObject</param>
    /// <returns>Slot脚本</returns>
    private Slot GetSlotScript(GameObject obj)
    {
        Slot slotScript = obj.transform.parent.GetComponent<Slot>();
        if (slotScript != null)
        {
            return slotScript;
        }
        else
        {
            Debug.LogError("slotScript是空的，请检查代码或者Hierarchy窗口！");
            return null;
        }
    }

    /// <summary>
    /// 用于交换数据
    /// </summary>
    private void SwapData()
    {
        //先通过策略工厂获取到源Slot交换策略
        ISlotSwapStrategy originalSlotStrategy = SlotSwapStrategyFactory.GetSwapStrategy(originalSlot);

        //通过策略判断是否能够交换数据
        if(originalSlotStrategy.CanSwap(originalSlot, targetSlot))
        {
            originalSlotStrategy.Swap(originalSlot, targetSlot);
        }

        //交换完之后更新以下所有Container
        InventoryManager.Instance.RefreshAllContainer();
    }
}
